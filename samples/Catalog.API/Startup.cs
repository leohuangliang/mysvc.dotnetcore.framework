using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MySvc.Framework.Infrastructure.Crosscutting.EventBus;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Catalog.API.Extensions;
using Catalog.API.Infrastructure.AutofacModules;
using Microsoft.Extensions.Hosting;

namespace Catalog.API
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

            services.AddCustomMVC(Configuration)
                 .AddCustomMongoDBContext()
                 .AddCustomOptions(Configuration)
                 .AddIntegrationServices(Configuration)
                 .AddEventBus(Configuration)
                 .AddSwagger();

            //services.AddCap(x => {
            //    //x.UseDashboard();
            //    x.UseMongoDB(o => {
            //        o.DatabaseConnection = "mongodb://admin:12345678@127.0.0.1:27017,127.0.0.1:27018,127.0.0.1:27019/?connectTimeoutMS=10000&authSource=admin&authMechanism=SCRAM-SHA-1";
            //        o.DatabaseName = "SampleCatalog";
            //        o.PublishedCollection = "cap.published";
            //        o.ReceivedCollection = "cap.received";
            //    });
            //    x.DefaultGroup = "sampleCatalog";
            //    x.UseRabbitMQ(o => {
            //        o.HostName = "127.0.0.1";
            //        o.Port = 5672;
            //        o.UserName = "admin";
            //        o.Password = "admin123456";
            //        o.VirtualHost = "frameworksample";
            //        o.ExchangeName = "frameworksample-exchange";
            //    });

            //});

            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule());
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            var pathBase = Configuration["PATH_BASE"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            app.UseCors("CorsPolicy");


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Catalog.API V1");
                });
            ConfigureEventBus(app);
        }

        protected virtual void ConfigureEventBus(IApplicationBuilder app)
        {
            //eventBus.Subscribe<OrderStatusChangedToAwaitingValidationIntegrationEvent, OrderStatusChangedToAwaitingValidationIntegrationEventHandler>();
            //eventBus.Subscribe<OrderStatusChangedToPaidIntegrationEvent, OrderStatusChangedToPaidIntegrationEventHandler>();
        }

    }
}
