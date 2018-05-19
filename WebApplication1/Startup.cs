using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Services;
using DAL.Repository_Realisation;
using DAL.Model_Classes;
using DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApplication1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SoccerContext>(options => options.UseSqlServer(connection));
            string authConnection = Configuration.GetConnectionString("AuthConnection");
            services.AddDbContext<Authentication_Context>(options => options.UseSqlServer(authConnection));

            /*services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });*/

            services.AddIdentity<DAL.Model_Classes.User, IdentityRole>()
                .AddEntityFrameworkStores<Authentication_Context>();
            
            services.AddTransient<DataContextProvider>();
            services.AddTransient<IRepository<Team>, TeamRepository>();
            services.AddTransient<IRepository<Player>, PlayerRepository>();
            services.AddTransient<IRepository<Tournament>, TournamentRepository>();
            services.AddTransient<IRepository<Reward>, RewardRepository>();
            services.AddTransient<IHighLevelSoccerManagerService, HighLevelSoccerManagerService>();
            services.AddTransient<ILowLevelSoccerManagmentService, LowLevelSoccerManagerService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMemoryCache();

            services.AddMvc();
            services.AddSession();
            services.AddDistributedMemoryCache();

            //services.AddTransient<DataProvider>();
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
            app.UseSession();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
