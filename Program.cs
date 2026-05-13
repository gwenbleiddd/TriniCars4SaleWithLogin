using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TriniCars4SaleWithLogin.Authorization;
using TriniCars4SaleWithLogin.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

//add fallback authorization
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// Authorization handlers.
builder.Services.AddScoped<IAuthorizationHandler,
                      VehicleIsOwnerAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationHandler,
                      VehicleAdministratorsAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationHandler,
                      VehicleManagerAuthorizationHandler>();
//multi login
var servicesLog = builder.Services;
var configuration = builder.Configuration;

builder.Services.AddAuthentication();
   /*.AddGoogle(options =>
   {
       IConfigurationSection googleAuthNSection =
       configuration.GetSection("Authentication:Google");
       options.ClientId = googleAuthNSection["ClientId"];
       options.ClientSecret = googleAuthNSection["ClientSecret"];
   })
  .AddMicrosoftAccount(microsoftOptions =>
  {
      microsoftOptions.ClientId = configuration["Authentication:Microsoft:ClientId"];
      microsoftOptions.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
  })
   .AddFacebook(options =>
   {
       IConfigurationSection FBAuthNSection =
       configuration.GetSection("Authentication:Facebook");
       options.AppId = FBAuthNSection["AppId"];
       options.AppSecret = FBAuthNSection["AppSecret"];
   });*/

var app = builder.Build();

//seed vehicles
using (var scope = app.Services.CreateScope())
{
    
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    // requires using Microsoft.Extensions.Configuration;
    // Set password with the Secret Manager tool.
    // dotnet user-secrets set SeedUserPW <pw>

    var Pw = builder.Configuration.GetValue<string>("SeedUserPW");

    await SeedData.Initialize(services, Pw);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
