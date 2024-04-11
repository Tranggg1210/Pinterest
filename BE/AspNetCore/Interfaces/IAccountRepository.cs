using Microsoft.AspNetCore.Identity;
using PixelPalette.Entities;
using PixelPalette.Models;

namespace PixelPalette.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel model);
        Task<bool> SignInAsync(SignInModel model);
        Task<bool> ChangePasswordAsync(ChangePasswordModel model);
        Task<string> CreateToken();
    }
}
