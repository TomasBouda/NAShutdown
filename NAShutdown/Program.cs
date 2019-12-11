using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Net;
using System.IO;
using System.Configuration;

namespace NAShutdown
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "NAShutdown";

			string ipAddress = ConfigurationManager.AppSettings["IpAddress"];

			if (NetHelper.IsValidIp(ipAddress))
			{
				Console.WriteLine($"Shutting down NAS({ipAddress})...");

				if (Shutdown(ipAddress))
					Console.WriteLine("NAS will shutdown now.");
				else
					Console.WriteLine("Failed to shutdown NAS!");
			}
			else
			{
				Console.WriteLine("Ip address is invalid or it wasn't provided in app.config!");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ipAddress">Ip address assigned to NAS</param>
		/// <param name="timeout"></param>
		/// <returns></returns>
		private static bool Shutdown(string ipAddress, int timeout = 5000)
		{
			var client = new RestClient($"http://{ipAddress}/cgi-bin/system_mgr.cgi");
			client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36";

			var request = new RestRequest(Method.POST);

			request.AddCookie("username", "admin");

			request.AddHeader("Host", ipAddress);
			request.AddHeader("Pragma", "no-cache");
			request.AddHeader("Cache-Control", "no-cache");
			request.AddHeader("Accept", "*/*");
			request.AddHeader("Origin", "http://" + ipAddress);
			request.AddHeader("X-Requested-With", "XMLHttpRequest");
			request.AddHeader("Accept-Language", "cs-CZ,cs;q=0.8,en;q=0.6");
			request.AddHeader("Accept-Encoding", "gzip, deflate");
			request.AddHeader("Referer", $"http://{ipAddress}/web/system_mgr/system.html");
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

			request.Timeout = timeout;

			request.AddParameter("cmd", "cgi_shutdown");

			IRestResponse response = client.Execute(request);

			Console.WriteLine("\t" + response?.ErrorMessage);

			return response.StatusCode == HttpStatusCode.OK;
		}
	}
}
