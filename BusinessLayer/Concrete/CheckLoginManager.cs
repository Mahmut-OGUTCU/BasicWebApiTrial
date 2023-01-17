using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CheckLoginManager : ICheckLogin
    {
        IPersonDal _personDal;

        public CheckLoginManager(IPersonDal personDal)
        {
            _personDal = personDal;
        }

        public Person PersonCheck(ControllerContext p)
        {
            var personValue = new Person
            {
                PersonID = -1
            };

            var token = "";

            if (p != null)
                if (p.HttpContext != null)
                    if (p.HttpContext.Request != null)
                        if (p.HttpContext.Request.Headers != null)
                            token = p.HttpContext.Request?.Headers["token"];

            //token boş ise direkt id -1 dön
            if (token?.Trim() == "" || token?.Trim() == null)
            {
                return personValue;
            }

            if (TokenIsValid(token))
            {
                if (token.Trim() != "" && token.Trim() != null)
                {
                    personValue = _personDal.Get(x => x.PersonToken == token && x.IsActive == true);
                }

                if (personValue == null)
                {
                    personValue = new Person
                    {
                        PersonID = -1
                    };
                }
            }

            return personValue;
        }

        public bool TokenIsValid(string token)
        {
            JwtSecurityToken jwtSecurityToken;

            jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            if (jwtSecurityToken.ValidTo > DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }
    }
}
