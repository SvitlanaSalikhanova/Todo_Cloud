using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using TodoList.Services.Interfaces;

namespace TodoList.Services
{
    public class EmailAWSService : IEmailService
    {
        private readonly IAmazonSimpleEmailService _amazonSimpleEmilService;
        private string _toAddress = "s.salikhanova@gmail.com";
        private string _fromAddress = "cloudlearning.testemail@gmail.com";
        private string _subject = "Cloud Learning test";
        private string _body = "<h1>Yes!</h1> <p>You got it";

        public EmailAWSService(IAmazonSimpleEmailService amazonSimpleEmailService)
        {
            _amazonSimpleEmilService = amazonSimpleEmailService;
        }

        public async Task<bool> SendEmail()
        {
            var sendEmailRequest = new SendEmailRequest()
            {
                Destination = new Destination() { ToAddresses = new List<string>() { _toAddress } },
                Message = new Message()
                {
                    Body = new Body()
                    {
                        Html = new Content() { Data = _body, Charset = "UTF-8" }
                    },
                    Subject = new Content() { Data = _subject, Charset = "UTF-8" }
                },
                Source = _fromAddress
            };

            var sendResult = await _amazonSimpleEmilService.SendEmailAsync(sendEmailRequest);

            if (sendResult.HttpStatusCode == System.Net.HttpStatusCode.OK)
                return true;
            else
                return false;
                
            }
        }
}
