using System;
using System.Collections.Generic;

namespace Expshare.Models
{
    public partial class Lozinka
    {
        public Guid ID { get; set; }
        public string LozinkaHash { get; set; }
        public string LozinkaSalt { get; set; }

        public Korisnik IdLozinkaNavigation { get; set; }
    }
}
