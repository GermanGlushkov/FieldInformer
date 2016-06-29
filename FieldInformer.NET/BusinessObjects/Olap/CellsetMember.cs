using System;
using System.Collections;

namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for CellsetMember.
	/// </summary>
	public class CellsetMember:Object
	{
		int _pos;
		int _mPos;
		int _childCount=0;
		short _levelDepth=0;

		internal CellsetMember(int Pos, int MPos, string UniqueName , string Name , int ChildCount , short LevelDepth)
		{
			_pos=Pos;
			_mPos=MPos;
			_uniqueName=UniqueName;
			_name=Name;
			_childCount=ChildCount;
			_levelDepth=LevelDepth;
		}

		public int Pos
		{
			get{return _pos;}
		}

		public int MPos
		{
			get{return _mPos;}
		}

		public int ChildCount
		{
			get{return _childCount;}
		}

		public short LevelDepth
		{
			get{return _levelDepth;}
		}		

	}



	/*

	public class CellsetMembers:IEnumerable
	{
		protected ArrayList _list=new ArrayList();

		internal virtual void Add(CellsetMember CellsetMember)
		{
			_list.Add(CellsetMember);
		}


		public virtual int Count
		{
			get{return _list.Count;}
		}

		internal virtual void Clear()
		{
			_list.Clear();
		}

		public virtual CellsetMember this[int index]
		{
			get{return (CellsetMember)_list[index]; }
		}

		public CellsetMember FindLinear(int startIndex, string UniqueName)
		{
			for(int i=startIndex;i<this.Count;i++)
				if(this[i].UniqueName==UniqueName)
					return this[i];

			return null;
		}

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return _list.GetEnumerator();
		}
		#endregion

	}
	*/


}
