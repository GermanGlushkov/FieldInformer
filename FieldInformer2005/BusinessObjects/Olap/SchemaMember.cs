using System;
using System.Collections;

namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for SchemaMember.
	/// </summary>
	public class SchemaMember:Member
	{
//		private SchemaMembers _siblings=null;
		private SchemaMembers _children=null;
		private bool _isOpen=false;
		private bool _isPlaceholder=false;

		internal SchemaMember(Hierarchy hier):base(hier)
		{
			_children=new SchemaMembers(hier, this);
		}

//		internal void OnAddToSchemaMembers(SchemaMembers collection)
//		{
//			if(collection!=null && collection.Hierarchy!=this.Hierarchy)
//				throw new ArgumentException("Hierarchy mismatch");
//
//			this._siblings=collection;
//		}

		internal SchemaMember(Hierarchy hier , DataMember SrcMem):base(hier)
		{
			this._uniqueName=SrcMem.UniqueName;
			this._name=SrcMem.Name;
			this._childCount=SrcMem.ChildCount;
			this._levelDepth=SrcMem.LevelDepth;
			//this._isVisible=SrcMem.IsVisible;
		}

		internal SchemaMember(Hierarchy hier , CalculatedMember ParentCalcMember):base(hier)
		{
			this._uniqueName=ParentCalcMember.UniqueName;
			this._name=ParentCalcMember.Name;
			this._childCount=ParentCalcMember.ChildCount;
			this._levelDepth=ParentCalcMember.LevelDepth;
			//this._isVisible=SrcMem.IsVisible;
		}

		public SchemaMember(Hierarchy hier , System.Xml.XmlElement xmlEl):base(hier)
		{
			base.LoadFromXml (xmlEl);
		}


//		public SchemaMember Parent
//		{
//			get{return (_siblings==null ? null : _siblings.ParentMember);}
//		}

		public SchemaMembers Children
		{
			get{return _children;}
		}

//		public SchemaMembers Siblings
//		{
//			get{return _siblings;}
//		}

//		public SchemaMember ParentMember
//		{
//			get{return (_siblings==null ? null : _siblings.ParentMember);}
//		}

		public bool IsOpen
		{
			get{return _isOpen;}
		}

		public bool IsPlaceholder
		{
			get{return _isPlaceholder;}
		}

		public void Open()
		{
			if(this.IsLeaf)
				return;

			// load children if not loaded before
			if(this.Children.Count==0)			
			{				
				System.Xml.XmlElement parentEl=Schema.GetMemberChildren(this.UniqueName, false);
				LoadChildrenFromXmlSchema(parentEl);
			}

			this._isOpen=true;

			// update hierarchy's open nodes
			if(Schema.OpenNodes.Contains(this.UniqueName)==false)
				Schema.OpenNodes.Add(this.UniqueName);
		}

		public void Close()
		{
			this._isOpen=false;
			Schema.OpenNodes.Remove(this.UniqueName);
		}


		public SchemaMember Find(string UniqueName , short MaxLevelDepth)
		{
			if(this.UniqueName==UniqueName)
				return this;
			
			if(this.LevelDepth>=MaxLevelDepth)
				return null;

			return this.Children.Find(UniqueName, MaxLevelDepth);
		}


		public SchemaMember Find(string UniqueName)
		{
			if(this._uniqueName==UniqueName)
				return this;

			return this.Children.Find(UniqueName);
		}


		public void LoadFromXmlSchema(System.Xml.XmlElement xmlEl) //, SchemaMembers siblings)
		{
			base.LoadFromXml (xmlEl);
			this._isOpen=(xmlEl.GetAttribute("O")=="1"?true:false);
			this._isPlaceholder=(xmlEl.GetAttribute("PH")=="1"?true:false);
//			this.OnAddToSchemaMembers(siblings);
			LoadChildrenFromXmlSchema(xmlEl);
		}

		public void LoadChildrenFromXmlSchema(System.Xml.XmlElement xmlEl)
		{
			foreach(System.Xml.XmlElement childEl in xmlEl.ChildNodes)
			{
				SchemaMember child=new SchemaMember(this.Hierarchy);
				child.LoadFromXmlSchema(childEl);// , this.Siblings);
				this.Children.Add(child, false);
			}
		}

	}






	
	public class SchemaMembers:Objects
	{
		private Hierarchy _hier=null;
		private SchemaMember _parentMem=null;

		internal SchemaMembers(Hierarchy hier, SchemaMember parentMem)
		{
			if(hier==null)
				throw new ArgumentNullException("Hierarchy is null");
			_hier=hier;

			if(parentMem!=null && parentMem.Hierarchy!=hier)
				throw new Exception("Hierarchy mismatch");
			_parentMem=parentMem;
		}

		public Hierarchy Hierarchy
		{
			get { return _hier;}
		}

		public SchemaMember ParentMember
		{
			get { return _parentMem;}
		}

		protected internal override void Insert(int Index, Object Object, bool replaceExisting)
		{
			if( ((SchemaMember)Object).Hierarchy!=this.Hierarchy)
				throw new ArgumentException("Hierarchy mismatch");

			base.Insert (Index, Object, replaceExisting);
		}


		public new SchemaMember this[int index]
		{
			get{return (SchemaMember)base[index]; }
		}

		public new SchemaMember this[string UniqueName]
		{
			get{return (SchemaMember)base[UniqueName]; }
		}

		public SchemaMember[] ToArray()
		{
			return (SchemaMember[])base.ToArray(typeof(SchemaMember));
		}

		public SchemaMember[] ToSortedByNameArray()
		{
			return (SchemaMember[])base.ToSortedByNameArray(typeof(SchemaMember));
		}

		public SchemaMember[] ToSortedByUniqueNameArray()
		{
			return (SchemaMember[])base.ToSortedByUniqueNameArray(typeof(SchemaMember));
		}

		public SchemaMember Find(string UniqueName , short MaxLevelDepth)
		{
			for(int i=0;i<this.Count;i++)
			{
				SchemaMember mem=(SchemaMember)this[i].Find(UniqueName , MaxLevelDepth);
				if(mem!=null)
					return mem;
			}
			return null;
		}


		public SchemaMember Find(string UniqueName)
		{
			for(int i=0;i<this.Count;i++)
			{
				SchemaMember mem=(SchemaMember)this[i].Find(UniqueName);
				if(mem!=null)
					return mem;
			}
			return null;
		}

	}



}
