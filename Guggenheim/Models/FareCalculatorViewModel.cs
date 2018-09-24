using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuggenheimTest.Models
{
    public class FareCalculatorViewModel
    {
        public int MinutesAboveSixMph { get; set; }
        public int DistanceUnitsBelowSixMph { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }
}