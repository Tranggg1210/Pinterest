using Microsoft.AspNetCore.Identity;
using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Models;

namespace PixelPalette.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel model);
        Task<bool> SignInAsync(SignInModel model);
        Task<bool> ChangePasswordAsync(string userName, ChangePasswordParams model);
        Task<string> CreateToken();
    }
}
