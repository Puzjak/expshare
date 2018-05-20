using System;
using System.Collections.Generic;

namespace Expshare.Models
{
    public partial class Grupa
    {
        public Grupa()
        {
            GrupaKorisnik = new HashSet<GrupaKorisnik>();
            TrenutnoStanjeKorisnikaUgrupi = new HashSet<TrenutnoStanjeKorisnikaUgrupi>();
            Uplata = new HashSet<Transakcija>();
        }

        public Guid ID { get; set; }
        public string NazivGrupa { get; set; }
        
        public ICollection<GrupaKorisnik> GrupaKorisnik { get; set; }
        public ICollection<TrenutnoStanjeKorisnikaUgrupi> TrenutnoStanjeKorisnikaUgrupi { get; set; }
        public ICollection<Transakcija> Uplata { get; set; }
        public ICollection<StanjeIzmeduKorisnika> StanjeIzmeduKorisnika { get; set; }
    }
}
