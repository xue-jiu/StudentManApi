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
            //????MVC
            services.AddControllers(setupAction => {
                setupAction.ReturnHttpNotAcceptable = true;//????false????????????accept??????????
            })
            .AddNewtonsoftJson(setupAction=> { //????????xml??????????patch??????????????xml
                setupAction.SerializerSettings.ContractResolver=
                                    new CamelCasePropertyNamesContractResolver();//????????jsondocument
            })
            .AddXmlDataContractSerializerFormatters()//????????,????????Xml????????
            .ConfigureApiBehaviorOptions(setupAction=> 
            {
                setupAction.InvalidModelStateResponseFactory = context =>//????????????????????????????
                {
                    var problemDetail = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "any",
                        Title = "????????????",
                        Status = StatusCodes.Status422UnprocessableEntity,//????????????????422
                        Detail = "????????????",
                        Instance = context.HttpContext.Request.Path
                    };
                    problemDetail.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                    return new UnprocessableEntityObjectResult(problemDetail) {
                        ContentTypes = { "application/problem+json" }//??????????????????
                    };
                };
            });
            //????????
            services.AddTransient<IStuRepository, StudentRepository>();
            //??????????
            services.AddDbContext<SchoolDbcontext>(option =>
            {
                option.UseSqlServer(_confirguration["DbContext:ConnectionString"]);
            });
            //automapper????
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
        // ????????
        app.UseRouting();
        // ????????
        app.UseAuthentication();
        // ??????????????????????????
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
