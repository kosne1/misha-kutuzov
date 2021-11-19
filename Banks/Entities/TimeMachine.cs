using System;

namespace Banks.Entities
{
    public class TimeMachine
    {
        private readonly DateTime _currentTime;

        public TimeMachine(DateTime currentTime)
        {
            _currentTime = currentTime;
        }

        public DateTime AddDays(int days)
        {
            return _currentTime.AddDays(days);
        }

        public DateTime AddMonths(int months)
        {
            return _currentTime.AddMonths(months);
        }

        public DateTime AddYear(int years)
        {
            return _currentTime.AddYears(years);
        }
    }
}