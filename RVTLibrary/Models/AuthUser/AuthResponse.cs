using System;
using System.Collections.Generic;
using System.Text;

namespace RVTLibrary.Models.AuthUser
{
    public class AuthResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string IDVN { get; set; }
    }
}
