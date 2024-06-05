using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Models;
using AutoMapper;

namespace PixelPalette.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager,
           RoleManager<Role> roleManager, IMapper mapper)
        {
            if (await userManager.Users.AnyAsync()) return;

            //var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            //var users = JsonSerializer.Deserialize<List<User>>(userData);
            //if (users == null) return;
            //foreach (var user in users)
            //{
            //    user.UserName = user.UserName.ToLower();
            //    await userManager.CreateAsync(user, "abcxyz123$");
            //    await userManager.AddToRoleAsync(user, "Member");
            //}

            var roles = new List<Role>
            {
                new Role{Name = "Member"},
                new Role{Name = "Admin"},
                new Role{ Name = "Blocker"}
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            var adminSignUp = new SignUpModel
            {
                Email = "admin@gmail.com",
                Password = "abcxyz123$"
            };

            var adminRoles = new List<string>() { "Member", "Admin" };
            var admin = mapper.Map<User>(adminSignUp);
            await userManager.CreateAsync(admin, adminSignUp.Password);
            await userManager.AddToRolesAsync(admin, adminRoles);
        }
    }
}
