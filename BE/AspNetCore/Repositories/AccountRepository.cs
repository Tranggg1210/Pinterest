using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PixelPalette.Data;
using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Interfaces;
using PixelPalette.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PixelPalette.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PixelPaletteContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private User? _user;

        public AccountRepository(PixelPaletteContext context, IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            _user!.Token = token;
            _context.Users.Update(_user);
            await _context.SaveChangesAsync();

            return token;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256Signature);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var authClaims = new List<Claim> { new Claim(ClaimTypes.Name, _user!.UserName) };
            var userRoles = await _userManager.GetRolesAsync(_user!);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
            return authClaims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JWT:expires"])),
                claims: claims,
                signingCredentials: signingCredentials
            );
        }

        public async Task<bool> SignInAsync(SignInModel model)
        {
            _user = await _userManager.FindByNameAsync(model.UserName);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, model.Password));

            if (!result)
                return false;

            return result;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Member");
            }
            return result;
        }
        public async Task<bool> ChangePasswordAsync(string userName, ChangePasswordParams entryParams)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, entryParams.OldPassword);

                if (!result && !entryParams.NewPassword.Equals(entryParams.ComfirmPassword))
                    return false;

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, entryParams.OldPassword, entryParams.NewPassword);
                if (changePasswordResult.Succeeded)
                    return true;
            }
            return false;
        }
    }
}
