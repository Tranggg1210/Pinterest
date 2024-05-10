using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PixelPalette.Data;
using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Interfaces;
using PixelPalette.Models;

namespace PixelPalette.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly PixelPaletteContext _context;
        private readonly UserManager<User> _userManager;

        public AdminRepository(PixelPaletteContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IList<string>> EditRoles(int id, List<string> roles)
        {
            var selectedRoles =roles.Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1)).ToList();
            
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null!;

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach(string role in selectedRoles) 
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    return null!;
                }
            }

            var result = await _userManager.AddToRolesAsync(user, selectedRoles);
            if (!result.Succeeded) return null!;

            result = await _userManager.RemoveFromRolesAsync(user, userRoles);
            if (!result.Succeeded) return null!;

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<UserRoleModel>> GetUsersWithRole()
        {
            var roles = await _userManager.Users
                .Include(d => d.UserRoles)
                .OrderBy(u => u.UserName)
                .Select(u => new UserRoleModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Roles = u.UserRoles.Select(ur => _context.Roles.FirstOrDefault(r => r.Id == ur.RoleId)!.Name).ToList()
                })
                .ToListAsync();
            return roles;
        }
    }
}
