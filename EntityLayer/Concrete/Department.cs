using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Department : BaseEntity
    {
        public Department()
        {
            Persons = new List<Person>();
        }

        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentDescription { get; set; }

        public ICollection<Person> Persons { get; set; }
    }
}
