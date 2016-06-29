using System;
using System.IO;

namespace FI.Common
{
	/// <summary>
	/// Summary description for Serialization.
	/// </summary>
	public class Serialization
	{
		public Serialization()
		{
		}

		public static void SerializeValue(Stream stream, object propVal)
		{
			if(propVal==null || propVal==DBNull.Value)
				throw new Exception("Unable to serialize null or DBNull value");

			byte[] bytes;
			Type t=propVal.GetType();
			if(t==typeof(bool))
				bytes=BitConverter.GetBytes((bool)propVal);
			else if(t==typeof(byte))
				bytes=new byte[]{(byte)propVal};
			else if(t==typeof(short))
				bytes=BitConverter.GetBytes((short)propVal);
			else if(t==typeof(int))
				bytes=BitConverter.GetBytes((int)propVal);
			else if(t==typeof(long))
				bytes=BitConverter.GetBytes((long)propVal);
			else if(t==typeof(float))
				bytes=BitConverter.GetBytes((float)propVal);
			else if(t==typeof(double) || t==typeof(decimal))
				bytes=BitConverter.GetBytes(Convert.ToDouble(propVal));
			else if(t==typeof(string))
			{		
				bytes=System.Text.UnicodeEncoding.Unicode.GetBytes((string)propVal);

				// length
				byte[] len=BitConverter.GetBytes(bytes.Length);
				stream.Write(len,0, len.Length);
			}
			else if(t==typeof(DateTime))
				bytes=BitConverter.GetBytes(((DateTime)propVal).Ticks); 
			else
				throw new Exception("Serialization not supported for type " + t.ToString());
			
			// write to stream
			stream.Write(bytes,0, bytes.Length);
		}


		public static object DeserializeValue(Stream stream, Type dataType)
		{												
			if(dataType==typeof(bool))
				return stream.ReadByte()!=0;
			else if(dataType==typeof(byte))
				return stream.ReadByte();
			else if(dataType==typeof(short))
			{
				byte[] val=new byte[2];
				stream.Read(val, 0, val.Length);
				return BitConverter.ToInt16(val,0);
			}
			else if(dataType==typeof(int))
			{
				byte[] val=new byte[4];
				stream.Read(val, 0, val.Length);
				return BitConverter.ToInt32(val,0);
			}
			else if(dataType==typeof(long))
			{
				byte[] val=new byte[8];
				stream.Read(val, 0, val.Length);
				return BitConverter.ToInt64(val,0);
			}
			else if(dataType==typeof(float))
			{
				byte[] val=new byte[4];
				stream.Read(val, 0, val.Length);
				return BitConverter.ToSingle(val,0);
			}
			else if(dataType==typeof(double) || dataType==typeof(decimal))
			{
				byte[] val=new byte[8];
				stream.Read(val, 0, val.Length);
				return Convert.ChangeType(BitConverter.ToDouble(val,0), dataType);
			}
			else if(dataType==typeof(string))
			{
				// length
				byte[] val=new byte[4];
				stream.Read(val, 0, val.Length);
				int len=BitConverter.ToInt32(val,0);

				// string
				if(len==0)
					return string.Empty;
				val=new byte[len];
				stream.Read(val, 0, val.Length);
				return System.Text.UnicodeEncoding.Unicode.GetString(val);
			}
			else if(dataType==typeof(DateTime))
			{
				byte[] val=new byte[8];
				stream.Read(val, 0, val.Length);
				return new DateTime(BitConverter.ToInt64(val,0)); 
			}
			
			throw new Exception("Deserialization not supported for type: " + dataType.ToString());
		}

	}
}
