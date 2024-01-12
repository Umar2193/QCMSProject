using Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace QCMS
{
    public class Startup
    {
        private static IConfiguration Configuration ;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.Configure<ConnectionClass>(Configuration.GetSection("AppSettings"));
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(50); // Adjust this value
                                                                // Other session configuration options...
            });
         
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
            
        }
    }
}
