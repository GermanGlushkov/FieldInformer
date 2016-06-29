using System;
using System.Collections;
using System.Text;
using FI.BusinessObjects.Olap.CalculatedMemberTemplates;

namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for CalculatedMember.
	/// </summary>
	public abstract class CalculatedMember:DataMember
	{

		public enum FormatEnum
		{
			Default=0,
			Integer=1,
			Thousands=2,
			Millions=3,
			Decimal=4,
			Percent=5
		}

		internal string _formula="";
		internal Objects _mdxParameters=new Objects(); // used to parse formula
		protected internal FormatEnum _format=FormatEnum.Default;
		protected internal int _solveOrder=0;
		protected internal bool _prompt=false;
		internal static string TypeString;

		//public override event EventHandler BeforeChange;

		internal CalculatedMember(string name, Hierarchy hier):base(hier)
		{			
			_mdxParameters.BeforeAdd+=new ObjectEventHandler(this.OnBeforeAddParameter);
			_mdxParameters.BeforeRemove+=new ObjectEventHandler(this.OnBeforeRemoveParameter);
			_mdxParameters.BeforeChangeItem+=new ObjectEventHandler(this.OnBeforeChangeParameter);
		}
				
		protected virtual void OnBeforeAddParameter(Object sender)
		{
			this.OnBeforeChange();
		}

		protected virtual void OnBeforeRemoveParameter(Object sender)
		{
			this.OnBeforeChange();
		}

		protected virtual void OnBeforeChangeParameter(Object sender)
		{
			this.OnBeforeChange();
		}
		
		protected abstract string BuildDefaultName();
		public abstract CalculatedMember Clone(string name);

		public string GetDefaultName()
		{
			string name=this.BuildDefaultName();
			if(name.StartsWith("*")==false)
				name="*" + name;
			if(name.EndsWith("*")==false)
				name=name+"*";
			return name;
		}

		protected internal void AssignName(string name)
		{
			if(name==null || name.Trim()==string.Empty)
				name=this.BuildDefaultName();

			// remove invalid characters
			name=name.Replace("[","");
			name=name.Replace("]","");
			name=name.Replace("<","");
			name=name.Replace(">","");
			name=name.Replace("'","");
			name=name.Replace("\"","");

			// set asterizes in front and back
			if(name.StartsWith("*")==false)
				name="*" + name;
			if(name.EndsWith("*")==false)
				name=name+"*";

			_name = name;
			_uniqueName=BuildUniqueName(name);
		}

		protected virtual string BuildUniqueName(string Name)
		{
			return this.Hierarchy.UniqueName + ".[" + Name + "]";
		}

		public override bool CanDrillUp
		{
			get { return false;}
		}

		public override bool CanDrillDown
		{
			get { return false;}
		}		

		public virtual FormatEnum Format
		{
			get {return _format;}
			set
			{
				this.OnBeforeChange(this);

				_format=value;
			}
		}

		public string MDXFormatStringClause
		{
			get 
			{
				switch(_format)
				{
					case FormatEnum.Integer:
						return ", FORMAT_STRING='#,#' ";
					case FormatEnum.Thousands:
						return ", FORMAT_STRING='#,#,' ";
					case FormatEnum.Millions:
						return ", FORMAT_STRING='#,#,,' ";
					case FormatEnum.Percent:
						return ", FORMAT_STRING='Percent' ";
					case FormatEnum.Decimal:
						return ", FORMAT_STRING='#,#.00' ";
					default:
						return string.Empty;
				}
			}
		}


		public string MDXSolveOrderClause
		{
			get 
			{
				return ", SOLVE_ORDER=" + this.SolveOrder.ToString() ;
			}
		}


		public virtual int SolveOrder
		{
			get {return _solveOrder;}
			set
			{
				this.OnBeforeChange();

				_solveOrder=value;
			}
		}


		public bool Prompt
		{
			get {return _prompt;}
			set
			{
				this.OnBeforeChange();

				_prompt=value;
			}
		}

		public ArrayList GetPromptMembers()
		{
			ArrayList list=new ArrayList();

			if(this.Prompt)
				list.Add(this);

			foreach(Object obj in this._mdxParameters)
			{
				CalculatedMember cmem=obj as CalculatedMember;
				if(cmem!=null)
				{
					list.AddRange(cmem.GetPromptMembers());
				}
			}

			return list;
		}


		public Member ReplaceParameterMember(Member toReplace, Member replaceWith)
		{			
			for(int i=0;i<_mdxParameters.Count;i++)
			{
				Object obj=_mdxParameters[i];
				if(toReplace==obj)				
					return (Member)_mdxParameters.ReplaceAtIndex(i, replaceWith);

				// --
				CalculatedMember cmem=obj as CalculatedMember;
				if(cmem!=null)
				{
					Member ret=cmem.ReplaceParameterMember(toReplace, replaceWith);
					if(ret!=null)
						return ret;
				}
			}

			return null;
		}


		public override void LoadFromXml(System.Xml.XmlElement xmlEl )
		{
			base.LoadFromXml(xmlEl);
			string prompt=xmlEl.GetAttribute("PR");
			this._prompt=(prompt=="1"?true:false);
			string formatString=xmlEl.GetAttribute("FS").Trim();
			this._format=(formatString==null || formatString=="")?FormatEnum.Default:(FormatEnum)(int.Parse(formatString));

			foreach(System.Xml.XmlElement childEl in xmlEl.ChildNodes) //calculated member references
			{
				if(childEl.Name=="D") // dimension
				{
					throw new NotImplementedException();
				}
				else if(childEl.Name=="H") // hierarchy
				{
					string uniqueName=childEl.GetAttribute("UN");
					string ignoreError=childEl.GetAttribute("IE");

					Hierarchy hier=Schema.Hierarchies[uniqueName];
					if(hier==null && ignoreError!="1")
						throw new InvalidMemberException("Invalid object: " + uniqueName);

					_mdxParameters.Add(hier, false);
				}
				else if(childEl.Name=="L") // level
				{
					string uniqueName=childEl.GetAttribute("UN");
					string ignoreError=childEl.GetAttribute("IE");

					Level lev=Schema.Levels[uniqueName];
					if(lev==null && ignoreError!="1")
						throw new InvalidMemberException("Invalid object: " + uniqueName);

					_mdxParameters.Add(lev, false);
				}
				else if(childEl.Name=="M") // member
				{
					string hierUN=childEl.GetAttribute("H");
					string ignoreError=childEl.GetAttribute("IE");
					DataMember dmem=null;
					Hierarchy hier=null;

					try
					{
						if(hierUN!="")
						{
							hier=Schema.Hierarchies[hierUN];
							if(hier==null && ignoreError!="1")
								throw new InvalidMemberException("Invalid object: " + hier);
						}

						dmem=DataMember.GetFromXml( (hier==null?this.Hierarchy:hier), childEl);
					}
					catch(InvalidMemberException imexc)
					{
						if(ignoreError!="1")
							throw imexc;
					}

					_mdxParameters.Add(dmem, false);
				}

			}
		}

		public override void SaveToXml(System.Xml.XmlElement xmlEl , System.Xml.XmlDocument doc)
		{
			base.SaveToXml(xmlEl, doc);
			xmlEl.SetAttribute("C", "1");
			xmlEl.SetAttribute("FS" , ((int)this.Format).ToString() );
			if(this._prompt)
				xmlEl.SetAttribute("PR", "1");

			foreach(Object obj in this._mdxParameters) //calculated member references
			{
				if(obj is Dimension) // dimension
				{
					throw new NotImplementedException();
				}
				else if(obj is Hierarchy) // hierarchy
				{
					System.Xml.XmlElement childEl=doc.CreateElement("H");
					childEl.SetAttribute("UN" , obj.UniqueName);
					xmlEl.AppendChild(childEl);
				}
				else if(obj is Level) // level
				{
					System.Xml.XmlElement childEl=doc.CreateElement("L");
					childEl.SetAttribute("UN" , obj.UniqueName);
					xmlEl.AppendChild(childEl);
				}
				else if(obj is Member) // member
				{
					Member mem=obj as Member;

					System.Xml.XmlElement childEl=doc.CreateElement("M");
					mem.SaveToXml(childEl, doc);
					if(mem.Hierarchy!=this.Hierarchy)
						childEl.SetAttribute("H" , mem.Hierarchy.UniqueName);
					xmlEl.AppendChild(childEl);
				}

			}
		}

		public virtual string MDXDefinitions
		{
			get
			{
				StringBuilder sb=new StringBuilder();
				foreach(Object obj in _mdxParameters)
				{
					CalculatedMember cmem=obj as CalculatedMember;
					if(cmem!=null)
						sb.Append(cmem.MDXDefinitions);
				}
				return sb.ToString();
			}
		}

	}










	public class CalculatedMembers:Objects
	{
		private Hierarchy _hier;

		internal CalculatedMembers(Hierarchy hier)
		{
			if(hier==null)
				throw new ArgumentNullException();

			_hier=hier;
		}

		public Hierarchy Hierarchy
		{ 
			get {return _hier;}
		}

		public new CalculatedMember this[int index]
		{
			get{return (CalculatedMember)base[index]; }
		}

		public new CalculatedMember this[string UniqueName ]
		{
			get{return (CalculatedMember)base[UniqueName]; }
		}

		public void Add(CalculatedMember Member, bool replaceExisting)
		{
			base.Add(Member, true);
		}
		
		public void Insert(int Index, CalculatedMember Member, bool replaceExisting)
		{
			if(Member.Hierarchy!=this.Hierarchy)
				throw new ArgumentException("Hierarchy mismatch");

			base.Insert (Index, Member, replaceExisting);
		}

		public void ClearSets()
		{
			for(int i=this.Count-1;i>=0;i--)
				if(this[i] is CalculatedMemberTemplates.Set)				
					this.RemoveAt(i);				
		}

		public Member ReplaceParameterMember(Member toReplace, Member replaceWith)
		{			
			Member ret=null;
			for(int i=0;i<this.Count;i++)
			{
				ret=this[i].ReplaceParameterMember(toReplace, replaceWith);
				if(ret!=null)
					break;
			}

			return ret;
		}

		public VisualAggregate GetVisualAggregate(VisualAggregate.AggregateFunction aggr)
		{
			foreach(CalculatedMember cmem in this)
			{
				VisualAggregate vamem=cmem as VisualAggregate;
				if(vamem!=null && vamem.Aggregation==aggr)
					return vamem;
			}
			return null;
		}

		public MemberChildrenSet GetMemberChildrenSet(string memUniqueName)
		{
			foreach(CalculatedMember cmem in this)
			{
				MemberChildrenSet mcs=cmem as MemberChildrenSet;
				if(mcs!=null && mcs.Member.UniqueName==memUniqueName)
					return mcs;
			}
			return null;
		}

		public CalculatedMember[] ToArray()
		{
			return (CalculatedMember[])base.ToArray(typeof(CalculatedMember));
		}

		public CalculatedMember[] ToSortedByNameArray()
		{
			return (CalculatedMember[])base.ToSortedByNameArray(typeof(CalculatedMember));
		}

		public CalculatedMember[] ToSortedByUniqueNameArray()
		{
			return (CalculatedMember[])base.ToSortedByUniqueNameArray(typeof(CalculatedMember));
		}

	}
}
