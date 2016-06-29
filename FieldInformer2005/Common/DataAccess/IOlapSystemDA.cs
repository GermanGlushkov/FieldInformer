using System;

namespace FI.Common.DataAccess
{
	public interface IOlapSystemDA
	{
		void ResetOlapSystem();

		void CancelOlapCommand(string TaskId);

		bool IsTaskCanceled(string TaskId);

		Data.FIDataTable GetOlapProcessorInfo();

		string BuildCellset(string Server , string Database ,  string Mdx, string TaskId, string TaskTag);

		string ValidateReportXml(string Server, string Database, string Cube, string InReportXml);

		string GetReportSchemaXml(string Server , string Database, string Cube, string OpenNodesXml);

		string GetSchemaMembers(string Server , string Database, string Cube, string[] UniqueNames);

		string GetLevelMembers(string Server , string Database, string Cube, string LevelUniqueName);

		string GetMemberChildren(string Server , string Database, string Cube, string MemUniqueName, bool IfLeafAddItself);

		string GetMemberParentWithSiblings(string Server , string Database, string Cube, string HierUniqueName, string MemUniqueName);

		string GetMemberGrandParent(string Server , string Database, string Cube, string HierUniqueName, string MemUniqueName);

		string GetMemberParent(string Server , string Database, string Cube, string HierUniqueName, string MemUniqueName);

	}
}
