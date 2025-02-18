using WealthWiseAPI.DTOs;
using WealthWiseAPI.Models;

namespace WealthWiseAPI.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetNotificationsByUserId(long userId);
        Task<NotificationDto> SendNotification(NotificationDto notificationDto);
    }
}
