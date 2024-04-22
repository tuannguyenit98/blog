using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace Common.Runtime.Security
{
    public class CloudinarySettings
    {
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}
