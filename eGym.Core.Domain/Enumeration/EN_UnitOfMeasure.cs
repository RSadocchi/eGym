using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eGym.Core.Domain
{

    public enum UnitOfMeasureConverterSign
    {
        Multiplier = 1,
        Divider = 2
    }

    public class EN_UnitOfMeasure : Enumeration<short>
    {
        public string Abbr { get; set; }
        public int Decimals { get; set; }
        public string StringFormat { get; set; }

        public List<(short umId, double converter, UnitOfMeasureConverterSign sign, MidpointRounding midpointRounding)> Conversions { get; set; }

        public EN_UnitOfMeasure() : base() { }
        public EN_UnitOfMeasure(short id, string code) : base(id, code) { }
        public EN_UnitOfMeasure(short id, string code, string abbr, int decimals, string stringFormat) : base(id, code)
        {
            Abbr = abbr;
            Decimals = decimals;
            StringFormat = stringFormat;
        }

        public static IEnumerable<EN_UnitOfMeasure> GetAll() => GetAll<EN_UnitOfMeasure>();
        public static EN_UnitOfMeasure FromID(short id) => FromID<EN_UnitOfMeasure>(id);

        public static double? Convert(double value, short fromUmId, short toUmId)
        {
            if (value == 0 || fromUmId == toUmId) return value;

            var umFrom = EN_UnitOfMeasure.FromID(fromUmId);
            if (umFrom == null) return null;
            if (umFrom.Conversions?.Where(t => t.umId == toUmId)?.Count() > 0)
            {
                var conversion = umFrom.Conversions.FirstOrDefault(t => t.umId == toUmId);
                var umTo = EN_UnitOfMeasure.FromID(conversion.umId);
                switch (conversion.sign)
                {
                    case UnitOfMeasureConverterSign.Multiplier:
                        return Math.Round((value * conversion.converter), umTo.Decimals, conversion.midpointRounding);

                    case UnitOfMeasureConverterSign.Divider:
                        return Math.Round((value / conversion.converter), umTo.Decimals, conversion.midpointRounding);
                }
            }

            return null;
        }


        public static EN_UnitOfMeasure Kilograms = new EN_UnitOfMeasure(1, nameof(Kilograms), "kg", 2, "0.##")
        {
            Conversions = new List<(short umId, double converter, UnitOfMeasureConverterSign sign, MidpointRounding midpointRounding)>()
            {
                (2, 1000, UnitOfMeasureConverterSign.Multiplier, MidpointRounding.AwayFromZero),
                (3, 2.205, UnitOfMeasureConverterSign.Multiplier, MidpointRounding.AwayFromZero)
            }
        };

        public static EN_UnitOfMeasure Grams = new EN_UnitOfMeasure(2, nameof(Grams), "gr", 2, "0.##")
        {
            Conversions = new List<(short umId, double converter, UnitOfMeasureConverterSign sign, MidpointRounding midpointRounding)>()
            {
                (1, 1000, UnitOfMeasureConverterSign.Divider, MidpointRounding.AwayFromZero)
            }
        };


        public static EN_UnitOfMeasure Pound = new EN_UnitOfMeasure(10, nameof(Pound), "lbs", 2, "0.##")
        {
            Conversions = new List<(short umId, double converter, UnitOfMeasureConverterSign sign, MidpointRounding midpointRounding)>()
            {
                (1, 2.205, UnitOfMeasureConverterSign.Divider, MidpointRounding.AwayFromZero)
            }
        };
    }
}
