using EntityLayer.Concrete.MailModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
