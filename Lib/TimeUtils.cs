using System;

namespace LibUnity
{
	public static class TimeUtils
	{
		public static string FormatSeconds(this ulong seconds)
		{
			var ts = TimeSpan.FromSeconds(seconds);

			if (seconds < 99) return $"{seconds}";

			var minutes = Math.Floor(ts.TotalMinutes);
			if (minutes < 99) return $"{minutes}m";

			var hours = Math.Floor(ts.TotalHours);
			if (hours < 99) return $"{hours}h";

			var days = Math.Floor(ts.TotalDays);
			return $"{days}d";
		}

		public static string FormatMilliseconds(this ulong ms, bool showDecimals = false)
		{
			var ts = TimeSpan.FromMilliseconds(ms);

			var seconds = ts.TotalSeconds;
			if (seconds < 10) return showDecimals ? $"{seconds:N1}" : $"{seconds:N0}";
			return FormatSeconds(ms / 1000);
		}

		public static string FormatSeconds(this long seconds)
		{
			return FormatSeconds((ulong)Math.Abs(seconds));
		}

		public static string FormatMilliseconds(this long ms, bool showDecimals = false)
		{
			return FormatMilliseconds((ulong)Math.Abs(ms), showDecimals);
		}

		public static DateTime FromUnixTime(this long unixTime, DateTimeKind dateTimeKind = DateTimeKind.Utc)
		{
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, dateTimeKind);
			return epoch.AddSeconds(unixTime);
		}
	}
}