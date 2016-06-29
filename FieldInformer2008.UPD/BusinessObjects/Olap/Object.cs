using System;
using System.Collections;
using System.Collections.Specialized;

namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for Object.
	/// </summary>
	public abstract class Object
	{
		
		protected string _uniqueName;
		protected string _name;

		public event ObjectEventHandler BeforeChange;

		internal Object()
		{
		}

		protected internal void OnBeforeChange()
		{
			OnBeforeChange(this);
		}

		protected internal void OnBeforeChange(Object sender)
		{
			if (BeforeChange != null)
				BeforeChange(sender);
		}

		internal Object(string UniqueName)
		{
			_uniqueName=UniqueName;
		}		

		public virtual string UniqueName
		{
			get{return _uniqueName;}
		}

		public virtual string Name
		{
			get{return _name;}
		}

		public bool BelongsTo(Hierarchy hier)
		{
			return (this.UniqueName.StartsWith(hier.UniqueName));
		}
	}


//	#region Event Handlers

//	public delegate void EventHandler(Object sender, ObjectChangeEventArgs e);
//
//	public class ObjectChangeEventArgs:System.EventArgs
//	{
//		public enum Properties
//		{
//			AddMember,
//			RemoveMember,
//			ChangeMember,
//			Custom
//		}
//
//		private Properties _property=Properties.Custom;
//		private object _value;
//
//		public ObjectChangeEventArgs(Properties Property, object Value)
//		{
//			_property=Property;
//			_value=Value;
//		}
//		public Properties Property
//		{
//			get
//			{
//				return _property;
//			}
//		}
//		public object Value
//		{
//			get
//			{
//				return _value;
//			}
//		}
//	}
//	#endregion



	public delegate void ObjectEventHandler(Object sender);

	public class Objects: IEnumerable 
	{
		private ArrayList _list=new ArrayList();		
		private SortedList _sortedList=new SortedList(new CaseInsensitiveComparer());
		private Type _type=null;
		private bool _allowInheritedTypes=false;
		
		public event ObjectEventHandler BeforeChangeItem;
		public event ObjectEventHandler BeforeAdd;
		public event ObjectEventHandler BeforeRemove;		


		internal Objects()
		{
		}

		private void OnBeforeAdd(Object obj)
		{			
			if (BeforeAdd != null)
				BeforeAdd(obj);
		}

		private void OnBeforeRemove(Object obj)
		{			
			if (BeforeRemove != null)
				BeforeRemove(obj);
		}

		private void OnBeforeChangeItem(Object obj)
		{			
			if (BeforeChangeItem != null)
				BeforeChangeItem(obj);
		}

		private void SubscribeChange(Object obj)
		{
			obj.BeforeChange+=new ObjectEventHandler(Object_BeforeChange);
		}

		private void UnsubscribeChange(Object obj)
		{
			obj.BeforeChange-=new ObjectEventHandler(Object_BeforeChange);
		}
		
		private void Object_BeforeChange(Object sender)
		{
			if(BeforeChangeItem!=null)
				BeforeChangeItem(sender);
		}

		protected internal void SetCollectionType(Type type, bool allowInheritedTypes)
		{
			if(type==null || type.IsSubclassOf(typeof(Object))==false)
				throw new ArgumentException();

			for(int i=0;i<this.Count;i++)
				this.ValidateCollectionType(this[i], true);

			_type=type;
			_allowInheritedTypes=allowInheritedTypes;
		}

		public Type CollectionType
		{
			get { return _type;}
		}

		public bool AllowInheritedCollectionTypes
		{
			get { return _allowInheritedTypes;}
		}

		public bool ValidateCollectionType(Object obj, bool throwException)
		{
			if(_type==null)
				return true;;

			if(_allowInheritedTypes && obj.GetType().IsSubclassOf(_type))
				return true;
			else if(_type==obj.GetType())
				return true;

			if(throwException)
				throw new Exception("Collection type validation failed");
			return false;
		}
		
		virtual protected internal void Insert(int Index, Object Object, bool replaceExisting)
		{			
			if(Object==null)
				return;
			this.ValidateCollectionType(Object, true);

			if(replaceExisting)
			{
				int sortedIndex=_sortedList.IndexOfKey(Object.UniqueName);				
				if(sortedIndex>=0)
				{
					Object existObject=(Object)_sortedList.GetByIndex(sortedIndex);
					if(existObject!=Object)
					{
						this.OnBeforeRemove(existObject);
						this.OnBeforeAdd(Object);

						this.UnsubscribeChange(existObject);		
				
						_list[_list.IndexOf(existObject)]=Object; // slow linear search
						_sortedList.SetByIndex(sortedIndex, Object);

						this.SubscribeChange(Object);
					}
					return;
				}
			}

			this.OnBeforeAdd(Object);

			if(Index<0 || Index>=_list.Count )
			{
				_sortedList.Add(Object.UniqueName, Object); // this might give exception
				_list.Add(Object);
			}
			else
			{
				_sortedList.Add(Object.UniqueName, Object); // this might give exception
				_list.Insert(Index, Object);
			}			

			this.SubscribeChange(Object);
		}

		protected internal void Add(Object Object, bool replaceExisting)
		{
			this.Insert(this.Count , Object, replaceExisting);
		}

		public void Remove(string UniqueName)
		{
			int sortedIndex=_sortedList.IndexOfKey(UniqueName);
			if(sortedIndex>=0)
			{
				Object obj=(Object)_sortedList.GetByIndex(sortedIndex);

				this.OnBeforeRemove(obj);

				_sortedList.RemoveAt(sortedIndex);
				_list.Remove(obj);				

				this.UnsubscribeChange(obj);
			}
		}

		public void RemoveAt(int index)
		{			
			Object obj=this[index];
			
			this.OnBeforeRemove(obj);

			_sortedList.Remove(obj.UniqueName);
			_list.RemoveAt(index);

			this.UnsubscribeChange(obj);
		}
		
		public void SetIndex(int prevIndex, int newIndex)
		{
			if(prevIndex<0 || prevIndex>this.Count || prevIndex==newIndex)
				return;

			Object obj=(Object)_list[prevIndex];

			this.OnBeforeChangeItem(obj);

			_list.RemoveAt(prevIndex);
			_list.Insert( (newIndex<0 || newIndex>this.Count ? this.Count : newIndex) , obj); 			
		}

		public Object ReplaceAtIndex(int index, Object obj)
		{			
			Object prevObj=(Object)_list[index];
			if(prevObj==obj)
				return prevObj;
			
			this.OnBeforeRemove(prevObj);
			this.OnBeforeAdd(obj);
			
			this.UnsubscribeChange(prevObj);
			_sortedList.Remove(prevObj.UniqueName);
			try
			{
				_sortedList.Add(obj.UniqueName, obj); // this might give an exception				
			}
			catch(Exception exc)
			{
				_list.Remove(prevObj);
				throw exc;
			}
			_list[index]=obj;
			this.SubscribeChange(obj);

			return obj;
		}

		/// <summary>
		/// slow linear search
		/// </summary>
		public int IndexOf(string UniqueName)
		{
			for(int i=0;i<_list.Count;i++)
				if( ((Object)_list[i]).UniqueName==UniqueName)
					return i;
			return -1;
		}
		
		public Object this[int index]
		{
			get{return (Object)_list[index]; }
		}
		
		public Object this[string UniqueName]
		{
			get
			{
				return (Object)_sortedList[UniqueName];
			}
		}

		public int Count
		{
			get{return _list.Count;}
		}

		public virtual void Clear()
		{
			while(this.Count>0)
				this.RemoveAt(0);
		}

		protected Object[] ToArray(Type type)
		{
			return (Object[])_list.ToArray(type);
		}

		protected Object[] ToSortedByNameArray(Type type)
		{
			Object[] ret=this.ToArray(type);
			Array.Sort(ret, ObjectNameComparer.Inst);
			return ret;
		}

		protected Object[] ToSortedByUniqueNameArray(Type type)
		{
			Object[] ret=this.ToArray(type);
			Array.Sort(ret, ObjectUniqueNameComparer.Inst);
			return ret;
		}
		
		#region IEnumerable Members

			public IEnumerator GetEnumerator()
			{
				return _list.GetEnumerator();
			}
		#endregion

	}

	internal class ObjectNameComparer:IComparer
	{
		// singleton pattern
		private ObjectNameComparer(){}
		public static readonly ObjectNameComparer Inst=new ObjectNameComparer();
		// singleton pattern

		public int Compare(object x, object y)
		{
			return string.Compare( ((Object)x).Name, ((Object)y).Name);
		}
	}

	internal class ObjectUniqueNameComparer:IComparer
	{
		// singleton pattern
		private ObjectUniqueNameComparer(){}
		public static readonly ObjectUniqueNameComparer Inst=new ObjectUniqueNameComparer();
		// singleton pattern

		public int Compare(object x, object y)
		{
			return string.Compare( ((Object)x).UniqueName, ((Object)y).UniqueName);
		}
	}

}
