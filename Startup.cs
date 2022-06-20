using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using StudentManApi.Models;
using StudentManApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManApi
{
    public class Startup
    {
        public IConfiguration _confirguration { get; }
        public Startup(IConfiguration configuration)
        {
            _confirguration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<Teacher, IdentityRole>().AddEntityFrameworkStores<SchoolDbcontext>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                options =>
                {
                    var secretByte = Encoding.UTF8.GetBytes(_confirguration["Authentication:SecretKey"]);
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _confirguration["Authentication:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = _confirguration["Authentication:Audience"],

                        ValidateLifetime = true,

                        IssuerSigningKey = new SymmetricSecurityKey(secretByte)
                    };
                });
            //引入MVC
            services.AddControllers(setupAction => {
                setupAction.ReturnHttpNotAcceptable = true;//若为false则忽略请求头accept指定的格式
            })
            .AddNewtonsoftJson(setupAction=> { //若与下方xml写反，则当patch时会先默认使用xml
                setupAction.SerializerSettings.ContractResolver=
                                    new CamelCasePropertyNamesContractResolver();//注册解析jsondocument
            })
            .AddXmlDataContractSerializerFormatters()//内容协商,可以使用Xml输出数据
            .ConfigureApiBehaviorOptions(setupAction=> 
            {
                setupAction.InvalidModelStateResponseFactory = context =>//数据验证失败时自定义报错信息
                {
                    var problemDetail = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "any",
                        Title = "数据验证失败",
                        Status = StatusCodes.Status422UnprocessableEntity,//指定验证失败返回422
                        Detail = "查看详细说明",
                        Instance = context.HttpContext.Request.Path
                    };
                    problemDetail.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                    return new UnprocessableEntityObjectResult(problemDetail) {
                        ContentTypes = { "application/problem+json" }//指定错误信息的格式
                    };
                };
            });
            //数据仓库
            services.AddTransient<IStuRepository, StudentRepository>();
            //数据库连接
            services.AddDbContext<SchoolDbcontext>(option =>
            {
                option.UseSqlServer(_confirguration["DbContext:ConnectionString"]);
            });
            //automapper注入
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
        // 你在哪？
        app.UseRouting();
        // 你是谁？
        app.UseAuthentication();
        // 你可以干什么？有什么权限？
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllers();
            });
        }
    }
}
