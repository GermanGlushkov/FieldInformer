using System;

namespace FI.BusinessObjects.Olap.CalculatedMemberTemplates
{

	public class MeasureInheritedNull:CalculatedMember
	{
		new internal static string TypeString="MEAINHNULL";

		public MeasureInheritedNull(string name, Hierarchy HostHier, Member Measure1 , Member Measure2):base(name, HostHier)
		{
			if(HostHier.UniqueName!="[Measures]")
				throw new Exception("Cannot create on non-measures hierarchy");

			_mdxParameters.Add(Measure1, false);

			if(Measure1.UniqueName!=Measure2.UniqueName)
				_mdxParameters.Add(Measure2, false); //add if not equal

			Initialize(name);
		}

		public MeasureInheritedNull(Hierarchy HostHier, System.Xml.XmlElement xmlEl):base(null, HostHier)
		{
			if(HostHier.UniqueName!="[Measures]")
				throw new Exception("Cannot create on non-measures hierarchy");

			LoadFromXml(xmlEl);
		}

		public override CalculatedMember Clone(string name)
		{
			return new MeasureInheritedNull(name, this.Hierarchy, this.Measure1, this.Measure2);;
		}

		public Member Measure1
		{
			get {return (Member)_mdxParameters[0]; }
		}

		public Member Measure2
		{
			get {return (Member)(_mdxParameters.Count==1 ? _mdxParameters[0] : _mdxParameters[1]); }
		}

		private new void LoadFromXml(System.Xml.XmlElement xmlEl)
		{
			base.LoadFromXml (xmlEl);
			if(this._mdxParameters.Count<1 || this._mdxParameters.Count>2)
				throw new Exception("Number of references expected: 1 or 2");

			Initialize(_name);
		}

		public override void SaveToXml(System.Xml.XmlElement xmlEl , System.Xml.XmlDocument doc)
		{
			base.SaveToXml(xmlEl, doc);
			xmlEl.SetAttribute("T" , TypeString);
		}


		private void Initialize(string name)
		{
			if(Measure1.Hierarchy!=this.Hierarchy || Measure2.Hierarchy!=this.Hierarchy)
				throw new InvalidMemberException("Invalid Measure");

			this._solveOrder=30;
			this.AssignName(name);
		}

		protected override string BuildDefaultName()
		{
			return Measure1.Name + " (NULL:" + Measure2.Name + ")";
		}

		public override string MDXDefinitions
		{
			get
			{
				System.Text.StringBuilder sb=new System.Text.StringBuilder();

				Member mem1=this.Measure1;
				Member mem2=this.Measure2;

				sb.Append("\r\nMEMBER ");
				sb.Append(this.UniqueName); 
				sb.Append(" AS '");
				string def="IIF((" + mem2.UniqueName + ")=NULL , NULL , " + mem1.UniqueName + ")";
				sb.Append(def.Replace("'","''") + "' ");
				sb.Append(this.MDXSolveOrderClause);
				sb.Append(this.MDXFormatStringClause);
				return sb.ToString();
			}
		}



	}
}
