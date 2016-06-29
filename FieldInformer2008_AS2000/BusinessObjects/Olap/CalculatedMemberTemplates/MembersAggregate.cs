using System;

namespace FI.BusinessObjects.Olap.CalculatedMemberTemplates
{
	public class MembersAggregate:CalculatedMember
	{
		new internal static string TypeString="MEM_AGR";

		public MembersAggregate(string name, Hierarchy hier):base(name, hier)
		{
			this.Initialize(name);
		}

		public MembersAggregate(Hierarchy hier, System.Xml.XmlElement xmlEl):base(null, hier)
		{
			LoadFromXml(xmlEl);
		}

		public override CalculatedMember Clone(string name)
		{
			return new MembersAggregate(name, this.Hierarchy);
		}

		public void AddMember(Member mem)
		{
			_mdxParameters.Add(mem, true);
		}

		public void RemoveMember(string UniqueName)
		{
			_mdxParameters.Remove(UniqueName);
		}

		public Objects Members
		{
			get{return _mdxParameters;}
		}

		public void ClearMembers()
		{
			_mdxParameters.Clear();
		}

		internal void Initialize(string name)
		{
			this._mdxParameters.SetCollectionType(typeof(Member), true);
			this._solveOrder=-100;
			this.AssignName(name);
		}
		
		protected override string BuildDefaultName()
		{
			return "SUM";
		}

		private new void LoadFromXml(System.Xml.XmlElement xmlEl )
		{
			base.LoadFromXml (xmlEl);
			Initialize(_name);
		}

		public override void SaveToXml(System.Xml.XmlElement xmlEl , System.Xml.XmlDocument doc)
		{
			base.SaveToXml(xmlEl, doc);
			xmlEl.SetAttribute("T" , TypeString);
		}

		public override string MDXDefinitions
		{
			get
			{
				System.Text.StringBuilder sb=new System.Text.StringBuilder();
				sb.Append(base.MDXDefinitions); //childrens definitions

				sb.Append("\r\nMEMBER ");
				sb.Append(this.UniqueName); 
				sb.Append(" AS 'SUM({");
				foreach(Object obj in this._mdxParameters)
				{
					sb.Append(obj.UniqueName.Replace("'" , "''"));
					sb.Append(",");
				}

				if(sb[sb.Length-1]==',') //if there are members, remove last comma
					sb.Remove(sb.Length-1,1);

				sb.Append("})' ");
				sb.Append(this.MDXSolveOrderClause);
				//sb.Append(" , FORMAT_STRING=");
				//sb.Append(this.FormatString);
				return sb.ToString();
			}
		}
	}
}
