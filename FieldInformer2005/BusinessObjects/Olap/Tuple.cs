using System;

namespace FI.BusinessObjects.Olap
{
	public class Tuple:Objects
	{
		private Schema _schema=null;

		public Tuple(Schema schema):base()
		{
			if(schema==null)
				throw new ArgumentNullException("Schema==null");
			_schema=schema;

			SetCollectionType(typeof(DataMember), true);
		}

		public new DataMember this[int index]
		{
			get{return (DataMember)base[index]; }
		}

		public new DataMember this[string UniqueName ]
		{
			get{return (DataMember)base[UniqueName]; }
		}

		
		public DataMember GetByHierarchy(Hierarchy hier)
		{
			foreach(DataMember mem in this)
				if(mem.BelongsTo(hier))
					return mem;

			return null;
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

		protected internal override void Insert(int Index, Object Object, bool replaceExisting)
		{
			// also replace members with same hierarchy
			for(int i=0;i<this.Count;i++)
				if(this[i].Hierarchy==((DataMember)Object).Hierarchy)
				{
					this.ReplaceAtIndex(i, Object);
					return;
				}

			base.Insert (Index, Object, replaceExisting);
		}


		internal void LoadFromXml(System.Xml.XmlElement xmlEl)
		{
			foreach(System.Xml.XmlElement childEl in xmlEl.ChildNodes) //children
			{
				if(childEl.Name=="M") // members
				{
					try
					{
						DataMember mem=_schema.GetDataMemberFromXml(childEl);
						this.Add(mem);
					}
					catch(InvalidMemberException exc)
					{
						//do nothing , just not load this member
					}
				}
			}
		}


		internal void SaveToXml(System.Xml.XmlElement xmlEl, System.Xml.XmlDocument doc)
		{			
			foreach(DataMember dmem in this) //members
			{
				System.Xml.XmlElement childEl=doc.CreateElement("M");
				dmem.SaveToXml(childEl , doc);
				xmlEl.AppendChild(childEl);
			}	
		}

	}
}
