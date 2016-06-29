using System;
using System.Runtime.Serialization;
using System.Data;

namespace FI.Common.Data
{
	[Serializable]
	public class OlapReportData:Auto.OlapReportData, ISerializable
	{
		public OlapReportData():base()
		{}


		// deserialization constructor
		public OlapReportData(SerializationInfo info, StreamingContext context)//:base(info , context)
		{
			Serialization.RawDeSerialize(info.GetString("State") , this , false);
		}

		// serialization method
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("State" , Serialization.RawSerialize(this , false));
		}


		public OlapReportData.LevelsRow LookupLevel(OlapReportData.HierarchiesRow hier, short LevelDepth)
		{
			OlapReportData rpt=(OlapReportData)hier.Table.DataSet;
			OlapReportData.LevelsRow[] levels=(OlapReportData.LevelsRow[])rpt.Levels.Select("Hierid=" + hier.Id.ToString() + " and Depth=" + LevelDepth.ToString());
			if(levels==null || levels.Length==0)
				return null;

			return levels[0];
		}

	
	}
}
