using System;

namespace RateApp.Services
{
    public static class DateTimeProvider
    {
        private static IDateTimeProvider _current = new DefaultDateTimeProvider();

        public static IDateTimeProvider Current
        {
            get { return _current; }
            set { _current = value; }
        }

        private class DefaultDateTimeProvider : IDateTimeProvider
        {
            public DateTime UtcNow => DateTime.UtcNow;
        }
    }
}
