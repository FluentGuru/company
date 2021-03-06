﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Entities
{
    public class Company : EntityBase
    {
        public string Isin { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string Ticker { get; set; }
        public string Website { get; set; }
    }
}
