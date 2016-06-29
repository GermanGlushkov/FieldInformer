using System;
using System.Collections;
using System.Xml;
using FI.BusinessObjects.Olap;
using FI.BusinessObjects.Olap.CalculatedMemberTemplates;

namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for DataMember.
	/// </summary>
	public class DataMember:Member
	{

//		public override event EventHandler BeforeRemoveFromCollection;

		internal DataMember(Hierarchy hier):base(hier)
		{
		}

		internal DataMember(Hierarchy hier , XmlElement xmlEl):base(hier)
		{
			this.LoadFromXml(xmlEl);
		}

		public DataMember(SchemaMember schemaMem):base(schemaMem.Hierarchy)
		{			
			this._uniqueName=schemaMem.UniqueName;
			this._name=schemaMem.Name;
			this._childCount=schemaMem.ChildCount;
			this._levelDepth=schemaMem.LevelDepth;
		}

		public static DataMember GetFromXml(Hierarchy hier , XmlElement xmlEl)
		{
			if(xmlEl.GetAttribute("C")=="1") //calculated member
			{
				string calcType=xmlEl.GetAttribute("T");

				if(calcType==MembersAggregate.TypeString)
					return new MembersAggregate(hier , xmlEl);
				else if(calcType==VisualAggregate.TypeString)
					return new VisualAggregate(hier , xmlEl);
				else if(calcType==MeasureToMeasure.TypeString)
					return new MeasureToMeasure(hier , xmlEl);
				else if(calcType==MeasureInheritedNull.TypeString)
					return new MeasureInheritedNull(hier , xmlEl);
				else if(calcType==MemToVisAggr.TypeString)
					return new MemToVisAggr(hier , xmlEl);
				else if(calcType==MemToParent.TypeString)
					return new MemToParent(hier , xmlEl);
				else if(calcType==MemToAll.TypeString)
					return new MemToAll(hier , xmlEl);
				else if(calcType==FilteredByNameSet.TypeString)
                    return new FilteredByNameSet(hier, xmlEl);
                else if (calcType == FilteredByMeasureSet.TypeString)
                    return new FilteredByMeasureSet(hier, xmlEl);
				else if(calcType==MemberChildrenSet.TypeString)
					return new MemberChildrenSet(hier , xmlEl);
				else if(calcType==MeasureWrapper.TypeString)				
					return new MeasureWrapper(hier , xmlEl);
				else if(calcType==MemberWrapper.TypeString)				
					return new MemberWrapper(hier , xmlEl);
				else
					throw new InvalidMemberException("Unknown calc member type: " + calcType);
			}
			else if(xmlEl.GetAttribute("E")=="1") //data member with error
			{
				throw new InvalidMemberException("Invalid Member: " + xmlEl.GetAttribute("UN"));
			}
			else //valid data member
			{
				return new DataMember(hier , xmlEl);
			}
		}
		

//
//		internal virtual void OnAddToCollection(DataMembers DataMembers)
//		{
//			bool test=false;
//		}
//
//		internal virtual void OnRemoveFromCollection(DataMembers DataMembers)
//		{
//			if (BeforeRemoveFromCollection != null)
//				BeforeRemoveFromCollection(this, new ObjectChangeEventArgs(ObjectChangeEventArgs.Properties.Custom,null));
//		}
//		

	}





	public class DataMembers:Objects
	{
		private Hierarchy _hier;

		internal DataMembers(Hierarchy hier)
		{
			if(hier==null)
				throw new ArgumentNullException();

			_hier=hier;
		}

		public Hierarchy Hierarchy
		{ 
			get {return _hier;}
		}

		public new DataMember this[int index]
		{
			get{return (DataMember)base[index]; }
		}

		public new DataMember this[string UniqueName ]
		{
			get{return (DataMember)base[UniqueName]; }
		}

		public void Add(Member Member)
		{
			this.Insert(this.Count, Member, true);
		}

		public void Add(Member Member, bool replaceExisting)
		{
			this.Insert(this.Count, Member, replaceExisting);
		}

		public void Insert(int Index, Member Member)
		{
			this.Insert(Index, Member, true);
		}

		public void Insert(int Index, Member Member, bool replaceExisting)
		{
			SchemaMember smem=Member as SchemaMember;
			if(smem!=null)
				this.Insert(Index, smem, replaceExisting);
			else
				this.Insert(Index, (DataMember)Member, replaceExisting);
		}

		public void Add(DataMember Member)
		{
			this.Add(Member, true);
		}

		public void Add(DataMember Member, bool replaceExisting)
		{
			base.Add(Member, replaceExisting);
		}

		public void Insert(int Index, DataMember Member)
		{
			this.Insert(Index, Member, true);
		}

		public void Insert(int Index, DataMember Member, bool replaceExisting)
		{
			if(Member.Hierarchy!=this.Hierarchy)
				throw new ArgumentException("Hierarchy mismatch");

			base.Insert (Index, Member, replaceExisting);
		}

		public void Add(SchemaMember member, bool replaceExisting)
		{
			this.Insert(this.Count, member, replaceExisting);
		}

		public void Insert(int index, SchemaMember member, bool replaceExisting)
		{			
			DataMember dmem=new DataMember(member);		
			this.Insert(index, dmem, replaceExisting);
		}

		public DataMember[] ToArray()
		{
			return (DataMember[])base.ToArray(typeof(DataMember));
		}

		public DataMember[] ToSortedByNameArray()
		{
			return (DataMember[])base.ToSortedByNameArray(typeof(DataMember));
		}

		public DataMember[] ToSortedByUniqueNameArray()
		{
			return (DataMember[])base.ToSortedByUniqueNameArray(typeof(DataMember));
		}

	}


}
