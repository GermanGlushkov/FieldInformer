using System;
using System.Xml;

namespace FI.DataAccess.Serviced
{

	//
	// designed following Singleton Pattern
	//


	public class OlapConnPool : MarshalByRefObject
	{
		private int _counter=0;
		private System.Collections.Hashtable _poolObject = new System.Collections.Hashtable();
		private System.Collections.Hashtable _poolConnString = new System.Collections.Hashtable();
		private System.Collections.Hashtable _poolOleDbConnString = new System.Collections.Hashtable();
		private System.Collections.Hashtable _poolBusy = new System.Collections.Hashtable();


		//in order to live forever
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// singleton pattern
		private OlapConnPool(){}
		public static readonly OlapConnPool Instance=new OlapConnPool();
		// singleton pattern



		XmlDocument _measuresHierarchyDoc=null;

		// in singleton , because need to keep XmlDocument open, not load it every time
		public string GetMeasuresHierarchyXml(string Server , string Database, string Cube)
		{
			if(FI.Common.AppConfig.DA_MeasuresHierarchyConfig=="")
				return null;

			//load fist time
			if(_measuresHierarchyDoc==null)
			{
				_measuresHierarchyDoc=new XmlDocument();
				_measuresHierarchyDoc.Load(FI.Common.AppConfig.DA_MeasuresHierarchyConfig);
			}

			// get specified measures hier
			foreach(XmlElement measureHierEl in _measuresHierarchyDoc.GetElementsByTagName("MEASURES_HIERARCHY"))
			{
				foreach(XmlElement schemaEl in measureHierEl.GetElementsByTagName("SCHEMA"))
				{
					string schemaServer=schemaEl.GetAttribute("SERVER").ToUpper();
					string schemaDb=schemaEl.GetAttribute("DATABASE").ToUpper();
					string schemaCube=schemaEl.GetAttribute("CUBE").ToUpper();

					Server=Server.ToUpper();
					Database=Database.ToUpper();
					Cube=Cube.ToUpper();

					if(schemaServer==Server)
						if(schemaDb==Database || schemaDb=="*" || ( schemaDb.EndsWith("*") && Database.StartsWith(schemaDb.Substring(0, schemaDb.Length-1))))
							if(schemaCube==Cube || schemaCube=="*" || ( schemaCube.EndsWith("*") && Cube.StartsWith(schemaCube.Substring(0, schemaCube.Length-1))))
							{
								XmlElement hierEl=(XmlElement)measureHierEl.GetElementsByTagName("H")[0];
								return hierEl.OuterXml;
							}
				}
			}

			return null;
		}



