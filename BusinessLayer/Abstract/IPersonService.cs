using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IPersonService
    {
        List<Person> PersonGetAll();

        Person PersonGetByID(int id);

        Person PersonGetByMail(string email);

        Person PersonGetByMailForUnique(string email);

        Person PersonGetByMailForUnique(int id, string email);

        Person PersonGetByPhoneForUnique(string phone);

        Person PersonGetByPhoneForUnique(int id, string phone);

        void PersonAdd(Person p);

        void PersonUpdate(Person p);

        void PersonDelete(Person p);
    }
}
