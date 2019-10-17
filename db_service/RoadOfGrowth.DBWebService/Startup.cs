using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadOfGrowth.DBWebService.Configuration;
using System;

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

            app.UseHttpsRedirection();
            app.UseMvc();


            // 在程序停止时执行Autofac的释放函数
            appLifetime.ApplicationStopped.Register(() => { AutofacContainer.Dispose(); });
        }
    }
}
