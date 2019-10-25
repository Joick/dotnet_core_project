using CoreTask.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace CoreTask.Startup
{
    /// <summary>
    /// 启动
    /// </summary>
    public static class Start
    {
        /// <summary>
        /// 启动入口
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            LogTask.Startup();
        }
    }
}
