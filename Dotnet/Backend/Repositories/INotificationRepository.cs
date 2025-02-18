using WealthWiseAPI.Models;

namespace WealthWiseAPI.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetNotificationsByUserId(long userId);
        Task<Notification> SendNotification(Notification notification);
    }
}
