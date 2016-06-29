using System;
using FI.BusinessObjects;
using FI.BusinessObjects.Olap;
using FI.BusinessObjects.Olap.CalculatedMemberTemplates;
using System.Collections;

namespace FI.UI.Web.OlapReport
{
	/// <summary>
	/// Summary description for Controller.
	/// </summary>
	public class Controller
	{
		private FI.BusinessObjects.OlapReport _report=null;
		private System.Web.UI.Page _page=null;

		public Controller(FI.BusinessObjects.OlapReport Report , System.Web.UI.Page Page)
		{
			if(Report==null)
				throw new NullReferenceException();

			_report=Report;
			_page=Page;
		}


		// -----------------------------------------------------------------------------------------------
		#region Tabview methods

		public void Transfer(string Location)
		{
			_page.Response.Redirect(Location,true);
		}

		#endregion
		// -----------------------------------------------------------------------------------------------


		// -----------------------------------------------------------------------------------------------
		#region OlapReport methods

		public void BeginExecute()
		{
			_report.BeginExecute();
			_report.ExecuteWaitHandle.WaitOne(30000 , false); //wait 30 secs
		}

		public void CancelExecute()
		{
			_report.CancelExecute();
		}

		public void Undo()
		{
			if(_report.UndoStateCount>0)
				_report.Undo();
		}


		public void Redo()
		{
			if(_report.RedoStateCount>0)
				_report.Redo();
		}


		public void SetGraph(string GraphType , string GraphTheme, short GraphWidth, short GraphHeight, 
			bool ShowValues, bool ShowSeries, bool ShowCat ,bool SetScal, bool RotateAxes)
		{
			FI.BusinessObjects.OlapReport.GraphTypeEnum graphType=(FI.BusinessObjects.OlapReport.GraphTypeEnum)Enum.Parse(typeof(FI.BusinessObjects.OlapReport.GraphTypeEnum) , GraphType , true);
			_report.GraphType=graphType;

			_report.GraphTheme=GraphTheme;
			_report.GraphWidth=GraphWidth;
			_report.GraphHeight=GraphHeight;

			FI.BusinessObjects.OlapReport.GraphOptionsEnum graphOptions=FI.BusinessObjects.OlapReport.GraphOptionsEnum.None;
			if(ShowValues)
				graphOptions=(graphOptions | FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowValues);
			if(ShowSeries)
				graphOptions=(graphOptions | FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowSeries);
			if(ShowCat)
				graphOptions=(graphOptions | FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowCategories);
			if(SetScal)
				graphOptions=(graphOptions | FI.BusinessObjects.OlapReport.GraphOptionsEnum.SetScaling);
			if(RotateAxes)
				graphOptions=(graphOptions | FI.BusinessObjects.OlapReport.GraphOptionsEnum.RotateAxes);
			_report.GraphOptions=graphOptions;

			_report.SaveHeader();
		}

		#endregion
		// -----------------------------------------------------------------------------------------------


		// -----------------------------------------------------------------------------------------------
		#region Axis methods

		public void Pivot()
		{
			_report.Pivot();
		}

		public void SetEmptyTupleOption(Axis.EmptyTupleOptionEnum Axis0Option , Axis.EmptyTupleOptionEnum Axis1Option)
		{
			if(Axis0Option!=Axis.EmptyTupleOptionEnum.HIDE_NONEMPTY) // first must be set value other than HIDE_NONEMPTY , because both axes cannot have HIDE_NONEMPTY
			{
				_report.Axes[0].EmptyTupleOption=Axis0Option;
				_report.Axes[1].EmptyTupleOption=Axis1Option;
			}
			else
			{
				_report.Axes[1].EmptyTupleOption=Axis1Option;
				_report.Axes[0].EmptyTupleOption=Axis0Option;
			}
		}

