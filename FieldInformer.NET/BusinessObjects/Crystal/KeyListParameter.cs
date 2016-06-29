using System;
using System.Collections.Specialized;


namespace FI.BusinessObjects.Crystal
{
	/// <summary>
	/// Summary description for KeyListParameter.
	/// </summary>
	public abstract class KeyListParameter:Parameter
	{
		protected StringCollection _keyList=new StringCollection();
		protected int _maxKeyCount=-1;


		public KeyListParameter(string Caption, int MaxKeyCount, string OltpDatabase):base(Caption, OltpDatabase)
		{
			_maxKeyCount=MaxKeyCount;
		}

		public KeyListParameter(System.Xml.XmlElement el, string OltpDatabase):base(el, OltpDatabase)
		{
			_caption=el.GetAttribute("MAXCNT");
		}

		public void AddKey(string key)
		{
			if(key==null || key.Trim()=="")
				throw new Exception("Invalid key value");

			if(_maxKeyCount>=0 && _maxKeyCount<=_keyList.Count)
				throw new Exception("Key count limit exceeded");

			if(_keyList.Contains(key)==false)
			{
				this.OnChange();
				_keyList.Add(key);
			}
		}

		public void RemoveKey(string key)
		{
			this.OnChange();
			_keyList.Remove(key);
		}

		public void ClearKeys()
		{
			this.OnChange();
			_keyList.Clear();
		}

		public int KeyCount
		{
			get { return _keyList.Count;}
		}

		public int MaxKeyCount
		{
			get { return _maxKeyCount;}
			set
			{
				if(_maxKeyCount!=value)
				{
					if(value<_keyList.Count)
						throw new Exception("Cannot set MaxKeyCount value less than KeyCount");

					this.OnChange();
					_maxKeyCount=value;
				}
			}
		}

		

		protected override void SaveToXml(System.Xml.XmlDocument doc, System.Xml.XmlElement el)
		{
			base.SaveToXml(doc, el);

			el.SetAttribute("MAXCNT" , this.MaxKeyCount.ToString());

			foreach(string key in this._keyList)
			{
				System.Xml.XmlElement childEl=doc.CreateElement("KEY");
				childEl.Value=key;
				el.AppendChild(childEl);
			}
		}


		protected override void LoadFromXml(System.Xml.XmlElement el)
		{
			base.LoadFromXml(el);

			this._maxKeyCount=int.Parse(el.GetAttribute("MAXCNT"));

			foreach(System.Xml.XmlElement childEl in el.ChildNodes)
			{
				if(childEl.Name=="KEY")
					this.AddKey(childEl.Value);
			}
		}

	}
}
