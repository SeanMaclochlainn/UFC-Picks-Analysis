using FightData.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace FightDataUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<FightPicksContext>(options => options.UseSqlServer(Configuration.GetConnectionString("FightPicks")));
            services.AddScoped<FightPicksContext>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            SetApplicationCulture("en-IE");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Exhibition/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Exhibition}/{action=Index}/{id?}");
            });
        }

        private static void SetApplicationCulture(string name)
        {
            CultureInfo cultureInfo = new CultureInfo(name);
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}