		public void SetSort(System.Collections.Specialized.StringCollection Identifiers)
		{
			
			bool axis0done=false, axis1done=false;
			bool axis0tupleChanged=false, axis1tupleChanged=false;
			System.Collections.Specialized.StringCollection newAxis0SortMems=new System.Collections.Specialized.StringCollection();
			System.Collections.Specialized.StringCollection newAxis1SortMems=new System.Collections.Specialized.StringCollection();

			// remove all sorts if no members selected
			if(Identifiers.Count==0)
			{
				_report.Axes[0].Order=FI.BusinessObjects.Olap.Axis.OrderEnum.NONE;
				_report.Axes[1].Order=FI.BusinessObjects.Olap.Axis.OrderEnum.NONE;
				return;
			}

			for(int i=0;i<Identifiers.Count;i++)
			{
				short axisOrdinal=-1;
				int pos=-1;
				int mpos=-1;
				this.CellsetPositionFromIdentifier(Identifiers[i] , ref axisOrdinal , ref pos, ref mpos);

				//assign order from all members of selected tuple
				if(axisOrdinal==0)
				{
					if(axis0done)
						continue; //cannot have multiple tuples in order

					for(int j=0;j<_report.Cellset.Axis0TupleMemCount;j++)
						newAxis0SortMems.Add(_report.Cellset.GetCellsetMember(0 , j , pos).UniqueName);

					axis0done=true;
				}
				else
				{
					if(axis1done)
						continue; //cannot have multiple tuples in order

					for(int j=0;j<_report.Cellset.Axis1TupleMemCount;j++)
						newAxis1SortMems.Add(_report.Cellset.GetCellsetMember(1 , j , pos).UniqueName);
					
					axis1done=true;
				}
			}
			

			// axis 0
			if(axis0done)
			{
				for(int j=0;j<_report.Axes[0].Hierarchies.Count;j++)
				{
					Hierarchy hier=_report.Axes[0].Hierarchies[j];
					string sortUN=newAxis0SortMems[j];
					if(hier.OrderTupleMember!=sortUN)
					{
						hier.OrderTupleMember=sortUN;
						axis0tupleChanged=true;
					}
				}
				if(axis0tupleChanged)
					_report.Axes[0].Order=FI.BusinessObjects.Olap.Axis.OrderEnum.BDESC;
				else if(_report.Axes[0].Order==FI.BusinessObjects.Olap.Axis.OrderEnum.NONE)
					_report.Axes[0].Order=FI.BusinessObjects.Olap.Axis.OrderEnum.BDESC;
				else if(_report.Axes[0].Order==FI.BusinessObjects.Olap.Axis.OrderEnum.BASC)
					_report.Axes[0].Order=FI.BusinessObjects.Olap.Axis.OrderEnum.BDESC;
				else if(_report.Axes[0].Order==FI.BusinessObjects.Olap.Axis.OrderEnum.BDESC)
					_report.Axes[0].Order=FI.BusinessObjects.Olap.Axis.OrderEnum.BASC;
			}



			// axis 1
			if(axis1done)
			{
				for(int j=0;j<_report.Axes[1].Hierarchies.Count;j++)
				{
					Hierarchy hier=_report.Axes[1].Hierarchies[j];
					string sortUN=newAxis1SortMems[j];
					if(hier.OrderTupleMember!=sortUN)
					{
						hier.OrderTupleMember=sortUN;
						axis1tupleChanged=true;
					}
				}
				if(axis1tupleChanged)
					_report.Axes[1].Order=FI.BusinessObjects.Olap.Axis.OrderEnum.BDESC;
				else if(_report.Axes[1].Order==FI.BusinessObjects.Olap.Axis.OrderEnum.NONE)
					_report.Axes[1].Order=FI.BusinessObjects.Olap.Axis.OrderEnum.BDESC;
				else if(_report.Axes[1].Order==FI.BusinessObjects.Olap.Axis.OrderEnum.BASC)
					_report.Axes[1].Order=FI.BusinessObjects.Olap.Axis.OrderEnum.BDESC;
				else if(_report.Axes[1].Order==FI.BusinessObjects.Olap.Axis.OrderEnum.BDESC)
					_report.Axes[1].Order=FI.BusinessObjects.Olap.Axis.OrderEnum.BASC;
			}


		}

		#endregion
		// -----------------------------------------------------------------------------------------------

		
		// -----------------------------------------------------------------------------------------------
		#region Dimension methods
		public void OpenFolder(string Name)
		{
			_report.Schema.OpenNode(Name);
		}


		public void CloseFolder(string Name)
		{
			_report.Schema.CloseNode(Name);
		}
		#endregion
		// -----------------------------------------------------------------------------------------------

		// -----------------------------------------------------------------------------------------------
		#region Hierarchy methods

