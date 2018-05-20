using System;
using System.Collections.Generic;

namespace Expshare.Models
{
    public partial class GrupaKorisnik
    {
        public Guid ID { get; set; }
        public Guid IdKorisnik { get; set; }
        public Guid IdGrupa { get; set; }

        public Grupa IdGrupaNavigation { get; set; }
        public Korisnik IdKorisnikNavigation { get; set; }
    }
}
