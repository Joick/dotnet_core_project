using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadOfGrowth.Web.Middlewares;
using RoadOfGrowth.Web.Registers;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;

namespace RoadOfGrowth.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IContainer AutofacContainer;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(s =>
            {
                //s.DocInclusionPredicate((version, apiDesc) =>
                //{
                //    if (version.Equals(apiDesc.GroupName))
                //        return false;

                //    var values = apiDesc.RelativePath.Split("/")
                //    .Select(v => v.Replace("v{version}", apiDesc.GroupName));

                //    apiDesc.RelativePath = string.Join("/", values);

                //    return true;
                //});
                s.SwaggerDoc("v1", new Info
                {
                    Contact = new Contact
                    {
                        Name = "test name",
                        Email = "test email",
                        Url = "test url"
                    },
                    Title = "test title",
                    Description = "test description",
                    Version = "v1"
                });
            });

            // 使用Autofac注入
            ContainerBuilder builder = new ContainerBuilder();

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

            app.UseHttpsRedirection();

            app.UseLogRequest();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "lalala");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "page", template: "{controller}/{action}");
            });


            // 在程序停止时执行Autofac的释放函数
            appLifetime.ApplicationStopped.Register(() => { AutofacContainer.Dispose(); });
        }
    }
}
