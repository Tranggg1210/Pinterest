
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PixelPalette.Data;
using PixelPalette.Entities;
using PixelPalette.Interfaces;
using PixelPalette.Models;

namespace PixelPalette.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PixelPaletteContext _context;
        private readonly IMapper _mapper;

        public UserRepository(PixelPaletteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddUserAsync(UserModel model)
        {
            var newUser = _mapper.Map<User>(model);
            _context.Users!.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser.Id;
        }

        public async Task DeleteUserAsync(int id)
        {
            var deleteUser = _context.Users!.SingleOrDefault(u => u.Id == id);
            if(deleteUser != null)
            {
                _context.Users!.Remove(deleteUser);
                await _context.SaveChangesAsync();
            }
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

        public async Task UpdateUserAsync(int id, UserModel model)
        {
            if (id != model.Id)
            {
                var updateUser = _mapper.Map<User>(model);
                _context.Users!.Update(updateUser);
                await _context.SaveChangesAsync();
            }
        }
    }
}
