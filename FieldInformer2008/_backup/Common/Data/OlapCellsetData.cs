using System;
using System.Runtime.Serialization;

namespace FI.Common.Data
{
	[Serializable]
	public class OlapCellsetData:Auto.OlapCellsetData, ISerializable
	{
		public OlapCellsetData():base()
		{}


		// deserialization constructor
		public OlapCellsetData(SerializationInfo info, StreamingContext context)//:base(info , context)
		{
			Serialization.RawDeSerialize(info.GetString("State") , this , false);
		}

		// serialization method
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("State" , Serialization.RawSerialize(this , false));
		}



		public void Pivot()
		{
			int ax0Count=this.Ax0Mem.Rows.Count;
			int ax1Count=this.Ax1Mem.Rows.Count;
			// add 0 to 1
			for(int i=0;i<ax0Count;i++)
				this.Ax1Mem.Rows.Add(this.Ax0Mem.Rows[i].ItemArray);

			// add 1 to 0
			for(int i=0;i<ax1Count;i++)
				this.Ax0Mem.Rows.Add(this.Ax1Mem.Rows[i].ItemArray);

			//remove first rows from 0
			for(int i=0;i<ax0Count;i++)
				this.Ax0Mem.Rows[0].Delete();

			//remove first rows from 1
			for(int i=0;i<ax1Count;i++)
				this.Ax1Mem.Rows[0].Delete();


			//metadata
			int ax0PosCount=CellsetMetaData[0].Ax0PosCount;
			int ax1PosCount=CellsetMetaData[0].Ax1PosCount;
			short ax0MemCount=CellsetMetaData[0].Ax0MemCount;
			short ax1MemCount=CellsetMetaData[0].Ax1MemCount;
			CellsetMetaData[0].Ax0PosCount=ax1PosCount;
			CellsetMetaData[0].Ax1PosCount=ax0PosCount;
			CellsetMetaData[0].Ax0MemCount=ax1MemCount;
			CellsetMetaData[0].Ax1MemCount=ax0MemCount;


			// cells
			int nVal;
			foreach(OlapCellsetData.ClRow row in Cl.Rows)
			{
				nVal=row.Ax0;
				row.Ax0=row.Ax1;
				row.Ax1=nVal;
			}


		}
	}

}
