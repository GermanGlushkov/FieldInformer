
using System;
using System.Collections;
using System.Xml;

//using System.EnterpriseServices;

namespace FI.DataAccess.Serviced
{
	//
	//didn' find how to set Server Process Shutdown COM+ setting, so don't forget to change this setting in Component Services
	//
	//[ObjectPooling(Enabled=true, MinPoolSize=1, MaxPoolSize=2, CreationTimeout=20000)]
	public class XmlCellsetWrapper : MarshalByRefObject //: ServicedComponent 
	{

		public XmlCellsetWrapper()
		{
			//if (FI.Common.AppConfig.ReadSetting("LogPoolSize", "FALSE").ToUpper()=="TRUE")
			//	_logPoolSize=true;
		}

		public override object InitializeLifetimeService()
		{
			// live forever
			return null;
		}


		private FIXMLCellset.XMLCellsetClass _xmlCst=new FIXMLCellset.XMLCellsetClassClass();
		private FIXMLCellset.OlapSchema _xmlSchema=new FIXMLCellset.OlapSchemaClass();
		//private bool _logPoolSize=false;


		public bool Ping()
		{
			return true;
		}

		public string BuildCellset(string Server , string Database, string Mdx )
		{
			string connString=this.GetConnectionString(Server , Database);
			_xmlCst.SetConnectionFromPool(ref connString);
			string xml=null;
			bool retry=false;

			try
			{
				xml=_xmlCst.BuildCellset(ref Mdx);
			}
			catch(System.Runtime.InteropServices.COMException exc)
			{
				retry=true;
				this.HandleADOMDException(exc , _xmlCst, ref connString, retry);
			}

			if(retry)
				try
				{
					xml=_xmlCst.BuildCellset(ref Mdx);
				}
				catch(System.Runtime.InteropServices.COMException exc)
				{
					retry=false;
					this.HandleADOMDException(exc , _xmlCst, ref connString, retry);
				}

			_xmlCst.ReturnCurrentConnectionToPool();
			return xml;
		}


		public string GetSchemaMembers(string Server , string Database, string Cube, string[] UniqueNames)
		{
			string connString=this.GetConnectionString(Server , Database);
			_xmlSchema.SetConnectionFromPool(ref connString);
			_xmlSchema.SetCube(ref Cube);
			string xml=null;
			System.Array uniqueNames=(System.Array)UniqueNames;
			bool retry=false;

			try
			{
				xml=_xmlSchema.GetSchemaMembers(ref uniqueNames);
			}
			catch(System.Runtime.InteropServices.COMException exc)
			{
				retry=true;
				this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
			}

			if(retry)
				try
				{
					xml=_xmlSchema.GetSchemaMembers(ref uniqueNames);
				}
				catch(System.Runtime.InteropServices.COMException exc)
				{
					retry=false;
					this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
				}
			
			_xmlSchema.ReturnCurrentConnectionToPool();
			return xml;
		}


		public string GetMemberChildren(string Server , string Database, string Cube, string UniqueName , bool IfLeafAddItself)
		{
			string connString=this.GetConnectionString(Server , Database);
			_xmlSchema.SetConnectionFromPool(ref connString);
			_xmlSchema.SetCube(ref Cube);
			string xml=null;
			bool retry=false;

			try
			{
				xml=_xmlSchema.GetMemChildren(ref UniqueName , IfLeafAddItself);
			}
			catch(System.Runtime.InteropServices.COMException exc)
			{
				retry=true;
				this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
			}

			if(retry)
				try
				{
					xml=_xmlSchema.GetMemChildren(ref UniqueName , IfLeafAddItself);
				}
				catch(System.Runtime.InteropServices.COMException exc)
				{
					retry=false;
					this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
				}
			
			_xmlSchema.ReturnCurrentConnectionToPool();
			return xml;
		}


		public string GetMemberParentWithSiblings(string Server , string Database, string Cube, string HierUniqueName, string MemUniqueName)
		{
			string connString=this.GetConnectionString(Server , Database);
			_xmlSchema.SetConnectionFromPool(ref connString);
			_xmlSchema.SetCube(ref Cube);
			string xml=null;
			bool retry=false;

			try
			{
				xml=_xmlSchema.GetMemParentWithSiblings(ref HierUniqueName , ref MemUniqueName);
			}
			catch(System.Runtime.InteropServices.COMException exc)
			{
				retry=true;
				this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
			}

			if(retry)
				try
				{
					xml=_xmlSchema.GetMemParentWithSiblings(ref HierUniqueName , ref MemUniqueName);
				}
				catch(System.Runtime.InteropServices.COMException exc)
				{
					retry=false;
					this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
				}
			
			_xmlSchema.ReturnCurrentConnectionToPool();
			return xml;
		}


		public string GetMemberGrandParent(string Server , string Database, string Cube, string HierUniqueName, string MemUniqueName)
		{
			string connString=this.GetConnectionString(Server , Database);
			_xmlSchema.SetConnectionFromPool(ref connString);
			_xmlSchema.SetCube(ref Cube);
			string xml=null;
			bool retry=false;

			try
			{
				xml=_xmlSchema.GetMemGrandParent(ref HierUniqueName , ref MemUniqueName);
			}
			catch(System.Runtime.InteropServices.COMException exc)
			{
				retry=true;
				this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
			}

			if(retry)
				try
				{
					xml=_xmlSchema.GetMemGrandParent(ref HierUniqueName , ref MemUniqueName);
				}
				catch(System.Runtime.InteropServices.COMException exc)
				{
					retry=false;
					this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
				}
			
			_xmlSchema.ReturnCurrentConnectionToPool();
			return xml;
		}



