using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICheckLogin
    {
        Person PersonCheck(ControllerContext p);

        bool TokenIsValid(string token);
    }
}
