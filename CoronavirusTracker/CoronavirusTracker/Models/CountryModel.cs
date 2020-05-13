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
        public bool IsFavourite { get; set; }
        public string IconPath { get; set; } = "https://img.icons8.com/ios/50/000000/star.png";
    }
}
