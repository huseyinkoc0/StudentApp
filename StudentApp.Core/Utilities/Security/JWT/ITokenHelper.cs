using Azure.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        
            AccessToken CreateToken(List<Claim> claims);
        
    }
}
