
using System;
using System.Collections;

namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for Member.
	/// </summary>
	public abstract class Member:Object
	{
		protected Hierarchy _hierarchy=null;
		protected int _childCount=0;
		protected short _levelDepth=0;

		public virtual event EventHandler BeforeRemoveFromCollection;

		internal Member(Hierarchy hier)
		{
			_hierarchy=hier;
		}

		public Schema Schema
		{
			get{return _hierarchy.Schema;}
		}

		public Hierarchy Hierarchy
		{
			get{return _hierarchy;}
		}


		public int ChildCount
		{
			get{return _childCount;}
		}

		public short LevelDepth
		{
			get{return _levelDepth;}
		}


		public virtual bool CanDrillUp
		{
			get { return (this.LevelDepth>0);}
		}

		public virtual bool CanDrillDown
		{
			get { return !this.IsLeaf;}
		}

		public bool IsLeaf
		{
			get{return (_childCount==0?true:false);}
		}


		public virtual void LoadFromXml(System.Xml.XmlElement xmlEl)
		{
			if(xmlEl.GetAttribute("E")=="1")
				throw new InvalidMemberException("Member not valid");

			_uniqueName=xmlEl.GetAttribute("UN");
			_name=xmlEl.GetAttribute("N");
			_childCount=short.Parse(xmlEl.GetAttribute("CC"));
			_levelDepth=short.Parse(xmlEl.GetAttribute("LD"));
		}


		public virtual void SaveToXml(System.Xml.XmlElement xmlEl, System.Xml.XmlDocument doc)
		{
			xmlEl.SetAttribute("UN" , _uniqueName);
			xmlEl.SetAttribute("N" , _name);
			xmlEl.SetAttribute("CC" , _childCount.ToString());
			xmlEl.SetAttribute("LD" , _levelDepth.ToString());
		}

	}





	/*
	public class Members:Objects
	{
		internal Members(){}

		public new Member this[int index]
		{
			get{return (Member)base[index]; }
		}

		public new Member this[int index , bool WithSort]
		{
			get{return (Member)base[index , WithSort]; }
		}

		public new Member this[string UniqueName , bool BinarySearch]
		{
			get{return (Member)base[UniqueName , BinarySearch]; }
		}


		internal override void Insert(int Index, Object Object)
		{
			((Member)Object).OnAddToCollection(this);
			base.Insert (Index, Object);
		}
		internal override void Remove(Object Object)
		{
			((Member)Object).OnRemoveFromCollection(this);
			base.Remove (Object);
		}


	}

	*/

}
