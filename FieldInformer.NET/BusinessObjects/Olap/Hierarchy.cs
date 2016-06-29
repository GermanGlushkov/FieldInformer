using System;
using System.Collections;
using FI.BusinessObjects.Olap.CalculatedMemberTemplates;

namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for Hierarchy.
	/// </summary>
	public class Hierarchy:Object
	{
		private Axis _axis=null;
		private Dimension _dimension=null;
		private Levels _levels=new Levels();
		private DataMembers _dataMembers=null;
		private CalculatedMembers _calcMembers=null;
		private SchemaMembers _schemaMembers=null;
		internal string _orderTupleMember=null;
		private bool _isOpen;
		private DataMember _filterMember=null;
		//private bool _isAggregated=false;


		internal Hierarchy(Dimension dim)
		{
			_dimension=dim;
			_dataMembers=new DataMembers(this);
			_dataMembers.SetCollectionType(typeof(DataMember), false); // allowed only DataMembers , not even inherited

			_calcMembers=new CalculatedMembers(this);
			_schemaMembers=new SchemaMembers(this, null);

			_dataMembers.BeforeAdd+=new ObjectEventHandler(_dataMembers_BeforeAdd);
			_dataMembers.BeforeRemove+=new ObjectEventHandler(_dataMembers_BeforeRemove);
			_dataMembers.BeforeChangeItem+=new ObjectEventHandler(_dataMembers_BeforeChange);

			_calcMembers.BeforeAdd+=new ObjectEventHandler(_calcMembers_BeforeAdd);
			_calcMembers.BeforeRemove+=new ObjectEventHandler(_calcMembers_BeforeRemove);
			_calcMembers.BeforeChangeItem+=new ObjectEventHandler(_calcMembers_BeforeChange);
		}
		
		private void _dataMembers_BeforeAdd(Object sender)
		{
			OnAddMember((DataMember)sender, false);
		}

		private void _dataMembers_BeforeRemove(Object sender)
		{			
			OnRemoveMember((DataMember)sender, false);
		}

		private void _dataMembers_BeforeChange(Object sender)
		{
			OnChangeMember((DataMember)sender, false);
		}

		private void _calcMembers_BeforeAdd(Object sender)
		{
			OnAddMember((DataMember)sender, true);
		}

		private void _calcMembers_BeforeRemove(Object sender)
		{
			OnRemoveMember((DataMember)sender, true);
		}

		private void _calcMembers_BeforeChange(Object sender)
		{
			OnChangeMember((DataMember)sender, true);
		}

		private void OnAddMember(DataMember mem, bool isCalc)
		{
			// event
			this.OnBeforeChange(this);

			// check filter hier
			if(this.Axis.Ordinal==2)
			{
				if(mem is Set)
					throw new Exception("Set of members cannot act as filter, please use aggregate instead");

				if(_filterMember!=null && _filterMember.UniqueName!=mem.UniqueName)
					this.RemoveMember(_filterMember);
				_filterMember=mem;
			}
		}

		private void OnRemoveMember(DataMember mem, bool isCalc)
		{
			// event
			this.OnBeforeChange(this);

			//zero order tuple member out
			if(mem.UniqueName==this.OrderTupleMember)
				this.OrderTupleMember=null;

			// if datamember has been deleted on filter axis, there's no datamember anymore (it can be only one)
			if(this.Axis.Ordinal==2 && _filterMember!=null && _filterMember.UniqueName==mem.UniqueName)
				_filterMember=null;
		}

		private void OnChangeMember(DataMember mem, bool isCalc)
		{
			// event
			this.OnBeforeChange(this);
		}

		public void AddMember(Member mem, bool replaceExisting)
		{
			CalculatedMember cmem=mem as CalculatedMember;
			if(cmem!=null)
				this.CalculatedMembers.Add(cmem, replaceExisting);
			else
				this.DataMembers.Add(mem, replaceExisting);
		}

		public void RemoveMember(Member mem)
		{
			CalculatedMember cmem=mem as CalculatedMember;
			if(cmem!=null)
				this.CalculatedMembers.Remove(cmem.UniqueName);
			else
				this.DataMembers.Remove(mem.UniqueName);
		}

		public Member ReplaceMember(Member toReplace, Member replaceWith)
		{
			if(toReplace==null || replaceWith==null)
				throw new ArgumentNullException();
			if(toReplace==replaceWith)
				return toReplace;

			CalculatedMember calcToReplace=toReplace as CalculatedMember;
			int existIndex=(calcToReplace!=null ? this.CalculatedMembers.IndexOf(calcToReplace.UniqueName) : this.DataMembers.IndexOf(toReplace.UniqueName));
			
			// this might be one of nested members
			if(existIndex<0)
			{
				// this might be one of nested members
				if(calcToReplace!=null)
					return this.CalculatedMembers.ReplaceParameterMember(calcToReplace, replaceWith);

				return null;
			}

			if(calcToReplace!=null)
				this.CalculatedMembers.RemoveAt(existIndex);
			else
				this.DataMembers.RemoveAt(existIndex);

			try
			{
				this.AddMember(replaceWith, false); //add new member
			}
			catch(Exception exc)
			{
				// rollback in case of error
				if(existIndex>=0)
				{
					if(calcToReplace!=null)
						this.CalculatedMembers.Insert(existIndex, calcToReplace, false);
					else
						this.DataMembers.Insert(existIndex, toReplace, false);
				}

				throw exc;
			}

			return replaceWith;
		}


		# region LoadFrom XML

		internal void LoadFromXmlSchema(System.Xml.XmlElement xmlEl)
		{
			_uniqueName=xmlEl.GetAttribute("UN");
			_name=xmlEl.GetAttribute("N");
			_isOpen=(xmlEl.GetAttribute("O")=="1"?true:false);
			
			// check if there's special measures hierarchy
			if(_uniqueName=="[Measures]")
			{
				System.Xml.XmlElement measuresHier=Schema.GetMeasuresHierarchy();
				if(measuresHier!=null)
				{
					foreach(System.Xml.XmlElement childEl in measuresHier.ChildNodes)
					{
						if(childEl.Name=="L") //levels
							LoadLevelFromXmlSchema(childEl);
						else if(childEl.Name=="M") //schema members
						{
							SchemaMember smem=LoadMemberFromXmlSchema(childEl);
							//because it's special measures hierarchy, and it doesn't have isopen info, we'll do it manually
							if(smem.ChildCount>0 && Schema.OpenNodes.Contains(smem.UniqueName))
								smem.Open();
						}
					}					
					Schema.Hierarchies.Add(this);
					return;
				}
			}
			

			// normal load
			foreach(System.Xml.XmlElement childEl in xmlEl.ChildNodes)
			{
				if(childEl.Name=="L") //levels
					LoadLevelFromXmlSchema(childEl);
				else if(childEl.Name=="M") //schema members
					LoadMemberFromXmlSchema(childEl);
			}

			Schema.Hierarchies.Add(this);
		}

		private Level LoadLevelFromXmlSchema(System.Xml.XmlElement xmlEl)
		{
			Level lev=new Level(this);
			lev.LoadFromXmlSchema(xmlEl);
			this.Levels.Add(lev, false);

			Schema.Levels.Add(lev, false);
			return lev;
		}

		private SchemaMember LoadMemberFromXmlSchema(System.Xml.XmlElement xmlEl)
		{
			return LoadMemberFromXmlSchema(xmlEl,-1);
		}

		private SchemaMember LoadMemberFromXmlSchema(System.Xml.XmlElement xmlEl , int InsertIndex)
		{
			SchemaMember mem=new SchemaMember(this);
			mem.LoadFromXmlSchema(xmlEl);
			if(InsertIndex>=0)
				this.SchemaMembers.Insert(InsertIndex , mem, false);
			else
				this.SchemaMembers.Add(mem, false);
			return mem;
		}


		internal void LoadFromXml(System.Xml.XmlElement xmlEl)
		{
			this.DataMembers.Clear();
			this.CalculatedMembers.Clear();

			foreach(System.Xml.XmlElement childEl in xmlEl.ChildNodes) //children
			{
				if(childEl.Name=="M") // members
				{
					try
					{
						DataMember mem=DataMember.GetFromXml(this , childEl);
						this.AddMember(mem, true);
					}
					catch(InvalidMemberException exc)
					{
						//do nothing , just not load this member
					}
				}
				else if(childEl.Name=="OTM") // order tuple member
				{
					string uniqueName=childEl.GetAttribute("UN");

					if(xmlEl.GetAttribute("C")=="1")
					{
						// calculated member, lookup
						if(this.CalculatedMembers[uniqueName]==null)
							continue; // invalid member
					}

					if(xmlEl.GetAttribute("E")=="1")
						continue; // invalid member

					this.OrderTupleMember=uniqueName;
				}
			}
		}


		internal void SaveToXml(System.Xml.XmlElement xmlEl, System.Xml.XmlDocument doc)
		{
			xmlEl.SetAttribute("UN" , this.UniqueName);

			foreach(DataMember dmem in this.DataMembers) //members
			{
				System.Xml.XmlElement childEl=doc.CreateElement("M");
				dmem.SaveToXml(childEl , doc);
				xmlEl.AppendChild(childEl);
			}	

			foreach(DataMember cmem in this.CalculatedMembers) //calc mems
			{
				System.Xml.XmlElement childEl=doc.CreateElement("M");
				cmem.SaveToXml(childEl , doc);
				xmlEl.AppendChild(childEl);
			}	

			if(this._orderTupleMember!=null) // order tuple member
			{
				System.Xml.XmlElement childEl=doc.CreateElement("OTM");
				childEl.SetAttribute("UN" , this._orderTupleMember);
				xmlEl.AppendChild(childEl);
			}

		}

		
/*
		private void SaveOpenNodesToXml(System.Xml.XmlElement xmlEl, System.Xml.XmlDocument doc)
		{
			foreach(string  openNodeUn in this.OpenNodes) //open nodes
			{
				System.Xml.XmlElement childEl=doc.CreateElement("ON");
				childEl.SetAttribute("UN" , openNodeUn);
				xmlEl.AppendChild(childEl);
			}
		}
*/

		#endregion

		#region MDX 

		internal string MDXHierarchySetName(bool WithCalcMembers)
		{
			string setName=this._uniqueName;
			if(WithCalcMembers)
			{
				setName=setName.Replace("].[" , "_");
				setName=setName.Replace("]" , "_set_wcalc]");
			}
			else
			{
				setName=setName.Replace("].[" , "_");
				setName=setName.Replace("]" , "_set]");
			}
			return setName;
		}


		internal string MDXHierarchyFilterMember()
		{
			if(this.FilterMember!=null)
				return this.FilterMember.UniqueName;

			return "";
		}

		internal string MDXDefinitions()
		{
			if(Axis.Ordinal==2)
			{
				// calc definitions
				System.Text.StringBuilder sb=new System.Text.StringBuilder();
				for(int i=0;i<this.CalculatedMembers.Count;i++)
					sb.Append(((CalculatedMember)this.CalculatedMembers[i]).MDXDefinitions);
				return sb.ToString();
			}
			else
			{
				// set without calcs
				System.Text.StringBuilder sb=new System.Text.StringBuilder();
				sb.Append("\r\nSET ");
				sb.Append(MDXHierarchySetName(false));
				sb.Append(" AS '{");

				foreach(DataMember mem in this.DataMembers)
				{
					sb.Append(mem.UniqueName.Replace("'" , "''"));
					sb.Append(",");
				}

				//  set must be added here, not where calc members, because it's set, and we might want to get
				// some calc members out of it. but definiton is being added later, together with calc members
				// also measure wrapper must be here, because it's an alias of real measure
				foreach(DataMember mem in this.CalculatedMembers)
				{
					if(mem is CalculatedMemberTemplates.Set || mem is MeasureWrapper)
					{
						sb.Append(mem.UniqueName.Replace("'" , "''"));
						sb.Append(",");
					}
				}

				if(sb[sb.Length-1]==',')
					sb.Remove(sb.Length-1,1);//remove last comma

				sb.Append("}'");


				// set with calcs
				System.Text.StringBuilder sb1=new System.Text.StringBuilder();
				sb1.Append("\r\nSET ");
				sb1.Append(MDXHierarchySetName(true));
				sb1.Append(" AS '{");

				sb1.Append("{");
				sb1.Append(MDXHierarchySetName(false));
				sb1.Append("},");

				foreach(CalculatedMember mem in this.CalculatedMembers)
				{
					// definition first
					sb1.Insert(0, mem.MDXDefinitions);

					if ( (mem is CalculatedMemberTemplates.Set)==false && (mem is MeasureWrapper)==false ) // beacuse set  and wrapper were added already to set withoput calc members
					{
						sb1.Append(mem.UniqueName.Replace("'","''"));
						sb1.Append(",");
					}
				}

				if(sb1[sb1.Length-1]==',')
					sb1.Remove(sb1.Length-1,1);//remove last comma

				sb1.Append("}'");

				sb.Append(sb1.ToString());
				return sb.ToString();
			}
		}

		
		#endregion



		#region public properties

		public Schema Schema
		{
			get { return _dimension.Schema;}
		}

		public Axis Axis
		{
			get { return _axis;}
			set
			{
				if(_axis==value)
					return;

				this.OnBeforeChange(this);

				Axis prevAxis=_axis;
				_axis=value;			
				bool cleanup=(_axis is FiltersAxis || prevAxis is FiltersAxis);
				_filterMember=null;		

				if(prevAxis!=null)
					prevAxis.Hierarchies.Remove(this.UniqueName);
				if(_axis!=null && _axis.Hierarchies[this.UniqueName]==null)
					_axis.Hierarchies.Add(this);
				
				// if to or from filter, clear and add default
				if(cleanup)
					this.SetDefaultMember();
		
			}
		}

		public Dimension Dimension
		{
			get {return _dimension;}
		}

		public Levels Levels
		{
			get {return _levels;}
		}

		public DataMembers DataMembers
		{
			get {return _dataMembers;}
		}

		public CalculatedMembers CalculatedMembers
		{
			get {return _calcMembers;}
		}

		public DataMember GetMember(string uniqueName)
		{
			DataMember ret=null;
			ret=this.DataMembers[uniqueName];
			if(ret==null)
				ret=this.CalculatedMembers[uniqueName];
			return ret;
		}

		public SchemaMembers SchemaMembers
		{
			get {return _schemaMembers;}
		}

		public bool IsOpen
		{
			get{return _isOpen;}
		}

		public string OrderTupleMember
		{
			get{return _orderTupleMember;}
			set
			{
				this.OnBeforeChange(this);

				_orderTupleMember=value;
			}
		}

		public DataMember FilterMember
		{
			get{return _filterMember;}
		}		

		public int Ordinal
		{ 
			get { return (Axis==null ? -1 : Axis.Hierarchies.IndexOf(this.UniqueName)); }
		}

		#endregion

		#region public methods

		public string GetName()
		{
			return (this.Name==string.Empty ? this.Dimension.Name : this.Name);
		}

		public void SetDefaultMember()
		{
			this.DataMembers.Clear();
			this.CalculatedMembers.Clear();

			this.DataMembers.Add(this.Levels[0].DefaultMember, false);
		}

		public void Open()
		{
			if(this.IsOpen)
				return;

			//get from schema and load
			if(this.SchemaMembers.Count==0)
			{
				System.Xml.XmlElement parentEl=Schema.GetLevelMembers(this.Levels[0].UniqueName);
				int insertIndex=0;
				foreach(System.Xml.XmlElement childEl in parentEl.ChildNodes)
				{
					LoadMemberFromXmlSchema(childEl,insertIndex);
					insertIndex++;
				}
			}

			this._isOpen=true;

			// update pen nodes
			if(Schema.OpenNodes.Contains(this.UniqueName)==false)
				Schema.OpenNodes.Add(this.UniqueName);
		}

		public void Close()
		{
			this._isOpen=false;
			// update open nodes
			Schema.OpenNodes.Remove(this.UniqueName);
		}

		public void DrillUp(CellsetMember[] cstMems)
		{			
			if(cstMems==null || cstMems.Length==0)
				return;

			short axisOrd=this.Axis.Ordinal;
			int hierOrd=this.Ordinal;
			bool cleanupDone=false;
			foreach(CellsetMember mem in cstMems)
			{
				if(!mem.BelongsTo(this))
					continue;
				if(mem.LevelDepth==0)
					continue;

				//delete data members and sets
				if( !cleanupDone )
				{
					this.DataMembers.Clear();
					this.CalculatedMembers.ClearSets();
					cleanupDone=true;
				}

				//add new members
				MemberChildrenSet mcs=this.AddGrandParentChildrenSet(mem.UniqueName);
				if(mcs==null) // this means parent grandpa is hierarchy
					this.AddParentMemberWithSiblings(mem.UniqueName);				
			}
		}		

		public void DrillDown(CellsetMember[] cstMems, bool IfLeafAddItself)
		{			
			if(cstMems==null || cstMems.Length==0)
				return;

			short axisOrd=this.Axis.Ordinal;
			int hierOrd=this.Ordinal;
			bool cleanupDone=false;
			foreach(CellsetMember mem in cstMems)
			{
				if(!mem.BelongsTo(this))
					continue;
				if(mem.ChildCount<=0 && !IfLeafAddItself)
					continue;

				//delete data members and sets
				if( !cleanupDone )
				{
					this.DataMembers.Clear();
					this.CalculatedMembers.ClearSets();
					cleanupDone=true;
				}

				//add new members
				this.AddMemberChildrenSet(mem.UniqueName, IfLeafAddItself);
			}
		}		

		public void AddMemberChildren(string uniqueName, bool IfLeafAddItself)
		{
			SchemaMembers smems=Schema.GetMemberChildren(this, uniqueName, IfLeafAddItself); 
			if(smems==null || smems.Count==0)
				return;

			for(int i=0; i<smems.Count;i++)
				this.DataMembers.Add(smems[i], true);
		}


		public void AddParentMemberWithSiblings(string uniqueName)
		{
			SchemaMembers smems=Schema.GetMemberParentWithSiblings(this, uniqueName);
			if(smems==null || smems.Count==0)
				return;

			for(int i=0; i<smems.Count;i++)
				this.DataMembers.Add(smems[i], true);
		}


		public MemberChildrenSet AddGrandParentChildrenSet(string uniqueName)
		{
			SchemaMember smem=Schema.GetMemberGrandParent(this, uniqueName);
			if(smem==null) // this means parent grandpa is hierarchy
				return null;
			else
			{
				MemberChildrenSet mcs=new MemberChildrenSet(null, this, smem);
				this.AddMember(mcs, true);
				return mcs;
			}
		}

		public void AddMemberChildrenSet(string uniqueName, bool IfLeafAddItself)
		{
			SchemaMembers smems=Schema.GetSchemaMembers(this, new string [] { uniqueName });
			if(smems.Count>0)
			{
				if(smems[0].IsLeaf)
					this.AddMember(smems[0], true);
				else
				{
					MemberChildrenSet mcs=new MemberChildrenSet(null, this, smems[0]);
					this.AddMember(mcs, true);
				}
			}
		}


		public void RemoveRootSchemaMembers()
		{
//			// calc members
//			while(this.CalculatedMembers.Count>0)
//				this.CalculatedMembers.RemoveAt(0);

			for(int i=0;i<this.SchemaMembers.Count;i++)
				this.DataMembers.Remove(this.SchemaMembers[i].UniqueName);
		}

		public void RemoveSchemaMemberChildren(SchemaMember smem)
		{
			for(int i=0;i<smem.Children.Count;i++)
				this.DataMembers.Remove(smem.Children[i].UniqueName);
		}

		
		public ArrayList GetPromptCalcMembers()
		{
			ArrayList list=new ArrayList();

			foreach(CalculatedMember cmem in this.CalculatedMembers)
				list.AddRange(cmem.GetPromptMembers());

			return list;
		}

		#endregion

	}







	public class Hierarchies:Objects
	{
		private Axis _axis;

		internal Hierarchies()
		{
		}

		internal Axis Axis
		{
			get { return _axis;}
			set
			{
				if(_axis==value)
					return;

				_axis=value;
				Hierarchy[] hiers=(Hierarchy[])this.ToArray();
				foreach(Hierarchy hier in hiers)
					hier.Axis=_axis;
			}
		}

		internal void Add(Hierarchy hier)
		{
			this.Insert(this.Count, hier);
		}

		internal void Insert(int Index, Hierarchy hier)
		{
			base.Insert(Index, hier, false);
		}

		public void MoveUp(Hierarchy Hierarchy)
		{
			int index=this.IndexOf(Hierarchy.UniqueName);
			if(index==-1 || index==0)
				return;

			this.RemoveAt(index);
			this.Insert(index-1,Hierarchy);
		}

		public  void MoveDown(Hierarchy Hierarchy)
		{
			int index=this.IndexOf(Hierarchy.UniqueName);
			if(index==-1 || index==this.Count-1)
				return;

			this.RemoveAt(index);
			this.Insert(index+1,Hierarchy);
		}

		public new Hierarchy this[int index]
		{
			get{return (Hierarchy)base[index]; }
		}

		public new Hierarchy this[string UniqueName]
		{
			get 
			{ 
				return (Hierarchy)base[UniqueName];
			}
		}

		public Hierarchy[] ToArray()
		{
			return (Hierarchy[])base.ToArray(typeof(Hierarchy));
		}

		public Hierarchy[] ToSortedByNameArray()
		{
			return (Hierarchy[])base.ToSortedByNameArray(typeof(Hierarchy));
		}

		public Hierarchy[] ToSortedByUniqueNameArray()
		{
			return (Hierarchy[])base.ToSortedByUniqueNameArray(typeof(Hierarchy));
		}

		public Dimension FindDimension(string UniqueName)
		{
			foreach(Hierarchy hier in this)
			{
				if (hier.Dimension.UniqueName==UniqueName)
					return hier.Dimension;
			}
			return null;
		}


		public Level FindLevel(string UniqueName)
		{
			foreach(Hierarchy hier in this)
			{
				Level lev=hier.Levels[UniqueName];
				if(lev!=null)
					return lev;
			}
			return null;
		}


		public DataMember FindMember(string UniqueName)
		{
			foreach(Hierarchy hier in this)
			{
				DataMember mem=hier.DataMembers[UniqueName];
				if(mem==null)
					mem=hier.CalculatedMembers[UniqueName];
				if(mem!=null)
					return mem;
			}
			return null;
		}

	}


}
