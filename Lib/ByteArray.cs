using System.Text;

namespace LibUnity
{
	public static class ByteArray
	{
		public static string ToHexString(this byte[] ba)
		{
			var hex = new StringBuilder(ba.Length * 2);
			foreach (var b in ba)
				hex.AppendFormat("{0:x2}", b);
			return hex.ToString();
		}

		public static string ToUTF8String(this byte[] ba) => Encoding.UTF8.GetString(ba);
		public static string ToDefaultString(this byte[] ba) => Encoding.Default.GetString(ba);
		public static string ToASCIIString(this byte[] ba) => Encoding.ASCII.GetString(ba);

	}
}