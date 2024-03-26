using Microsoft.EntityFrameworkCore;
using Integrity.Data;
using Integrity.Areas.Identity.Data;
using sib_api_v3_sdk.Client;

var builder = WebApplication.CreateBuilder(args);

Configuration.Default.ApiKey.Add("api-key", builder.Configuration["BrevoAPI:APIKey"]);

var connectionString = builder.Configuration.GetConnectionString("IntegrityContextConnection") ?? throw new InvalidOperationException("Connection string 'IntegrityContextConnection' not found.");

builder.Services.AddDbContext<IntegrityContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IntegrityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IntegrityContext>();
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

app.Run();
