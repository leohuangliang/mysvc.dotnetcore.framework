using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using MySvc.Framework.Infrastructure.Crosscutting.Options;
using Sample.Order.Api.DI.AutofacModules;
using System;
using System.IO;
using Microsoft.Extensions.Hosting;
using Sample.Order.Api.Extensions;
using Sample.Order.Application.Profiles;

namespace Sample.Order.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //配置数据库连接, MQ
            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDBForWrite")); //默认读写
            services.Configure<MongoDBSettings>("MongoDBForWrite", Configuration.GetSection("MongoDBForWrite"));
            services.Configure<MongoDBSettings>("MongoDBForRead", Configuration.GetSection("MongoDBForRead"));

            //添加AutoMapper的支持
            services.AddAutoMapper(typeof(OrderProfile).Assembly);

            services.AddCustomSwaggers();
            services.AddCustomMassTransit(Configuration);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            //builder.RegisterModule(new MyApplicationModule());
            //注册各个模块
            builder.RegisterModule(new CommonModule());
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new MediatorModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseCustomSwaggers();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
