using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expshare.ViewModels
{
    public class KreirajUplatuViewModel
    {
        public Guid IdKorisnik { get; set; }
        public Guid IdGrupa { get; set; }
        public List<Guid> KorisniciZaUplatu { get; set; }
        public decimal Iznos { get; set; }
        public bool RaspodijeliSamnom { get; set; }
    }
}
