using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scbwi.Models {
    public class Comprehensive {
        public int id { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        public string presenters { get; set; }
        public int maxattendees { get; set; }

        public decimal price { get; set; }
    }
}
