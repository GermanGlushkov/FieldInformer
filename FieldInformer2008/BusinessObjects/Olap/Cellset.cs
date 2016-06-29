using System;
using System.Xml;
using System.Collections.Specialized;
using System.Collections;


namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for Cellset.
	/// </summary>
	public class Cellset
	{
        private static string __del1 = new string(new char[] { (char)1, (char)8 });
        private static string __del2 = new string(new char[] { (char)2, (char)7 });
        private static string __del3 = new string(new char[] { (char)3, (char)6 });
        private static string __del4 = new string(new char[] { (char)4, (char)5 });

		private bool _isValid=false;
		private bool _pivot=false;

		private int _axis0TupleMemCount=0;
		private int _axis0PosCount=0;
		private int _axis1TupleMemCount=0;
		private int _axis1PosCount=0;

		private CellsetMember[,] _axis0Members=null;
		private CellsetMember[,] _axis1Members=null;
		private Cell[,] _cells=null;

		public Cellset()
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
            _axis1Members = null;
			_cells=null;
			_isValid=false;
		}

		public bool IsValid
		{
			get{ return _isValid;}
		}

        public void LoadCellset(string DelimitedString)
        {
            LoadCellset(DelimitedString, 0, 0);
        }


		public void LoadCellset(string DelimitedString, int maxAxis0Pos, int maxAxis1Pos)
		{
			_pivot=false;
			this.Clear();


            string[] _level1Parts = DelimitedString.Split(new string[] { __del4 }, StringSplitOptions.None);
			if(_level1Parts.Length!=5)
				throw new Exception("Invalid DelimitedString, split");

			//axis0 metadata
            string[] _axis0Metadata = _level1Parts[0].Split(new string[] { __del2 }, StringSplitOptions.None);
			if(_axis0Metadata.Length!=2)
				throw new Exception("Invalid DelimitedString, axis0 metadata");
			_axis0PosCount=int.Parse(_axis0Metadata[0]);
            if (maxAxis0Pos>0 && _axis0PosCount > maxAxis0Pos)
                _axis0PosCount = maxAxis0Pos;
			_axis0TupleMemCount=int.Parse(_axis0Metadata[1]);

			//second part - axis1 metadata
            string[] _axis1Metadata = _level1Parts[1].Split(new string[] { __del2 }, StringSplitOptions.None);
			if(_axis1Metadata.Length!=2)
				throw new Exception("Invalid DelimitedString, axis1 metadata");
			_axis1PosCount=int.Parse(_axis1Metadata[0]);
            if (maxAxis1Pos > 0 && _axis1PosCount > maxAxis1Pos)
                _axis1PosCount = maxAxis1Pos;
			_axis1TupleMemCount=int.Parse(_axis1Metadata[1]);

			//axis0 members
			if(this._axis0PosCount>0)
			{
                string[] axis0PosArr = _level1Parts[2].Split(new string[] { __del3 }, StringSplitOptions.None); 
				if(axis0PosArr.Length<this._axis0PosCount)
					throw new Exception("Invalid DelimitedString, axis0 pos count");
				for(int i=0;i<this._axis0PosCount;i++)
				{
                    string[] axis0MemArr = axis0PosArr[i].Split(new string[] { __del2 }, StringSplitOptions.None); 
					for(int j=0;j<this._axis0TupleMemCount;j++)
					{
                        string[] memProps = axis0MemArr[j].Split(new string[] { __del1 }, StringSplitOptions.None);
                        CellsetMember mem = CreateCellsetMember(0, i, j, memProps[0], memProps[1], int.Parse(memProps[2]), short.Parse(memProps[3]));
						if(this._axis0Members==null) // initialize
							this._axis0Members=new CellsetMember[this._axis0TupleMemCount , this._axis0PosCount];

						this._axis0Members[j,i]=mem;
					}
				}
			}

			//axis1 members
			if(this._axis1PosCount>0)
			{
                string[] axis1PosArr = _level1Parts[3].Split(new string[] { __del3 }, StringSplitOptions.None); 
				if(axis1PosArr.Length<this._axis1PosCount)
					throw new Exception("Invalid DelimitedString, axis1 pos count");
				for(int i=0;i<this._axis1PosCount;i++)
				{
                    string[] axis1MemArr = axis1PosArr[i].Split(new string[] { __del2 }, StringSplitOptions.None); 
					for(int j=0;j<this._axis1TupleMemCount;j++)
					{
                        string[] memProps = axis1MemArr[j].Split(new string[] { __del1 }, StringSplitOptions.None);
                        CellsetMember mem = CreateCellsetMember(1, i, j, memProps[0], memProps[1], int.Parse(memProps[2]), short.Parse(memProps[3]));
						if(this._axis1Members==null) // initialize
							this._axis1Members=new CellsetMember[this._axis1TupleMemCount , this._axis1PosCount];

						this._axis1Members[j,i]=mem;
					}
				}
			}



			//cells
			if(this._axis0PosCount>0 && this._axis1PosCount>0)
			{
				// intialize cells
				_cells=new Cell[_axis0PosCount , _axis1PosCount];


                string[] axis0CellPosArr = _level1Parts[4].Split(new string[] { __del3 }, StringSplitOptions.None); 
				if(axis0CellPosArr.Length<this._axis0PosCount)
					throw new Exception("Invalid DelimitedString, axis0 cell pos count");
				for(int i=0;i<this._axis0PosCount;i++)
				{
                    string[] axis1CellPosArr = axis0CellPosArr[i].Split(new string[] { __del2 }, StringSplitOptions.None);
                    if (axis1CellPosArr.Length < this._axis1PosCount)
                        throw new Exception("Invalid DelimitedString, axis1 cell pos count");
					for(int j=0;j<this._axis1PosCount;j++)
					{
                        string[] cellValues = axis1CellPosArr[j].Split(new string[] { __del1 },StringSplitOptions.None); 
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

        private CellsetMember CreateCellsetMember(byte axis, int pos, int mPos, string uniqueName, string name, int childCount, short levelDepth)
        {
            //if (_report != null)
            //{
            //    Hierarchy hier=_report.Axes[axis].Hierarchies[mPos];
            //    Olap.CalculatedMemberTemplates.MemberWrapper mw = hier.CalculatedMembers[uniqueName] as Olap.CalculatedMemberTemplates.MemberWrapper;
            //    if (mw != null && mw.IsRenameOnly)
            //        name = mw.Name;
            //}

            return new CellsetMember(axis, pos, mPos, uniqueName, name, childCount, levelDepth);
        }

        public string ToXmlString(int maxAxis0Pos, int maxAxis1Pos)
        {
            XmlDocument doc = new XmlDocument();

            XmlElement cstEl = (XmlElement)doc.AppendChild(doc.CreateElement("CELLSET"));
            XmlElement colHdrEl = (XmlElement)cstEl.AppendChild(doc.CreateElement("COLHDR"));
            XmlElement rowHdrEl = (XmlElement)cstEl.AppendChild(doc.CreateElement("ROWHDR"));
            XmlElement cellsEl = (XmlElement)cstEl.AppendChild(doc.CreateElement("CELLS"));

            int axis0PosCount = (maxAxis0Pos <= 0 || maxAxis0Pos > this.Axis0PosCount ? this.Axis0PosCount : maxAxis0Pos);
            int axis0TupleMemCount = this.Axis0TupleMemCount;
            int axis1PosCount = (maxAxis1Pos <= 0 || maxAxis1Pos > this.Axis1PosCount ? this.Axis1PosCount : maxAxis1Pos);
            int axis1TupleMemCount = this.Axis1TupleMemCount;

            // col headers
            colHdrEl.SetAttribute("ROWS", axis0TupleMemCount.ToString());
            colHdrEl.SetAttribute("COLS", axis0PosCount.ToString());
            for (int j = 0; j < axis0TupleMemCount; j++)
            {
                XmlElement rowEl = (XmlElement)colHdrEl.AppendChild(doc.CreateElement("R"));
                for (int i = 0; i < axis0PosCount; i++)
                {
                    XmlElement colEl = (XmlElement)rowEl.AppendChild(doc.CreateElement("C"));
                    CellsetMember mem=this.GetCellsetMember(0, j, i);
                    colEl.SetAttribute("UN", mem.UniqueName);
                    colEl.SetAttribute("N", mem.Name);
                }
            }

            // row headers
            rowHdrEl.SetAttribute("ROWS", axis1PosCount.ToString());
            rowHdrEl.SetAttribute("COLS", axis1TupleMemCount.ToString());
            for (int j = 0; j < axis1PosCount; j++) 
            {
                XmlElement rowEl = (XmlElement)rowHdrEl.AppendChild(doc.CreateElement("R"));
                for (int i = 0; i < axis1TupleMemCount; i++)
                {
                    XmlElement colEl = (XmlElement)rowEl.AppendChild(doc.CreateElement("C"));
                    CellsetMember mem = this.GetCellsetMember(1, i, j);
                    colEl.SetAttribute("UN", mem.UniqueName);
                    colEl.SetAttribute("N", mem.Name);
                }
            }

            // cells headers
            cellsEl.SetAttribute("ROWS", axis1PosCount.ToString());
            cellsEl.SetAttribute("COLS", axis0PosCount.ToString());
            for (int j = 0; j < axis1PosCount; j++)
            {
                XmlElement rowEl = (XmlElement)cellsEl.AppendChild(doc.CreateElement("R"));
                for (int i = 0; i < axis0PosCount; i++)
                {
                    XmlElement colEl = (XmlElement)rowEl.AppendChild(doc.CreateElement("C"));
                    Cell c = this.GetCell(i, j);
                    colEl.InnerXml = c.FormattedValue;
                }
            }

            return doc.OuterXml;
        }


        public Cell LookupCell(StringCollection uniqueNames, out int axis0Pos, out int axis1Pos)
        {
            axis0Pos = -1;
            axis1Pos = -1;
            if (_isValid == false)
                throw new Exception("Cellset is not valid");

            // if no data
            if (this.Axis0PosCount == 0 || this.Axis1PosCount == 0)
                return null;

            // if not enough dimensions in lookup
            if (uniqueNames.Count < (this.Axis0TupleMemCount + this.Axis1TupleMemCount))
                return null;

            // lookup axis 0
            for (int i = 0; i < this.Axis0PosCount; i++)
            {
                int matchCount=0;

                for (int j = 0; j < this.Axis0TupleMemCount; j++)
                    for (int n = 0; n < uniqueNames.Count; n++)
                        if (string.Compare(this.GetCellsetMember(0, j, i).UniqueName, uniqueNames[n], true) == 0)
                        {
                            matchCount++;
                            break;
                        }

                if(matchCount==this.Axis0TupleMemCount)
                {
                    axis0Pos=i;
                    break;
                }
            }
            if (axis0Pos < 0)
                return null;

            // lookup axis 1
            for (int i = 0; i < this.Axis1PosCount; i++)
            {
                int matchCount = 0;

                for (int j = 0; j < this.Axis1TupleMemCount; j++)
                    for (int n = 0; n < uniqueNames.Count; n++)
                        if (string.Compare(this.GetCellsetMember(1, j, i).UniqueName, uniqueNames[n], true) == 0)
                        {
                            matchCount++;
                            break;
                        }

                if (matchCount == this.Axis1TupleMemCount)
                {
                    axis1Pos = i;
                    break;
                }
            }
            if (axis1Pos < 0)
                return null;
                        

            // return cell
            return this.GetCell(axis0Pos, axis1Pos);
        }


        //private void BuildMemberIndex()
        //{
        //    if (_isValid == false || _memIndex!=null)
        //        return;

        //    _memIndex = new Objects();

        //    for (int i = 0; i < this.Axis0PosCount; i++)
        //        for (int j = 0; j < this.Axis0TupleMemCount; j++)
        //        {
        //            CellsetMember mem = this.GetCellsetMember(0, j, i);
        //            _memIndex.Add(mem, true);
        //        }

        //    for (int i = 0; i < this.Axis1PosCount; i++)
        //        for (int j = 0; j < this.Axis1TupleMemCount; j++)
        //        {
        //            CellsetMember mem = this.GetCellsetMember(1, j, i);
        //            _memIndex.Add(mem, true);
        //        }
        //}

	}
}
