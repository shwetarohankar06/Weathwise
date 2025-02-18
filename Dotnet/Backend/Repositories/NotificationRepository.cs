using Microsoft.EntityFrameworkCore;
using WealthWiseAPI.Data;
using WealthWiseAPI.Models;

namespace WealthWiseAPI.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserId(long userId)
        {
            return await _context.Notifications
                .Where(n => n.ToUserId == userId)
                .OrderByDescending(n => n.Timestamp)
                .ToListAsync();
        }

        public async Task<Notification> SendNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }
    }
}
