using Microsoft.AspNetCore.Identity;
using PixelPalette.Helpers;
using PixelPalette.Models;

namespace PixelPalette.Interfaces
{
    public interface IAnalysisesRepository
    {
        Task<AnalysisModel> GetAnalysisTodayAsync();
        Task<IEnumerable<NotificationModel>> GetNotificationsAsync();
        Task<bool> DeleteNotificationAsync(int id);
        Task<NotificationModel> AddNotificationAsync(int userId, NotificationParam entryParams);
    }
}
