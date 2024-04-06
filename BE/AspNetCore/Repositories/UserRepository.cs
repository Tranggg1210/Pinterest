
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PixelPalette.Data;
using PixelPalette.Entities;
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

        public async Task<bool> EditAvatar(int id, IFormFile file)
        {
            var user = await _context.Users!.FindAsync(id);

            if (user != null)
            {
                var addResult = await _photoService.AddPhotoAsync(file);
                if (addResult.Error != null) return false;

                if (user.AvatarId != null)
                {
                    var deleteResult = await _photoService.DeletePhotoAsync(user.AvatarId);
                    if (deleteResult.Error != null) return false;
                }

                user.AvatarUrl = addResult.SecureUrl.AbsoluteUri;
                user.AvatarId = addResult.PublicId;

                _context.Users!.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
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

        public async Task<bool> UpdateUserAsync(int id, UserModel model)
        {
            var updateUser = await _context.Users!.FindAsync(id);
            if (updateUser != null && id == model.Id)
            {
                Transmit(model, ref updateUser);
                _context.Users!.Update(updateUser);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateProfileAsync(int id, ProfileModel model)
        {
            var updateProfile = await _context.Users!.FindAsync(id);
            if (updateProfile != null && id == model.Id)
            {
                Transmit(model, ref updateProfile);
                _context.Users!.Update(updateProfile);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAccountAsync(int id, AccountModel model)
        {
            var updateAccount = await _context.Users!.FindAsync(id);
            if (updateAccount != null && id == model.Id)
            {
                Transmit(model, ref updateAccount);
                _context.Users!.Update(updateAccount);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
