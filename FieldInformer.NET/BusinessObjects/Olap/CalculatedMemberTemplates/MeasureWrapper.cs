using System;

namespace FI.BusinessObjects.Olap.CalculatedMemberTemplates
{

	/// <summary>
	/// Obsolete class, MemberWrapper must be used instead
	/// </summary>
	public class MeasureWrapper:MemberWrapper
	{
		new internal static string TypeString="MEA_WRA";

		public MeasureWrapper(string Name, Hierarchy HostHier, Member SourceMember):base(Name, HostHier, SourceMember)
		{
		}

		public MeasureWrapper(Hierarchy HostHier, System.Xml.XmlElement xmlEl):base(HostHier, xmlEl)
		{
		}

//		public MeasureWrapper(string name, Hierarchy HostHier, Member SourceMeasure):base(name, HostHier)
//		{
//			if(HostHier.UniqueName!="[Measures]")
//				throw new Exception("Cannot create on non-measures hierarchy");
//
//			_mdxParameters.Add(SourceMeasure, false);
//			Initialize(name);
//		}
//
//		public MeasureWrapper(Hierarchy HostHier, System.Xml.XmlElement xmlEl):base(null, HostHier)
//		{
//			if(HostHier.UniqueName!="[Measures]")
//				throw new Exception("Cannot create on non-measures hierarchy");
//
//			LoadFromXml(xmlEl);
//		}
//
//		public override CalculatedMember Clone(string name)
//		{
//			return new MeasureWrapper(name, this.Hierarchy, this.SourceMeasure);
//		}
//
//
//		public Member SourceMeasure
//		{
//			get {return (Member)_mdxParameters[0]; }
//		}
//
//		private new void LoadFromXml(System.Xml.XmlElement xmlEl)
//		{
//			base.LoadFromXml (xmlEl);
//			if(this._mdxParameters.Count!=1)
//				throw new Exception("Number of references expected: 1 ");
//
//			Initialize(_name);
//		}
//
//		public override void SaveToXml(System.Xml.XmlElement xmlEl , System.Xml.XmlDocument doc)
//		{
//			base.SaveToXml(xmlEl, doc);
//			xmlEl.SetAttribute("T" , TypeString);
//		}
//
//		private void Initialize(string name)
//		{
//			if(SourceMeasure.Hierarchy!=this.Hierarchy)
//				throw new InvalidMemberException("Invalid Measure");
//
//			this._solveOrder=-5;
//			this.AssignName(name);
//		}
//
//		protected override string BuildDefaultName()
//		{
//			return SourceMeasure.Name;
//		}
//
//		public override string MDXDefinitions
//		{
//			get
//			{
//				System.Text.StringBuilder sb=new System.Text.StringBuilder();
//
//				Member srcMeasure=this.SourceMeasure;
//
//				sb.Append("\r\nMEMBER ");
//				sb.Append(this.UniqueName); 
//				sb.Append(" AS '");
//				string def=srcMeasure.UniqueName;
//				sb.Append(def.Replace("'","''") + "' ");
//				sb.Append(this.MDXSolveOrderClause);
//				sb.Append(this.MDXFormatStringClause);
//				return sb.ToString();
//			}
//		}



	}
}
