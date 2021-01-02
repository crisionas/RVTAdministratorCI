using System;
using System.Collections.Generic;
using System.Text;

namespace RVTLibrary.Models.UserIdentity
{
    public class RegistrationMessage
    {
        
        public string IDNP { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Gender { get; set; }
        
        public DateTime Birth_date { get; set; }
        
        public string Ip_address { get; set; }
        
        public string Phone_Number { get; set; }
        
        public string Email { get; set; }
        public string Region { get; set; }
        
        public DateTime RegisterDate { get; set; }
    }
}
