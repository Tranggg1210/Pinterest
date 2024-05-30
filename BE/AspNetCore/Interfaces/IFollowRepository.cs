using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Models;

namespace PixelPalette.Interfaces
{
    public interface IFollowRepository
    {
        Task<bool> FollowHandleAsync(int id, int followingId);
        Task<bool> UnfollowHandleAsync(int id, int followingId);
        Task<bool> CheckFollowAsync(int id, int followingId);
    }
}
