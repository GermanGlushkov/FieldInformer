using System;

namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for Cellset.
	/// </summary>
	public class Cellset
	{
		private bool _isValid=false;
		private bool _pivot=false;

		private int _axis0TupleMemCount=0;
		private int _axis0PosCount=0;
		private int _axis1TupleMemCount=0;
		private int _axis1PosCount=0;

		private CellsetMember[,] _axis0Members=null;
		private CellsetMember[,] _axis1Members=null;
		private Cell[,] _cells=null;

		internal Cellset()
		{
		}


		public void Clear()
		{
			if(_isValid==false)
				return;

			_pivot=false;
			_axis0TupleMemCount=0;
			_axis0PosCount=0;
			_axis1TupleMemCount=0;
			_axis1PosCount=0;

			_axis0Members=null;
			_axis1Members=null;
			_cells=null;
			_isValid=false;
		}

		public bool IsValid
		{
			get{ return _isValid;}
		}

		internal void LoadCellset(string DelimitedString)
		{
			_pivot=false;
			this.Clear();


			string[] _level1Parts=DelimitedString.Split(new char[]{(char)13});
			if(_level1Parts.Length!=5)
				throw new Exception("Invalid DelimitedString, split");

			//first part - axis0 metadata
			string[] _axis0Metadata=_level1Parts[0].Split(new char[]{(char)9});
			if(_axis0Metadata.Length!=2)
				throw new Exception("Invalid DelimitedString, axis0 metadata");
			_axis0PosCount=int.Parse(_axis0Metadata[0]);
			_axis0TupleMemCount=int.Parse(_axis0Metadata[1]);

			//second part - axis1 metadata
			string[] _axis1Metadata=_level1Parts[1].Split(new char[]{(char)9});
			if(_axis1Metadata.Length!=2)
				throw new Exception("Invalid DelimitedString, axis1 metadata");
			_axis1PosCount=int.Parse(_axis1Metadata[0]);
			_axis1TupleMemCount=int.Parse(_axis1Metadata[1]);

			//third part - axis0 members
			if(this._axis0PosCount>0)
			{
				string[] axis0PosArr=_level1Parts[2].Split(new char[]{(char)10}); 
				if(axis0PosArr.Length!=this._axis0PosCount)
					throw new Exception("Invalid DelimitedString, axis0 pos count");
				for(int i=0;i<this._axis0PosCount;i++)
				{
					string[] axis0MemArr=axis0PosArr[i].Split(new char[]{(char)9}); 
					for(int j=0;j<this._axis0TupleMemCount;j++)
					{
						string[] memProps=axis0MemArr[j].Split(new char[]{(char)8}); 
						CellsetMember mem=new CellsetMember(i, j, memProps[0] , memProps[1] , int.Parse(memProps[2]) , short.Parse(memProps[3]));
						if(this._axis0Members==null) // initialize
							this._axis0Members=new CellsetMember[this._axis0TupleMemCount , this._axis0PosCount];

						this._axis0Members[j,i]=mem;
					}
				}
			}

			//fourth part - axis1 members
			if(this._axis1PosCount>0)
			{
				string[] axis1PosArr=_level1Parts[3].Split(new char[]{(char)10}); 
				if(axis1PosArr.Length!=this._axis1PosCount)
					throw new Exception("Invalid DelimitedString, axis1 pos count");
				for(int i=0;i<this._axis1PosCount;i++)
				{
					string[] axis1MemArr=axis1PosArr[i].Split(new char[]{(char)9}); 
					for(int j=0;j<this._axis1TupleMemCount;j++)
					{
						string[] memProps=axis1MemArr[j].Split(new char[]{(char)8}); 
						CellsetMember mem=new CellsetMember(i, j, memProps[0] , memProps[1] , int.Parse(memProps[2]) , short.Parse(memProps[3]));
						if(this._axis1Members==null) // initialize
							this._axis1Members=new CellsetMember[this._axis1TupleMemCount , this._axis1PosCount];

						this._axis1Members[j,i]=mem;
					}
				}
			}



			//fifth part - cells
			if(this._axis0PosCount>0 && this._axis1PosCount>0)
			{
				// intialize cells
				_cells=new Cell[_axis0PosCount , _axis1PosCount];


				string[] axis0CellPosArr=_level1Parts[4].Split(new char[]{(char)10}); 
				if(axis0CellPosArr.Length!=this._axis0PosCount)
					throw new Exception("Invalid DelimitedString, axis0 cell pos count");
				for(int i=0;i<this._axis0PosCount;i++)
				{
					string[] axis1CellPosArr=axis0CellPosArr[i].Split(new char[]{(char)9}); 
					for(int j=0;j<this._axis1PosCount;j++)
					{
						string[] cellValues=axis1CellPosArr[j].Split(new char[]{(char)8}); 
						this._cells[i,j]=new Cell(cellValues[0] , cellValues[1]);
					}
				}
			}

			_isValid=true;
		}


		internal void Pivot()
		{
			if(_isValid)			
				_pivot=!(_pivot);			
		}

		public int Axis0TupleMemCount
		{
			get { return (_pivot?_axis1TupleMemCount:_axis0TupleMemCount); }
		}

		public int Axis0PosCount
		{
			get { return (_pivot?_axis1PosCount:_axis0PosCount); }
		}

		public int Axis1TupleMemCount
		{
			get { return (_pivot?_axis0TupleMemCount:_axis1TupleMemCount); }
		}

		public int Axis1PosCount
		{
			get { return (_pivot?_axis0PosCount:_axis1PosCount); }
		}



		public CellsetMember GetCellsetMember(byte Axis , int TupleMemberOrdinal , int PositionOrdinal)
		{
			if(_isValid==false)
				throw new Exception("Cellset is not valid");

			if(Axis==0)
				return (_pivot?this._axis1Members[TupleMemberOrdinal , PositionOrdinal]:this._axis0Members[TupleMemberOrdinal , PositionOrdinal]);
			else if(Axis==1)
				return (_pivot?this._axis0Members[TupleMemberOrdinal , PositionOrdinal]:this._axis1Members[TupleMemberOrdinal , PositionOrdinal]);
			else
				throw new Exception("Wrong axis identifier");
		}

		public Cell GetCell(int Axis0PositionOrdinal , int Axis1PositionOrdinal)
		{
			if(_isValid==false)
				throw new Exception("Cellset is not valid");

			if(_cells!=null)
				return (_pivot?_cells[Axis1PositionOrdinal , Axis0PositionOrdinal]:_cells[Axis0PositionOrdinal , Axis1PositionOrdinal]);
			else
				return null;
		}

	}
}