		/*
		public string IdentifierFromHierarchy(Hierarchy hier)
		{
			Axis axis=hier.Axis;
			return axis.Ordinal + ":" + hier.UniqueName;
		}


		public Hierarchy HierarchyFromIdentifier(string Identifier)
		{
			int start=0;
			int end=0;

			start=0;
			end=Identifier.IndexOf(":");
			short axisOrdinal=short.Parse(Identifier.Substring(start , end-start));

			start=end+1;
			end=Identifier.Length;
			string hierUn=Identifier.Substring(start , end-start);

			return _report.Axes[axisOrdinal].Hierarchies[hierUn,false];
		}
		*/

		public void HierarchyToAxis(string UniqueName , short DestAxis)
		{
			/*
			if(_report.State==Report.StateEnum.Executing)
				throw new Exception("Cannot proceed while report is executing..");
			*/
			

			Hierarchy hier=_report.Schema.Hierarchies[UniqueName];
			if(hier==null)
				throw new Exception("Error: invalid identifier");
			
			hier.Axis=_report.Axes[DestAxis];
		}



		public void HierarchyUp(string UniqueName)
		{
			Hierarchy hier=_report.Schema.Hierarchies[UniqueName];
			if(hier==null)
				throw new Exception("Error: invalid identifier");

			hier.Axis.Hierarchies.MoveUp(hier);
		}

		public void HierarchyDown(string UniqueName)
		{
			Hierarchy hier=_report.Schema.Hierarchies[UniqueName];
			if(hier==null)
				throw new Exception("Error: invalid identifier");

			hier.Axis.Hierarchies.MoveDown(hier);
		}


		public void OpenHierarchy(string UniqueName)
		{
			Hierarchy hier=_report.Schema.Hierarchies[UniqueName];
			if(hier==null)
				throw new Exception("Error: invalid identifier");

			hier.Open();
		}


		public void CloseHierarchy(string UniqueName)
		{
			Hierarchy hier=_report.Schema.Hierarchies[UniqueName];
			if(hier==null)
				throw new Exception("Error: invalid identifier");

			hier.Close();
		}


		public void AddHierarchyChildren(string UniqueName)
		{
			Hierarchy hier=_report.Schema.Hierarchies[UniqueName];
			if(hier==null)
				throw new Exception("Error: invalid identifier");

			for(int i=0;i<hier.SchemaMembers.Count;i++)
			{
				SchemaMember smem=hier.SchemaMembers[i];
				hier.AddMember(smem, true);
			}			
		}


		public void RemoveHierarchyChildren(string UniqueName)
		{
			Hierarchy hier=_report.Schema.Hierarchies[UniqueName];
			if(hier==null)
				throw new Exception("Error: invalid identifier");

			// check if autoselect
			hier.RemoveRootSchemaMembers();			
		}


		public void SetHierarchyAggregate(string UniqueName , bool Value)
		{
			Hierarchy hier=_report.Schema.Hierarchies[UniqueName];
			if(hier==null)
				throw new Exception("Error: invalid identifier");
			
			if(Value==true)
			{
				MembersAggregate aggr=new MembersAggregate(null, hier , MembersAggregate.AggregateFunction.AGGREGATE);
				hier.AddMember(aggr, true);
			}
			else
				hier.AddMember(hier.Levels[0].DefaultMember, true);
		}

		
		public void AddFilteredByNameSet(Level lev , string LessOrEq, string GrOrEq , bool Prompt)
		{
			FilteredByNameSet levSet=new FilteredByNameSet(
				null,
				lev.Hierarchy , 
				lev , 
				LessOrEq , 
				GrOrEq);
			levSet.Prompt=Prompt;

			if(lev.Hierarchy.Axis.Ordinal==2)
			{
				MembersAggregate aggr=lev.Hierarchy.FilterMember as MembersAggregate;
				if(aggr!=null)
					aggr.AddMember(levSet); //if aggegated
				else
					lev.Hierarchy.AddMember(levSet, true);
			}
			else
				lev.Hierarchy.AddMember(levSet, true);
		}



