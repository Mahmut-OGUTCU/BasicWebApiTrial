using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }

        public int CreatedID { get; set; }

        public bool IsActive { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateID { get; set; }
    }
}
