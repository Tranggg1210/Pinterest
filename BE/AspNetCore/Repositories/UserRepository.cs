
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PixelPalette.Data;
using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Interfaces;
using PixelPalette.Models;
using PixelPalette.Services;

namespace PixelPalette.Repositories
{
    public class UserRepository : TransmitService, IUserRepository
    {
        private readonly PixelPaletteContext _context;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UserRepository(PixelPaletteContext context, IMapper mapper, IPhotoService photoService)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var deleteUser = _context.Users!.SingleOrDefault(u => u.Id == id);
            if (deleteUser != null)
            {
                _context.Users!.Remove(deleteUser);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<string> EditAvatar(int id, IFormFile file)
        {
            var user = await _context.Users!.FindAsync(id);

            if (user != null)
            {
                var addResult = await _photoService.AddPhotoAsync(file);
                if (addResult.Error != null) return string.Empty;

                if (user.AvatarId != null)
                {
                    var deleteResult = await _photoService.DeletePhotoAsync(user.AvatarId);
                    if (deleteResult.Error != null) return string.Empty;
                }
                try
                {
                    var Url = addResult.SecureUrl.AbsoluteUri;
                    user.AvatarUrl = Url;
                    user.AvatarId = addResult.PublicId;
                    _context.Users!.Update(user);
                    await _context.SaveChangesAsync();
                    return Url;
                }
                catch { }
            }
            return string.Empty;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var users = await _context.Users!.ToListAsync();
            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            var user = await _context.Users!.FindAsync(id);
            return _mapper.Map<UserModel>(user);
        }
        public async Task<UserModel> UpdateProfileAsync(int id, ProfileParams param)
        {
            var updateProfile = await _context.Users!.FindAsync(id);
            if (updateProfile != null)
            {
                Transmit(param, ref updateProfile);
                _context.Users!.Update(updateProfile);
                await _context.SaveChangesAsync();
                return _mapper.Map<UserModel>(updateProfile);
            }
            return null!;
        }

        public async Task<UserModel> UpdateAccountAsync(int id, AccountParams param)
        {
            var updateAccount = await _context.Users!.FindAsync(id);
            if (updateAccount != null)
            {
                Transmit(param, ref updateAccount);
                _context.Users!.Update(updateAccount);
                await _context.SaveChangesAsync();
                return _mapper.Map<UserModel>(updateAccount);
            }
            return null!;
        }
    }
}
