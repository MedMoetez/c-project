using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

public static class RoleSeeder
{
    private static readonly string[] Roles = new string[] { "Admin", "Gestionnaire", "Collaborateur" };

    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in Roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                if (!result.Succeeded)
                {
                    
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating role {role}: {error.Description}");
                    }
                }
            }
        }
    }
}