		public void AddRatioMeasure(string TypeString, string HierUniqueName , string MeasureName)
		{
			Hierarchy measuresHier=_report.Schema.Hierarchies["[Measures]"];
			Hierarchy ratioHier=_report.Schema.Hierarchies[HierUniqueName];
			Member ratioMeasure=measuresHier.SchemaMembers.Find("[Measures].[" + MeasureName.Replace("]" , "]]") + "]");

			if(ratioHier==null)
				throw new Exception("Unknown hierarchy");

			if(ratioMeasure==null)
				throw new Exception("Unknown measure");

			CalculatedMember calcMem=this.CreateRatioMeasure(TypeString , ratioHier , ratioMeasure);
			if(calcMem==null)
				return;

			if(measuresHier.Axis.Ordinal==2)
			{
				if((measuresHier.FilterMember is CalculatedMember)==false)
				{
					DataMember existMeasure=measuresHier.FilterMember;
					measuresHier.ReplaceMember(existMeasure, calcMem);
				}
				else
					throw new Exception("Cannot add: remove existing calculated member first");
			}
			else
				measuresHier.AddMember(calcMem, true);
		}

		private CalculatedMember CreateRatioMeasure(string Type , Hierarchy Hier , Member Measure )
		{
			CalculatedMember calcMem=null;
			Hierarchy hostHier=_report.Schema.Hierarchies["[Measures]"];

			switch(Type)
			{
				case "Ratio To Visual Sum":
					calcMem=new MemToVisAggr(null, hostHier, Measure , Hier , VisualAggregate.AggregateFunction.SUM);
					break;
				case "Ratio To Visual Avg":
					calcMem=new MemToVisAggr(null, hostHier, Measure , Hier , VisualAggregate.AggregateFunction.AVG);
					break;
				case "Ratio To Visual Min":
					calcMem=new MemToVisAggr(null, hostHier, Measure , Hier , VisualAggregate.AggregateFunction.MIN);
					break;
				case "Ratio To Visual Max":
					calcMem=new MemToVisAggr(null, hostHier, Measure , Hier , VisualAggregate.AggregateFunction.MAX);
					break;
				case "Ratio To Parent Member":
					calcMem=new MemToParent(null, hostHier, Measure , Hier);
					break;
				case "Ratio To (All) Member":
					calcMem=new MemToAll(null, hostHier, Measure , Hier);
					break;
				default:
					throw new Exception("Unknown Type");
			}

			return calcMem;
		}

		public void AddCalculatedMeasure(string MeasureName1 , string MeasureName2, string Operation)
		{
			Hierarchy measuresHier=_report.Schema.Hierarchies["[Measures]"];
			Member mea1=measuresHier.SchemaMembers.Find("[Measures].[" + MeasureName1.Replace("]" , "]]") + "]");
			Member mea2=measuresHier.SchemaMembers.Find("[Measures].[" + MeasureName2.Replace("]" , "]]") + "]");

			if(measuresHier==null)
				throw new Exception("Unknown hierarchy");

			if(mea1==null || mea2==null)
				throw new Exception("Unknown measure");

			CalculatedMember calcMem=this.CreateCalculatedMeasure(mea1, mea2, Operation);
			if(calcMem==null)
				return;

			if(measuresHier.Axis.Ordinal==2)
			{
				if((measuresHier.FilterMember is CalculatedMember)==false)
				{
					DataMember existMeasure=measuresHier.FilterMember;
					measuresHier.ReplaceMember(existMeasure, calcMem);
				}
				else
					throw new Exception("Cannot add: remove existing calculated member first");
			}
			else
				measuresHier.AddMember(calcMem, true);			
		}


		private CalculatedMember CreateCalculatedMeasure(Member Measure1, Member Measure2, string Operation )
		{
			CalculatedMember calcMem=null;
			Hierarchy meaHier=_report.Schema.Hierarchies["[Measures]"];

			switch(Operation)
			{
				case "-":					
					calcMem=new MeasureToMeasure(null, meaHier , Measure1, Measure2, MeasureToMeasure.Operations.SUBTRACT);
					break;
				case "+":					
					calcMem=new MeasureToMeasure(null, meaHier , Measure1, Measure2, MeasureToMeasure.Operations.ADD);
					break;
				case "/":					
					calcMem=new MeasureToMeasure(null, meaHier , Measure1, Measure2, MeasureToMeasure.Operations.DIVIDE);
					break;
				case "*":					
					calcMem=new MeasureToMeasure(null, meaHier , Measure1, Measure2, MeasureToMeasure.Operations.MULTIPLY);
					break;
				case "Inherite NULL":
					calcMem=new MeasureInheritedNull(null, meaHier, Measure1 , Measure2);
					break;
				default:
					throw new Exception("Unknown Operation");
			}

			return calcMem;
		}


