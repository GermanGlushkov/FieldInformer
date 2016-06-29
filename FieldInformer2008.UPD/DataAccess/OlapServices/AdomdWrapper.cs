using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.Text;
using System.Data;
using Microsoft.AnalysisServices.AdomdClient;


namespace FI.DataAccess.OlapServices
{

	public class AdomdWrapper : MarshalByRefObject , IDisposable
	{
		private AdomdConnection _conn=null;
        private string _connSessionId = null;
        private AdomdCommand _cmd = null;
        private bool _isExecuting;
        private static string __del1 = new string(new char[] { (char)1, (char)8 });
        private static string __del2 = new string(new char[] { (char)2, (char)7 });
        private static string __del3 = new string(new char[] { (char)3, (char)6 });
        private static string __del4 = new string(new char[] { (char)4, (char)5 });


		private AdomdConnection GetConnection(string server , string database)
		{
			if(_conn==null || _conn.State!=ConnectionState.Open)
			{
                try
                {
                    // try to continue session
                    _conn = new AdomdConnection(this.GetConnectionString(server, database));                    
                    _conn.SessionID = _connSessionId;
                    _conn.Open();
                }
                catch (Exception exc)
                {
                    // create as new session                        
                    _conn = new AdomdConnection(this.GetConnectionString(server, database));
                    _conn.SessionID = null;
                    _conn.Open();
                }                    
			}
            _connSessionId = _conn.SessionID;
			return _conn;
		}

		private void ReturnConnection(bool closeSession)
		{
            if (_conn != null && !(_conn.State == ConnectionState.Broken || _conn.State == ConnectionState.Closed))
            {
                _conn.Close(closeSession);
                _conn = null;
                if (closeSession)
                    _connSessionId = null;
            }
		}

        private CellSet ExecuteCellset(string commandText)
        {
            if (_cmd != null)
                throw new Exception("Previous command not disposed");

            CellSet ret = null;
            try
            {
                _isExecuting = true;
                _cmd = _conn.CreateCommand();
                _cmd.CommandText = commandText;
                _cmd.CommandTimeout = Common.AppConfig.DA_MdxCommandTimeout;
                ret = _cmd.ExecuteCellSet();
            }
            finally
            {
                _isExecuting = false;
            }

            return ret;
        }

        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
        }

        private void DisposeCommand()
        {
            if (_cmd != null)
            {
                lock (this)
                {
                    if (_cmd != null)
                    {
                        if(this.IsExecuting)
                            _cmd.Cancel();

                        ReturnConnection(true);

                        _cmd.Dispose();
                        _cmd = null;
                    }
                }
            }
        }


        public void CancelCommand()
        {
            DisposeCommand();
        }

        public void Dispose()
        {
            DisposeCommand();
            ReturnConnection(true);
        }

