using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySvc.Framework.Infrastructure.Authorization.Client;
using System.Text.Json;
using System.Text.Json.Serialization;
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
                // 全局配置Json序列化处理 - 使用System.Text.Json
                .AddJsonOptions(options =>
                {
                    // 忽略循环引用
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    // 设置属性命名策略为驼峰命名
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    // 枚举转换为字符串
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    // 允许读取注释
                    options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                });

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
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Auth.ClientTestAPI");
            //    c.OAuthClientId("AuthClientTestApiSwaggerUI");
            //    c.OAuthAppName("AuthClientTestApi Swagger UI");

            //});
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
