using System;

namespace FI.BusinessObjects.Olap.CalculatedMemberTemplates
{
	public class FilteredByMeasureSet:Set
	{
        public enum Operators
        {
            Equals = 0,
            DoesNotEqual = 1,
            GreaterThan = 2,
            GreaterThanOrEquals = 3,
            LessThan = 4,
            LessThanOrEquals=5
        }

        protected internal Operators _operator = Operators.Equals;
        protected internal string _value = null;
		new internal static string TypeString="FILTMEALEVSET";

        public FilteredByMeasureSet(string name, Level lev, Member measure, Operators op, string value)
            : base(name, lev.Hierarchy)
		{
            if (lev.Hierarchy.UniqueName == "[Measures]")
                throw new Exception("Cannot create on measures hierarchy");

            _operator = op;
            value = (value==null ? "" : value.Trim());
            if (value.Length > 50)
                value = value.Substring(0, 50);
            decimal decVal=0;
            if (value.ToUpper() == "NULL")
                _value = "NULL";
            else if (decimal.TryParse(value.Replace(",", "."), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo, out decVal))
                _value = decVal.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
            else
                _value="\"" + value + "\"";

            this.MdxParameters.Add(lev, true);
            this.MdxParameters.Add(measure, false);

			Initialize(name);
		}

        public FilteredByMeasureSet(Hierarchy hier, System.Xml.XmlElement xmlEl)
            : base(null, hier)
		{
			LoadFromXml(xmlEl);
		}

		public override CalculatedMember Clone(string name)
		{
            return new FilteredByMeasureSet(name, this.Level, this.Measure, this.Operator, this.Value);
		}

        public Operators Operator
        {
            get { return _operator; }
        }

        public Level Level
        {
            get { return (Level)this.MdxParameters[0]; }
        }

        public Member Measure
        {
            get { return (Member)this.MdxParameters[1]; }
        }

        private string OperatorToString()
        {
            switch (this.Operator)
            {
                case Operators.Equals:
                    return "=";
                case Operators.DoesNotEqual:
                    return "<>";
                case Operators.GreaterThan:
                    return ">";
                case Operators.GreaterThanOrEquals:
                    return ">=";
                case Operators.LessThan:
                    return "<";
                case Operators.LessThanOrEquals:
                    return "<=";
            }

            throw new ArgumentException("Invalid operator: " + this.Operator.ToString());
        }

        private string Value
        {
            get { return _value; }
        }

		private new void LoadFromXml(System.Xml.XmlElement xmlEl)
		{
			base.LoadFromXml (xmlEl);
			if(this.MdxParameters.Count!=2)
				throw new Exception("Number of references expected: 2");

            if (xmlEl.HasAttribute("O")) // backward compat
                _operator = (Operators)System.Enum.Parse(typeof(Operators), xmlEl.GetAttribute("O"));
            _value = xmlEl.GetAttribute("V");

			Initialize(_name);
		}

		public override void SaveToXml(System.Xml.XmlElement xmlEl , System.Xml.XmlDocument doc)
		{
			base.SaveToXml(xmlEl, doc);
            xmlEl.SetAttribute("T", TypeString);
            xmlEl.SetAttribute("O", this.Operator.ToString());
            xmlEl.SetAttribute("V", this.Value);
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
			return "SET " + this.Level.UniqueName + " (" + this.Measure.Name + this.OperatorToString() + this.Value + ")";
		}

		public override string MDXDefinitions
		{
			get
			{
				System.Text.StringBuilder sb=new System.Text.StringBuilder();

                Level lev = this.Level;
                Member mea = this.Measure;

				sb.Append("\r\nSET ");
				sb.Append(this.UniqueName); 
				sb.Append(" AS '{");
                string def = "Filter(" + lev.UniqueName + ".Members , " + mea.UniqueName + this.OperatorToString() + this.Value + ")";
				sb.Append(def.Replace("'","''"));
				sb.Append("}'");
				return sb.ToString();
			}
		}

	}
}
