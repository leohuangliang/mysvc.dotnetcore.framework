using System;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers
{
    public static class DateTimeExtenion
    {
        public static DateTime ConvertToUtc(this DateTime datetime)
        {
            if (datetime.Kind == DateTimeKind.Unspecified)
            {
                datetime = new DateTime(datetime.Year, datetime.Month, datetime.Day,
                    datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond, DateTimeKind.Utc);
            }

            return datetime;
        }

        public static DateTime? ConvertToUtc(this DateTime? datetime)
        {
            if (datetime.HasValue)
            {
                if (datetime.Value.Kind == DateTimeKind.Unspecified)
                {
                    datetime = new DateTime(datetime.Value.Year, datetime.Value.Month, datetime.Value.Day,
                        datetime.Value.Hour, datetime.Value.Minute, datetime.Value.Second, datetime.Value.Millisecond, DateTimeKind.Utc);
                }
            }


            return datetime;
        }
    }
}