		public string BuildCellset(string server , string database, string mdx )
		{
			AdomdConnection conn=null;
			CellSet cst=null;

			try
			{
				// create connection
				conn=this.GetConnection(server, database);
                AdomdCommand cmd;


				// open cellset
                cst = this.ExecuteCellset(mdx);                

				// build xml
				StringBuilder sb=new StringBuilder();

				int ax0PosCount=cst.Axes[0].Positions.Count;
				int ax0MemCount=cst.Axes[0].Set.Hierarchies.Count;
				int ax1PosCount=cst.Axes[1].Positions.Count;
				int ax1MemCount=cst.Axes[1].Set.Hierarchies.Count;

				// header
				sb.Append(ax0PosCount);
				sb.Append(__del2);
				sb.Append(ax0MemCount);
				sb.Append(__del4);
				sb.Append(ax1PosCount);
				sb.Append(__del2);
				sb.Append(ax1MemCount);
				sb.Append(__del4);

				// axis 0
				for(int i=0;i<ax0PosCount;i++)
				{
					for(int j=0;j<ax0MemCount;j++)
					{
						Member mem=cst.Axes[0].Positions[i].Members[j];

						sb.Append(mem.UniqueName);
						sb.Append(__del1);
						sb.Append(mem.Caption);
						sb.Append(__del1);
						sb.Append(mem.ChildCount);
						sb.Append(__del1);
						sb.Append(mem.LevelDepth);						
						
						if(j<ax0MemCount-1)
							sb.Append(__del2);
					}				

					if(i<ax0PosCount-1)
						sb.Append(__del3);
				}
				sb.Append(__del4);

				// axis 1
				for(int i=0;i<ax1PosCount;i++)
				{
					for(int j=0;j<ax1MemCount;j++)
					{
						Member mem=cst.Axes[1].Positions[i].Members[j];

						sb.Append(mem.UniqueName);
						sb.Append(__del1);
						sb.Append(mem.Caption);
						sb.Append(__del1);
						sb.Append(mem.ChildCount);
						sb.Append(__del1);
						sb.Append(mem.LevelDepth);						
						
						if(j<ax1MemCount-1)
							sb.Append(__del2);
					}				

					if(i<ax1PosCount-1)
						sb.Append(__del3);
				}
				sb.Append(__del4);

				// cells
				for(int i=0; i<ax0PosCount; i++)
				{
					for(int j=0; j<ax1PosCount; j++)
					{
						Cell cell=cst.Cells[i,j];

						sb.Append(this.GetCellValue(cell));
						sb.Append(__del1);
						sb.Append(this.GetCellFormattedValue(cell));						

						if(j<ax1PosCount-1)
							sb.Append(__del2);
					}

					if(i<ax0PosCount-1)
						sb.Append(__del3);
				}


				return sb.ToString();
			}
			catch(Exception exc)
			{
				throw new Exception(exc.Message);
			}
			finally
            {
                // dispose command               
                DisposeCommand();

				// dispose resources
				this.ReturnConnection(false);
			}
		}

		private string GetCellValue(Cell cell)
		{
			try
			{
				return (cell.Value==null ? string.Empty : Convert.ToString(cell.Value, System.Globalization.CultureInfo.InvariantCulture));
			}
			catch(Exception exc)
			{
				return "#Err";
			}
		}

		private string GetCellFormattedValue(Cell cell)
		{
			try
			{
				return (cell.FormattedValue==null ? string.Empty : cell.FormattedValue);
			}
			catch(Exception exc)
			{
				return "#Err";
			}
		}

		public string GetSchemaMembers(string server , string database, string cube, string[] uniqueNames)
		{
			AdomdConnection conn=null;

			try
			{
				// create connection				
				conn=this.GetConnection(server, database);

				// check if cube exists
				CubeDef cubeDef=null;
				try
				{
					cubeDef=conn.Cubes[cube];
				}
				catch(Exception exc)
				{
					throw new Exception(string.Format("Cube '{0}.{1}' does not exist or is not processed", database, cube));
				}

				// create xml document
				XmlDocument doc=new XmlDocument();
				XmlElement rootEl=doc.CreateElement("OlapMemSet");
				rootEl.SetAttribute( "xmlns", "http://tempuri.org/OlapMemSet.xsd");
				doc.AppendChild(rootEl);

				// lookup unique names
				if(uniqueNames!=null)
					foreach(string uniqueName in uniqueNames)
					{
						if(uniqueName==null)
							continue;
						
						Member m=(Member)this.GetSchemaObject(cubeDef, SchemaObjectType.ObjectTypeMember, uniqueName, false);	
						if(m!=null)
							AppendMemberElement(rootEl, m);
					}

				// return
				return doc.OuterXml;
			}				
			finally
			{
				// dispose resources
                this.ReturnConnection(false);
			}
		}


		public string GetMemberChildren(string server , string database, string cube, string uniqueName , bool ifLeafAddItself)
		{
			AdomdConnection conn=null;

			try
			{
				// create connection
				conn=this.GetConnection(server, database);

				// create xml document
				XmlDocument doc=new XmlDocument();
				XmlElement rootEl=doc.CreateElement("SCHEMA");
				doc.AppendChild(rootEl);

				// get objects
				CubeDef cubeDef=conn.Cubes[cube];
				Member mem=(Member)this.GetSchemaObject(cubeDef, SchemaObjectType.ObjectTypeMember, uniqueName, true);

				// write members	
				if(mem.ChildCount>0)
				{
					MemberCollection mems=mem.GetChildren();
					foreach(Member childMem in mems)			
						AppendMemberElement(rootEl, childMem);
				}
				else
				{
					if(ifLeafAddItself)									
						AppendMemberElement(rootEl, mem);
				}

				// return
				return doc.OuterXml;
			}				
			finally
			{
				// dispose resources
                this.ReturnConnection(false);
			}
		}


