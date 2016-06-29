using System;
using FI.BusinessObjects.Olap;

namespace FI.BusinessObjects.Olap.CalculatedMemberTemplates
{

	public class MemberWrapper:CalculatedMember
	{
		new internal static string TypeString="MEM_WRA";

		public MemberWrapper(string Name, Hierarchy HostHier, Member SourceMember):base(Name, HostHier)
		{
			if(SourceMember is Set)
				throw new ArgumentException("Invalid parameter: Set");

            this.MdxParameters.Add(SourceMember, true);
			Initialize(Name);
		}

		public MemberWrapper(Hierarchy HostHier, System.Xml.XmlElement xmlEl):base(null, HostHier)
		{
			LoadFromXml(xmlEl);
		}

		public override CalculatedMember Clone(string name)
		{
			return new MemberWrapper(name, this.Hierarchy, this.SourceMember);
		}


		public Member SourceMember
		{
            get { return (Member)this.MdxParameters[0]; }
		}

		private new void LoadFromXml(System.Xml.XmlElement xmlEl)
		{
			base.LoadFromXml (xmlEl);
            if (this.MdxParameters.Count != 1)
				throw new Exception("Number of references expected: 1 ");

			Initialize(_name);
		}

		public override void SaveToXml(System.Xml.XmlElement xmlEl , System.Xml.XmlDocument doc)
		{
			base.SaveToXml(xmlEl, doc);
			xmlEl.SetAttribute("T" , TypeString);
		}

		private void Initialize(string name)
		{
			if(SourceMember.Hierarchy!=this.Hierarchy)
				throw new InvalidMemberException("Invalid Member");

			if(this.Hierarchy.UniqueName=="[Measures]")
				this._solveOrder=100; //lower priority
			else
				this._solveOrder=101; // higher priority

			this.AssignName(name);
		}
		
		public override int SolveOrder
		{
			get { return base.SolveOrder; }
			set
			{
				base.SolveOrder = value;
			}
		}

		public override FormatEnum Format
		{
			get { return base.Format; }
			set 
			{ 
				base.Format = value;

				// if not measure and default solve order, prioritize percent over default
				if(value==FormatEnum.Percent && this.Hierarchy.UniqueName!="[Measures]" && this.SolveOrder==101)
					this.SolveOrder=102;
			}
		}

		protected override string BuildDefaultName()
		{
			return SourceMember.Name;
		}

        public bool IsRenameOnly
        {
            get { return (this.Format == FormatEnum.Default); }
        }

		public override string MDXDefinitions
		{
			get
			{
				System.Text.StringBuilder sb=new System.Text.StringBuilder();

				Member srcMember=this.SourceMember;

				sb.Append("\r\nMEMBER ");
				sb.Append(this.UniqueName); 
				sb.Append(" AS '");
				string def=srcMember.UniqueName;
				sb.Append(def.Replace("'","''") + "' ");
                sb.Append(this.MDXSolveOrderClause);
                sb.Append(this.MDXFormatStringClause);

				return sb.ToString();
			}
		}



	}
}
