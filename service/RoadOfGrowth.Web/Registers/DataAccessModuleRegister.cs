using Autofac;
using RoadOfGrowth.DataAccess;
using RoadOfGrowth.Utility;
using System.Linq;

namespace RoadOfGrowth.Web.Registers
{
    /// <summary>
    /// 数据访问注册类
    /// </summary>
    public class DataAccessModuleRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dependencyBase = typeof(IDataDependency);
            System.Reflection.Assembly[] assemblies = AssemblyUtility.ListAssemblies("RoadOfGrowth.DataAccess");

            builder.RegisterAssemblyTypes(assemblies).Where(t => dependencyBase.IsAssignableFrom(t) && t != dependencyBase)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
