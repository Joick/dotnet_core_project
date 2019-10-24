using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoadOfGrowth.DBUtility
{
    /// <summary>
    /// rabbitmq帮助类
    /// </summary>
    public static class RabbitMQUtility
    {
        private static readonly Dictionary<string, object> Config = ConfigUtility.GetSectionObj("RabbitMQConfig", "ErrorLog");
        private static readonly string QueueName = Config["QueueName"].ToString();
        private static readonly ConnectionFactory Factory = new ConnectionFactory()
        {
            HostName = Config["Host"].ToString()
        };

        /// <summary>
        /// 入队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pushData"></param>
        public static async Task PushLog<T>(T pushData, string routingKey = "")
        {
            await Task.Factory.StartNew(() =>
            {
                using (var connection = Factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(QueueName, true, false, false, null);

                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(pushData));
                    var properties = channel.CreateBasicProperties();

                    // 持久化消息
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: properties, body: body);
                }
            });
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pushData"></param>
        public static async Task PushLog(string logData, string routingKey = "")
        {
            await Task.Factory.StartNew(() =>
            {
                using (var connection = Factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(QueueName, true, false, false, null);

                    var body = Encoding.UTF8.GetBytes(logData);
                    var properties = channel.CreateBasicProperties();

                    // 持久化消息
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: properties, body: body);
                }
            });
        }

    }
}