		public string GetMemberParentWithSiblings(string server , string database, string cube, string hierUniqueName, string memUniqueName)
		{
			AdomdConnection conn=null;

			try
			{
				// create connection
				conn=this.GetConnection(server, database);

				// create xml document
				XmlDocument doc=new XmlDocument();
				XmlElement rootEl=doc.CreateElement("SCHEMA");
				doc.AppendChild(rootEl);

				// get objects
				CubeDef cubeDef=conn.Cubes[cube];				
				Member mem=(Member)this.GetSchemaObject(cubeDef, SchemaObjectType.ObjectTypeMember, memUniqueName, true);

				// write members	
				if(mem.LevelDepth==0)				
					AppendMemberElement(rootEl, mem);				
				else if(mem.LevelDepth==1)
				{
					Hierarchy hier=(Hierarchy)this.GetSchemaObject(cubeDef, SchemaObjectType.ObjectTypeHierarchy, hierUniqueName, true);
					MemberCollection mems=hier.Levels[0].GetMembers();					
					foreach(Member childMem in mems)			
						AppendMemberElement(rootEl, childMem);
				}
				else
				{
					Member memGrandPa=mem.Parent.Parent;
					MemberCollection mems=memGrandPa.GetChildren();
					foreach(Member childMem in mems)						
						AppendMemberElement(rootEl, childMem);
				}

				// return
				return doc.OuterXml;
			}				
			finally
			{
				// dispose resources
                this.ReturnConnection(false);
			}
		}


		public string GetMemberGrandParent(string server , string database, string cube, string hierUniqueName, string memUniqueName)
		{
			AdomdConnection conn=null;

			try
			{
				// create connection
				conn=this.GetConnection(server, database);

				// create xml document
				XmlDocument doc=new XmlDocument();
				XmlElement rootEl=doc.CreateElement("SCHEMA");
				doc.AppendChild(rootEl);

				// get objects
				CubeDef cubeDef=conn.Cubes[cube];				
				Member mem=(Member)this.GetSchemaObject(cubeDef, SchemaObjectType.ObjectTypeMember, memUniqueName, true);

				// write members	
				if(mem.LevelDepth>1)
				{
					Member memGrandPa=mem.Parent.Parent;
					AppendMemberElement(rootEl, memGrandPa);
				}

				// return
				return doc.OuterXml;
			}				
			finally
			{
				// dispose resources
                this.ReturnConnection(false);
			}
		}



		public string GetMemberParent(string server , string database, string cube, string hierUniqueName, string memUniqueName)
		{
			AdomdConnection conn=null;

			try
			{
				// create connection				
				conn=this.GetConnection(server, database);

				// create xml document
				XmlDocument doc=new XmlDocument();
				XmlElement rootEl=doc.CreateElement("SCHEMA");
				doc.AppendChild(rootEl);

				// get objects
				CubeDef cubeDef=conn.Cubes[cube];				
				Member mem=(Member)this.GetSchemaObject(cubeDef, SchemaObjectType.ObjectTypeMember, memUniqueName, true);

				// write members	
				if(mem.LevelDepth>0)
				{
					Member memPar=mem.Parent;
					AppendMemberElement(rootEl, memPar);
				}

				// return
				return doc.OuterXml;
			}				
			finally
			{
				// dispose resources
                this.ReturnConnection(false);
			}
		}


		public string GetLevelMembers(string server , string database, string cube, string uniqueName)
		{
			AdomdConnection conn=null;

			try
			{
				// create connection
				conn=this.GetConnection(server, database);

				// create xml document
				XmlDocument doc=new XmlDocument();
				XmlElement rootEl=doc.CreateElement("SCHEMA");
				doc.AppendChild(rootEl);

				// get objects
				CubeDef cubeDef=conn.Cubes[cube];
				Level l=(Level)this.GetSchemaObject(cubeDef, SchemaObjectType.ObjectTypeLevel, uniqueName, true);
				MemberCollection mems=l.GetMembers();

				// write members				
				foreach(Member m in mems)
					AppendMemberElement(rootEl, m);

				// return
				return doc.OuterXml;
			}				
			finally
			{
				// dispose resources
                this.ReturnConnection(false);
			}
		}


