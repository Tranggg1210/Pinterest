using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PixelPalette.Data;
using PixelPalette.Entities;
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
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private User? _user;

        public AccountRepository(PixelPaletteContext context, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims();
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

        private List<Claim> GetClaims() => new List<Claim> { new Claim(ClaimTypes.Name, _user!.UserName) };

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
            return await _userManager.CreateAsync(user, model.Password);
        }
        public async Task<bool> ChangePasswordAsync(ChangePasswordModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, model.OldPassword);

                if (!result || !model.NewPassword.Equals(model.ComfirmPassword))
                    return false;

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (changePasswordResult.Succeeded)
                    return true;
            }
            return false;
        }
    }
}
