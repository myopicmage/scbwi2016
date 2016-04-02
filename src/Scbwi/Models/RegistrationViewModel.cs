﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scbwi.Models {
    public class RegistrationViewModel {
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
        public int packageid { get; set; }
        public int comprehensiveid { get; set; }
        public int trackid { get; set; }
        public string coupon { get; set; }
        public int manuscript { get; set; }
        public int portfolio { get; set; }
    }
}
