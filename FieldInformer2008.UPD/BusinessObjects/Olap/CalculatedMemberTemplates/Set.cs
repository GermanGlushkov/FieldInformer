using System;

namespace FI.BusinessObjects.Olap.CalculatedMemberTemplates
{
	/// <summary>
	/// Summary description for Set.
	/// </summary>
	public abstract class Set:CalculatedMember
	{
		internal Set(string name, Hierarchy hier):base(null, hier)
		{
		}

		protected override string BuildUniqueName(string Name)
		{
			return "[" + (Name==null ? "" : Name.Replace("]", "").Replace("[","")) + "]";
		}
	}
}
