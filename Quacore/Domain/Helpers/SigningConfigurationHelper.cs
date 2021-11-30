using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Quacore.Domain.Helpers
{
    public class SigningConfigurationHelper
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurationHelper()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SECRET_KEY_892j32rd038jp3l;r09wa7fu-2"));
            }
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
