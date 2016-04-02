using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scbwi.Models {
    public class Package {
        public int id { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        public decimal regularprice { get; set; }
        public decimal lateprice { get; set; }
        public bool member { get; set; }
        public int max { get; set; }
    }
}
