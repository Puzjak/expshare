using System;
using System.Collections.Generic;

namespace Expshare.Models
{
    public partial class Korisnik
    {
        public Korisnik()
        {
            GrupaKorisnik = new HashSet<GrupaKorisnik>();
            TransakcijaIdPlatiteljNavigation = new HashSet<Transakcija>();
            TransakcijaIdPrimateljNavigation = new HashSet<Transakcija>();
        }

        public Guid ID { get; set; }
        public string EmailKorisnik { get; set; }
        public string Nickname { get; set; }

        public Lozinka Lozinka { get; set; }
        public TrenutnoStanjeKorisnika TrenutnoStanjeKorisnika { get; set; }
        public TrenutnoStanjeKorisnikaUgrupi TrenutnoStanjeKorisnikaUgrupi { get; set; }
        public ICollection<GrupaKorisnik> GrupaKorisnik { get; set; }
        public ICollection<Transakcija> TransakcijaIdPlatiteljNavigation { get; set; }
        public ICollection<Transakcija> TransakcijaIdPrimateljNavigation { get; set; }

        public ICollection<StanjeIzmeduKorisnika> StanjeIzmeduKorisnikaKorisnik { get; set; }
        public ICollection<StanjeIzmeduKorisnika> StanjeIzmeduKorisnikaDugovatelj { get; set; }
    }
}