		public void AddVisualAggr(string HierUN , string Aggr)
		{
			VisualAggregate.AggregateFunction aggr=(VisualAggregate.AggregateFunction)System.Enum.Parse(typeof(VisualAggregate.AggregateFunction) , Aggr , true);
			Hierarchy hier=_report.Schema.Hierarchies[HierUN];

			VisualAggregate mem=new VisualAggregate(null, hier, aggr);
			hier.AddMember(mem, true);
		}


		public void RemoveVisualAggr(string HierUN , string Aggr)
		{
			VisualAggregate.AggregateFunction aggr=(VisualAggregate.AggregateFunction)System.Enum.Parse(typeof(VisualAggregate.AggregateFunction) , Aggr , true);
			Hierarchy hier=_report.Schema.Hierarchies[HierUN];

			CalculatedMember cmem=hier.CalculatedMembers.GetVisualAggregate(aggr);
			if(cmem!=null)
				hier.RemoveMember(cmem);
		}

		public void SetDefaultMember(string HierUN)
		{
			Hierarchy hier=_report.Schema.Hierarchies[HierUN];
			hier.SetDefaultMember();
		}

		#endregion
		// -----------------------------------------------------------------------------------------------



		// -----------------------------------------------------------------------------------------------
		#region Member methods

		public string IdentifierFromSchemaMember(Member mem)
		{
			Axis axis=mem.Hierarchy.Axis;
			return axis.Ordinal + ":" +  mem.Hierarchy.Ordinal.ToString() + ":" + mem.UniqueName;
		}


		public SchemaMember SchemaMemberFromIdentifier(string Identifier)
		{
			int start=0;
			int end=0;

			start=0;
			end=Identifier.IndexOf(":");
			short axisOrdinal=short.Parse(Identifier.Substring(start , end-start));

			start=end+1;
			end=Identifier.IndexOf(":" , start);
			short hierOrdinal=short.Parse(Identifier.Substring(start , end-start));

			start=end+1;
			end=Identifier.Length;
			string memUn=Identifier.Substring(start , end-start);

			return _report.Axes[axisOrdinal].Hierarchies[hierOrdinal].SchemaMembers.Find(memUn);
		}



		public string IdentifierFromCellsetPosition(short AxisOrdinal , int Pos , int MPos)
		{
			return AxisOrdinal.ToString() + ":" +  Pos.ToString() + ":" + MPos.ToString();
		}

		public void CellsetPositionFromIdentifier(string Identifier , ref short AxisOrdinal , ref int Pos , ref int MPos )
		{
			int start=0;
			int end=0;

			start=0;
			end=Identifier.IndexOf(":");
			AxisOrdinal=short.Parse(Identifier.Substring(start , end-start));

			start=end+1;
			end=Identifier.IndexOf(":" , start);
			Pos=int.Parse(Identifier.Substring(start , end-start));

			start=end+1;
			end=Identifier.Length;
			MPos=int.Parse(Identifier.Substring(start , end-start));
		}



		public void OpenMember(string Identifier)
		{
			SchemaMember mem=SchemaMemberFromIdentifier(Identifier);
			if(mem==null)
				throw new Exception("Error: invalid identifier");

			mem.Open();
		}


		public void CloseMember(string Identifier)
		{
			SchemaMember mem=SchemaMemberFromIdentifier(Identifier);
			if(mem==null)
				throw new Exception("Error: invalid identifier");

			mem.Close();
		}


		public void AddMemberChildren(string Identifier, bool AutoSelect)
		{
			SchemaMember parentMem=SchemaMemberFromIdentifier(Identifier);
			if(parentMem==null)
				throw new Exception("Error: invalid identifier");

			Hierarchy hier=parentMem.Hierarchy;			
			if(AutoSelect)
			{
				// remove children first
				this.RemoveMemberChildren(Identifier);

				// add children set
				MemberChildrenSet mcs=new MemberChildrenSet(null, hier, parentMem);
				hier.AddMember(mcs, true);
			}
			else
			{
				for(int i=0;i<parentMem.Children.Count;i++)
					hier.AddMember(parentMem.Children[i], true);
			}
		}


