using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonLoginController : ControllerBase
    {
        PersonLoginManager plm = new PersonLoginManager(new EfPersonDal());
        PersonManager pem = new PersonManager(new EfPersonDal());

        PasswordManager password = new PasswordManager();

        [HttpPost]
        [Route("person-login")]
        public ApiReturn<dynamic> Login([FromBody] Person p)
        {
            try
            {
                p.PersonPassword = password.PasswordToSHA256Hash(p.PersonPassword);
                // kullanıcı login olurken yeni bir token oluşturma
                var userToken = plm.LoginPerson(p.PersonEmail, p.PersonPassword);
                if (userToken == null)
                    return new ApiReturn<dynamic> { status = false, message = "Lütfen Şifrenizi Kontrol Edin." };

                var userValue = pem.PersonGetByMail(p.PersonEmail);
                if (userValue == null)
                    return new ApiReturn<dynamic> { status = false, message = "Lütfen Mailinizi ve Şifrenizi Kontrol Edin." };

                userValue.PersonToken = userToken;
                userValue.PersonLastActivited = DateTime.Now;
                pem.PersonUpdate(userValue);

                return new ApiReturn<dynamic> { status = true, message = "Başarılı.", data = userValue };
            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}
