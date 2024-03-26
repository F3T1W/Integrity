using Microsoft.EntityFrameworkCore;
using Integrity.Data;
using Integrity.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using sib_api_v3_sdk.Client;

public class Program
{
    private static void /*async Task*/ Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("IntegrityContextConnection") ?? throw new InvalidOperationException("Connection string 'IntegrityContextConnection' not found.");
        Configuration.Default.ApiKey.Add("api-key", builder.Configuration["BrevoAPI:APIKey"]);

        builder.Services.AddDbContext<IntegrityContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddDefaultIdentity<IntegrityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IntegrityContext>();

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.MapRazorPages();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        //using (var scope = app.Services.CreateScope())
        //{
        //    var roleManager =
        //        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //    var roles = new[] { "Admin", "User" };

        //    foreach (var role in roles)
        //    {
        //        if (!await roleManager.RoleExistsAsync(role))
        //            await roleManager.CreateAsync(new IdentityRole(role));
        //    }
        //}

        //using (var scope = app.Services.CreateScope())
        //{
        //    var userManager =
        //        scope.ServiceProvider.GetRequiredService<UserManager<IntegrityUser>>();

        //    string email = "particular0010abyss@gmail.com";
        //    string password = "Cbkf[23rekfrf!";

        //    var x = await userManager.FindByEmailAsync(email);

        //    if (x is not null)
        //        await userManager.AddToRoleAsync(x, "Admin");

        //}

        app.Run();
    }
}