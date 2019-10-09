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
        private static IConfiguration GetInstance()
        {
            if (configuration == null)
            {
                InitConfiguration();
            }

            return configuration;
        }

        /// <summary>
        /// 获取区域对象值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSectionValue(string key)
        {
            return GetInstance().GetSection(key).Value;
        }

        /// <summary>
        /// 获取区域对象内指定键值
        /// </summary>
        /// <param name="section">区域名</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetSectionValue(string section, string key)
        {
            var sectionObj = GetInstance().GetSection(section);

            if (sectionObj != null)
            {
                return sectionObj.GetSection(key).Value;
            }

            return null;
        }
    }
}
