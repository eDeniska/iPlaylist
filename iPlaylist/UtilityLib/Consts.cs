using System;
using System.Globalization;

namespace UtilityLib
{
    public abstract class Consts
    {
        public abstract class Locales
        {
            public const String Russian = "ru";
            public const String English = "en";
            public static readonly CultureInfo RussianCulture = new CultureInfo(Consts.Locales.Russian);
            public static readonly CultureInfo EnglishCulture = new CultureInfo(Consts.Locales.English);
        }
    }
}
