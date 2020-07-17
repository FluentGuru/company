using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Types
{
    public class SearchCompaniesFilter
    {
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string Ticker { get; set; }
    }
}
