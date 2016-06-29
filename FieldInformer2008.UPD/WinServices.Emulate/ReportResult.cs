using System;
using System.Collections.Generic;
using System.Text;

namespace WinServices.Emulate
{
    public class ReportResult
    {
        public class CellsetMember
        {

            int _axisPos;
            int _tuplePos;
            int _childCount = 0;
            short _levelDepth = 0;
            string _uniqueName = null;
            string _name = null;

            public CellsetMember(int axisPos, int tuplePos, string uniqueName, string name, int childCount, short levelDepth)
            {
                _axisPos = axisPos;
                _tuplePos = tuplePos;
                _uniqueName = uniqueName;
                _name = name;
                _childCount = childCount;
                _levelDepth = levelDepth;
            }

            public int AxisPos
            {
                get { return _axisPos; }
            }

            public int TuplePos
            {
                get { return _tuplePos; }
            }

            public string UniqueName
            {
                get { return _uniqueName; }
            }

            public string Name
            {
                get { return _name; }
            }

            public int ChildCount
            {
                get { return _childCount; }
            }

            public short LevelDepth
            {
                get { return _levelDepth; }
            }

        }


        public class Cell
        {
            private string _value = null;
            private string _fValue = null;

            internal Cell(string value, string formattedValue)
            {
                _value = value;
                _fValue = formattedValue;

                if (_fValue != null && _fValue != string.Empty)
                {
                    // custom formatting
                    /*
                    string formatString=FI.Common.AppConfig.CustomNumberFormatString;
                    if(_fValue!=null && _fValue.EndsWith("%") && !formatString.EndsWith("%"))
                        formatString+="%";

                    if(formatString!=null && formatString!=string.Empty && _value!=null && _value!=string.Empty)
                    {
                        System.Globalization.NumberFormatInfo formatInfo=FI.Common.AppConfig.CustomNumberFormatInfo;				

                        double d=0;
                        if(double.TryParse(_value, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out d))
                            _fValue=d.ToString(formatString, formatInfo);
                    }
                    */

                    /*
                    string replaceComma = FI.Common.AppConfig.NumberFormatReplaceComma;
                    if (replaceComma != string.Empty)
                        _fValue = _fValue.Replace(",", replaceComma);

                    string replaceDot = FI.Common.AppConfig.NumberFormatReplaceDot;
                    if (replaceDot != string.Empty)
                        _fValue = _fValue.Replace(".", replaceDot);

                    string replaceSpace = FI.Common.AppConfig.NumberFormatReplaceSpace;
                    if (replaceSpace != string.Empty)
                        _fValue = _fValue.Replace(" ", replaceSpace);
                    */

                    // strip last dot or comma				
                    if (_fValue.EndsWith(".") || _fValue.EndsWith(","))
                        _fValue = _fValue.Substring(1);
                }
            }

            public string Value
            {
                get { return _value; }
            }

            public string FormattedValue
            {
                get { return _fValue; }
            }
        }


        private static string __del1 = new string(new char[] { (char)1, (char)8 });
        private static string __del2 = new string(new char[] { (char)2, (char)7 });
        private static string __del3 = new string(new char[] { (char)3, (char)6 });
        private static string __del4 = new string(new char[] { (char)4, (char)5 });

        int _columnCount;
        int _columnTupleMembers;
        int _rowCount;
        int _rowTupleMembers;
        CellsetMember[,] _rowMembers;
        CellsetMember[,] _columnMembers;
        Cell[,] _cells;

        public int RowCount
        {
            get { return _columnCount; }
        }

        public int RowTupleMembers
        {
            get { return _columnTupleMembers; }
        }

        public int ColumnCount
        {
            get { return _rowCount; }
        }

        public int ColumnTupleMembers
        {
            get { return _rowTupleMembers; }
        }


        public CellsetMember GetRowMember(int tuplePos, int axisPos)
        {
            if (_columnMembers == null)
                throw new Exception("Cellset not loaded");
            return (CellsetMember)_rowMembers[tuplePos, axisPos];
        }

        public CellsetMember[] GetRowTuple(int rowPos)
        {
            if (_columnMembers == null)
                throw new Exception("Cellset not loaded");
            CellsetMember[] ret = new CellsetMember[_rowTupleMembers];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = GetRowMember(i, rowPos);
            return ret;
        }

        public CellsetMember GetColumnMember(int tuplePos, int axisPos)
        {
            if (_columnMembers == null)
                throw new Exception("Cellset not loaded");
            return (CellsetMember)_columnMembers[tuplePos, axisPos];
        }

        public CellsetMember[] GetColumnTuple(int columnPos)
        {
            if (_columnMembers == null)
                throw new Exception("Cellset not loaded");
            CellsetMember[] ret = new CellsetMember[_columnTupleMembers];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = GetColumnMember(i, columnPos);
            return ret;
        }        

        public Cell GetCell(int rowPos, int columnPos)
        {
            if (_cells == null)
                throw new Exception("Cellset not loaded");
            return (Cell)_cells[rowPos, columnPos];
        }


