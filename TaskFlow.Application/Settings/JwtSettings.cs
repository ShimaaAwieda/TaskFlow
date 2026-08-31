using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Application.Settings
{
    public class JwtSettings
    {
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}