		public void RemoveMemberChildren(string Identifier)
		{
			SchemaMember parentMem=SchemaMemberFromIdentifier(Identifier);
			if(parentMem==null)
				throw new Exception("Error: invalid identifier");

			// check if autoselect
			MemberChildrenSet mcs=parentMem.Hierarchy.CalculatedMembers.GetMemberChildrenSet(parentMem.UniqueName);
			if(mcs!=null)
				parentMem.Hierarchy.RemoveMember(mcs);
			else
				parentMem.Hierarchy.RemoveSchemaMemberChildren(parentMem);
		}


		public void AddMembersAndRemoveSiblings(System.Collections.Specialized.StringCollection UniqueNames , bool MultiSelect)
		{
			if(MultiSelect)
			{
				for(int j=0;j<_report.Axes.Count;j++)
					for(int i=0;i<_report.Axes[j].Hierarchies.Count;i++)
					{
						Hierarchy hier=_report.Axes[j].Hierarchies[i];
						if(hier.IsOpen)
						{
							bool IsAggregateHierarchy=(hier.Axis.Ordinal==2 && hier.FilterMember is MembersAggregate?true:false); //only aggregate can be multi-selected

							if(IsAggregateHierarchy==false && hier.Axis.Ordinal==2)
								continue; //beacuse if MultiSelect memebrs are  not applicable to non-aggr hier on filter

//							AddMembersAndRemoveSiblings(hier.SchemaMembers, UniqueNames ,  IsAggregateHierarchy); //it's not filter OR aggr hier on filter
							// deal with schema members
							SortedList reAddMembers=new SortedList();
							this.RecursiveRemoveMembers(hier.SchemaMembers , IsAggregateHierarchy, ref reAddMembers);
							this.AddMembers(hier , UniqueNames, reAddMembers);

							// deal with calc members
							if(IsAggregateHierarchy)
							{
								MembersAggregate maggr=hier.FilterMember as MembersAggregate;
								if(maggr!=null)
								{
									for(int n=0;n<maggr.Members.Count;n++)
										if(UniqueNames.Contains(maggr.Members[n].UniqueName)==false)
										{
											maggr.Members.RemoveAt(n);
											n--;
										}
								}
							}
							else
							{
								for(int n=0;n<hier.CalculatedMembers.Count;n++)
									if(UniqueNames.Contains(hier.CalculatedMembers[n].UniqueName)==false)
									{
										hier.CalculatedMembers.RemoveAt(n);
										n--;
									}
							}
						}
					}
			}
			else
			{
				// axis2
				// UniqueNames are actually Identifiers
				for(int i=0;i<UniqueNames.Count;i++)
				{
					SchemaMember mem=this.SchemaMemberFromIdentifier(UniqueNames[i]);
					if(mem!=null)
						mem.Hierarchy.AddMember(mem, true);
				}
			}
		}


		private void RecursiveRemoveMembers(SchemaMembers smems, bool isAggr, ref SortedList ReAddDataMembers)
		{
			for(int i=0;i<smems.Count;i++)
			{
				SchemaMember smem=smems[i];								

				// attempt to remove
				if(isAggr)
				{
					if(smem.UniqueName!=smem.Hierarchy.FilterMember.UniqueName)
					{
						MembersAggregate filtMem=(MembersAggregate)smem.Hierarchy.FilterMember;
						// check if readd
						Member mem=(Member)filtMem.Members[smem.UniqueName];
						if(mem!=null)
							ReAddDataMembers.Add(mem.UniqueName, mem);

						//remove
						filtMem.RemoveMember(smem.UniqueName);
					}
				}
				else
				{
					// check if readd
					Member mem=smem.Hierarchy.DataMembers[smem.UniqueName];
					if(mem!=null)
						ReAddDataMembers.Add(mem.UniqueName, mem);

					//remove
					smem.Hierarchy.RemoveMember(smem);
				}

				if(smem.IsOpen)
					this.RecursiveRemoveMembers(smem.Children, isAggr, ref  ReAddDataMembers);
			}
		}

