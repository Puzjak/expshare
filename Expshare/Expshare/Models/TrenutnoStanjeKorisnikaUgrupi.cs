using System;
using System.Collections.Generic;

namespace Expshare.Models
{
    public partial class TrenutnoStanjeKorisnikaUgrupi
    {
        public Guid Id { get; set; }
        public Guid IdKorisnik { get; set; }
        public Guid IdGrupa { get; set; }
        public decimal Stanje { get; set; }

        public Grupa IdGrupaNavigation { get; set; }
        public Korisnik IdKorisnikNavigation { get; set; }
    }
}
