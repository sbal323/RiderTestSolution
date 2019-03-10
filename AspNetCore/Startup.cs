using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.BL;
using AspNetCore.BL.Contracts;
using AspNetCore.Configuration;
using AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AspNetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();
            //Configuration = configuration;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
//            services.Configure<CookiePolicyOptions>(options =>
//            {
//                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
//                options.CheckConsentNeeded = context => true;
//                options.MinimumSameSitePolicy = SameSiteMode.None;
//            });
////
//
           services.AddSingleton<IConfigurationRoot>(Configuration);
           services.AddOptions();
           services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

           services.AddAuthentication(options =>
               {
                   options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                   options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                   options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                   options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
               })
               .AddCookie(options =>
               {
                   options.LoginPath = new PathString("/Account/Login");
                   options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                   options.SlidingExpiration = true;
                   options.AccessDeniedPath = new PathString("/Account/Denied");
               });
            services.AddAuthentication(TwitterDefaults.AuthenticationScheme)
                .AddTwitter(options =>
                {
                    options.SignInScheme = "TEMP";
                    options.ConsumerKey = "IUFmcK7v4KNuyTmY4E8nPfTUa";
                    options.ConsumerSecret = "";
                })
                .AddCookie("TEMP");
           services.AddMvc(options =>
           {
               options.Filters.Add(new CultureAttribute());
           })
               .AddRazorOptions(options => options.ViewLocationFormats.Insert(0,"/Customizations/Views/{1}/{0}.cshtml"))
               .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
           
           

           services.AddTransient(typeof(IMovieRepository), typeof(MovieRepository));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
//
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();
                //app.UseMvcWithDefaultRoute();

            //
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

//            app.Run(async (context) =>
//            {
//                if (context.Request.Path.ToString().Contains("json"))
//                {
//                    await context.Response.WriteAsync(JsonConvert.SerializeObject(env));
//                }
//                else
//                {
//                    await context.Response .WriteAsync($"<html><body>Courtesy of <b> Programming ASP.NET Core </b>!<hr>{SsoSettings.DefaultPassword}ENVIRONMENT = {env.EnvironmentName} </body></html>"); 
//                }
//            });
        }
    }
}