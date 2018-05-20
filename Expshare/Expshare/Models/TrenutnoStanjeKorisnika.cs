using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expshare.Models
{
    public class TrenutnoStanjeKorisnika
    {
        public Guid IdKorisnik { get; set; }
        public decimal Stanje { get; set; }

        public Korisnik IdKorisnikNavigation { get; set; }
    }
}
