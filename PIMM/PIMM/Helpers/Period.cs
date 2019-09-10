using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PIMM.Helpers
{
    public enum PeriodType
    {
        Week = 0,
        Month = 1,
        Year = 2,
        All = 3
    }

    public class Period : IPeriod
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        /// <summary>
        /// SelectedDate is the first day of the week of the selected period(year,month,week)
        /// </summary>
        public DateTime SelectedDate { get; set; }

        private CultureInfo currentCulture;

        public PeriodType Type { get; set; }

        public void Init(DateTime current, PeriodType type = PeriodType.Month, CultureInfo currentCulture = null)
        {
            if (currentCulture == null)
                this.currentCulture = CultureInfo.CurrentUICulture;
            else
                this.currentCulture = currentCulture;

            this.SelectedDate = current;
            //SelectedDate = FirstDayOfTheWeek(SelectedDate);
            this.Type = type;

            CalculateDates();
        }

        public void ResetSelectedDate(DateTime date)
        {
            SelectedDate = date;

            CalculateDates();
        }

        public string GetDescription()
        {
            string description = "";
            switch (Type)
            {
                case PeriodType.Week:
                    description = $"{FromDate.ToString("dd")} - {ToDate.ToString("dd")}  {SelectedDate.ToString("MMM")}";
                    break;
                case PeriodType.Month:
                    description = $"{SelectedDate.ToString("MMMM")} {SelectedDate.Year}";
                    break;
                case PeriodType.Year:
                    description = $"{SelectedDate.Year}";
                    break;
                case PeriodType.All:
                    description = $"All";
                    break;
            }
            return description;
        }

        public void CalculateDates()
        {
            SelectedDate = FirstDayOfTheWeek(SelectedDate);
            switch (Type)
            {
                case PeriodType.Week:
                    FromDate = FirstDayOfTheWeek(SelectedDate);
                    ToDate = LastDayOfTheWeek(SelectedDate);
                    break;
                case PeriodType.Month:
                    FromDate = FirstDayOfTheMonth(SelectedDate);
                    ToDate = LastDayOfTheMonth(SelectedDate);
                    break;
                case PeriodType.Year:
                    FromDate = FirstDayOfTheYear(SelectedDate);
                    ToDate = LastDayOfTheYear(SelectedDate);
                    break;
                case PeriodType.All:
                    FromDate = DateTime.MinValue;
                    ToDate = DateTime.MaxValue;
                    break;
            }
        }

        public void MoveToNext()
        {
            switch (Type)
            {
                case PeriodType.Week:
                    SelectedDate = SelectedDate.AddDays(7);
                    break;
                case PeriodType.Month:
                    SelectedDate = SelectedDate.AddMonths(1);
                    break;
                case PeriodType.Year:
                    SelectedDate = SelectedDate.AddYears(1);
                    break;
                case PeriodType.All:
                    /// intentionally blank space
                    break;
            }
            CalculateDates();
        }

        public void MoveToPrevious()
        {
            switch (Type)
            {
                case PeriodType.Week:
                    SelectedDate = SelectedDate.AddDays(-7);
                    break;
                case PeriodType.Month:
                    SelectedDate = SelectedDate.AddMonths(-1);
                    break;
                case PeriodType.Year:
                    SelectedDate = SelectedDate.AddYears(-1);
                    break;
                case PeriodType.All:
                    /// intentionally blank space
                    break;
            }
            CalculateDates();
        }

        public DateTime FirstDayOfTheMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public DateTime LastDayOfTheMonth(DateTime date)
        {
            return FirstDayOfTheMonth(date).AddMonths(1).AddTicks(-1);
        }

        public DateTime FirstDayOfTheYear(DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        public DateTime LastDayOfTheYear(DateTime date)
        {
            return FirstDayOfTheYear(date).AddMonths(12).AddTicks(-1);
        }

        public DateTime FirstDayOfTheWeek(DateTime date)
        {
            if (date == DateTime.MaxValue || date == DateTime.MinValue)
                return date;
            DayOfWeek firstDayFromCulture = currentCulture.DateTimeFormat.FirstDayOfWeek;
            int daysPassed = date.DayOfWeek - firstDayFromCulture;
            return date.AddDays(-daysPassed);
        }

        public DateTime LastDayOfTheWeek(DateTime date)
        {
            return FirstDayOfTheWeek(date).AddDays(7).AddTicks(-1);
        }

        public int WeekOfTheYearNet(DateTime date)
        {
            return currentCulture.Calendar.GetWeekOfYear(
                date,
                currentCulture.DateTimeFormat.CalendarWeekRule,
                currentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public int WeekOfTheYear(DateTime date)
        {
            var day = (int)currentCulture.Calendar.GetDayOfWeek(date);
            return currentCulture.Calendar.GetWeekOfYear(
                date.AddDays(4 - (day == 0 ? 7 : day)),
                CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        }



    }
}
