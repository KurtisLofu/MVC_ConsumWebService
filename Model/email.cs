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

        public email (string email1, string tipus, int contacteId)
        {
            this.email1 = email1;
            this.tipus = tipus;
            this.contacteId = contacteId;
        }
    }
}
