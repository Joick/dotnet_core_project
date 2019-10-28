using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadOfGrowth.Web.Controllers;
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

            // 添加版本控制
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            }).AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'VVV";
                option.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddSwaggerGen(s =>
            {
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(v =>
                {
                    s.SwaggerDoc(v, new Info
                    {
                        Title = "Webapi document",
                        Description = "no description",
                        Version = v
                    });

                    s.DocInclusionPredicate((version, apiDesc) =>
                    {
                        if (!apiDesc.RelativePath.Split('/')[1].Equals(version))
                            return false;

                        var values = apiDesc.RelativePath
                        .Split('/')
                        .Select(i => i.Replace("v{version}", apiDesc.GroupName));

                        apiDesc.RelativePath = string.Join("/", values);

                        // 取消模拟请求参数中api-version字段
                        var _versionParam = apiDesc.ParameterDescriptions.FirstOrDefault(p => p.Name.Equals("api-version"));
                        if (_versionParam != null)
                        {
                            apiDesc.ParameterDescriptions.Remove(_versionParam);
                        }

                        return true;
                    });
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
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(v =>
                {
                    s.SwaggerEndpoint($"/swagger/{v}/swagger.json", $"webapi document {v}");
                });
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
