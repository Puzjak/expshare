using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expshare.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Nickname { get; set; }
        public bool ZapamtiMe { get; set; } = false;
    }
}
