using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Options;
using Sample.Product.Api.DI.AutofacModules;
using Sample.Product.Application.IntegrationEvents.EventHandling;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace Sample.Product.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //配置数据库连接, MQ
            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDBForWrite")); //默认读写
            services.Configure<MongoDBSettings>("MongoDBForWrite", Configuration.GetSection("MongoDBForWrite"));
            services.Configure<MongoDBSettings>("MongoDBForRead", Configuration.GetSection("MongoDBForRead"));
            //services.Configure<RabbitMQEventBusSettings>(Configuration.GetSection("RabbitMQEventBus"));

            //增加Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Sample Product Service API", Version = "v1" });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Sample.Product.Api.xml");
                c.IncludeXmlComments(xmlPath);
            });
            //配置Swagger
            services.ConfigureSwaggerGen(options =>
            {
                // UseFullTypeNameInSchemaIds replacement for .NET Core
                options.CustomSchemaIds(x => x.FullName);
            });

            //添加AutoMapper的支持
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<OrderCreatedIntegrationEventHandler>();

            services.AddCap(x => {
                
                //x.UseDashboard();
                x.UseMongoDB(o => {
                    o.DatabaseConnection = "mongodb://admin:12345678@127.0.0.1:27017,127.0.0.1:27018,127.0.0.1:27019/?connectTimeoutMS=10000&authSource=admin&authMechanism=SCRAM-SHA-1";
                    o.DatabaseName = "SampleProduct";
                    o.PublishedCollection = "cap.published";
                    o.ReceivedCollection = "cap.received";
                });
                x.DefaultGroup = "sampleProduct";
                x.UseRabbitMQ(o => {
                    o.HostName = "127.0.0.1";
                    o.Port = 5672;
                    o.UserName = "admin";
                    o.Password = "admin123456";
                    o.VirtualHost = "frameworksample";
                    o.ExchangeName = "frameworksample-exchange";
                });

            });

            //Autofac 容器
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            //注册各个模块
            containerBuilder.RegisterModule(new CommonModule());
            containerBuilder.RegisterModule(new RepositoryModule());
            containerBuilder.RegisterModule(new ApplicationModule());
            containerBuilder.RegisterModule(new MediatorModule());

            return new AutofacServiceProvider(containerBuilder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            //启动Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Product Service API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
