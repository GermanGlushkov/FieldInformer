using System;

namespace FI.Common
{
	/// <summary>
	/// Summary description for Net.
	/// </summary>
	public class Net
	{
		public Net()
		{
		}

		public static bool IsValidIpAddress(string IpAddress)
		{
			try
			{
				IpAddressTyByteArray(IpAddress);
			}
			catch
			{
				return false;
			}
			return true;
		}


		public static byte[] IpAddressTyByteArray(string IpAddress)
		{
			byte[] bytes=new byte[4];

			string[] segments=IpAddress.Split('.');
				
			if(segments.Length!=4)
				throw new Exception("Invalid Ip: " + IpAddress);

			byte segm=0;
			for(int i=0;i<4;i++)
			{
				try
				{
					segm=byte.Parse(segments[i]);
				}
				catch
				{
					throw new Exception("Invalid Ip: " + IpAddress);
				}
				bytes[i]=segm;
			}

			return bytes;
		}


		public static short CompareIpAddresses(string IpAddress1 , string IpAddress2)
		{
			byte[] ipBytes1=IpAddressTyByteArray(IpAddress1);
			byte[] ipBytes2=IpAddressTyByteArray(IpAddress2);

			for(int i=0;i<4;i++)
			{
				if(ipBytes1[i]<ipBytes2[i])
					return -1;
				else if (ipBytes1[i]>ipBytes2[i])
					return 1;
			}

			return 0; // if equal
		}

	}
}
