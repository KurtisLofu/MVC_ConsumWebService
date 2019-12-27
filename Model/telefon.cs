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

        public int contacteId { get; set; }

        public contacte contacte { get; set; }
        public telefon()
        {
        }
    }
}
