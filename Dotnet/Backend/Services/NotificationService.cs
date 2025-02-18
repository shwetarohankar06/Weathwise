using WealthWiseAPI.DTOs;
using WealthWiseAPI.Models;
using WealthWiseAPI.Repositories;

namespace WealthWiseAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<NotificationDto>> GetNotificationsByUserId(long userId)
        {
            var notifications = await _notificationRepository.GetNotificationsByUserId(userId);
            return notifications.Select(n => new NotificationDto
            {
                Id = n.Id,
                FromUserId = n.FromUserId,
                ToUserId = n.ToUserId,
                Amount = n.Amount,
                Message = n.Message,
                Timestamp = n.Timestamp
            });
        }

        public async Task<NotificationDto> SendNotification(NotificationDto notificationDto)
        {
            var notification = new Notification
            {
                FromUserId = notificationDto.FromUserId,
                ToUserId = notificationDto.ToUserId,
                Amount = notificationDto.Amount,
                Message = notificationDto.Message
            };

            var savedNotification = await _notificationRepository.SendNotification(notification);
            return new NotificationDto
            {
                Id = savedNotification.Id,
                FromUserId = savedNotification.FromUserId,
                ToUserId = savedNotification.ToUserId,
                Amount = savedNotification.Amount,
                Message = savedNotification.Message,
                Timestamp = savedNotification.Timestamp
            };
        }
    }
}
