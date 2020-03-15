using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using SuitSupply.Messages;
using SuitSupply.Order.Repositories;

namespace SuitSupply.Order
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public  void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(x =>
            {
                x.ClearProviders();
                x.AddConfiguration(Configuration.GetSection("Logging"));
                x.AddNLog("NLog.config");
            });
            services.AddDbContext<SuitSupplyContext>(opt=> opt.UseSqlServer(Configuration.GetConnectionString("conStr")));
            services.RegisterEasyNetQ(Configuration.GetSection("BusConnectionString").Value);
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient(typeof(CreateOrderHandler));
            services.AddTransient(typeof(OrderFinishedHandler));
            services.AddTransient(typeof(OrderPaidHandler));
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.ApplicationServices.GetService(typeof(CreateOrderHandler));
            app.ApplicationServices.GetService(typeof(OrderPaidHandler));
            app.ApplicationServices.GetService(typeof(OrderFinishedHandler));

        }
    }
}