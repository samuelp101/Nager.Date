﻿using Nager.Date.Contract;
using Nager.Date.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nager.Date.PublicHolidays
{
    /// <summary>
    /// Australia
    /// </summary>
    public class AustraliaProvider : IPublicHolidayProvider, ICountyProvider
    {
        private readonly ICatholicProvider _catholicProvider;

        /// <summary>
        /// AustraliaProvider
        /// </summary>
        /// <param name="catholicProvider"></param>
        public AustraliaProvider(ICatholicProvider catholicProvider)
        {
            this._catholicProvider = catholicProvider;
        }

        ///<inheritdoc/>
        public IDictionary<string, string> GetCounties()
        {
            return new Dictionary<string, string>
            {
                { "AUS-ACT", "Australian Capital Territory" },
                { "AUS-NSW", "New South Wales" },
                { "AUS-NT", "Northern Territory" },
                { "AUS-QLD", "Queensland" },
                { "AUS-SA", "South Australia" },
                { "AUS-TAS", "Tasmania" },
                { "AUS-VIC", "Victoria" },
                { "AUS-WA", "Western Australia" }
            };
        }

        ///<inheritdoc/>
        public IEnumerable<PublicHoliday> Get(int year)
        {
            var countryCode = CountryCode.AU;
            var easterSunday = this._catholicProvider.EasterSunday(year);

            var firstMondayInMarch = DateSystem.FindDay(year, Month.March, DayOfWeek.Monday, Occurrence.First);
            var secondMondayInMarch = DateSystem.FindDay(year, Month.March, DayOfWeek.Monday, Occurrence.Second);
            var firstMondayInMay = DateSystem.FindDay(year, Month.May, DayOfWeek.Monday, Occurrence.First);
            var firstMondayAfterOr27May = DateSystem.FindDay(year, 5, 27, DayOfWeek.Monday);
            var firstMondayInJune = DateSystem.FindDay(year, Month.June, DayOfWeek.Monday, Occurrence.First);
            var secondMondayInJune = DateSystem.FindDay(year, Month.June, DayOfWeek.Monday, Occurrence.Second);
            var firstMondayInAugust = DateSystem.FindDay(year, Month.August, DayOfWeek.Monday, Occurrence.First);
            var firstMondayInOctober = DateSystem.FindDay(year, Month.October, DayOfWeek.Monday, Occurrence.First);

            var items = new List<PublicHoliday>();
            items.Add(new PublicHoliday(year, 1, 1, "New Year's Day", "New Year's Day", countryCode));
            items.Add(new PublicHoliday(year, 1, 26, "Australia Day", "Australia Day", countryCode));
            items.Add(new PublicHoliday(firstMondayInMarch, "Labour Day", "Labour Day", countryCode, null, new string[] { "AUS-WA" }));
            items.Add(new PublicHoliday(secondMondayInMarch, "Canberra Day", "Canberra Day", countryCode, null, new string[] { "AUS-ACT" }));
            items.Add(new PublicHoliday(secondMondayInMarch, "March Public Holiday", "March Public Holiday", countryCode, null, new string[] { "AUS-SA" }));
            items.Add(new PublicHoliday(secondMondayInMarch, "Eight Hours Day", "Eight Hours Day", countryCode, null, new string[] { "AUS-TAS" }));
            items.Add(new PublicHoliday(secondMondayInMarch, "Labour Day", "Labour Day", countryCode, null, new string[] { "AUS-VIC" }));
            items.Add(new PublicHoliday(easterSunday.AddDays(-2), "Good Friday", "Good Friday", countryCode));
            items.Add(new PublicHoliday(easterSunday.AddDays(-1), "Easter Eve", "Holy Saturday", countryCode, null, new string[] { "AUS-ACT", "AUS-NSW", "AUS-NT", "AUS-QLD", "AUS-SA", "AUS-VIC" }));
            items.Add(new PublicHoliday(easterSunday, "Easter Sunday", "Easter Sunday", countryCode, null, new string[] { "AUS-ACT", "AUS-NSW", "AUS-VIC" }));
            items.Add(new PublicHoliday(easterSunday.AddDays(1), "Easter Monday", "Easter Monday", countryCode));
            items.Add(new PublicHoliday(year, 4, 25, "Anzac Day", "Anzac Day", countryCode));
            items.Add(new PublicHoliday(firstMondayInMay, "May Day", "May Day", countryCode, null, new string[] { "AUS-NT" }));
            items.Add(new PublicHoliday(firstMondayInMay, "Labour Day", "Labour Day", countryCode, null, new string[] { "AUS-QLD" }));
            items.Add(new PublicHoliday(firstMondayAfterOr27May, "Reconciliation Day", "Reconciliation Day", countryCode, 2018, new string[] { "AUS-ACT" }));
            items.Add(new PublicHoliday(firstMondayInJune, "Western Australia Day", "Western Australia Day", countryCode, null, new string[] { "AUS-WA" }));
            items.Add(new PublicHoliday(secondMondayInJune, "Queen's Birthday", "Queen's Birthday", countryCode, null, new string[] { "AUS-ACT", "AUS-NSW", "AUS-NT", "AUS-SA", "AUS-TAS", "AUS-VIC" }));
            items.Add(new PublicHoliday(firstMondayInAugust, "Picnic Day", "Picnic Day", countryCode, null, new string[] { "AUS-NT" }));
            items.Add(new PublicHoliday(firstMondayInOctober, "Labour Day", "Labour Day", countryCode, null, new string[] { "AUS-ACT", "AUS-NSW", "AUS-SA" }));
            items.Add(new PublicHoliday(year, 12, 25, "Christmas Day", "Christmas Day", countryCode));
            items.Add(new PublicHoliday(year, 12, 26, "Boxing Day", "St. Stephen's Day", countryCode));

            return items.OrderBy(o => o.Date);
        }

        ///<inheritdoc/>
        public IEnumerable<string> GetSources()
        {
            return new string[]
            {
                "https://en.wikipedia.org/wiki/Public_holidays_in_Australia"
            };
        }
    }
}
