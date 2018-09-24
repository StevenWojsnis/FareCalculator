using GuggenheimTest.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuggenheimTest.Services
{
    public class FareCalculatorService
    {
        public FareCalculatorService() { }

        public decimal CalculateFare(FareCalculatorDataModel model)
        {
            return (
                EntryFee()
                + NewYorkTax()
                + PeakTimeSurcharge(model.Date, model.Time)
                + NightTimeSurcharge(model.Time)
                + PriceFromMinutesAboveSixMph(model.MinutesAboveSixMph)
                + PriceFromDistanceUnitsBelowSixMph(model.DistanceUnitsBelowSixMph)
                );
        }


        private decimal PeakTimeSurcharge(DateTime date, DateTime time)
        {
            return ((time.Hour >= 16 && time.Hour < 20) && IsWeekDay(date.DayOfWeek)) ? (decimal) 1.00 : 0;
        }

        private decimal NightTimeSurcharge(DateTime time)
        {
            return (time.Hour >= 20 || time.Hour < 6) ? (decimal) 0.50 : 0;
        }

        private decimal PriceFromMinutesAboveSixMph(int minutes)
        {
            return PriceFromUnits(minutes);
        }

        private decimal PriceFromDistanceUnitsBelowSixMph(int distanceUnits)
        {
            return PriceFromUnits(distanceUnits);
        }

        private decimal PriceFromUnits(int units)
        {
            return units * (decimal) 0.35;
        }

        private decimal EntryFee()
        {
            return (decimal) 3.00;
        }

        private decimal NewYorkTax()
        {
            return (decimal) .50;
        }

        private bool IsWeekDay(DayOfWeek day)
        {
            switch (day)
            {
                case (DayOfWeek.Monday): return true;
                case (DayOfWeek.Tuesday): return true;
                case (DayOfWeek.Wednesday): return true;
                case (DayOfWeek.Thursday): return true;
                case (DayOfWeek.Friday): return true;
                default: return false;
            }

        }
    }
}