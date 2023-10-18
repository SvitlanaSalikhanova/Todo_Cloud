using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using TodoList.Services.Interfaces;

namespace TodoList.Services
{
    public class NotificationAWSService : INotificationService
    {
        private readonly IAmazonSimpleNotificationService _notificationService;

        public NotificationAWSService(IAmazonSimpleNotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<bool> SendNotification(string message, string topicArn)
        {
            var request = new PublishRequest()
            {
                Message = message,
                TopicArn = topicArn
            };

            var response = await _notificationService.PublishAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                return true;
            else
                return false;
        }
    }
}
