using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Services
{
    public interface IMailService
    {
        void SendEmail(string to, string token);
    }
}
