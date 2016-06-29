using System;
using System.Collections;
using System.Xml;
using System.Collections.Specialized;

namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for Schema.
	/// </summary>
	public class Schema
	{
		
		// singleton pattern
		public Schema(){}
		// singleton pattern

		public string Server="";
		public string Database="";
		public string Cube="";
		private Dimensions _dimensions=new Dimensions();
		private Hierarchies _hierarchies=new Hierarchies();
		private Levels _levels=new Levels();
		private StringCollection _openNodes=new StringCollection();

		public void DiscardSchema()
		{
			_dimensions.Clear();
			_hierarchies.Clear();
			_levels.Clear();
		}


		public Dimensions Dimensions
		{
			get{return _dimensions;}
		}

		public Hierarchies Hierarchies
		{
			get{return _hierarchies;}
		}

		public Levels Levels
		{
			get{return _levels;}
		}

		public StringCollection OpenNodes
		{
			get {return _openNodes;}
		}

		public Hierarchy GetHierarchyFromMemberUniqueName(string uniqueName)
		{			
			if(uniqueName==null || uniqueName==string.Empty)
				return null;

			bool insideBrackets=false;
			for(int i=0;i<uniqueName.Length;i++)
			{
				if(uniqueName[i]=='[')
					insideBrackets=true;
				else if(uniqueName[i]==']')
					insideBrackets=false;
				else if(uniqueName[i]=='.')
				{
					if(!insideBrackets)
					{
						Hierarchy hier=this.Hierarchies[uniqueName.Substring(0, i)];
						if(hier!=null)
							return hier;
					}
				}
			}

			return this.Hierarchies[uniqueName];
		}

		
		public DataMember GetDataMemberFromXml(XmlElement xmlEl)
		{
			string uniqueName=xmlEl.GetAttribute("UN");
			Hierarchy hier=GetHierarchyFromMemberUniqueName(uniqueName);
			if(hier==null)
				throw new InvalidMemberException("Invalid Member: " + uniqueName);

			return DataMember.GetFromXml(hier, xmlEl);
		}

		public XmlElement GetLevelMembers(string UniqueName)
		{
			FI.Common.DataAccess.IOlapSystemDA dacObj=DataAccessFactory.Instance.GetOlapSystemDA();
			string xml=dacObj.GetLevelMembers(Server , Database , Cube , UniqueName);

			XmlDocument doc=new XmlDocument();
			doc.LoadXml(xml);
			return (XmlElement)doc.FirstChild;
		}

		public XmlElement GetSchemaMembers(string[] UniqueNames)
		{
			FI.Common.DataAccess.IOlapSystemDA dacObj=DataAccessFactory.Instance.GetOlapSystemDA();
			string xml=dacObj.GetSchemaMembers(Server , Database , Cube , UniqueNames);

			XmlDocument doc=new XmlDocument();
			doc.LoadXml(xml);
			return (XmlElement)doc.FirstChild;
		}

		public XmlElement GetMemberChildren(string UniqueName, bool IfLeafAddItself)
		{
			FI.Common.DataAccess.IOlapSystemDA dacObj=DataAccessFactory.Instance.GetOlapSystemDA();
			string xml=dacObj.GetMemberChildren(Server , Database , Cube , UniqueName, IfLeafAddItself);

			XmlDocument doc=new XmlDocument();
			doc.LoadXml(xml);
			return (XmlElement)doc.FirstChild;
		}


		public XmlElement GetMemberParentWithSiblings(string HierUniqueName, string MemUniqueName)
		{
			FI.Common.DataAccess.IOlapSystemDA dacObj=DataAccessFactory.Instance.GetOlapSystemDA();
			string xml=dacObj.GetMemberParentWithSiblings(Server , Database , Cube , HierUniqueName, MemUniqueName);

			XmlDocument doc=new XmlDocument();
			doc.LoadXml(xml);
			return (XmlElement)doc.FirstChild;
		}

		public XmlElement GetMemberParent(string HierUniqueName, string MemUniqueName)
		{
			FI.Common.DataAccess.IOlapSystemDA dacObj=DataAccessFactory.Instance.GetOlapSystemDA();
			string xml=dacObj.GetMemberParent(Server , Database , Cube , HierUniqueName, MemUniqueName);

			XmlDocument doc=new XmlDocument();
			doc.LoadXml(xml);
			return (XmlElement)doc.FirstChild;
		}

		public XmlElement GetMemberGrandParent(string HierUniqueName, string MemUniqueName)
		{
			FI.Common.DataAccess.IOlapSystemDA dacObj=DataAccessFactory.Instance.GetOlapSystemDA();
			string xml=dacObj.GetMemberGrandParent(Server , Database , Cube , HierUniqueName, MemUniqueName);

			XmlDocument doc=new XmlDocument();
			doc.LoadXml(xml);
			return (XmlElement)doc.FirstChild;
		}



		public SchemaMembers GetSchemaMembers(Hierarchy hier, string[] UniqueNames)
		{			
			System.Xml.XmlElement parentEl=GetSchemaMembers(UniqueNames);
			
			SchemaMembers smems=new SchemaMembers(hier, null);
			foreach(System.Xml.XmlElement childEl in parentEl.ChildNodes)
			{
				SchemaMember child=new SchemaMember(hier);
				child.LoadFromXmlSchema(childEl);
				smems.Add(child, false);
			}
			return smems;
		}

		public SchemaMembers GetMemberChildren(Hierarchy hier, string UniqueName, bool IfLeafAddItself)
		{
			SchemaMember smem=new SchemaMember(hier);
			System.Xml.XmlElement parentEl=GetMemberChildren(UniqueName, IfLeafAddItself);
			smem.LoadChildrenFromXmlSchema(parentEl);
			return smem.Children;
		}


		public SchemaMembers GetMemberParentWithSiblings(Hierarchy hier, string MemUniqueName)
		{
			SchemaMember smem=new SchemaMember(hier);
			System.Xml.XmlElement parentEl=GetMemberParentWithSiblings(hier.UniqueName, MemUniqueName);
			smem.LoadChildrenFromXmlSchema(parentEl);
			return smem.Children;
		}

		public SchemaMember GetMemberGrandParent(Hierarchy hier, string MemUniqueName)
		{
			System.Xml.XmlElement parentEl=GetMemberGrandParent(hier.UniqueName, MemUniqueName);
			XmlElement memEl=(XmlElement)parentEl.FirstChild;
			if(memEl!=null)
			{
				SchemaMember smem=new SchemaMember(hier);
				smem.LoadFromXml(memEl);			
				return smem;
			}
			return null;
		}

		public SchemaMember GetMemberParent(Hierarchy hier, string MemUniqueName)
		{
			System.Xml.XmlElement parentEl=GetMemberParent(hier.UniqueName, MemUniqueName);
			XmlElement memEl=(XmlElement)parentEl.FirstChild;
			if(memEl!=null)
			{
				SchemaMember smem=new SchemaMember(hier);
				smem.LoadFromXml(memEl);			
				return smem;
			}
			return null;
		}


	}
}
