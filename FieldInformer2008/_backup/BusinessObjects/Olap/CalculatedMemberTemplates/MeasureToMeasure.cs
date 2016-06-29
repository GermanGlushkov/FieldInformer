using System;

namespace FI.BusinessObjects.Olap.CalculatedMemberTemplates
{

	public class MeasureToMeasure:CalculatedMember
	{
		public enum Operations
		{
			SUBTRACT=0,
			ADD=1,
			DIVIDE=2,
			MULTIPLY=3
		}
		
		protected internal Operations _operation=Operations.DIVIDE;
		new internal static string TypeString="MEA2MEA";

		public MeasureToMeasure(string name, Hierarchy HostHier, Member Measure1 , Member Measure2, Operations op):base(name, HostHier)
		{
			if(HostHier.UniqueName!="[Measures]")
				throw new Exception("Cannot create on non-measures hierarchy");

			_operation=op;

			//DataMember measure1=this.Hierarchy.AddDataMember(Measure1);
			//DataMember measure2=this.Hierarchy.AddDataMember(Measure2);
			_mdxParameters.Add(Measure1, false);

			if(Measure1.UniqueName!=Measure2.UniqueName)
				_mdxParameters.Add(Measure2, false); //add if not equal

			Initialize(name);
		}

		public MeasureToMeasure(Hierarchy HostHier, System.Xml.XmlElement xmlEl):base(null, HostHier)
		{
			if(HostHier.UniqueName!="[Measures]")
				throw new Exception("Cannot create on non-measures hierarchy");

			LoadFromXml(xmlEl);
		}

		public override CalculatedMember Clone(string name)
		{
			return new MeasureToMeasure(name, this.Hierarchy, this.Measure1, this.Measure2, this.Operation);
		}

		public Operations Operation
		{
			get {return _operation; }

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

			if( xmlEl.HasAttribute("F")) // backward compat
				_operation=(Operations)System.Enum.Parse(typeof(Operations) , xmlEl.GetAttribute("F"));

			Initialize(_name);
		}

		public override void SaveToXml(System.Xml.XmlElement xmlEl , System.Xml.XmlDocument doc)
		{
			base.SaveToXml(xmlEl, doc);
			xmlEl.SetAttribute("T" , TypeString);
			xmlEl.SetAttribute("F" , this.Operation.ToString());
		}


		private void Initialize(string name)
		{
			if(Measure1.Hierarchy!=this.Hierarchy || Measure2.Hierarchy!=this.Hierarchy)
				throw new InvalidMemberException("Invalid Measure");

			this._solveOrder=30;
			this._format=FormatEnum.Decimal;
//			if(this.Operation==Operations.DIVIDE)
//				this._format=FormatEnum.Percent;
			this.AssignName(name);
		}

		protected override string BuildDefaultName()
		{			
			return Measure1.Name + this.OperationToString() + Measure2.Name;
		}

		private string OperationToString()
		{
			switch(this.Operation)
			{
				case Operations.SUBTRACT:
					return "-"; 
				case Operations.ADD:
					return "+"; 
				case Operations.DIVIDE:
					return "/"; 
				case Operations.MULTIPLY:
					return "*"; 
			}

			throw new ArgumentException("Invalid operation: " + this.Operation.ToString());
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

				string def=null;
				if(this.Operation==Operations.DIVIDE)
					def="IIF((" + mem2.UniqueName + ")=0 , NULL , ( " + mem1.UniqueName + " )/(" + mem2.UniqueName + "))";
				else
					def=mem1.UniqueName + this.OperationToString() + mem2.UniqueName;

				sb.Append(def.Replace("'","''") + "' ");
				sb.Append(this.MDXSolveOrderClause);
				sb.Append(this.MDXFormatStringClause);
				sb.Append(", SCOPE_ISOLATION=CUBE"); // SSAS 2005 SP2 feature for backward compatibility 
				return sb.ToString();
			}
		}



	}
}
