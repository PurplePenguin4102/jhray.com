using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using jhray.com.Engine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.OData.Query.SemanticAst;
using jhray.com.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using jhray.com.Repository;
using System.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using jhray.com.Models;
using Microsoft.AspNetCore.Identity;

namespace jhray.com
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Paths>(Configuration.GetSection("Paths"));
            var direc = Configuration.GetSection("Paths").GetValue<string>("CredsDirectory");
            
            var sqlConnectionString = string.Format(Configuration.GetConnectionString("Postgres"), GetUsernameFromFile(direc), GetPasswordFromFile(direc));

            services.AddEntityFrameworkNpgsql().AddDbContext<JhrayDataContext>(options =>
                options.UseNpgsql(
                    sqlConnectionString,
                    b => b.MigrationsAssembly("jhray.com")));
            //services.AddDbContext<JhrayDataContext>(options =>
            //    options.UseNpgsql(
            //        sqlConnectionString,
            //        b => b.MigrationsAssembly("jhray.com")
            //    )
            //);

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<JhrayDataContext>()
                .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 5;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Account/Unauthorized";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/Forbidden";
                options.SlidingExpiration = true;
            });


            services.AddAuthorization();
            services.AddAuthentication();
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();
            services.AddScoped<IJhrayRepository, JhrayRepository>();
            services.AddMvc( config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        private string GetPasswordFromFile(string direc)
        {
            return File.ReadAllText(Path.Combine(direc, @"Password.txt"));
        }

        private string GetUsernameFromFile(string direc)
        {
            return File.ReadAllText(Path.Combine(direc, @"UserName.txt"));
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseResponseCompression();

            app.UseStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "chilledCast",
                    template: "ChilledCast",
                    defaults: new { controller = "Home", action = "ChilledESports" });
                routes.MapRoute(
                    name: "chilledLong",
                    template: "ChilledESports",
                    defaults: new { controller = "Home", action = "ChilledESports" });
                routes.MapRoute(
                    name: "chilledShort",
                    template: "chilled",
                    defaults: new { controller = "Home", action = "ChilledESports" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            app.UseAuthentication();


        }
    }
}
