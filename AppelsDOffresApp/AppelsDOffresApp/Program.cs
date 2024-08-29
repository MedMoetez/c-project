using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppelsDOffresApp.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using AppelsDOffresApp.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity setup using Utilisateur and IdentityRole
builder.Services.AddIdentity<Utilisateur, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddSignalR();
builder.Services.AddHostedService<DeadlineNotificationService>();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<ChatHub>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chathub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Role seeding and admin user creation
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Utilisateur>>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        var context = scope.ServiceProvider.GetService<AppDbContext>();
        context.Database.EnsureCreated();

        // Create roles
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Check if the admin user exists
        var adminUser = await userManager.FindByEmailAsync("admin@example.com");
        if (adminUser == null)
        {
            // Create the admin user
            adminUser = new Utilisateur
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(adminUser, "Admin123!");

            if (result.Succeeded)
            {
                // Assign the admin user to the Admin role
                await userManager.AddToRoleAsync(adminUser, "Admin");
                logger.LogInformation("Admin user created successfully.");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    logger.LogError($"Error creating admin user: {error.Description}");
                }
            }
        }
        else
        {
            var isInRole = await userManager.IsInRoleAsync(adminUser, "Admin");
            if (!isInRole)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                logger.LogInformation("Admin user already exists and was assigned to the Admin role.");
            }
            else
            {
                logger.LogInformation("Admin user already exists and is already assigned to the Admin role.");
            }
        }
    }
    catch (DbUpdateException ex)
    {
        var innerException = ex.InnerException;
        logger.LogError($"Database update error: {innerException?.Message}");
    }
    catch (Exception ex)
    {
        logger.LogError($"Unexpected error: {ex.Message}");
    }
}

// Additional route configuration
app.MapControllerRoute(
    name: "publicChat",
    pattern: "{controller=PublicChat}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "privateChat",
    pattern: "PrivateChat/{action=Index}/{toUserId?}",
    defaults: new { controller = "PrivateChat" });

app.Run();
