using System;
using System.Collections.Generic;
using System.Text;

namespace CoronavirusTracker.Models
{
    public class CountryModel
    {
        public string Country { get; set; }
        public string Slug { get; set; }
        public string ISO2 { get; set; }
        public bool IsFavourite { get; set; } = false;
        public string Description { get; set; } = "Dodaj";
    }
}
