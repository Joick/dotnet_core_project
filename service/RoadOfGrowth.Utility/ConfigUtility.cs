using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace RoadOfGrowth.Utility
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public static class ConfigUtility
    {
        private static IConfiguration configuration;

        static ConfigUtility()
        {
            InitConfiguration();
        }

        /// <summary>
        /// initialize configuration object
        /// </summary>
        private static void InitConfiguration()
        {
            const string fileName = "appsetting.json";
            var directory = AppContext.BaseDirectory;
            directory = directory.Replace("\\", "/");

            var filePath = $"{directory}{fileName}";

            if (!File.Exists(filePath))
            {
                var length = directory.IndexOf("/bin");
                filePath = $"{directory.Substring(0, length)}/{fileName}";
            }

            var builder = new ConfigurationBuilder().AddJsonFile(filePath, false, true);

            configuration = builder.Build();
        }

        /// <summary>
        /// get configuration object instance
        /// </summary>
        /// <returns></returns>
        public static IConfiguration GetInstance()
        {
            if (configuration == null)
            {
                InitConfiguration();
            }

            return configuration;
        }
    }
}