		public string GetReportSchemaXml(string server , string database, string cube, ref string openNodesXml)
		{
			AdomdConnection conn=null;

			try
			{
				// read openNodesXml
				StringCollection openNodes=new StringCollection();
				if(openNodesXml!=null && openNodesXml!="")
				{
					XmlDocument openNodesDoc=new XmlDocument();
					openNodesDoc.LoadXml(openNodesXml);
					foreach(XmlElement el in openNodesDoc.GetElementsByTagName("ON"))			
					{
						string s=el.GetAttribute("UN");
						if(s!=null && s!="" && !openNodes.Contains(s))
							openNodes.Add(s);
					}
				}

				// create connection
				conn=this.GetConnection(server, database);

				// create xml document
				XmlDocument doc=new XmlDocument();
				XmlElement rootEl=doc.CreateElement("SCHEMA");
				doc.AppendChild(rootEl);

				// get cube
				CubeDef cubeDef=conn.Cubes[cube];

				// dimensions
				foreach(Dimension dim in cubeDef.Dimensions)
				{
					XmlElement dimEl=doc.CreateElement("D");
					dimEl.SetAttribute("UN", dim.UniqueName);
					dimEl.SetAttribute("N", dim.Name);
					rootEl.AppendChild(dimEl);
					
					// isopen attribute
					if(openNodes.Contains(dim.UniqueName))
						dimEl.SetAttribute("O", "1");

					// hierarchies
					foreach(Hierarchy hier in dim.Hierarchies)
					{
						XmlElement hierEl=doc.CreateElement("H");
						hierEl.SetAttribute("UN", hier.UniqueName);
                        hierEl.SetAttribute("N", hier.Name);
                        hierEl.SetAttribute("F", hier.DisplayFolder);
						dimEl.AppendChild(hierEl);

						// levels
						foreach(Level lev in hier.Levels)
						{
							XmlElement levEl=doc.CreateElement("L");
							levEl.SetAttribute("UN", lev.UniqueName);
                            levEl.SetAttribute("N", lev.Name);
							levEl.SetAttribute("LD", lev.LevelNumber.ToString());
							hierEl.AppendChild(levEl);
							
							// default member under highest level
							if(lev.LevelNumber==0)
							{
								// get default member by explicit property or as simply first member in level
								Member defMem=null;
								if(hier.DefaultMember!=null && hier.DefaultMember!="")																	
									defMem=(Member)this.GetSchemaObject(cubeDef, SchemaObjectType.ObjectTypeMember, hier.DefaultMember, false);								
								
								if(defMem==null && lev.MemberCount>0)
									defMem=lev.GetMembers(0,1)[0];

								if(defMem!=null)
								{
									XmlElement defMemEl=doc.CreateElement("DM");
									defMemEl.SetAttribute("UN", defMem.UniqueName);
									defMemEl.SetAttribute("N", defMem.Name);
									defMemEl.SetAttribute("CC", defMem.ChildCount.ToString());
									defMemEl.SetAttribute("LD", defMem.LevelDepth.ToString());
									levEl.AppendChild(defMemEl);
								}
							}
						}

                        
                        // hier isopen attibute
                        if (openNodes.Contains(hier.UniqueName))
                            hierEl.SetAttribute("O", "1");

                        // measures handled differently, always included regardless of isopen attribute
                        if (dim.DimensionType == DimensionTypeEnum.Measure)
                        {
                            MeasureCollection mems = cubeDef.Measures;
                            foreach (Measure m in mems)
                                AppendMeasure(hierEl, m, openNodes);                            
                        }
                        else // other
                        {
                            // only if hier is open
                            if (openNodes.Contains(hier.UniqueName))
                            {
                                // members recursive
                                MemberCollection mems = hier.Levels[0].GetMembers();
                                foreach (Member m in mems)
                                    AppendMemberHierarchy(hierEl, m, openNodes);
                            }                        
                        }                        
					}
				}

				// return
				return doc.OuterXml;
			}				
			finally
			{
				// dispose resources
                this.ReturnConnection(false);
			}
		}


