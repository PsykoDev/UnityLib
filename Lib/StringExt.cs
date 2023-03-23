using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LibUnity
{
	public static class StringExt
	{
		public static byte[] ToByteArrayHex(this string hexStr)
		{
			var numberChars = hexStr.Length / 2;
			var bytes = new byte[numberChars];
			using var sr = new StringReader(hexStr);
			for (var i = 0; i < numberChars; i++)
				bytes[i] = Convert.ToByte(new string(new[] { (char)sr.Read(), (char)sr.Read() }), 16);
			return bytes;
		}

		public static byte[] ToByteArray(this string str)
		{
			var ret = new byte[str.Length];
			for (var i = 0; i < str.Length; i++)
			{
				ret[i] = Convert.ToByte(str[i]);
			}
			return ret;
		}

		public static string UnescapeHtml(this string str)
		{
			str = str.Replace("&lt;", "<");
			str = str.Replace("&gt;", ">");
			str = str.Replace("&#xA", "\n");
			str = str.Replace("&quot;", "\"");
			str = str.Replace("&amp;", "&");
			return str;
		}

		public static string EscapeHtml(this string str)
		{
			str = str.Replace("<", "&lt;");
			str = str.Replace(">", "&gt;");
			str = str.Replace("\n", "&#xA");
			str = str.Replace("\"", "&quot;");
			str = str.Replace("&", "&amp;");
			return str;
		}

		public static string ReplaceFirstOccurrenceCaseInsensitive(this string input, string search, string replacement)
		{
			var pos = input.IndexOf(search, StringComparison.InvariantCultureIgnoreCase);
			if (pos < 0) return input;
			var result = input[..pos] + replacement + input[(pos + search.Length)..];
			return result;
		}

		public static string ReplaceCaseInsensitive(this string input, string search, string replacement)
		{
			var result = Regex.Replace(
				input,
				Regex.Escape(search),
				replacement.Replace("$", "$$"),
				RegexOptions.IgnoreCase
			);
			return result;
		}

		public static string AddFontTagsIfMissing(this string msg)
		{
			var sb = new StringBuilder();
			if (!msg.StartsWith("<font", StringComparison.InvariantCultureIgnoreCase))
			{
				if (msg.IndexOf("<font", StringComparison.OrdinalIgnoreCase) > 0)
				{
					sb.Append("<font>");
					sb.Append(msg[..msg.IndexOf("<font", StringComparison.OrdinalIgnoreCase)]);
					sb.Append("</font>");
					sb.Append(msg[msg.IndexOf("<font", StringComparison.OrdinalIgnoreCase)..]);
				}
				else
				{
					sb.Append("<font>");
					sb.Append(msg);
					sb.Append("</font>");
				}
			}
			else sb.Append(msg);
			var openCount = Regex.Matches(msg, "<font").Count;
			var closeCount = Regex.Matches(msg, "</font>").Count;
			if (openCount > closeCount) sb.Append("</font>");
			return sb.ToString();
		}

		public static string ToCapital(this string str)
		{
			var sb = new StringBuilder(str[0].ToString().ToUpper());
			sb.Append(str[1..].ToLower());
			return sb.ToString();
		}

		public static bool StartsWithAny(this string input, string[] tokens, StringComparison options = StringComparison.InvariantCultureIgnoreCase)
		{
			if (tokens.Length == 0)
			{
				throw new ArgumentException($"{nameof(tokens)} cannot be empty");
			}
			return tokens.Any(x => input.StartsWith(x, options));
		}

		public static bool EndsWithAny(this string input, string[] tokens, StringComparison options = StringComparison.InvariantCultureIgnoreCase)
		{
			if (tokens.Length == 0)
			{
				throw new ArgumentException($"{nameof(tokens)} cannot be empty");
			}
			return tokens.Any(x => input.EndsWith(x, options));
		}
	}
}