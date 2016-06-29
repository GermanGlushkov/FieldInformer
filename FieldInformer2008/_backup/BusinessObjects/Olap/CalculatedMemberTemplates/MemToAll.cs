using System;

namespace FI.BusinessObjects.Olap.CalculatedMemberTemplates
{
	public class MemToAll:CalculatedMember
	{
		new internal static string TypeString="MEM2ALL";

		public MemToAll(string name, Hierarchy HostHier, Member Measure , Hierarchy MemHierarchy):base(name, HostHier)
		{
			if(HostHier.UniqueName!="[Measures]")
				throw new Exception("Cannot create on non-measures hierarchy");

			//DataMember measure=this.Hierarchy.AddDataMember(Measure);
			_mdxParameters.Add(Measure, false);
			_mdxParameters.Add(MemHierarchy, false);
			Initialize(name);
		}

		public MemToAll(Hierarchy HostHier, System.Xml.XmlElement xmlEl):base(null, HostHier)
		{
			if(HostHier.UniqueName!="[Measures]")
				throw new Exception("Cannot create on non-measures hierarchy");

			LoadFromXml(xmlEl);
		}

		public override CalculatedMember Clone(string name)
		{
			return new MemToAll(name, this.Hierarchy, this.Measure, this.MemHierarchy);
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

			Initialize(_name);
		}

		
		public override void SaveToXml(System.Xml.XmlElement xmlEl , System.Xml.XmlDocument doc)
		{
			base.SaveToXml(xmlEl, doc);
			xmlEl.SetAttribute("T" , TypeString);
		}



		private void Initialize(string name)
		{
			if(Measure.Hierarchy!=this.Hierarchy)
				throw new InvalidMemberException("Invalid Measure");

			this._solveOrder=30;
			this._format=FormatEnum.Percent;
			this.AssignName(name);
		}
		
		protected override string BuildDefaultName()
		{
			string memHierName=(MemHierarchy.Name=="" ? MemHierarchy.Dimension.Name : MemHierarchy.Dimension.Name + "." + MemHierarchy.Name);
			return Measure.Name + ",(" + memHierName + ".Current/" + memHierName + ".(All))";
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

				string def="NULL";
				if(memHier.Levels[0].Name=="(All)")
					def="IIF((" + memHier.Levels[0].DefaultMember.UniqueName  + " , " + measure.UniqueName + ")=0 , NULL , (" + memHier.UniqueName + ".CurrentMember , " + measure.UniqueName + ")/(" + memHier.Levels[0].DefaultMember.UniqueName + " , " + measure.UniqueName +  "))";

				sb.Append(def.Replace("'","''"));
				sb.Append("' ");
				sb.Append(this.MDXSolveOrderClause);
				sb.Append(this.MDXFormatStringClause);
				sb.Append(", SCOPE_ISOLATION=CUBE"); // SSAS 2005 SP2 feature for backward compatibility 
				return sb.ToString();
			}
		}



	}
}
