using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class DepartmentManager : IDepartmentService
    {
        IDepartmentDal _departmentDal;

        public DepartmentManager(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }

        public void DepartmentAdd(Department p)
        {
            _departmentDal.Insert(p);
        }

        public void DepartmentDelete(Department p)
        {
            _departmentDal.Delete(p);
        }

        public List<Department> DepartmentGetAll()
        {
            return _departmentDal.List(x => x.IsActive == true);
        }

        public Department DepartmentGetByID(int id)
        {
            return _departmentDal.Get(x => x.DepartmentID == id && x.IsActive == true);
        }

        public Department DepartmentGetByNameForUnique(string name)
        {
            return _departmentDal.Get(x => x.DepartmentName == name && x.IsActive == true);
        }

        public Department DepartmentGetByNameForUnique(int id, string name)
        {
            return _departmentDal.Get(x => x.DepartmentName == name && x.DepartmentID != id && x.IsActive == true);
        }

        public void DepartmentUpdate(Department p)
        {
            _departmentDal.Update(p);
        }
    }
}
