using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ModelClasses;
using Services;
using DAL.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Soccer_Manager
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
            services.AddMvc();

            string connnection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<SoccerContext>(options => options.UseSqlServer("safasf"));
            services.AddTransient<DataContextProvider>();
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IHighLevelSoccerManagerService, HighLevelSoccerManagerService>();
            services.AddTransient<ILowLevelSoccerManagmentService, LowLevelSoccerManagmerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=SoccerManager}/{id?}");
            });

        }

    }

}
