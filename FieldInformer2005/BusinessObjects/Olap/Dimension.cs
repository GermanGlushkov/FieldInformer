using System;
using System.Collections;
using System.Collections.Specialized;

namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for Dimension.
	/// </summary>
	public class Dimension:Object
	{
		private Schema _schema=null;
		private Hierarchies _hierarchies=new Hierarchies();
		private bool _isOpen;

		internal Dimension(Schema schema)
		{
			if(schema==null)
				throw new ArgumentNullException("Schema is null");

			_schema=schema;
		}

		public bool IsOpen
		{
			get{return _isOpen;}
			//set{_isOpen=value;}
		}

		public void Open()
		{
			this._isOpen=true;

			// update open nodes
			if(Schema.OpenNodes.Contains(this.UniqueName)==false)
				Schema.OpenNodes.Add(this.UniqueName);
		}

		public void Close()
		{
			this._isOpen=false;
			// update open nodes
			Schema.OpenNodes.Remove(this.UniqueName);
		}


		public Schema Schema
		{
			get{return _schema;}
		}

		public Hierarchies Hierarchies
		{
			get{return _hierarchies;}
		}


		public void LoadFromXmlSchema(System.Xml.XmlElement xmlEl, Axis axis)
		{
			_uniqueName=xmlEl.GetAttribute("UN");
			_name=xmlEl.GetAttribute("N");
			_isOpen=(xmlEl.GetAttribute("O")=="1"?true:false);

			//hierarchies
			foreach(System.Xml.XmlElement hierEl in xmlEl.ChildNodes)
			{
				Hierarchy hier=new Hierarchy(this);
				hier.LoadFromXmlSchema(hierEl );
				hier.Axis=axis;
				this.Hierarchies.Add(hier);
			}

			Schema.Dimensions.Add(this, false);
		}

	}








	public class Dimensions:Objects
	{
		public new Dimension this[int index]
		{
			get{return (Dimension)base[index]; }
		}

		public new Dimension this[string UniqueName]
		{
			get { return (Dimension)base[UniqueName];}
		}

		public Dimension[] ToArray()
		{
			return (Dimension[])base.ToArray(typeof(Dimension));
		}

		public Dimension[] ToSortedByNameArray()
		{
			return (Dimension[])base.ToSortedByNameArray(typeof(Dimension));
		}

		public Dimension[] ToSortedByUniqueNameArray()
		{
			return (Dimension[])base.ToSortedByUniqueNameArray(typeof(Dimension));
		}

	}


}
