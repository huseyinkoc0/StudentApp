using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Utilities.Security.JWT
{
    public class AccessToken
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
