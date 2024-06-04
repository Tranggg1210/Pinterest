using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PixelPalette.Data;
using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Interfaces;
using PixelPalette.Models;

namespace PixelPalette.Repositories
{
    public class AnalysisesRepository : IAnalysisesRepository
    {
        private readonly PixelPaletteContext _context;
        private readonly IMapper _mapper;
        private readonly ITool _tools;

        public AnalysisesRepository(PixelPaletteContext context, IMapper mapper, ITool tools)
        {
            _context = context;
            _mapper = mapper;
            _tools = tools;
        }
        public async Task<AnalysisModel> GetAnalysisTodayAsync()
        {
            var analysis = await _context.Analysises.FirstOrDefaultAsync(a => a.CreateAt.Year == DateTime.Today.Year && a.CreateAt.Month == DateTime.Today.Month && a.CreateAt.Day == DateTime.Today.Day);
            return _mapper.Map<AnalysisModel>(analysis);
        }

        public async Task<IEnumerable<NotificationModel>> GetNotificationsAsync()
        {
            var notification = await _context.Notifications.ToListAsync();
            return _mapper.Map<IEnumerable<NotificationModel>>(notification);
        }

        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var deleteNotification = _context.Notifications!.SingleOrDefault(n => n.Id == id);
            if (deleteNotification != null)
            {
                _context.Notifications!.Remove(deleteNotification);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<NotificationModel> AddNotificationAsync(int userId, NotificationParam entryParams)
        {
            var notification = new Notification();
            _tools.Duplicate(entryParams, ref notification);
            notification.UserId = userId;
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return _mapper.Map<NotificationModel>(notification);
        }
    }
}
