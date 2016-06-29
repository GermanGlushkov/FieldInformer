using System;

namespace FI.BusinessObjects
{
	/// <summary>
	/// Summary description for Object.
	/// </summary>
	public class Object
	{
		private bool _isNew=false;
		private bool _isDirty=false;
		private bool _isProxy=false;

		public Object(int ID)
		{
			_isProxy=true;
		}

		public bool IsNew
		{
			get {return false; }
		}

		public bool IsDirty
		{
			get {return false; }
		}

		public bool IsProxy
		{
			get {return false; }
		}

	}
}
