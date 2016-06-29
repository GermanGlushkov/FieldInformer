using System;
using System.Collections;
using System.Xml;

namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for Axis.
	/// </summary>
	public abstract class Axis
	{
		
		public enum OrderEnum
		{
			NONE=0,
			BDESC=1,
			BASC=2
		}


		public enum EmptyTupleOptionEnum
		{
			NONE=0,
			HIDE_EMPTY=1,
			HIDE_NONEMPTY=2
		}

		private Axes _axes;
		private Hierarchies _hierarchies;
		private OrderEnum _order=OrderEnum.NONE;
		private EmptyTupleOptionEnum _emptyTupleOption=EmptyTupleOptionEnum.HIDE_EMPTY;

		public event EventHandler BeforeChange;


		internal Axis(Axes axes)
		{
			_axes=axes;
			_hierarchies=new Hierarchies();
			_hierarchies.Axis=this;

			//subscribe 
			_hierarchies.BeforeChangeItem+=new ObjectEventHandler(OnBeforeChangeHierarchies);
			_hierarchies.BeforeAdd+=new ObjectEventHandler(OnBeforeChangeHierarchies);
			_hierarchies.BeforeRemove+=new ObjectEventHandler(OnBeforeChangeHierarchies);
		}

		private void OnBeforeChangeHierarchies(Object sender)
		{
			OnBeforeChange();
		}

		public abstract string Name
		{
			get;
		}

		public abstract short Ordinal
		{
			get;
		}

		public Schema Schema
		{
			get{return _axes.Schema;}
		}

		public Hierarchies Hierarchies
		{
			get{return _hierarchies;}
		}

		public EmptyTupleOptionEnum EmptyTupleOption
		{
			get{return this._emptyTupleOption;}
			set
			{
				if(value==this._emptyTupleOption)
					return;

				// cannot set hide_nonempty on both axes
				if(value==EmptyTupleOptionEnum.HIDE_NONEMPTY)
					if(this.Ordinal==0 && this._axes[1].EmptyTupleOption==EmptyTupleOptionEnum.HIDE_NONEMPTY)
						throw new Exception("Cannot set HIDE_NONEMPTY on both visible axes"); 
					else if(this.Ordinal==1 && this._axes[0].EmptyTupleOption==EmptyTupleOptionEnum.HIDE_NONEMPTY)
						throw new Exception("Cannot set HIDE_NONEMPTY on both visible axes"); 
					
				OnBeforeChange();

				this._emptyTupleOption=value;
			}
		}


		public OrderEnum Order
		{
			get{return this._order;}
			set
			{			
				OnBeforeChange();

				this._order=value;
			}
		}
		

		internal void SetOrderFromCellset(int AxisOrdinal, int CellsetPos , int CellsetMPos, OrderEnum Order)
		{
			OnBeforeChange();
		}


		private void OnBeforeChange()
		{
			if (BeforeChange != null)
				BeforeChange(this, EventArgs.Empty);
		}

		internal void LoadFromXml(XmlElement xmlEl)
		{
			
			if(this.Ordinal!=2)
			{
				this._order=(OrderEnum)System.Enum.Parse(typeof(OrderEnum) , xmlEl.GetAttribute("SO") , true);
				this._emptyTupleOption=(EmptyTupleOptionEnum)System.Enum.Parse(typeof(EmptyTupleOptionEnum) , xmlEl.GetAttribute("ETO") , true);
			}

			foreach(XmlElement hierEl in xmlEl.ChildNodes) //hierarchies
			{
				string uniqueName=hierEl.GetAttribute("UN");

				Olap.Hierarchy hier=Schema.Hierarchies[uniqueName];
				if(hier==null)
					continue;
				
				//move to specified axis
				hier.Axis=this;
				hier.LoadFromXml(hierEl);
			}
		}


		internal void SaveToXml(System.Xml.XmlElement xmlEl, System.Xml.XmlDocument doc)
		{
			xmlEl.SetAttribute("ORD" , this.Ordinal.ToString());
			xmlEl.SetAttribute("SO", this.Order.ToString());
			xmlEl.SetAttribute("ETO", this.EmptyTupleOption.ToString());

			foreach(Hierarchy hier in this.Hierarchies) //hierarchies
			{
				System.Xml.XmlElement childEl=doc.CreateElement("H");
				hier.SaveToXml(childEl , doc);
				xmlEl.AppendChild(childEl);
			}
		}




		public virtual void MDXOrderTuple(ref string OrderTuple , ref OrderEnum Order)
		{
			OrderTuple=null;

			if(this.Ordinal==2)
				return;

			for(int i=0;i<this.Hierarchies.Count; i++)
			{
				Hierarchy hier=this.Hierarchies[i];

				if(hier.OrderTupleMember!=null)
				{
					if(_order==OrderEnum.NONE)
						hier._orderTupleMember=null;
					else
					{
						OrderTuple=OrderTuple + hier.OrderTupleMember + ",";
					}
				}
				else if(hier.OrderTupleMember==null)
				{
					if(_order!=OrderEnum.NONE)
					{
						_order=OrderEnum.NONE;
						i=-1; //restart for
					}
					OrderTuple=null;
				}
			}

			//remove last comma
			if(OrderTuple!=null && OrderTuple.EndsWith(","))
				OrderTuple=OrderTuple.Remove(OrderTuple.Length-1,1);

			Order=_order;
		}


		public virtual string MDXDefinitions
		{
			get
			{
				System.Text.StringBuilder sb=new System.Text.StringBuilder();
				for(int i=0;i<Hierarchies.Count;i++)
				{
					sb.Append(Hierarchies[i].MDXDefinitions());
				}
				return sb.ToString();
			}
		}


		public virtual string MDXClause
		{
			get
			{
				System.Text.StringBuilder sb=new System.Text.StringBuilder();

				// sort order
				string orderTuple=null;
				OrderEnum order=OrderEnum.NONE;
				if(this.Ordinal==0)
					this._axes[1].MDXOrderTuple(ref orderTuple , ref order);
				else if(this.Ordinal==1)
					this._axes[0].MDXOrderTuple(ref orderTuple , ref order);

				if(this.Ordinal!=2)
				{
					if(this._emptyTupleOption==EmptyTupleOptionEnum.HIDE_EMPTY)
						sb.Append(" NON EMPTY ");

					if(order==OrderEnum.NONE)
						sb.Append(" HIERARCHIZE(");
					else
						sb.Append(" ORDER(");

					if(this._emptyTupleOption==EmptyTupleOptionEnum.HIDE_NONEMPTY)
						sb.Append(" FILTER( ");
					
					sb.Append("{");
					for(int i=0;i<Hierarchies.Count;i++)
					{
						if(i!=0)
							sb.Append("*");

						sb.Append("{");
						sb.Append(Hierarchies[i].MDXHierarchySetName(true));
						sb.Append("}");
					}
					sb.Append("}");
					
					if(this._emptyTupleOption==EmptyTupleOptionEnum.HIDE_NONEMPTY)
					{
						sb.Append(",");
						if(this.Ordinal==1)
							sb.Append("Sum(Axis(0))=NULL");
						else
							sb.Append("Sum(Axis(1))=NULL");
						sb.Append(")");
					}

					if(order!=OrderEnum.NONE)
					{
						sb.Append(",(");
						sb.Append(orderTuple);
						sb.Append("),");
						sb.Append(order.ToString());
					}

					sb.Append(")"); // closing for ORDER OR HIERARCHIZE

					sb.Append(" ON ");
					sb.Append(this.Name);
				}
				else
				{
					if(Hierarchies.Count==0)
						return "";

					sb.Append("(");
					for(int i=0;i<Hierarchies.Count;i++)
					{
						string filterMember=Hierarchies[i].MDXHierarchyFilterMember();
						if(filterMember!="")
						{
							sb.Append(filterMember);
							sb.Append(",");
						}
					}

					// remove last coma
					if(sb[sb.Length-1]==',')
						sb.Remove(sb.Length-1,1);

					sb.Append(")");
				}

				return sb.ToString();
			}
		}

	}






	public class ColumnsAxis:Axis
	{
		internal ColumnsAxis(Axes axes):base(axes)
		{
		}

		public override string Name
		{
			get
			{
				return "Columns";
			}
		}

		public override short Ordinal
		{
			get
			{
				return 0;
			}
		}
	}



	public class RowsAxis:Axis
	{
		internal RowsAxis(Axes axes):base(axes)
		{
		}

		public override string Name
		{
			get
			{
				return "Rows";
			}
		}

		public override short Ordinal
		{
			get
			{
				return 1;
			}
		}
	}



	public class FiltersAxis:Axis
	{
		internal FiltersAxis(Axes axes):base(axes)
		{
		}

		public override string Name
		{
			get
			{
				return "Filters";
			}
		}

		public override short Ordinal
		{
			get
			{
				return 2;
			}
		}
	}








	


	public class Axes
	{		
		private Schema _schema;
		private ArrayList _list=new ArrayList(3);
		public event EventHandler BeforeChangeItem;

		internal Axes(Schema schema)
		{
			if(schema==null)
				throw new ArgumentNullException("Schema=null");
			_schema=schema;

			_list.Add(new ColumnsAxis(this));
			_list.Add(new RowsAxis(this));
			_list.Add(new FiltersAxis(this));

			this[0].BeforeChange+=new EventHandler(OnBeforeChangeItem);
			this[1].BeforeChange+=new EventHandler(OnBeforeChangeItem);
			this[2].BeforeChange+=new EventHandler(OnBeforeChangeItem);
		}

		private void OnBeforeChangeItem(object sender, EventArgs e)
		{
			if (BeforeChangeItem != null)
				BeforeChangeItem(sender, EventArgs.Empty);
		}

		public Schema Schema
		{
			get{return _schema;}
		}

		public int Count
		{
			get{return _list.Count;}
		}

		public Axis this[int index]
		{
			get{return (Axis)_list[index]; }
		}


		public Hierarchy FindHierarchy(string UniqueName)
		{
			Hierarchy hier=null;
			for(int i=0;i<this.Count;i++)
			{
				hier=this[i].Hierarchies[UniqueName];
				if(hier!=null)
					return hier;
			}
			return null;
		}

		internal void Pivot()
		{
			Hierarchy[] axis0hiers=(Hierarchy[])this[0].Hierarchies.ToArray();
			Hierarchy[] axis1hiers=(Hierarchy[])this[1].Hierarchies.ToArray();

			foreach(Hierarchy hier in axis0hiers)
				hier.Axis=this[1];

			foreach(Hierarchy hier in axis1hiers)
				hier.Axis=this[0];

			// empty tuple options
			Axis.EmptyTupleOptionEnum axis0eto=this[0].EmptyTupleOption;
			Axis.EmptyTupleOptionEnum axis1eto=this[1].EmptyTupleOption;

			this[0].EmptyTupleOption=Axis.EmptyTupleOptionEnum.NONE;
			this[1].EmptyTupleOption=axis0eto;
			this[0].EmptyTupleOption=axis1eto;
		}

	}

}

