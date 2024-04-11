using System.Configuration;
using IntexQueensSlay.Data;
using IntexQueensSlay.Models;
using IntexQueensSlay.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace IntexQueensSlay
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            var connectionStringLego = builder.Configuration.GetConnectionString("LegoConnection") ?? throw new InvalidOperationException("Connection string 'LegoConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                // Configure ApplicationDbContext to use SQL Server and enable retry on failure
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure();
                });
            });

            builder.Services.AddDbContext<LegoContext>(options =>
            {
                // Configure LegoContext to use SQL Server and enable retry on failure
                options.UseSqlServer(connectionStringLego, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure();
                });
            });

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddRazorPages();

            builder.Services.AddScoped<ISlayRepository, EFSlayRepository>();

            builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            builder.Services.AddSingleton<IHttpContextAccessor,
                HttpContextAccessor>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Add Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 4;
                options.Lockout.AllowedForNewUsers = true;

                // Add better Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 5;
            });

            builder.Services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            });

            builder.Services.AddSession();

            var app = builder.Build();

            //Add CSP header middleware
            // Add CSP header middleware

            //app.Use(async (context, next) => {
            //    context.Response.Headers.Add("script-src 'self' https://code.jquery.com/jquery-3.5.1.min.js https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js https://cdnjs.cloudflare.com/ajax/libs/moment-timezone/0.5.31/moment-timezone-with-data.js https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js https://cdn.jsdelivr.net/npm/axios/dist/axios.min.js; style-src 'self' https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css; font-src 'self' https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/webfonts/fa-brands-400.woff2 https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/webfonts/fa-brands-400.woff https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/webfonts/fa-brands-400.ttf https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/webfonts/fa-regular-400.woff2 https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/webfonts/fa-regular-400.woff https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/webfonts/fa-regular-400.ttf https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/webfonts/fa-solid-900.woff2 https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/webfonts/fa-solid-900.woff https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/webfonts/fa-solid-900.ttf; img-src 'self'; frame-src 'self'");

            //    await next();
            //});
            // Add CSP header middleware
        //    app.Use(async (context, next) =>
        //    {
        //        context.Response.Headers.Add("Content-Security-Policy-Report-Only", "default-src 'self'; " +
        //            "script-src 'self' 'unsafe-inline' https://localhost:44365/js/cookieConsent.js https://localhost:44365/js/site.js?v=hRQyftXiu1lLX2P9Ly9xa4gHJgLeR1uGN5qegUobtGo https://localhost:44365/lib/bootstrap/dist/js/bootstrap.bundle.min.js https://localhost:44365/lib/jquery/dist/jquery.min.js; " + // 'unsafe-inline' is used to allow inline scripts, use it with caution
        //            "style-src 'self'; 'unsafe-inline';" +
        //            "script-src-elem 'self' 'unsafe-inline' 'unsafe-eval' 'strict-dynamic' 'nonce-{nonce}' " +
        //"https://www.google-analytics.com https://www.googletagmanager.com https://localhost:44365;" +
        //            "font-src 'self'; " +
        //            "img-src 'self' data:; " +
        //            "media-src 'self'; " +
        //            "frame-src 'self'; " +
        //            "connect-src 'self'; " +
        //            "worker-src 'self'; " +
        //            "frame-ancestors 'self'; " +
        //            "form-action 'self'; " +
        //            "base-uri 'self'; " +
        //            "manifest-src 'self'; " +
        //            "object-src 'none'; " + // prevent loading any objects (e.g., Flash)
        //            "base-uri 'self'; " +
        //            "manifest-src 'self'; " +
        //            "require-sri-for script style; " + // require Subresource Integrity for scripts and stylesheets
        //            "script-src-elem 'self' 'unsafe-inline' 'unsafe-eval' " + // allow 'unsafe-inline' and 'unsafe-eval' for scripts loaded as elements
        //            "'strict-dynamic' 'nonce-{nonce}' https://www.google-analytics.com https://www.googletagmanager.com; " +
        //            "style-src-elem 'self' 'unsafe-inline' https://fonts.googleapis.com; " +
        //            "frame-ancestors 'none'; " + // prevent the document from being used in an iframe
        //            "upgrade-insecure-requests;" + // always attempt to load HTTPS resources
        //            "block-all-mixed-content;" + // prevent loading mixed (HTTP and HTTPS) content
        //            "reflected-xss block; " + // enable XSS filtering
        //            "referrer origin-when-cross-origin; " + // only send the origin of the document as the referrer for same-origin requests
        //            "feature-policy 'none'; " + // disable all browser features
        //            "sandbox allow-forms allow-same-origin allow-scripts; " + // sandbox the document
        //            "report-uri https://example.com/csp-report-endpoint"); // report violations to the specified URL

        //        await next();
        //    });




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Add CSP header middleware
           

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                        name: "ProductDetails",
                        pattern: "Home/ProductDetails/{id}",
                        defaults: new { Controller = "Home", action = "ProductDetails" }
                    );

            app.MapControllerRoute(
                name: "AboutUs",
                pattern: "AboutUs",
                defaults: new { Controller = "Home", action = "AboutUs" }
            );

            app.MapControllerRoute(
                name: "Index",
                pattern: "Index",
                defaults: new { Controller = "Home", action = "Index" }
            );

            app.MapControllerRoute(
                name: "ProductsByCategoryAndPage",
                pattern: "{productCat}/{pageNum:int}",
                defaults: new { Controller = "Home", action = "Products" }
            );

            app.MapControllerRoute(
                name: "ProductsByPage",
                pattern: "{pageNum:int}",
                defaults: new { Controller = "Home", action = "Products", productCat = (string)null }
            );

            app.MapControllerRoute(
                name: "ProductsByCategory",
                pattern: "{productCat}",
                defaults: new { Controller = "Home", action = "Products", pageNum = 1 }
            );

            app.MapDefaultControllerRoute();


            //app.MapControllerRoute("AboutUs", "AboutUs", new { Controller = "Home", action = "AboutUs" });
            ////app.MapControllerRoute("ProductDetails", "{id}", new { Controller = "Home", action = "ProductDetails"});
            //app.MapControllerRoute("Index", "Index", new { Controller = "Home", action = "Index" });
            ////app.MapControllerRoute("Secret", "Secret", new { Controller = "Home", action = "Secret" });
            //app.MapControllerRoute("pagenumandcat", "{productCat}/{pageNum}", new { Controller = "Home", action = "Products" });
            //app.MapControllerRoute("pagination", "{pageNum}", new { Controller = "Home", action = "Products", pageNum = 1 });
            //app.MapControllerRoute("productCat", "{productCat}", new { Controller = "Home", action = "Products", pageNum = 1 });
            //app.MapDefaultControllerRoute();

            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin", "Customer", "User" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string email = "admin@gmail.com";
                string password = "Admin1234!";


                if (await userManager.FindByEmailAsync(email)==null)
                {
                    var usera = new IdentityUser();
                    usera.UserName = email;
                    usera.Email = email;
                    usera.EmailConfirmed = true;

                    await userManager.CreateAsync(usera, password);

                    await userManager.AddToRoleAsync(usera, "Admin");
                }
                
            }

            app.Run();
        }
    }
}