		private void AddMembers(Hierarchy hier, System.Collections.Specialized.StringCollection UniqueNames, SortedList ReAddDataMembers)
		{
			bool isAggr=(hier.Axis.Ordinal==2 && hier.FilterMember is MembersAggregate?true:false);

			for(int i=0;i<UniqueNames.Count;i++)
			{
				if(isAggr && UniqueNames[i]==hier.FilterMember.UniqueName)
					continue;		
				
				// add 
				if(isAggr)
				{
					// check readd or from schema
					Member mem=(Member)ReAddDataMembers[UniqueNames[i]];
					if(mem!=null)
						((MembersAggregate)hier.FilterMember).AddMember(mem);
					else
					{
						SchemaMember smem=hier.SchemaMembers.Find(UniqueNames[i]);
						if(smem!=null)
							((MembersAggregate)hier.FilterMember).AddMember(smem);	
					}
				}
				else
				{
					// check readd or nfrom schema
					Member mem=(Member)ReAddDataMembers[UniqueNames[i]];
					if(mem!=null)
						hier.AddMember(mem, true);
					else
					{
						SchemaMember smem=hier.SchemaMembers.Find(UniqueNames[i]);
						if(smem!=null)
							hier.AddMember(smem, true);
					}
				}
			}
		}


		public void DrillDown(System.Collections.Specialized.StringCollection Identifiers)
		{
			ArrayList list=new ArrayList();
			for(int i=0;i<Identifiers.Count;i++)
			{
				short axisOrdinal=-1;
				int pos=-1;
				int mpos=-1;

				this.CellsetPositionFromIdentifier(Identifiers[i] , ref axisOrdinal , ref pos, ref mpos);
				CellsetMember mem=_report.Cellset.GetCellsetMember((byte)axisOrdinal  , mpos, pos);
				list.Add(mem);
			}
			_report.DrillDown( (CellsetMember[])list.ToArray(typeof(CellsetMember)));
		}

		public void DrillUp(System.Collections.Specialized.StringCollection Identifiers)
		{
			ArrayList list=new ArrayList();
			for(int i=0;i<Identifiers.Count;i++)
			{
				short axisOrdinal=-1;
				int pos=-1;
				int mpos=-1;

				this.CellsetPositionFromIdentifier(Identifiers[i] , ref axisOrdinal , ref pos, ref mpos);
				CellsetMember mem=_report.Cellset.GetCellsetMember((byte)axisOrdinal  , mpos, pos);
				list.Add(mem);
			}
			_report.DrillUp( (CellsetMember[])list.ToArray(typeof(CellsetMember)));
		}

		public void Remove(System.Collections.Specialized.StringCollection Identifiers)
		{
			ArrayList memList=new ArrayList();

			for(int i=0;i<Identifiers.Count;i++)
			{
				short axisOrdinal=-1;
				int pos=-1;
				int mpos=-1;
				this.CellsetPositionFromIdentifier(Identifiers[i] , ref axisOrdinal , ref pos, ref mpos);

				CellsetMember cstMem=_report.Cellset.GetCellsetMember( (byte)axisOrdinal  , mpos, pos);	
				memList.Add(cstMem);						
			}
			
			// remove members
			System.Collections.Specialized.StringCollection parentList=new System.Collections.Specialized.StringCollection();
			for(int i=0;i<memList.Count;i++)
			{
				CellsetMember cstMem=(CellsetMember)memList[i];
				Hierarchy hier=_report.Schema.GetHierarchyFromMemberUniqueName(cstMem.UniqueName);

				// get member, remove if exisits
				DataMember dmem=(DataMember)hier.GetMember(cstMem.UniqueName);
				if(dmem!=null)
				{
					dmem.Hierarchy.RemoveMember(dmem);
					continue;
				}

				// if not found by unique name, check if member was part of MemberChildrenSet (autoselect), 
				// in this case we convert it to not-autoselect
				if(cstMem.LevelDepth==0)
					continue;
				SchemaMember parentMem=_report.Schema.GetMemberParent(hier, cstMem.UniqueName);
				if(parentMem!=null)
				{
					if(parentList.Contains(parentMem.UniqueName))
						continue; // parent already handled

					parentList.Add(parentMem.UniqueName);
					MemberChildrenSet mcs=hier.CalculatedMembers.GetMemberChildrenSet(parentMem.UniqueName);
					if(mcs!=null)
					{
						// add children
						hier.RemoveMember(mcs);
						hier.AddMemberChildren(parentMem.UniqueName, false);
					}
				}
			}

			// finally remove members
			for(int i=0;i<memList.Count;i++)
			{
				CellsetMember cstMem=(CellsetMember)memList[i];
				Hierarchy hier=_report.Schema.GetHierarchyFromMemberUniqueName(cstMem.UniqueName);

				hier.DataMembers.Remove(cstMem.UniqueName);
			}
		}


