using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt
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

            builder.Services.AddScoped<IBudgetService, BudgetService>();
            builder.Services.AddScoped<IBuildingsService, BuildingsService>();
            builder.Services.AddScoped<IPanelsService, PanelsService>();
            builder.Services.AddScoped<IMaterialsService, MaterialsService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IServicesService, ServicesService>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

#if DEBUG

            using (var scope = app.Services.CreateScope())

            {

                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Rakendame kõik migratsioonid, et andmebaas oleks ajakohane

                context.Database.Migrate();  // Rakendab kõik migratsioonid, kui neid pole veel rakendatud

                // Täiendame andmebaasi, kui see on tühi

                SeedData.GenerateClients(context);
                SeedData.GeneratePanels(context);
                SeedData.GenerateMaterials(context);
                SeedData.GenerateService(context);
                SeedData.GenerateBuildings(context);
                SeedData.GenerateBudgets(context);

            }

#endif



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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();


            app.Run();
        }
    }
}