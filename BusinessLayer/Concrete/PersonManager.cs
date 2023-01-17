using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class PersonManager : IPersonService
    {
        IPersonDal _personDal;

        public PersonManager(IPersonDal personDal)
        {
            _personDal = personDal;
        }

        public void PersonAdd(Person p)
        {
            _personDal.Insert(p);
        }

        public void PersonDelete(Person p)
        {
            _personDal.Delete(p);
        }

        public List<Person> PersonGetAll()
        {
            return _personDal.List(x => x.IsActive == true);
        }

        public Person PersonGetByID(int id)
        {
            return _personDal.Get(x => x.PersonID == id && x.IsActive == true);
        }

        public Person PersonGetByMail(string email)
        {
            return _personDal.Get(x => x.PersonEmail == email && x.IsActive == true);
        }

        public Person PersonGetByMailForUnique(int id, string email)
        {
            return _personDal.Get(x => x.PersonEmail == email && x.PersonID != id && x.IsActive == true);
        }

        public Person PersonGetByMailForUnique(string email)
        {
            return _personDal.Get(x => x.PersonEmail == email && x.IsActive == true);
        }

        public Person PersonGetByPhoneForUnique(int id, string phone)
        {
            return _personDal.Get(x => x.PersonPhone == phone && x.PersonID != id && x.IsActive == true);
        }

        public Person PersonGetByPhoneForUnique(string phone)
        {
            return _personDal.Get(x => x.PersonPhone == phone && x.IsActive == true);
        }

        public void PersonUpdate(Person p)
        {
            _personDal.Update(p);
        }
    }
}
