using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IPasswordService
    {
        string PasswordToEncrypt(string password);

        string PasswordToDecrypt(string password);

        string PasswordToSHA256Hash(string password);
    }
}
