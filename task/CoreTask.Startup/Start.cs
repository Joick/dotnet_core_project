using System.ServiceProcess;

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
            using (var task = new LogTask())
            {
                ServiceBase.Run(task);
            }
        }

    }
}
