﻿//using AecTesteSeth.API;
using AeCTesteSeth.DAL.Dependency_Injection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
//using AeCTesteSeth.API.Data;
using Microsoft.AspNetCore.Components.Web;
//using MudBlazor.Services;

//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Mvc;
using AeCTesteSeth.BLL.Models;
using AeCTesteSeth.DAL.Context;
using AeCTesteSeth.WEB_APP.Controllers;

namespace AeCTesteSeth.WEB_APP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
           // configuration.root
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc( v => v.EnableEndpointRouting = true);
            services.AddDALServices(Configuration);
           
            services.AddControllers();
            //.RootComponents.Add<App>("#app");
           // builder.RootComponents.Add<HeadOutlet>("head::after");
           
           // services.AddMudServices();

           


            //services.AddHttpClient("API", client => {
            //    client.BaseAddress = new Uri(Configuration["APIServer:Url"]!);
            //    client.DefaultRequestHeaders.Add("Accept", "application/json");
            //});
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AeCTesteSeth.API", Version = "v1" });
            });
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddAuthentication
                 (JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,

                         ValidIssuer = Configuration["Jwt:Issuer"],
                         ValidAudience = Configuration["Jwt:Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey
                       (Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                     };
                 });
            
            services.AddControllersWithViews();
            ////services.AddDbContext<AeCTesteSethAPIContext>(options =>
            ////        options.UseSqlServer(Configuration.GetConnectionString("AeCTesteSethAPIContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            
            app.UseFileServer();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                
                app.UseSwaggerUI(c => 
                { 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AeCTesteSeth.API v1");
                    c.RoutePrefix = "endpoints";
                });
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.MapGet();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Login}/{action=Index}/{id?}");
            //});
            app.UseEndpoints(endpoints =>
            {
                
                endpoints
                .MapControllerRoute(
                    name: "login",
                    pattern: "{controller=Login}/{action=Index}/{id?}"
                 );
                endpoints.MapControllers();
            });

           


        }     
    }
}