		/*
		public ADODB.Connection GetConnection(string strConnect)
		{
			string newKey ;
			ADODB.ConnectionClass conn = null;

			lock(this)
			{
				System.Collections.IDictionaryEnumerator enu = _poolObject.GetEnumerator();
				while(enu.MoveNext())
				{
					if((bool)_poolBusy[enu.Key]==false&&(string)_poolConnString[enu.Key]==strConnect)
					{
						conn = (ADODB.ConnectionClass)enu.Entry.Value;

						if (IsConnectionValid(conn)==false || conn.State != (int)ADODB.ObjectStateEnum.adStateOpen)
						{
							conn = new ADODB.ConnectionClass();
							conn.Open(strConnect , "", "" , -1);
						}

						_poolBusy[enu.Key] = true;
						break;
					}
					else conn = null;
				}

				if(conn==null)
				{
					conn = new ADODB.ConnectionClass();
					conn.Open(strConnect , "", "" , -1);

					newKey=(++_counter).ToString();
					_poolObject.Add(newKey ,conn);
					_poolConnString.Add(newKey ,strConnect);
					_poolOleDbConnString.Add(newKey ,conn.ConnectionString );
					_poolBusy.Add(newKey ,true);
				}
			} //lock

			return conn;		
		}


		public void ReturnConnection(ADODB.Connection conn)
		{
			if(conn==null)
				return;

			lock(this)
			{

				System.Collections.IDictionaryEnumerator enu = _poolObject.GetEnumerator();
				while(enu.MoveNext())
				{
					if ( conn.Equals(enu.Value) )
					{
						_poolBusy[enu.Key] = false;
						break;
					}
				}

				//shrink pool 
				Shrink();

			} //lock

		}


		public void RemoveConnection(ADODB.Connection conn)
		{
			if(conn==null)
				return;

			System.Collections.ArrayList keysToDelete=new System.Collections.ArrayList();

			lock(this)
			{

				System.Collections.IDictionaryEnumerator enu = _poolObject.GetEnumerator();
				while(enu.MoveNext())
				{
					if ( conn.Equals(enu.Value) )
					{
						keysToDelete.Add(enu.Key);
						break;
					}
				}

				//remove connection
				Cleanup(keysToDelete);

			} //lock

		}


		public void Shrink()
		{
			System.Collections.ArrayList keysToDelete=new System.Collections.ArrayList();

			lock(this)
			{

				foreach (string enuKey in _poolConnString.Keys)
				{
					if ((bool)_poolBusy[enuKey]==false  &&  !(keysToDelete.Contains(enuKey))  )
						foreach (string enuKey2 in _poolConnString.Keys)
						{
							if (enuKey2.CompareTo(enuKey)!=0 
								&& (bool)_poolBusy[enuKey2]==false 
								&& !(keysToDelete.Contains(enuKey2))
								&& ((string)_poolConnString[enuKey]).CompareTo(_poolConnString[enuKey2])==0)
							{
								keysToDelete.Add(enuKey2);
							}
						}
				}

				Cleanup(keysToDelete);

			} //lock

		}

		public int PoolSize
		{
			get {return _poolObject.Count;}
		}



		



		public ADODB.Recordset GetSchemaRecordset(string Database, ADODB.Connection conn)
		{
			const int MDTREEOP_CHILDREN = 1;            // Returns only the immediate children.
			const int MDTREEOP_SIBLINGS = 2;              // Returns members on the same level.
			const int MDTREEOP_PARENT = 4;                // Returns only the immediate parent.
			const int MDTREEOP_SELF = 8;                     // Returns itself in the list of returned rows.
			const int MDTREEOP_DESCENDANTS = 16; // Returns all the descendants.
			const int MDTREEOP_ANCESTORS = 32;     // Returns all the ancestors.

			object objRst=null;
			object[]   args =
						{
							null,                               //CATALOG_NAME
							null,                               //SCHEMA_NAME
							"VIRTUAL",                      //CUBE_NAME
							null,                               //DIMENSION_UNIQUE_NAME
							null,                               //HIERARCHY_UNIQUE_NAME
							null,                               //LEVEL_UNIQUE_NAME
							null,                               //LEVEL_NUMBER
							null,                               //MEMBER_NAME
							null,								//MEMBER_UNIQUE_NAME
							null,                               //MEMBER_CAPTION
							null,                               //MEMBER_TYPE
							MDTREEOP_SELF //MDTREEOP
						};


			objRst=conn.GetType().InvokeMember(
				"OpenSchema" , 
				System.Reflection.BindingFlags.InvokeMethod,
				null,
				conn,
				new object[] {ADODB.SchemaEnum.adSchemaMembers , args}
				);

			return (ADODB.Recordset)objRst;
		}


		private bool IsConnectionValid(ADODB.Connection conn)
		{
			bool _isValid=false;
			try
			{
				_isValid=true;
			}
			catch(Exception exc)
			{
				// do nothing, connection is invalid
				_isValid=false;
			}
			return _isValid;
		}

		private void Cleanup(System.Collections.ArrayList keysToDelete)
		{
			System.Collections.IEnumerator enu = keysToDelete.GetEnumerator();	
			while(enu.MoveNext())
			{
				if (((ADODB.ConnectionClass)_poolObject[enu.Current]).State==(int)ADODB.ObjectStateEnum.adStateOpen )
					((ADODB.ConnectionClass)_poolObject[enu.Current]).Close();
				_poolObject.Remove(enu.Current);
				_poolConnString.Remove(enu.Current);
				_poolOleDbConnString.Remove(enu.Current);
				_poolBusy.Remove(enu.Current);
			}

		}
		*/







	}
}
