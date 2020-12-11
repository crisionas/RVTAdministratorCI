using System;
using System.Collections.Generic;

#nullable disable

namespace RVT_DataLayer.Entities
{
    public partial class FiscDatum
    {
        public string Idnp { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Region { get; set; }
    }
}
