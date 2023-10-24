using Services.Constants;

namespace Services.Extensions;

public static class DateTimeExtensions
{
    public static string CheckProductExpirationStatus(this DateTime expDate)
    {
        if (CheckProductExpired(expDate))
        {
            return ExpirationStatus.EXPIRED;
        }

        if (CheckProductExpiresNext7Days(expDate))
        {
            return ExpirationStatus.NEAR_EXPIRATION_7;
        }

        if (CheckProductExpiresNext14Days(expDate))
        {
            return ExpirationStatus.NEAR_EXPIRATION_14;
        }

        return ExpirationStatus.GOOD;
    }

    public static bool CheckProductExpired(DateTime expDate)
    {
        return DateTime.Today.CompareTo(expDate) >= 0;
    }

    public static bool CheckProductExpiresNext7Days(DateTime expDate)
    {
        return DateTime.Today.CompareTo(expDate) <= 0 && DateTime.Today.AddDays(7).CompareTo(expDate) >= 0;
    }

    public static bool CheckProductExpiresNext14Days(DateTime expDate)
    {
        return DateTime.Today.CompareTo(expDate) <= 0 && DateTime.Today.AddDays(14).CompareTo(expDate) >= 0;
    }
}
