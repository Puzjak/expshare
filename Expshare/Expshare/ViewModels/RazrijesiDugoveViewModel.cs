using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expshare.ViewModels
{
    public class RazrijesiDugoveViewModel
    {
        public Guid IdKorisnik { get; set; }
        public string PrimateljEmail { get; set; }
        public Guid IdGrupa { get; set; }
        public decimal Iznos { get; set; }
    }
}
