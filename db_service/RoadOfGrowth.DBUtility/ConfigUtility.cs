using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace RoadOfGrowth.DBUtility
{
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
            const string fileName = "appsettings.json";
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
        /// 获取指定键的值
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string GetSectionValueDeep(params string[] keys)
        {
            if (keys == null || keys.Length == 0)
            {
                return null;
            }

            string _path = string.Join(":", keys);

            IConfigurationSection section = GetInstance().GetSection(_path);

            if (section == null)
            {
                return null;
            }

            return section.Value;
        }

        /// <summary>
        /// 获取配置项对象
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetSectionObjDeep(params string[] keys)
        {
            if (keys == null || keys.Length == 0)
            {
                return default;
            }

            IConfigurationSection section = GetInstance().GetSection(keys[0]);

            if (section == null)
            {
                return default;
            }

            foreach (var key in keys)
            {
                section = section.GetSection(key);
            }

            return section.ToDictionary();
        }

        /// <summary>
        /// 将Iconfigurationsection对象转换成字典
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        private static Dictionary<string, object> ToDictionary(this IConfigurationSection section)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            foreach (var item in section.GetChildren())
            {
                data.Add(item.Key, item.Value);
            }

            return data;
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
