using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expshare.Models
{
    public class StanjeIzmeduKorisnika
    {
        public Guid ID { get; set; }
        public Guid IdKorisnik { get; set; }
        public Guid IdDugovatelj { get; set; }
        public Guid IdGrupa { get; set; }
        public decimal Stanje { get; set; }

        public Grupa IdGrupaNavigation { get; set; }
        public Korisnik IdKorisnikNavigation { get; set; }
        public Korisnik IdDugovateljNavigation { get; set; }
    }
}
