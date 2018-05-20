using System;
using System.Collections.Generic;

namespace Expshare.Models
{
    public partial class Transakcija
    {
        public Transakcija()
        {
        }

        public Guid ID { get; set; }
        public Guid IdPlatitelj { get; set; }
        public Guid IdPrimatelj { get; set; }
        public Guid IdGrupa { get; set; }
        public decimal Iznos { get; set; }
        public DateTime Datum { get; set; }

        public Grupa IdGrupaNavigation { get; set; }
        public Korisnik IdPlatiteljNavigation { get; set; }

        public Korisnik IdPrimateljNavigation { get; set; }
    }
}
