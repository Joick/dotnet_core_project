using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace RoadOfGrowth.DBUtility
{
    /// <summary>
    /// assembly帮助类
    /// </summary>
    public static class AssemblyUtility
    {
        /// <summary>
        /// get single assembly object by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Assembly GetAssemblyByDllName(string name)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath($"{AppContext.BaseDirectory}{name}.dll");
            return assembly;
        }

        /// <summary>
        /// get assembly object list by assembly names
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static Assembly[] ListAssemblies(params string[] names)
        {
            Assembly[] assemblies = new Assembly[names.Length];
            int count = 0;
            foreach (var name in names)
            {
                assemblies.SetValue(GetAssemblyByDllName(name), count++);
            }

            return assemblies;
        }

        /// <summary>
        /// 获取集合内的所有类对象
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static IEnumerable<Type> ListType(string assemblyName)
        {
            var objects = from t in Assembly.Load(assemblyName).GetTypes()
                          where t.IsClass && t.Namespace.Contains(assemblyName, StringComparison.InvariantCultureIgnoreCase)
                          select t;
            return objects;
        }

    }
}