		public string ValidateReportXml(string server , string database, string cube, ref string inReportXml)
		{
			AdomdConnection conn=null;
			string uName=null;
			object obj=null;
            Hierarchy hier;

			try
			{
				// read inReportXml
				XmlDocument doc=new XmlDocument();
				doc.LoadXml(inReportXml);

				// create connection				
				conn=this.GetConnection(server, database);

				// get cube
				CubeDef cubeDef=conn.Cubes[cube];

				// hierarchies
				foreach(XmlElement hierEl in doc.GetElementsByTagName("H"))
				{
                    bool checkHier = false;
					uName=hierEl.GetAttribute("UN");
					obj=this.GetSchemaObject(cubeDef, SchemaObjectType.ObjectTypeHierarchy, uName, false);
                    hier = obj as Hierarchy;

                    if (hier == null)
					{
						hierEl.SetAttribute("E", "1");
						continue; // next hierarchy
					}

					// levels
					foreach(XmlElement levEl in hierEl.GetElementsByTagName("L"))
					{
						uName=levEl.GetAttribute("UN");
                        checkHier = (levEl.GetAttribute("CH") == "1");

						obj=this.GetSchemaObject(cubeDef, SchemaObjectType.ObjectTypeLevel, uName, false);
                        Level lev = (Level)obj;

                        if (lev == null || (checkHier && string.Compare(lev.ParentHierarchy.UniqueName,hier.UniqueName,true)!=0))						
							levEl.SetAttribute("E", "1");						
					}

					// members
					foreach(XmlElement memEl in hierEl.GetElementsByTagName("M"))
					{
                        uName = memEl.GetAttribute("UN");
                        checkHier = (memEl.GetAttribute("CH") == "1");
						bool isCalc=(memEl.GetAttribute("C")=="1");

						// not checking calulated members
                        if (!isCalc)
						{
                            obj = this.GetSchemaObject(cubeDef, SchemaObjectType.ObjectTypeMember, uName, false);
                            Member mem = (Member)obj;

                            if (mem == null || (checkHier && string.Compare(mem.ParentLevel.ParentHierarchy.UniqueName,hier.UniqueName,true)!=0))						
								memEl.SetAttribute("E", "1");
							else
							{
								memEl.SetAttribute("N", mem.Name);
								memEl.SetAttribute("CC", mem.ChildCount.ToString());
								memEl.SetAttribute("LD", mem.LevelDepth.ToString());
							}
						}						
					}

					// order tuple members
					foreach(XmlElement otmEl in hierEl.GetElementsByTagName("OTM"))
					{
                        uName = otmEl.GetAttribute("UN");
                        checkHier = (otmEl.GetAttribute("CH") == "1");
						obj=this.GetSchemaObject(cubeDef, SchemaObjectType.ObjectTypeMember, uName, false);
                        Member mem = (Member)obj;

                        if (mem == null || (checkHier && string.Compare(mem.ParentLevel.ParentHierarchy.UniqueName,hier.UniqueName)!=0))						
							otmEl.SetAttribute("E", "1");
					}
				}

				// return
				return doc.OuterXml;
			}				
			finally
			{
				// dispose resources
                this.ReturnConnection(false);
			}
		}


        public DateTime GetCubeLastProcessed(string server, string database, string cube)
        {
            AdomdConnection conn = null;

            try
            {
                // create connection
                conn = this.GetConnection(server, database);

                // get objects
                CubeDef cubeDef = conn.Cubes[cube];
                return cubeDef.LastProcessed;
            }
            finally
            {
                // dispose resources
                this.ReturnConnection(false);
            }
        }

		private object GetSchemaObject(CubeDef cube, SchemaObjectType type, string uniqueName, bool throwNotFound)
		{
			try
			{
				return (cube==null ? null : cube.GetSchemaObject(type, uniqueName, true));
			}
			catch(Exception exc)
			{
                // object not found from 9.0 and 8.0 analysis services connections
                if (exc.Message.Contains("object was not found") || exc.Message.StartsWith("Invalid syntax for schema"))
                {
                    if (throwNotFound)
                        throw new Exception(string.Format("Member '{0}' not found in cube '{1}'", uniqueName, cube.Name));

                    return null;
                }

                throw exc; // other exception
			}
		}


