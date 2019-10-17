using Autofac;
using RoadOfGrowth.DBRepository;
using RoadOfGrowth.DBUtility;
using System.Linq;

namespace RoadOfGrowth.DBWebService.Configuration
{
    /// <summary>
    /// 数据库访问方法依赖注入
    /// </summary>
    public class RepositoryModuleRegister : Module
    {
        /// <summary>
        /// 重写注入方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            var dependencyBase = typeof(IDependency);
            System.Reflection.Assembly[] assemblies = AssemblyUtility.ListAssemblies("RoadOfGrowth.DBRepository");

            builder.RegisterAssemblyTypes(assemblies).Where(t => dependencyBase.IsAssignableFrom(t) && t != dependencyBase)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
