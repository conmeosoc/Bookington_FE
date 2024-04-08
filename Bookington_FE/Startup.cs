using Microsoft.Extensions.FileProviders;
using System.Configuration;

namespace Bookington_FE
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
            //Config
            ConfigAppSetting.LoadConfig(Configuration);
            //
            services.AddControllersWithViews();
            //
            //Session
            services.AddDistributedMemoryCache();
            //services.AddCors();
            services.AddSession(cfg =>
            {
                cfg.Cookie.Name = "Bookington_FE";
                cfg.IdleTimeout = new TimeSpan(0, 0, ConfigAppSetting.SessionTimeout);
            });
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //use session
            app.UseSession();
            //
            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseCors(opt => opt.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();

            app.UseAuthorization();
            //
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider("D:\\NewWeb\\Bookington_FE\\Bookington_FE\\wwwroot\\Asset"),
                RequestPath = "/Asset"
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Transaction}/{action=Index}/{id?}");
            });
        }

    }
}