		private void AppendMemberElement(XmlElement parent, Member mem)
		{
			XmlElement el=parent.OwnerDocument.CreateElement("M");
			el.SetAttribute("UN", mem.UniqueName);
			el.SetAttribute("N", mem.Name);
			el.SetAttribute("CC", mem.ChildCount.ToString());
			el.SetAttribute("LD", mem.LevelDepth.ToString());
			parent.AppendChild(el);
		}

        private void AppendMeasure(XmlElement hierEl, Measure mea, StringCollection openNodes)
        {
            // measure
            XmlElement el = hierEl.OwnerDocument.CreateElement("M");
            el.SetAttribute("UN", mea.UniqueName);
            el.SetAttribute("N", mea.Name);
            el.SetAttribute("CC", "0"); // child count
            el.SetAttribute("LD", "0"); // level depth

            // if display folder exists
            if (mea.DisplayFolder != null && mea.DisplayFolder != "")
            {
                // find or create folder element
                string uniqueName="[Measures].[" + mea.DisplayFolder + "]";
                XmlElement folderEl = (XmlElement)hierEl.SelectSingleNode("M[@PH=\"1\"][@UN=\"" + uniqueName + "\"]");
                if (folderEl == null)
                {
                    folderEl = hierEl.OwnerDocument.CreateElement("M");
                    folderEl.SetAttribute("UN", uniqueName);
                    folderEl.SetAttribute("N", mea.DisplayFolder);
                    folderEl.SetAttribute("CC", "0");
                    folderEl.SetAttribute("LD", "0");

                    // placeholder attribute, cannot be selected
                    folderEl.SetAttribute("PH", "1"); 

                    // isopen attribute
                    if (openNodes.Contains(uniqueName))
                        folderEl.SetAttribute("O", "1");

                    // iterate through hier children and try to insert in alphabetical order
                    for(int i=0;i<hierEl.ChildNodes.Count;i++)
                    {
                        string folderName=((XmlElement)hierEl.ChildNodes[i]).GetAttribute("N");
                        if (string.Compare(mea.DisplayFolder, folderName) < 0)
                        {
                            hierEl.InsertBefore(folderEl, hierEl.ChildNodes[i]);
                            break;
                        }
                    }
                    // append if not inserted
                    if(folderEl.ParentNode==null)
                        hierEl.AppendChild(folderEl);
                }


                // append measure
                el.SetAttribute("LD", "1"); // level depth
                folderEl.AppendChild(el);

                // update folder child count attribute
                folderEl.SetAttribute("CC", folderEl.ChildNodes.Count.ToString());
            }
            else            
                hierEl.AppendChild(el);            
        }

		private void AppendMemberHierarchy(XmlElement parent, Member mem, StringCollection openNodes)
		{
			// member
			XmlElement el=parent.OwnerDocument.CreateElement("M");
			el.SetAttribute("UN", mem.UniqueName);
			el.SetAttribute("N", mem.Name);
			el.SetAttribute("CC", mem.ChildCount.ToString());
			el.SetAttribute("LD", mem.LevelDepth.ToString());
			parent.AppendChild(el);

			// children
			if(mem.ChildCount>0 && openNodes!=null && openNodes.Contains(mem.UniqueName))
			{
				// isopen attribute
				el.SetAttribute("O", "1");

				MemberCollection mems=mem.GetChildren();
				foreach(Member childMem in mems)
					AppendMemberHierarchy(el, childMem, openNodes);
			}
		}



		private string GetConnectionString(string server , string database)
		{
            return string.Format("Data Source={0};Initial Catalog={1};Timeout={2};",
                server, database, Common.AppConfig.DA_MdxCommandTimeout.ToString());
            //return "Data Source=" + server + ";Initial Catalog=" + database + ";"; // Timeout = 14400; "; //;Provider=MSOLAP;ConnectTo=8.0;Locale Identifier=1033;";
		}





	}
}
