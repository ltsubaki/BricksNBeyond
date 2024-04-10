using IntexQueensSlay.Data;
using IntexQueensSlay.Models;
using IntexQueensSlay.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IntexQueensSlay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDbContext<LegoContext>(options =>
            {
                options.UseSqlite(builder.Configuration["ConnectionStrings:LegoConnection"]);
            });


            builder.Services.AddRazorPages();

            builder.Services.AddScoped<ISlayRepository, EFSlayRepository>();

            builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            builder.Services.AddSingleton<IHttpContextAccessor,
                HttpContextAccessor>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
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
                pattern: "Products/{productCat}/{pageNum:int}",
                defaults: new { Controller = "Home", action = "Products" }
            );

            app.MapControllerRoute(
                name: "ProductsByPage",
                pattern: "Products/Page/{pageNum:int}",
                defaults: new { Controller = "Home", action = "Products", productCat = (string)null }
            );

            app.MapControllerRoute(
                name: "ProductsByCategory",
                pattern: "Products/{productCat}",
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

            app.Run();
        }
    }
}
