using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using EShop.Data.Repository;
using EShop.Application.Services.Implementations;
using EShop.Application.Services.Interfaces;
using System.IO;
using Microsoft.AspNetCore.DataProtection;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using GoogleReCaptcha.V3.Interface;
using GoogleReCaptcha.V3;

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
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISmsService, SmsService>();
        services.AddHttpClient<ICaptchaValidator,GoogleReCaptchaValidator>();

        // استفاده از Configuration برای خواندن ConnectionString
        services.AddDbContext<EShop.Data.Context.ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddControllersWithViews();

        var keyPath = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Auth"));

        if (!keyPath.Exists)
            keyPath.Create();
            keyPath.Create();ّ

        services.AddDataProtection()
            .PersistKeysToFileSystem(keyPath)
            .SetApplicationName("ESop")
            .SetDefaultKeyLifetime(TimeSpan.FromDays(7));
        services.AddControllersWithViews();
        services.AddAuthentication(options =>
        {
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;


        }).AddCookie(options =>
        {
            options.LoginPath = "/Login";
            options.LogoutPath = "/Log-out";
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
            options.SlidingExpiration = true;

        });


    }


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
