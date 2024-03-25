using Microsoft.EntityFrameworkCore;
using Integrity.Data;
using Integrity.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("IntegrityContextConnection") ?? throw new InvalidOperationException("Connection string 'IntegrityContextConnection' not found.");

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

        using (var scope = app.Services.CreateScope())
        {
            var roleManager =
                scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "User", "Guest" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        using (var scope = app.Services.CreateScope())
        {
            var userManager =
                scope.ServiceProvider.GetRequiredService<UserManager<IntegrityUser>>();

            string email = "particular0010abyss@gmail.com";
            string password = "Jija123!";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IntegrityUser
                {
                    UserName = email,
                    Email = email
                };

                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        app.Run();
    }
}