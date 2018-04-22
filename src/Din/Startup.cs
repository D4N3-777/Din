﻿using System.Text;
using Din.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Din
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => { options.LoginPath = "/"; });
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession(options => { options.Cookie.Name = "DinCookie"; });
            var mysqlConnectionString = Configuration.GetConnectionString("MysqlConnectionString");
            services.AddDbContext<DinContext>(options =>
                options.UseMySql(
                    mysqlConnectionString)
            );
            services.AddLocalization(o => o.ResourcesPath = "Resources");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseAuthentication();
            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute("Login", "Home",
                    defaults: new { controller = "Authentication", action = "Login" });
                routes.MapRoute("Logout", "Logout",
                    defaults: new {controller = "Authentication", action = "Logout"});
                routes.MapRoute("SearchMovie", "MovieResults",
                    defaults: new {controller = "Content", Action = "SearchMovie"});
                routes.MapRoute("AddMovie", "AddMovie",
                    defaults: new { controller = "Content", Action = "AddMovie" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Main}/{action=Index}/{id?}");
            });
        }
    }
}
