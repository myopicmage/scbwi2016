using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scbwi.Models {
    public class RegistrationDTO {
        public int id { get; set; }

        public string first { get; set; }
        public string last { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string coupon { get; set; }

        public string package { get; set; }
        public string comprehensive { get; set; }
        public string track { get; set; }
        public int portfolio { get; set; }
        public int manuscript { get; set; }

        public DateTime submitted { get; set; }
        public DateTime paid { get; set; }
        public DateTime cleared { get; set; }

        public decimal subtotal { get; set; }
        public decimal total { get; set; }
    }
}
