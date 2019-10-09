using Autofac;
using RoadOfGrowth.IRepository;
using RoadOfGrowth.Utility;
using System.Linq;

namespace RoadOfGrowth.Web.Registers
{
    /// <summary>
    /// 业务逻辑注册类
    /// </summary>
    public class RepositoryModuleRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dependencyObj = typeof(IBLLDependency);
            System.Reflection.Assembly[] assemblies =
                AssemblyUtility.ListAssemblies("RoadOfGrowth.IRepository", "RoadOfGrowth.Repository");

            builder.RegisterAssemblyTypes(assemblies).Where(t => dependencyObj.IsAssignableFrom(t) && t != dependencyObj)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
