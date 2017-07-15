using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingCo.Services
{
    public class DebugMailService : IMailService
    {
        Task IMailService.SendMail(string to, string firstName, string lastName, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