		public void ResetFormattedMember(string uniqueName)
		{
			// get hierarchy and member
			Hierarchy hier=_report.Schema.GetHierarchyFromMemberUniqueName(uniqueName);
			if(hier==null)
				throw new Exception("Unable to resolve hierarchy from " + uniqueName);
			DataMember mem=hier.GetMember(uniqueName);
			if(mem==null)
				throw new Exception("Cannot find member: " + uniqueName);

			CalculatedMember cmem=mem as CalculatedMember;
			MemberWrapper mw=cmem as MemberWrapper;
			if(mw!=null) // if it's member wrapper, simply replace it with source member		
				cmem.Hierarchy.ReplaceMember(mw, mw.SourceMember);			
			else if(cmem!=null) // if other calculated member, set default name and format
			{
				CalculatedMember newMem=cmem.Clone(null);
				newMem.Format=CalculatedMember.FormatEnum.Default;
				newMem.Hierarchy.ReplaceMember(cmem, newMem);
			}	
		}

		public void SetFormattedMember(string uniqueName , string name , string format)
		{
			// get hierarchy and member
			Hierarchy hier=_report.Schema.GetHierarchyFromMemberUniqueName(uniqueName);

//			// debug
//			if(hier.UniqueName=="[Measures]")
//			{
//				TestFormatMeasure(uniqueName, name, format);
//				return;
//			}

			if(hier==null)
				throw new Exception("Unable to resolve hierarchy from " + uniqueName);
			DataMember mem=hier.GetMember(uniqueName);
			if(mem==null)
				throw new Exception("Cannot find member: " + uniqueName);
			if(mem is Set)
				throw new ArgumentException("Cannot format set: + uniqueName");

			// resolve formatEnum
			CalculatedMember.FormatEnum formatEnum=(CalculatedMember.FormatEnum)Enum.Parse(typeof(CalculatedMember.FormatEnum) , format, true);

			// format
			CalculatedMember cmem=mem as CalculatedMember;
			if(cmem!=null) // if calculated member, simply set different name and format
			{				
				if(cmem.Name==name && formatEnum==cmem.Format)
					return;  // if not changed

				CalculatedMember newMem=cmem.Clone(name);
				newMem.Format=formatEnum;
				cmem.Hierarchy.ReplaceMember(cmem, newMem);
			}
			else // if data member, create new wrapper
			{
				if(mem.Name==name && formatEnum==CalculatedMember.FormatEnum.Default)
					return;  // if not changed

				CalculatedMember newMem=new BusinessObjects.Olap.CalculatedMemberTemplates.MemberWrapper(name, hier, mem);		
				newMem.Format=formatEnum;
				newMem.Hierarchy.ReplaceMember(mem, newMem);
			}			
		}
		

//		public void TestFormatMeasure(string uniqueName , string NewName , string Format)
//		{
//			DataMember mem=_report.Schema.Hierarchies["[Measures]"].GetMember(uniqueName);
//			if(mem==null)
//				throw new Exception("Invalid UniqueName: " + uniqueName);
//
//			 CalculatedMember.FormatEnum format=(CalculatedMember.FormatEnum)Enum.Parse(typeof(CalculatedMember.FormatEnum) , Format, true);
//
//			CalculatedMember cmem=mem as CalculatedMember;
//			if(cmem==null)
//			{
//				if(mem.Name==NewName && format==CalculatedMember.FormatEnum.Default)
//					return;  // if not changed
//
//				cmem=new BusinessObjects.Olap.CalculatedMemberTemplates.MeasureWrapper(NewName, mem.Hierarchy , mem);				
//				cmem.Format=format;
//				cmem.Hierarchy.ReplaceMember(mem, cmem);
//			}
//			else
//			{
//				if(mem.Name==NewName && format==cmem.Format)
//					return;  // if not changed
//
//				CalculatedMember newMem=cmem.Clone(NewName);
//				newMem.Format=format;
//				cmem.Hierarchy.CalculatedMembers.Add(newMem, true); // with replcae option
//			}			
//		}

		#endregion
		// -----------------------------------------------------------------------------------------------

	}
}
