using System;
using System.ComponentModel;
using System.Globalization;

namespace PIMM.Helpers
{
    public interface IPeriod
    {
        string Description { get; }
        DateTime FromDate { get; set; }
        DateTime SelectedDate { get; set; }
        DateTime ToDate { get; set; }
        PeriodType Type { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        void ChooseNewPeriod(PeriodType type = PeriodType.Month);

        void Init(DateTime current, PeriodType type = PeriodType.Month, CultureInfo currentCulture = null);

        void MoveToNext();

        void MoveToPrevious();

        void ResetSelectedDate(DateTime date);
    }
}