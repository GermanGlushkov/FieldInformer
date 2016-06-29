using System;
using System.Collections;

namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for Level.
	/// </summary>
	public class Level:Object
	{
		private Hierarchy _hierarchy;
		private SchemaMember _defaultMember;
		private short _depth;

		internal Level(Hierarchy hier)
		{
			_hierarchy=hier;
		}


		public Hierarchy Hierarchy
		{
			get{return _hierarchy;}
		}

		public SchemaMember DefaultMember
		{
			get{return _defaultMember;}
		}

		public short Depth
		{
			get{return _depth;}
		}

		public bool IsAllLevel
		{
			get{return( (_name=="(All)" || _name=="All")?true:false);}
		}

		public virtual void LoadFromXmlSchema(System.Xml.XmlElement xmlEl)
		{
			_uniqueName=xmlEl.GetAttribute("UN");
			_name=xmlEl.GetAttribute("N");
			_depth=short.Parse(xmlEl.GetAttribute("LD"));
			
			System.Xml.XmlElement childEl=(System.Xml.XmlElement)xmlEl.FirstChild;
			
			if(childEl!=null && childEl.Name=="DM") //default member
			{
				SchemaMember smem=new SchemaMember(this.Hierarchy);
				smem.LoadFromXml(childEl);
				this._defaultMember=smem;
			}
		}

	}





	public class Levels:Objects
	{
		internal Levels(){}

		public new Level this[int index]
		{
			get{return (Level)base[index]; }
		}

		public new Level this[string UniqueName]
		{
			get{return (Level)base[UniqueName]; }
		}

		public Level[] ToArray()
		{
			return (Level[])base.ToArray(typeof(Level));
		}

		public Level[] ToSortedByNameArray()
		{
			return (Level[])base.ToSortedByNameArray(typeof(Level));
		}

		public Level[] ToSortedByUniqueNameArray()
		{
			return (Level[])base.ToSortedByUniqueNameArray(typeof(Level));
		}
	}


}
