using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class telefon
    {
        public int telId { get; set; }
        public string telefon1 { get; set; }
        public string tipus { get; set; }

        public bool serializeContacte = true;
        public Nullable<int> contacteId { get; set; }

        public virtual contacte contacte { get; set; }

        public telefon(string telefon1, string tipus, int contacteId)
        {
            this.telefon1 = telefon1;
            this.tipus = tipus;
            this.contacteId = contacteId;
        }
    }
}
