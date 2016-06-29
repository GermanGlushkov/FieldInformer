using System;

namespace FI.BusinessObjects.Olap.CalculatedMemberTemplates
{
	public class VisualAggregate:CalculatedMember
	{
		public enum AggregateFunction
		{
			SUM=0,
			MIN=1,
			MAX=2,
			AVG=3,
			COUNT=4
		}

		protected internal AggregateFunction _aggregation=AggregateFunction.SUM;
		new internal static string TypeString="VIS_AGR";

		public VisualAggregate(string name, Hierarchy hier, AggregateFunction aggr):base(name, hier)
		{
			_aggregation=aggr;
			this.Initialize(name, true);
		}

		public VisualAggregate(Hierarchy hier, System.Xml.XmlElement xmlEl):base(null, hier)
		{
			LoadFromXml(xmlEl);
		}

		public override CalculatedMember Clone(string name)
		{
			return new VisualAggregate(name, this.Hierarchy, this.Aggregation);
		}


		public AggregateFunction Aggregation
		{
			get { return _aggregation ;}
		}

        internal void Initialize(string name, bool asNew)
		{
			this._solveOrder=5;
            if(asNew)
			    this._format=FormatEnum.Decimal;
			this.AssignName(name);
		}

		protected override string BuildDefaultName()
		{
			return "VIS " +  Aggregation.ToString();
		}

		private new void LoadFromXml(System.Xml.XmlElement xmlEl )
		{
			base.LoadFromXml (xmlEl);
			_aggregation=(AggregateFunction)System.Enum.Parse(typeof(AggregateFunction) , xmlEl.GetAttribute("F"));
			Initialize(_name, false);
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

				sb.Append("\r\nMEMBER ");
				sb.Append(this.UniqueName); 
				sb.Append(" AS '");
				if(_hierarchy.Axis.Ordinal==2)
					sb.Append(_hierarchy.MDXHierarchyFilterMember().Replace("'" , "''"));
				else
				{
					sb.Append(this.Aggregation.ToString());
					sb.Append("(");
					sb.Append(_hierarchy.MDXHierarchySetName(false));
					sb.Append(")");
				}
				sb.Append("' ");
				sb.Append(this.MDXSolveOrderClause);
				sb.Append(this.MDXFormatStringClause);
				return sb.ToString();
			}
		}

	}
}
