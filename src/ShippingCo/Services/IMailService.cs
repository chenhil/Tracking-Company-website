using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingCo.Services
{
    public interface IMailService
    {
        Task SendMail(string to, string firstName, string lastName, string from, string body);
    }
}
