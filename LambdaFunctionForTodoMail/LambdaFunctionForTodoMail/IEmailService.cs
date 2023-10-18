using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaFunctionForTodoMail
{
    internal interface IEmailService
    {
        Task<string> SendEmail(string messsage);
    }
}
