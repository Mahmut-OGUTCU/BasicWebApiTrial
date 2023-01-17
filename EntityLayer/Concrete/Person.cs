using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Person : BaseEntity
    {
        [Key]
        public int PersonID { get; set; }

        public string PersonFirstName { get; set; }

        public string PersonLastName { get; set; }

        public string PersonEmail { get; set; }

        public string PersonPhone { get; set; }

        public string PersonPassword { get; set; }

        public string PersonToken { get; set; }

        public bool PersonIsAdmin { get; set; }

        public DateTime? PersonLastActivited { get; set; }

        public int DepartmentID { get; set; }
        public Department Department { get; set; }
    }
}
