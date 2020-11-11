using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySvc.DotNetCore.Framework.Infrastructure.Authorization.Client;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Json;
using MySvc.DotNetCore.Framework.Infrastructure.Json.NewtonsoftJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Auth.ClientTestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration,IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            this.WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                ////全局配置Json序列化处理
                //.AddJsonOptions(options =>
                //{
                //    //忽略循环引用
                //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //    //设置时间格式
                //    //options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                //    //不使用驼峰样式的key
                //    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();

                //    //自定义序列化
                //    var convers = options.SerializerSettings.Converters ?? new List<JsonConverter>();
                //    convers.Add(new StringEnumConverter());

                //    options.SerializerSettings.Converters = convers;
                //})
                .AddNewtonsoftJson()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddCustomAuthentication(Configuration);
            services.AddCustomSwagger(Configuration, this.WebHostEnvironment);
            services.Configure<AuthServiceOptions>(Configuration.GetSection("AuthServiceOptions"));
            services.AddScoped<IUserIdentityService, UserIdentityService>();
            services.AddSingleton<IJsonConverter, NewtonsoftJsonConverter>(); 
            services.AddHttpClient();

            var authServiceOptions = Configuration.GetSection("AuthServiceOptions").Get<AuthServiceOptions>();
            var authServiceHostAddress = Configuration.GetValue<string>("AuthServiceHostAddress");
            services.AddHttpClient("authService", x => {
                x.BaseAddress = new Uri(authServiceHostAddress);
            });

            services.AddStackExchangeRedisCache(opts =>
            {
                opts.Configuration = Configuration.GetValue<string>("RedisConnectionString");
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (WebHostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            //启动Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Auth.ClientTestAPI");
                c.OAuthClientId("AuthClientTestApiSwaggerUI");
                c.OAuthAppName("AuthClientTestApi Swagger UI");

            });
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
