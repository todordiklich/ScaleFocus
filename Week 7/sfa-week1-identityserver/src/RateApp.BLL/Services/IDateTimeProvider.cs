using System;

namespace RateApp.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
