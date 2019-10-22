using log4net;
using log4net.Config;
using log4net.Repository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceProcess;
using System.Text;

namespace CoreTask.Startup
{
    /// <summary>
    /// rabbitmq记录日志服务
    /// </summary>
    public class LogTask : ServiceBase
    {
        private static readonly string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}appsettings.json";
        private static readonly ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
        private static readonly ILog log;

        private static readonly Dictionary<string, string> Config = default;
        private static readonly string QueueName;
        private static readonly ConnectionFactory Factory = default;

        /// <summary>
        /// 
        /// </summary>
        public LogTask()
        {
            ServiceName = "log service";
        }

        static LogTask()
        {
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            log = LogManager.GetLogger(repository.Name, "NETCorelog4net");

            if (File.Exists(filePath))
            {
                var text = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(filePath));

                if (text["LogMQ"] != null)
                {
                    Config = JsonConvert.DeserializeObject<Dictionary<string, string>>(text["LogMQ"].ToString());

                    QueueName = Config["QueueName"];
                    Factory = new ConnectionFactory()
                    {
                        HostName = Config["Host"]
                    };
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                using (var connection = Factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: QueueName,
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body;
                            var message = Encoding.UTF8.GetString(body);
                            var logObj = JsonConvert.DeserializeObject<LogObject>(message);

                            Console.WriteLine(" [x] Received {0}", logObj.Message);

                            if (logObj.Error == null)
                            {
                                log.Info(logObj.Message);
                            }
                            else
                            {
                                log.Error(logObj.Message, logObj.Error);
                            }

                            // 手动发送确认信号
                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        };

                        channel.BasicConsume(queue: QueueName,
                            autoAck: false,
                            consumer: consumer);

                        Console.WriteLine(" Press [enter] to exit.");
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"处理出现错误,Exception:{ex}");
            }
        }


        [Serializable]
        public class LogObject
        {
            [JsonProperty(PropertyName = "msg")]
            public string Message { get; set; }

            [JsonProperty(PropertyName = "err")]
            public Exception Error { get; set; }
        }
    }
}
