using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Areas.Admin.Hubs;
using SocialMedia.Areas.Admin.Services;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Data;
using SocialMedia.Data.Models;
using SocialMedia.Services;
using SocialMedia.Services.Interfaces;

namespace SocialMedia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                .AddDefaultIdentity<ApplicationUser>
                (options =>
                    {
                        options.SignIn.RequireConfirmedAccount = false;
                    })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddSignalR();

            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddTransient<ICustomEmailSender, CustomEmailSender>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ICountryService, CountryService>();

            //Admin Area
            builder.Services.AddScoped<IReportedPostService, ReportedPostService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<IStatisticService, StatisticService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAdminChatService, AdminChatService>();

            builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

            builder.Services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
                googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
            });

            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.AccessDeniedPath = "/Home/AccessDenied";
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
               name: "post",
               pattern: "Post/Profile/{username}",
               defaults: new { controller = "Post", action = "Profile" });

            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.MapHub<AdminChatHub>("adminChatHub");

            app.Run();
        }
    }
}