		public string GetMemberParent(string Server , string Database, string Cube, string HierUniqueName, string MemUniqueName)
		{
			string connString=this.GetConnectionString(Server , Database);
			_xmlSchema.SetConnectionFromPool(ref connString);
			_xmlSchema.SetCube(ref Cube);
			string xml=null;
			bool retry=false;

			try
			{
				xml=_xmlSchema.GetMemParent(ref HierUniqueName , ref MemUniqueName);
			}
			catch(System.Runtime.InteropServices.COMException exc)
			{
				retry=true;
				this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
			}

			if(retry)
				try
				{
					xml=_xmlSchema.GetMemParent(ref HierUniqueName , ref MemUniqueName);
				}
				catch(System.Runtime.InteropServices.COMException exc)
				{
					retry=false;
					this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
				}
			
			_xmlSchema.ReturnCurrentConnectionToPool();
			return xml;
		}


		public string GetLevelMembers(string Server , string Database, string Cube, string UniqueName)
		{
			string connString=this.GetConnectionString(Server , Database);
			_xmlSchema.SetConnectionFromPool(ref connString);
			_xmlSchema.SetCube(ref Cube);
			string xml=null;
			bool retry=false;

			try
			{
				xml=_xmlSchema.GetLevelMembers(ref UniqueName);
			}
			catch(System.Runtime.InteropServices.COMException exc)
			{
				retry=true;
				this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
			}

			if(retry)
				try
				{
					xml=_xmlSchema.GetLevelMembers(ref UniqueName);
				}
				catch(System.Runtime.InteropServices.COMException exc)
				{
					retry=false;
					this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
				}
			
			_xmlSchema.ReturnCurrentConnectionToPool();
			return xml;
		}


		public string GetReportSchemaXml(string Server , string Database, string Cube, ref string OpenNodesXml)
		{
			string connString=this.GetConnectionString(Server , Database);
			_xmlSchema.SetConnectionFromPool(ref connString);
			_xmlSchema.SetCube(ref Cube);
			string xml=null;
			bool retry=false;

			try
			{
				xml=_xmlSchema.GetReportSchemaXml(ref OpenNodesXml);
			}
			catch(System.Runtime.InteropServices.COMException exc)
			{
				retry=true;
				this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
			}

			if(retry)
				try
				{
					xml=_xmlSchema.GetReportSchemaXml(ref OpenNodesXml);
				}
				catch(System.Runtime.InteropServices.COMException exc)
				{
					retry=false;
					this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
				}
			
			_xmlSchema.ReturnCurrentConnectionToPool();
			return xml;
		}


		public string GetReportXml(string Server , string Database, string Cube, ref string InReportXml)
		{
			string connString=this.GetConnectionString(Server , Database);
			_xmlSchema.SetConnectionFromPool(ref connString);
			_xmlSchema.SetCube(ref Cube);
			string xml=null;
			bool retry=false;

			try
			{
				xml=_xmlSchema.GetReportXml(ref InReportXml);
			}
			catch(System.Runtime.InteropServices.COMException exc)
			{
				retry=true;
				this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
			}

			if(retry)
				try
				{
					xml=_xmlSchema.GetReportXml(ref InReportXml);
				}
				catch(System.Runtime.InteropServices.COMException exc)
				{
					retry=false;
					this.HandleADOMDException(exc , _xmlSchema, ref connString, retry);
				}
			
			_xmlSchema.ReturnCurrentConnectionToPool();
			return xml;
		}







		private string GetConnectionString(string Server , string Database)
		{
			return "Data Source=" + Server + ";Initial Catalog=" + Database + ";Provider=MSOLAP;Locale Identifier=1033;";
		}





		private void HandleADOMDException(System.Runtime.InteropServices.COMException exc, object xmlCstObject, ref string connString, bool retry)
		{
			FI.Common.LogWriter.Instance.WriteEventLogEntry(exc);

			if(xmlCstObject==this._xmlCst)
			{
				if(retry) // reset connection
				{
					_xmlCst.DiscardCurrentConnection();
					_xmlCst.SetConnectionFromPool(ref connString);		
				}
				else 
					_xmlCst.ReturnCurrentConnectionToPool();
			}
			else if(xmlCstObject==this._xmlSchema)
			{
				if(retry) // reset connection
				{
					_xmlSchema.DiscardCurrentConnection();
					_xmlSchema.SetConnectionFromPool(ref connString);		
				}
				else
					_xmlSchema.ReturnCurrentConnectionToPool();		
			}
			else
				throw new Exception("Unknown object");

			
			if(retry)
				FI.Common.LogWriter.Instance.WriteEventLogEntry("Retry follows");
			else
				throw exc;


//			if(exc.Message.ToUpper()=="Item cannot be found in the collection corresponding to the requested name or ordinal.".ToUpper() || 
//				exc.Message.ToUpper().StartsWith("Unspecified Error".ToUpper())) // if cube access error  or unspecified error
//			{
//				_xmlCst.DiscardCurrentConnection();
//				_xmlSchema.DiscardCurrentConnection();
//				
//				Exception newExc=new Exception("Unable to open cube: " + exc.Message );
//				FI.Common.LogWriter.Instance.WriteEventLogEntry(newExc);
//				throw newExc;
//			}
//			else
//			{
//				FI.Common.LogWriter.Instance.WriteEventLogEntry(exc);
//				throw exc;
//			}
		}


		/*
		protected override void Activate()
		{
			// Called when removed from the pool.
		}
		protected override void Deactivate()
		{
			// Called before deactivating or placing back in pool.
		}
		protected override bool CanBePooled()
		{
			// Called after Deactivate. Indicate your vote here.
			return true;
		}

		*/



	}


}
