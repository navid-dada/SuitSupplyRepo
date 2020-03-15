using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using WebApplication.EventListeners;
using WebApplication.Helper;
using WebApplication.Services;

namespace WebApplication
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
            services.AddLogging(x =>
            {
                x.ClearProviders();
                x.AddConfiguration(Configuration.GetSection("Logging"));
                x.AddNLog("NLog.config");
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
            }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.RegisterEasyNetQ(Configuration.GetSection("BusConnectionString").Value, c =>
            {
                
            });
            services.AddTransient<IOrderService, OrderService>();

            services.AddSingleton(typeof(OrderCreatedEventListener));
            services.AddSingleton(typeof(OrderCreationFailedEventListener));
            services.AddSingleton(typeof(OrderFinishedEventListener));
            services.AddSingleton(typeof(OrderFinishingFailedEventListener));
            services.AddSingleton(typeof(OrderPaidEventListener));
            services.AddSingleton(typeof(OrderPaymentFailedEventListener));
            services.AddSingleton(typeof(TaskManager));

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            await IdentityDataInitializer.SeedData(userManager, roleManager);
            app.ApplicationServices.GetService(typeof(OrderCreatedEventListener));
            app.ApplicationServices.GetService(typeof(OrderCreationFailedEventListener));
            app.ApplicationServices.GetService(typeof(OrderFinishedEventListener));
            app.ApplicationServices.GetService(typeof(OrderFinishingFailedEventListener));
            app.ApplicationServices.GetService(typeof(OrderPaidEventListener));
            app.ApplicationServices.GetService(typeof(OrderPaymentFailedEventListener));
            app.ApplicationServices.GetService(typeof(TaskManager));

        }
    }
}