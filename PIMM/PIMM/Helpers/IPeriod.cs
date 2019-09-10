using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PIMM.Helpers
{
    public interface IPeriod
    {
        DateTime FromDate { get; set; }
        DateTime SelectedDate { get; set; }
        DateTime ToDate { get; set; }
        PeriodType Type { get; }

        DateTime FirstDayOfTheMonth(DateTime date);
        DateTime FirstDayOfTheWeek(DateTime date);
        DateTime FirstDayOfTheYear(DateTime date);
        string GetDescription();
        void Init(DateTime current, PeriodType type = PeriodType.Month, CultureInfo currentCulture = null);
        DateTime LastDayOfTheMonth(DateTime date);
        DateTime LastDayOfTheWeek(DateTime date);
        DateTime LastDayOfTheYear(DateTime date);
        void MoveToNext();
        void MoveToPrevious();
        void ResetSelectedDate(DateTime date);
        int WeekOfTheYear(DateTime date);
        int WeekOfTheYearNet(DateTime date);

        void CalculateDates();
    }
}
