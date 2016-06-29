using System;

namespace FI.BusinessObjects.Crystal
{
	/// <summary>
	/// Summary description for Parameter.
	/// </summary>
	public abstract class Parameter
	{
		protected string _caption="";
		protected string _oltpDatabase=null;
		public event EventHandler BeforeChange;

		public Parameter(string Caption , string OltpDatabase)
		{
			if(OltpDatabase==null || OltpDatabase.Trim()=="")
				throw new Exception("Incorrect parameter: OltpDatabase");

			_caption=Caption;
		}

		public Parameter(System.Xml.XmlElement el , string OltpDatabase)
		{
			if(OltpDatabase==null || OltpDatabase.Trim()=="")
				throw new Exception("Incorrect parameter: OltpDatabase");

			_caption=el.GetAttribute("CAPT");
		}

		protected internal void OnChange()
		{
			if (BeforeChange != null)
				BeforeChange(this,  EventArgs.Empty);
		}

		public string Caption
		{
			get { return _caption;}
			set
			{
				if(_caption!=value)
				{
					if(value==null || value.Trim()=="")
						throw new Exception("Caption cannot be empty");

					this.OnChange();
					_caption=value.Trim();
				}
			}
		}

		public string OltpDatabase
		{
			get { return _oltpDatabase;}
			set
			{
				if(_oltpDatabase!=value)
				{
					if(value==null || value.Trim()=="")
						throw new Exception("OltpDatabase value cannot be empty");

					this.OnChange();
					_oltpDatabase=value.Trim();
				}
			}
		}

		


		protected virtual void SaveToXml(System.Xml.XmlDocument doc, System.Xml.XmlElement el)
		{
			el.SetAttribute("CAPT" , this.Caption);
		}


		protected virtual void LoadFromXml(System.Xml.XmlElement el)
		{
			this._caption=el.GetAttribute("CAPT");
		}


	}
}
