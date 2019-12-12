using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class email
    {
        public int emailId { get; set; }
        public string email1 { get; set; }
        public string tipus { get; set; }
        public Nullable<int> contacteId { get; set; }

        public virtual contacte contacte { get; set; }

    }
}
