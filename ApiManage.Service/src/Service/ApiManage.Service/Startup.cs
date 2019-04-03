using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiManage.Service.Commons;
using ApiManage.Service.Filters;
using ApiManage.Service.Models;
using CommonExtention.Core.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace ApiManage.Service
{
    public class Startup
    {
        public Startup(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            HostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            AppSettings.AllowCross = configuration.GetValue<bool>("AppSettings:AllowCross");
            AppSettings.OpenRegister = configuration.GetValue<bool>("AppSettings:OpenRegister");
            AppSettings.VerificationRegisterEmail = configuration.GetValue<bool>("AppSettings:VerificationRegisterEmail");
            AppSettings.EmailName = configuration.GetValue<string>("AppSettings:EmailConfig:Name");
            AppSettings.EmailHost = configuration.GetValue<string>("AppSettings:EmailConfig:Host");
            AppSettings.EmailPort = configuration.GetValue<int>("AppSettings:EmailConfig:Port");
            AppSettings.EmailAccount = configuration.GetValue<string>("AppSettings:EmailConfig:Account");
            AppSettings.EmailPassword = configuration.GetValue<string>("AppSettings:EmailConfig:Password");
            AppSettings.TokenValidDays = configuration.GetValue<int>("AppSettings:TokenValidDays");
            AppSettings.AutoUpdateTokenValidTime = configuration.GetValue<bool>("AppSettings:AutoUpdateTokenValidTime");
            AppSettings.AesKey = configuration.GetValue<string>("AppSettings:AesKey");
            AppSettings.AesIv = configuration.GetValue<string>("AppSettings:AesIv");
        }

        public IConfiguration Configuration { get; }

        public static IHostingEnvironment HostingEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(AuthorizationFilter));
                config.Filters.Add(typeof(GlobalParameterValidationFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
            {
                // 默认
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            // 跨域
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithHeaders(new string[] { "authorization" });
                    if (AppSettings.AllowCross) policy.AllowAnyOrigin().AllowAnyMethod();
                    if (AppSettings.AutoUpdateTokenValidTime) policy.WithExposedHeaders("token");
                });
            });

            // 数据库链接
            ApiManageContext.ConnectionString = Configuration.GetConnectionString("SqlServerConnection");

            // 启用缓存
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            HostingEnvironment = env;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\file")),    // 磁盘目录
                RequestPath = new Microsoft.AspNetCore.Http.PathString("/file"),                                                // 访问地址
            });

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseMvcWithDefaultRoute();
        }
    }
}
