using System;

namespace FI.BusinessObjects.Olap.CalculatedMemberTemplates
{
	public class MembersAggregate:CalculatedMember
	{
		public enum AggregateFunction
		{
			AGGREGATE=0,
			SUM=1,
			MIN=2,
			MAX=3,
			AVG=4,
			COUNT=5
		}

		protected internal AggregateFunction _aggregation=AggregateFunction.SUM;
		new internal static string TypeString="MEM_AGR";

		public MembersAggregate(string name, Hierarchy hier, AggregateFunction aggr):base(name, hier)
		{
			_aggregation=aggr ;
			this.Initialize(name);
		}

		public MembersAggregate(Hierarchy hier, System.Xml.XmlElement xmlEl):base(null, hier)
		{
			LoadFromXml(xmlEl);
		}

		public override CalculatedMember Clone(string name)
		{
			return new MembersAggregate(name, this.Hierarchy, this.Aggregation);
		}


		public AggregateFunction Aggregation
		{
			get { return _aggregation ;}
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
			return Aggregation.ToString();
		}

		private new void LoadFromXml(System.Xml.XmlElement xmlEl )
		{
			base.LoadFromXml (xmlEl);
			_aggregation=(AggregateFunction)System.Enum.Parse(typeof(AggregateFunction) , xmlEl.GetAttribute("F"));
			Initialize(_name);
		}

		public override void SaveToXml(System.Xml.XmlElement xmlEl , System.Xml.XmlDocument doc)
		{
			base.SaveToXml(xmlEl, doc);
			xmlEl.SetAttribute("T" , TypeString);
			xmlEl.SetAttribute("F" , this.Aggregation.ToString());
		}

		public override string MDXDefinitions
		{
			get
			{
				System.Text.StringBuilder sb=new System.Text.StringBuilder();
				sb.Append(base.MDXDefinitions); //childrens definitions

				sb.Append("\r\nMEMBER ");
				sb.Append(this.UniqueName); 
				sb.Append(" AS '");
				sb.Append(this.Aggregation.ToString());
				sb.Append("({");
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
				sb.Append(", SCOPE_ISOLATION=CUBE"); // SSAS 2005 SP2 feature for backward compatibility 
				return sb.ToString();
			}
		}
	}
}
