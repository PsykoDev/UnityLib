using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibUnity
{
	public static class MiscUtils
	{
		public const string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";

		public static HttpClient GetDefaultHttpClient()
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			var client = new HttpClient();
			client.DefaultRequestHeaders.Add(HttpRequestHeader.UserAgent.ToString(), DefaultUserAgent);
			return client;
		}

		public static bool IsFileLocked(string filename, FileAccess fileAccess)
		{
			try
			{
				var fs = new FileStream(filename, FileMode.Open, fileAccess);
				fs.Close();
				return false;
			}
			catch (IOException)
			{
				return true;
			}
		}

		public static async Task WaitForFileUnlock(string filename, FileAccess access, int interval = 500, int timeout = 2000)
		{
			var elapsedTime = 0;
			while (elapsedTime < timeout)
			{
				if (IsFileLocked(filename, access))
				{
					elapsedTime += interval;
				}
				else
				{
					break;
				}

				Debug.WriteLine("Waiting to open file");
				await Task.Delay(interval);
			}
			if (IsFileLocked(filename, access)) throw new IOException($"{filename} is used by another process.");
		}

		public static T CastEnum<T>(object val) where T : Enum => (T)Enum.Parse(typeof(T), val.ToString());

	}
}