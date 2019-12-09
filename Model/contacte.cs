using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class contacte
    {
        public int contacteId { get; set; }
        public string nom { get; set; }
        public string cognoms { get; set; }

        public bool serializarEmail = false;
        public bool serializarTelefon = false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<email> emails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<telefon> telefons { get; set; }

        public contacte(string nom, string cognoms)
        {
            this.nom = nom;
            this.cognoms = cognoms;
        }
    }
}
