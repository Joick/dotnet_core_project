using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadOfGrowth.DBUtility;
using RoadOfGrowth.DBUtility.Providers.EntityExtension;
using RoadOfGrowth.DBWebService.Configuration;
using RoadOfGrowth.DBWebService.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoadOfGrowth.DBWebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private IContainer AutofacContainer;

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // 使用Autofac注入
            ContainerBuilder builder = new ContainerBuilder();

            // 注入数据库访接口
            builder.RegisterModule<RepositoryModuleRegister>();

            builder.Populate(services);
            AutofacContainer = builder.Build();

            return AutofacContainer.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionLog();
            app.UseLogRequest();
            app.UseHttpsRedirection();
            app.UseMvc();

            DapperEntityMapping();

            // 在程序停止时执行Autofac的释放函数
            appLifetime.ApplicationStopped.Register(() => { AutofacContainer.Dispose(); });
        }

        /// <summary>
        /// 映射实体类属性与表字段
        /// </summary>
        private void DapperEntityMapping()
        {
            var dependencyBase = typeof(BaseEntity);
            IEnumerable<Type> types = AssemblyUtility.ListType("RoadOfGrowth.DBCommon").Where(t => dependencyBase.IsAssignableFrom(t));

            foreach (var item in types)
            {
                SqlMapper.SetTypeMap(item, new ColumnAttributeTypeMapper(item));
            }
        }
    }
}
