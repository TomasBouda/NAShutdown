using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAShutdown
{
	public static class NetHelper
	{
		public static bool IsValidIp(string address)
		{
			if (string.IsNullOrWhiteSpace(address))
			{
				return false;
			}

			string[] splitValues = address.Split('.');
			if (splitValues.Length != 4)
			{
				return false;
			}

			byte tempForParsing;

			return splitValues.All(r => byte.TryParse(r, out tempForParsing));
		}
	}
}
