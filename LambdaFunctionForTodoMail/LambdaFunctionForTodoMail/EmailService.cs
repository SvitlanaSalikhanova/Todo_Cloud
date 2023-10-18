using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace LambdaFunctionForTodoMail
{
    public class EmailService : IEmailService
    {
        private string _toAddress = "s.salikhanova@gmail.com";
        private string _fromAddress = "cloudlearning.testemail@gmail.com";
        private string _subject = "Cloud Learning SNS Lambda test";
        
        public EmailService()
        {
            
        }

        public async Task<string> SendEmail(string message)
        {
            using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.EUNorth1))
            {
                var sendEmailRequest = new SendEmailRequest()
                {
                    Destination = new Destination() { ToAddresses = new List<string>() { _toAddress } },
                    Message = new Message()
                    {
                        Body = new Body()
                        {
                            Html = new Content() { Data = message, Charset = "UTF-8" }
                        },
                        Subject = new Content() { Data = _subject, Charset = "UTF-8" }
                    },
                    Source = _fromAddress
                };

                var sendResult = await client.SendEmailAsync(sendEmailRequest);
                if (sendResult.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return "Message sent";
                else
                    return "Messagenot sent";
            }
        }
    }
}
