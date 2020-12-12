using System;
using System.Collections.Generic;
using System.Text;

namespace RVTLibrary.Models.AuthUser
{
    public class AuthMessage
    {
        public string IDNP { get; set; }
        public string VnPassword { get; set; }
        public string Ip_address { get; set; }
        public DateTime Time { get; set; }
    }
}
