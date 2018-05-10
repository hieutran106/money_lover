using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MoneyLover.Models;
using MoneyLover.Models.SeedData;
using Microsoft.AspNetCore.Identity;
using MoneyLover.Infrastructure;
using jsreport.AspNetCore;
using jsreport.Local;
using jsreport.Binary;

namespace MoneyLover
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IHostingEnvironment CurrentEnvironment { get; set; }
        public Startup(IConfiguration config, IHostingEnvironment env) {
            Configuration = config;
            CurrentEnvironment = env;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // START - config identity framework
            services.AddDbContext<AppDbContext>(options => {
                if (CurrentEnvironment.IsDevelopment())
                {
                    options.UseSqlServer(Configuration["Data:MoneyLoverDb:ConnectionString"]);
                }
                else
                {
                    options.UseSqlServer(RdsHelper.GetRdsConnectionString(Configuration));
                }
                //options.UseSqlServer(RdsHelper.GetRdsConnectionString(Configuration));
            });

            services.AddIdentity<AppUser, IdentityRole>(opts => {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60*12);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/bbbb";
                options.SlidingExpiration = true;
            });
            services.AddTransient<UserManager<AppUser>>();
            // END - config identity framework
            services.AddTransient<IRepository, EFRepository>();
            services.AddMvc(opts => {
                opts.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
            });
            //pdf
            services.AddJsReport(new LocalReporting()
                .UseBinary(JsReportBinary.GetBinary())
                .AsUtility()
                .Create());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            SeedData.EnsurePopulated(app);          
        }
    }
}