        public void LoadCellset(string cellsetString)
        {
            _columnMembers = null;
            _columnMembers = null;
            _cells = null;

            string[] level1Parts = cellsetString.Split(new string[] { __del4 }, StringSplitOptions.None);
            if (level1Parts.Length != 5)
                throw new Exception("Invalid cellsetString, split");

            //first part - axis0 metadata
            string[] axis0Metadata = level1Parts[0].Split(new string[] { __del2 }, StringSplitOptions.None);
            if (axis0Metadata.Length != 2)
                throw new Exception("Invalid cellsetString, axis0 metadata");
            _columnCount = int.Parse(axis0Metadata[0]);
            _columnTupleMembers = int.Parse(axis0Metadata[1]);

            //second part - axis1 metadata
            string[] axis1Metadata = level1Parts[1].Split(new string[] { __del2 }, StringSplitOptions.None);
            if (axis1Metadata.Length != 2)
                throw new Exception("Invalid cellsetString, axis1 metadata");
            _rowCount = int.Parse(axis1Metadata[0]);
            _rowTupleMembers = int.Parse(axis1Metadata[1]);

            //third part - axis0 members
            if (_columnCount > 0)
            {
                string[] axis0PosArr = level1Parts[2].Split(new string[] { __del3 }, StringSplitOptions.None);
                if (axis0PosArr.Length != _columnCount)
                    throw new Exception("Invalid cellsetString, axis0 pos count");
                for (int i = 0; i < _columnCount; i++)
                {
                    string[] axis0MemArr = axis0PosArr[i].Split(new string[] { __del2 }, StringSplitOptions.None);
                    for (int j = 0; j < _columnTupleMembers; j++)
                    {
                        string[] memProps = axis0MemArr[j].Split(new string[] { __del1 }, StringSplitOptions.None);
                        CellsetMember mem = new CellsetMember(i, j, memProps[0], memProps[1], int.Parse(memProps[2]), short.Parse(memProps[3]));
                        if (_columnMembers == null) // initialize
                            _columnMembers = new CellsetMember[_columnTupleMembers, _columnCount];

                        _columnMembers[j, i] = mem;
                    }
                }
            }

            //fourth part - axis1 members
            if (_rowCount > 0)
            {
                string[] axis1PosArr = level1Parts[3].Split(new string[] { __del3 }, StringSplitOptions.None);
                if (axis1PosArr.Length != _rowCount)
                    throw new Exception("Invalid cellsetString, axis1 pos count");
                for (int i = 0; i < _rowCount; i++)
                {
                    string[] axis1MemArr = axis1PosArr[i].Split(new string[] { __del2 }, StringSplitOptions.None);
                    for (int j = 0; j < _rowTupleMembers; j++)
                    {
                        string[] memProps = axis1MemArr[j].Split(new string[] { __del1 }, StringSplitOptions.None);
                        CellsetMember mem = new CellsetMember(i, j, memProps[0], memProps[1], int.Parse(memProps[2]), short.Parse(memProps[3]));
                        if (_rowMembers == null) // initialize
                            _rowMembers = new CellsetMember[_rowTupleMembers, _rowCount];

                        _rowMembers[j, i] = mem;
                    }
                }
            }



            //fifth part - cells
            if (_columnCount > 0 && _rowCount > 0)
            {
                // intialize cells
                _cells = new Cell[_columnCount, _rowCount];


                string[] axis0CellPosArr = level1Parts[4].Split(new string[] { __del3 }, StringSplitOptions.None);
                if (axis0CellPosArr.Length != _columnCount)
                    throw new Exception("Invalid cellsetString, axis0 cell pos count");
                for (int i = 0; i < _columnCount; i++)
                {
                    string[] axis1CellPosArr = axis0CellPosArr[i].Split(new string[] { __del2 }, StringSplitOptions.None);
                    for (int j = 0; j < _rowCount; j++)
                    {
                        string[] cellValues = axis1CellPosArr[j].Split(new string[] { __del1 }, StringSplitOptions.None);
                        _cells[i, j] = new Cell(cellValues[0], cellValues[1]);
                    }
                }
            }
        }

        public string BuildCSV()
        {
            if (_cells == null)
                throw new Exception("Cellset not loaded");

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < this.ColumnTupleMembers; i++)
            {
                // placeholders
                for (int j = 0; j < this.RowTupleMembers; j++)                
                    sb.Append("\t");                

                // column members
                for (int j = 0; j < this.RowCount; j++)
                {
                    sb.Append(this.GetColumnMember(i,j).Name);
                    sb.Append("\t");
                }
                
                sb.Append("\r\n");
            }


            for (int i = 0; i < this.ColumnCount; i++)
            {
                // column members
                for (int j = 0; j < this.ColumnTupleMembers; j++)
                {
                    sb.Append(this.GetRowMember(j, i).Name);
                    sb.Append("\t");
                }

                // cells
                for (int j = 0; j < this.RowCount; j++)
                {
                    sb.Append(this.GetCell(j, i).FormattedValue);
                    sb.Append("\t");
                }

                sb.Append("\r\n");
            }

            return sb.ToString();
        }

    }
}
