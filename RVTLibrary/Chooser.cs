using System;
using System.Collections.Generic;
using System.Text;

namespace RVTLibrary
{
    public class Chooser
    {

        public string IDNP { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Gender { get; set; }

        public DateTime? Birth_date { get; set; }

        public DateTime Vote_date { get; set; }

        public int Region { get; set; }
        public string Phone_Number { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// IDUN ID unic numeric a votantului
        /// </summary>
        /// 

        public string IDVN { get; set; }



    }
}
