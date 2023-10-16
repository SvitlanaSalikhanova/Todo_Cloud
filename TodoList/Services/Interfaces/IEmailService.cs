namespace TodoList.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail();
    }
}
