using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IDepartmentService
    {
        List<Department> DepartmentGetAll();

        Department DepartmentGetByID(int id);

        Department DepartmentGetByNameForUnique(string name);

        Department DepartmentGetByNameForUnique(int id, string name);

        void DepartmentAdd(Department p);

        void DepartmentUpdate(Department p);

        void DepartmentDelete(Department p);
    }
}
