using System;

namespace FI.BusinessObjects.Olap.CalculatedMemberTemplates
{
	public class MemToVisAggr:CalculatedMember
	{
		new internal static string TypeString="MEM2VAGR";

		public MemToVisAggr(string name, Hierarchy HostHier, Member Measure , Hierarchy VisAggrHierarchy, VisualAggregate.AggregateFunction AggregateFunction):base(name, HostHier)
		{
			if(HostHier.UniqueName!="[Measures]")
				throw new Exception("Cannot create on non-measures hierarchy");

			//DataMember measure=this.Hierarchy.AddDataMember(Measure);
			VisualAggregate vAggr=new VisualAggregate(null, VisAggrHierarchy, AggregateFunction);
			//vAggr=(VisualAggregate)VisAggrHierarchy.AddDataMember(vAggr);

            this.MdxParameters.Add(Measure, true);
            this.MdxParameters.Add(vAggr, false);
			Initialize(name, true);
		}

		public MemToVisAggr(Hierarchy HostHier, System.Xml.XmlElement xmlEl):base(null, HostHier)
		{
			if(HostHier.UniqueName!="[Measures]")
				throw new Exception("Cannot create on non-measures hierarchy");

			LoadFromXml(xmlEl);
		}

		public override CalculatedMember Clone(string name)
		{
			return new MemToVisAggr(name, this.Hierarchy, this.Measure, this.VisAggregate.Hierarchy, this.VisAggregate.Aggregation);
		}

		public Member Measure
		{
            get { return (Member)this.MdxParameters[0]; }
		}

		public VisualAggregate VisAggregate
		{
            get { return (VisualAggregate)this.MdxParameters[1]; }
		}

		private new void LoadFromXml(System.Xml.XmlElement xmlEl)
		{
			base.LoadFromXml (xmlEl);
			if(this.MdxParameters.Count!=2)
				throw new Exception("Number of references expected: 2");

			Initialize(_name, false);
		}


		public override void SaveToXml(System.Xml.XmlElement xmlEl , System.Xml.XmlDocument doc)
		{
			base.SaveToXml(xmlEl, doc);
			xmlEl.SetAttribute("T" , TypeString);
		}


        private void Initialize(string name, bool asNew)
		{
			if(Measure.Hierarchy!=this.Hierarchy)
				throw new InvalidMemberException("Invalid Measure");

			this._solveOrder=30;
            if(asNew)
			    this._format=FormatEnum.Percent;

			this.AssignName(name);
			VisAggregate.AssignName(_name + "_param1");
		}

		protected override string BuildDefaultName()
		{			
			string visHierName=(VisAggregate.Hierarchy.Name=="" ? VisAggregate.Hierarchy.Dimension.Name : VisAggregate.Hierarchy.Dimension.Name + "." +  VisAggregate.Hierarchy.Name);
			return Measure.Name + ",(" + visHierName + ".Current/" + visHierName + "." +  VisAggregate.Aggregation.ToString() + ")";
		}

		public override string MDXDefinitions
		{
			get
			{
				System.Text.StringBuilder sb=new System.Text.StringBuilder();
				sb.Append(base.MDXDefinitions); //childrens definitions

				Member measure=this.Measure;
				VisualAggregate visAgr=this.VisAggregate;

				sb.Append("\r\nMEMBER ");
				sb.Append(this.UniqueName); 
				sb.Append(" AS '");
				string def="IIF((" + visAgr.UniqueName + "," + measure.UniqueName + ")=0 , NULL , ( " + visAgr.Hierarchy.UniqueName + ".CurrentMember," + measure.UniqueName + ")/(" + visAgr.UniqueName + "," + measure.UniqueName + "))";
				sb.Append(def.Replace("'","''") + "' ");
				sb.Append(this.MDXSolveOrderClause);
				sb.Append(this.MDXFormatStringClause);
				sb.Append(", SCOPE_ISOLATION=CUBE"); // SSAS 2005 SP2 feature for backward compatibility 
				return sb.ToString();
			}
		}



	}
}
