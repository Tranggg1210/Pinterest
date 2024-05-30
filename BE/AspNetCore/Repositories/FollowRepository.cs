using Microsoft.EntityFrameworkCore;
using PixelPalette.Data;
using PixelPalette.Entities;
using PixelPalette.Interfaces;

namespace PixelPalette.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly PixelPaletteContext _context;

        public FollowRepository(PixelPaletteContext context)
        {
            _context = context;
        }
        public async Task<bool> FollowHandleAsync(int id, int followingId)
        {
            if (id != followingId)
            {
                var user = await _context.Users!.FindAsync(id);
                var following = await _context.Users!.FindAsync(followingId);
                if (user != null && following != null)
                {
                    if (!(await CheckFollowAsync(id, followingId)))
                    {
                        var follwer = new Follower
                        {
                            FollowerUserId = id,
                            FollowingUserId = followingId
                        };
                        await _context.Followers.AddAsync(follwer);
                        user.Following++;
                        _context.Update(user);
                        following.Follower++;
                        _context.Update(following);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> UnfollowHandleAsync(int id, int followingId)
        {
            if (id != followingId)
            {
                var user = await _context.Users!.FindAsync(id);
                var following = await _context.Users!.FindAsync(followingId);
                if (user != null && following != null)
                {
                    var follower = await _context.Followers
                    .FirstOrDefaultAsync(f => f.FollowerUserId == id && f.FollowingUserId == followingId);
                    if (follower != null)
                    {
                        _context.Followers.Remove(follower);
                        user!.Following--;
                        _context.Update(user);
                        following!.Follower--;
                        _context.Update(following);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> CheckFollowAsync(int id, int followingId)
        {
            var follower = await _context.Followers
                    .FirstOrDefaultAsync(f => f.FollowerUserId == id && f.FollowingUserId == followingId);
            return follower != null ? true : false;
        }
    }
}
