using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using EShop.Data.Repository;
using EShop.Application.Services;
using EShop.Application.Services.Implementations;
using EShop.Application.Services.Interfaces;
using EShop.Application.Services.Implements;

public class Startup
{
    
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // این متد برای تزریق سرویس‌هاست
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRipository<>), typeof(GenericRipository<>));
            services.AddScoped<IUserService, IUserService>();
            services.AddScoped<ISmsService, SmsService>();
            

        // استفاده از Configuration برای خواندن ConnectionString
        services.AddDbContext<EShop.Data.Context.ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();
        }
        // ...
    


    // این متد برای تنظیمات Middleware (ترتیب اجرا) است
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
