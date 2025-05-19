using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.LogicServices.Services.Auth
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = default!;

        public int ExpiresHours { get; set; }
    }
}
