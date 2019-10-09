using log4net;
using log4net.Config;
using System;
using System.IO;

namespace RoadOfGrowth.Utility
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public static class LogUtility
    {
        private static readonly ILog logger;

        static LogUtility()
        {
            if (logger == null)
            {
                var repository = LogManager.CreateRepository("NETCoreRepository");

                // 从log4net.config文件中获取配置信息
                XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
                logger = LogManager.GetLogger(repository.Name, "InfoLogger");
            }
        }

        /// <summary>
        /// 日常日志
        /// </summary>
        /// <param name="info"></param>
        public static void Info(string info)
        {
            logger.Info(info);
        }

        /// <summary>
        /// 日常日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="args"></param>
        public static void Info(string info, params object[] args)
        {
            logger.InfoFormat(info, args);
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="info"></param>
        public static void Warn(string info)
        {
            logger.Warn(info);
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="args"></param>
        public static void Warn(string info, params object[] args)
        {
            logger.WarnFormat(info, args);
        }

        /// <summary>
        /// 报错日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ex"></param>
        public static void Error(string info, Exception ex = null)
        {
            logger.Error(info, ex);
        }

        /// <summary>
        /// 报错日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="args"></param>
        public static void Error(string info, params object[] args)
        {
            logger.ErrorFormat(info, args);
        }
    }
}
