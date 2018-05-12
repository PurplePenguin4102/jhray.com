﻿using System;
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
