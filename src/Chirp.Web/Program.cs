using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Chirp.Core;
using Chirp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    var connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services.AddDbContext<ChirpContext>(option => option.UseSqlServer(connectionString));
}
else if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ChirpContext>(option => option.UseSqlite("Data Source=mychirp.db"));
}

builder.Services.AddScoped<ICheepRepository, CheepRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<CheepValidator>();

builder.Services.AddDefaultIdentity<Author>()
    .AddEntityFrameworkStores<ChirpContext>();

builder.Services.AddAuthentication()
.AddGitHub(options =>
{
    options.ClientId = Environment.GetEnvironmentVariable("GITHUB_CLIENT_ID") ?? "No_Client_Id_Set";
    options.ClientSecret = Environment.GetEnvironmentVariable("GITHUB_CLIENT_SECRET") ?? "No_Client_Secret_Set";
    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "username");
    options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
});

builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ChirpContext>();

    if (app.Environment.IsDevelopment())
    {
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        DbInitializer.SeedDatabase(context);
    }
    else if (app.Environment.IsProduction())
    {
        context.Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});
app.MapRazorPages();

app.Run();

public partial class Program { }