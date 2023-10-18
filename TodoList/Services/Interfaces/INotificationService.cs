namespace TodoList.Services.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendNotification(string message, string topicArn);
    }
}
