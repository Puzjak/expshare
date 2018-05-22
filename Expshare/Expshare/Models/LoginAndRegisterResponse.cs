using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expshare.Models
{
    public class LoginAndRegisterResponse
    {
        public string Email { get; set; }
        public string Nickname { get; set; }
        public bool IsAuthenticated { get; set; }
        public string ErrorMessage { get; set; }
    }
}
