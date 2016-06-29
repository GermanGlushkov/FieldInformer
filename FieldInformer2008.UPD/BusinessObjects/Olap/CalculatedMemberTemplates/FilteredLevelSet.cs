using System;

namespace FI.BusinessObjects.Olap.CalculatedMemberTemplates
{
	public class FilteredLevelSet:Set
	{
		protected internal string _grOrEq="";
		protected internal string _lessOrEq="";
		new internal static string TypeString="FILTLEVSET";

		public FilteredLevelSet(string name, Hierarchy hier, Level lev,  string GrOrEq ,string LessOrEq):base(name, hier)
		{
			_mdxParameters.Add(lev, false);
			this._grOrEq=GrOrEq;
			this._lessOrEq=LessOrEq;
			Initialize(name);
		}

		public FilteredLevelSet(Hierarchy hier, System.Xml.XmlElement xmlEl):base(null, hier)
		{
			LoadFromXml(xmlEl);
		}

		public override CalculatedMember Clone(string name)
		{
			return new FilteredLevelSet(name, this.Hierarchy, this.Level, this.GrOrEq, this.LessOrEq);
		}


		private new void LoadFromXml(System.Xml.XmlElement xmlEl)
		{
			base.LoadFromXml (xmlEl);
			if(this._mdxParameters.Count!=1)
				throw new Exception("Number of references expected: 1");

			this._grOrEq=xmlEl.GetAttribute("GE");
			this._lessOrEq=xmlEl.GetAttribute("LE");
			Initialize(_name);
		}

		public override void SaveToXml(System.Xml.XmlElement xmlEl , System.Xml.XmlDocument doc)
		{
			base.SaveToXml(xmlEl, doc);
			xmlEl.SetAttribute("T" , TypeString);
			xmlEl.SetAttribute("GE", this.GrOrEq);
			xmlEl.SetAttribute("LE", this.LessOrEq);
		}

		private void Initialize(string name)
		{
			this._solveOrder=-500;

			if(this.Level.Hierarchy!=this.Hierarchy)
				throw new InvalidMemberException("Hierarchy mismatch");
			this.AssignName(name);
		}

		protected override string BuildDefaultName()
		{
			return "SET " + this.Level.UniqueName + " (" + GrOrEq + ":" + LessOrEq + ")";
		}

		public string LessOrEq
		{
			get { return _lessOrEq ;}
		}

		public string GrOrEq
		{
			get { return _grOrEq ;}
		}

		public Level Level
		{
			get { return (Level)_mdxParameters[0] ;}
		}

		public override string MDXDefinitions
		{
			get
			{
				System.Text.StringBuilder sb=new System.Text.StringBuilder();
				
				Level lev=this.Level;

				sb.Append("\r\nSET ");
				sb.Append(this.UniqueName); 
				sb.Append(" AS '{");
				string def=" Filter(" + lev.UniqueName +  ".Members , " + lev.Hierarchy.UniqueName + ".CurrentMember.Name>=\"" + this.GrOrEq +  "\"  and " + lev.Hierarchy.UniqueName + ".CurrentMember.Name<=\"" + this.LessOrEq +  "\"  ) ";
				sb.Append(def.Replace("'","''"));
				sb.Append("}'");
				return sb.ToString();
			}
		}

	}
}
