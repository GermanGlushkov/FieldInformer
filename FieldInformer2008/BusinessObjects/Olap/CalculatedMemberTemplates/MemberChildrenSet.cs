using System;

namespace FI.BusinessObjects.Olap.CalculatedMemberTemplates
{
	public class MemberChildrenSet:Set
	{
		new internal static string TypeString="MEMCHILDRENSET";

		public MemberChildrenSet(string name, Hierarchy hier, Member mem):base(name, hier)
		{
            this.MdxParameters.Add(mem, false);
			Initialize(name);
		}

		public MemberChildrenSet(Hierarchy hier, System.Xml.XmlElement xmlEl):base(null, hier)
		{
			LoadFromXml(xmlEl);
		}

		public override CalculatedMember Clone(string name)
		{
			return new MemberChildrenSet(name, this.Hierarchy, this.Member);
		}


		private new void LoadFromXml(System.Xml.XmlElement xmlEl)
		{
			base.LoadFromXml (xmlEl);
            if (this.MdxParameters.Count != 1)
				throw new Exception("Number of parameters expected: 1");

			Initialize(_name);
		}

		public override void SaveToXml(System.Xml.XmlElement xmlEl , System.Xml.XmlDocument doc)
		{
			base.SaveToXml(xmlEl, doc);
			xmlEl.SetAttribute("T" , TypeString);
		}

		private void Initialize(string name)
		{
			this._solveOrder=-500;

			if(this.Member.Hierarchy!=this.Hierarchy)
				throw new InvalidMemberException("Hierarchy mismatch");
			if(this.Member.ChildCount<=0)
				throw new InvalidMemberException("Member has no children: " + this.Member.UniqueName);

			// check if it's placeholder
			SchemaMember smem=this.Member as SchemaMember;
			if(smem!=null && smem.IsPlaceholder)
				throw new InvalidMemberException("'Children' function cannot be applied to placehodler member: " + this.Member.UniqueName);

			this.AssignName(name);
		}

		protected override string BuildDefaultName()
		{
			return "SET " + this.Hierarchy.Name + "." + this.Member.Name + ".Children";
		}

		public Member Member
		{
            get { return (Member)this.MdxParameters[0]; }
		}

		public override string MDXDefinitions
		{
			get
			{
				System.Text.StringBuilder sb=new System.Text.StringBuilder();				

				sb.Append("\r\nSET ");
				sb.Append(this.UniqueName); 
				sb.Append(" AS '{");
				string def=this.Member.UniqueName + ".Children";
				sb.Append(def.Replace("'","''"));
				sb.Append("}'");
				return sb.ToString();
			}
		}

	}
}