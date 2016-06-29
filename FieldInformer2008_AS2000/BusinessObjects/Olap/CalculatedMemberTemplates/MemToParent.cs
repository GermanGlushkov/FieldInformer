using System;

namespace FI.BusinessObjects.Olap.CalculatedMemberTemplates
{
	public class MemToParent:CalculatedMember
	{

		new internal static string TypeString="MEM2PAR";

		public MemToParent(string name, Hierarchy HostHier, Member Measure , Hierarchy MemHierarchy):base(name, HostHier)
		{
			if(HostHier.UniqueName!="[Measures]")
				throw new Exception("Cannot create on non-measures hierarchy");

			//DataMember measure=this.Hierarchy.AddDataMember(Measure);
			_mdxParameters.Add(Measure, false);
			_mdxParameters.Add(MemHierarchy, false);
			Initialize(name, true);
		}

		public MemToParent(Hierarchy HostHier, System.Xml.XmlElement xmlEl):base(null, HostHier)
		{
			if(HostHier.UniqueName!="[Measures]")
				throw new Exception("Cannot create on non-measures hierarchy");

			LoadFromXml(xmlEl);
		}

		public override CalculatedMember Clone(string name)
		{
			return new MemToParent(name, this.Hierarchy, this.Measure, this.MemHierarchy);
		}


		public Member Measure
		{
			get {return (Member)_mdxParameters[0]; }
		}

		public Hierarchy MemHierarchy
		{
			get {return (Hierarchy)_mdxParameters[1]; }
		}

		private new void LoadFromXml(System.Xml.XmlElement xmlEl)
		{
			base.LoadFromXml (xmlEl);
			if(this._mdxParameters.Count!=2)
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
		}
		
		protected override string BuildDefaultName()
		{
			string memHierName=(MemHierarchy.Name=="" ? MemHierarchy.Dimension.Name : MemHierarchy.Dimension.Name + "." + MemHierarchy.Name);
			return Measure.Name + ",(" + memHierName + ".Current/" + memHierName + ".Parent)";
		}

		public override string MDXDefinitions
		{
			get
			{
				System.Text.StringBuilder sb=new System.Text.StringBuilder();
				sb.Append(base.MDXDefinitions);

				Member measure=this.Measure;
				Hierarchy memHier=this.MemHierarchy;

				sb.Append("\r\nMEMBER ");
				sb.Append(this.UniqueName); 
				sb.Append(" AS '");
				string def="IIF(" + memHier.UniqueName + ".CurrentMember.Level.Ordinal=0, NULL , IIF((" + memHier.UniqueName +  ".CurrentMember.Parent , " + measure.UniqueName +  ")=0 , NULL , (" + memHier.UniqueName +  ".CurrentMember , " + measure.UniqueName + ")/(" + memHier.UniqueName + ".CurrentMember.Parent , " + measure.UniqueName + ")))";
				sb.Append(def.Replace("'","''") + "' ");
				sb.Append(this.MDXSolveOrderClause);
				sb.Append(this.MDXFormatStringClause);
				return sb.ToString();
			}
		}



	}
}
