using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using System;
using System.IO;
using System.IO.Compression;
using jhray.com.Database;
using jhray.com.Database.Entities;
using jhray.com.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = long.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });

            var user = Configuration.GetSection("EnvModel").GetValue<string>("UserName");
            var password = Configuration.GetSection("EnvModel").GetValue<string>("Password");
            var sqlConnectionString = string.Format(Configuration.GetConnectionString("Postgres"), user, password);

            services.AddDbContext<ChilledDbContext>(options =>
            {
                options.UseNpgsql(sqlConnectionString, b => b.MigrationsAssembly("jhray.com"));
            });

            services.AddIdentity<ChilledUser, IdentityRole>()
                .AddEntityFrameworkStores<ChilledDbContext>()
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
                options.LoginPath = "/GateKeeper/Login";
                options.AccessDeniedPath = "/Account/Forbidden";
                options.SlidingExpiration = true;
            });


            services.AddAuthorization();
            services.AddAuthentication();
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddMvc( config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName =="Debug")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseResponseCompression();
            
            app.UseStaticFiles();
            var paths = Configuration.GetSection("Paths").Get<Paths>();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(paths.StaticFilesDirectory)),
                    RequestPath = "/Uploads"
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "chilledCast",
                    pattern: "ChilledCast",
                    defaults: new { controller = "Home", action = "ChilledESports" });
                endpoints.MapControllerRoute(
                    name: "chilledLong",
                    pattern: "ChilledESports",
                    defaults: new { controller = "Home", action = "ChilledESports" });
                endpoints.MapControllerRoute(
                    name: "chilledShort",
                    pattern: "chilled",
                    defaults: new { controller = "Home", action = "ChilledESports" });
                endpoints.MapControllerRoute(
                    name: "yph",
                    pattern: "yowies",
                    defaults: new { controller = "Home", action = "YowiePowerHour" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
