﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Helpers
{
    public class AuthServiceOptions
    {
        public const string AuthService = "AuthService";
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecretForKey { get; set; } = string.Empty;
    }
}
