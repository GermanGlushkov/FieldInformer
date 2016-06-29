using System;

namespace FI.BusinessObjects
{
	[Serializable]
	public class Contact
	{
		public enum DistributionFormatEnum
		{
			MessageBody,
			Attachment,
			Body_And_Attachment
		}


		protected internal decimal _id=0;
		protected internal bool _isDirty=false;
		protected internal bool _isProxy=true;
		protected internal string _name="";
		protected internal string _email="";
		protected internal DistributionFormatEnum _distributionFormat=DistributionFormatEnum.MessageBody;
		protected internal User _owner=null;

		internal Contact(decimal ID , User Owner)
		{
			_id=ID;
			_owner=Owner;
			_isProxy=true;
			_isDirty=false;
		}

		internal Contact(User Owner)
		{
			_owner=Owner;

			FI.DataAccess.Contacts dacObj=DataAccessFactory.Instance.GetContactsDA();
			_id=dacObj.InsertContact(_owner.ID , this.Name , this.EMail , this.DistributionFormat.ToString());

			_isProxy=false;
			_isDirty=false;
		}

		private void OnChange()
		{
			_isDirty=true;
		}


		public bool IsDirty
		{
			get{return _isDirty;}
		}

		public bool IsProxy
		{
			get{ return _isProxy;}
		}

		public bool IsValid
		{
			get{ return Validate(false);}
		}


		public bool Validate(bool RaiseError)
		{
			if(_id==0)
				if(RaiseError)
					throw new Exception("Object is not valid");
				else
					return false;

			return true;
		}


		public decimal ID
		{
			get{return _id;}
		}

		public User Owner
		{
			get{return _owner;}
		}

		public string Name
		{
			get{return _name;}
			set
			{ 
				if(value.Trim()=="")
					throw new Exception("Name is not valid");

				OnChange();
				_name=value;
			}
		}

		public string EMail
		{
			get{return _email;}
			set
			{ 
				if(value.Trim()=="" || System.Text.RegularExpressions.Regex.IsMatch(value , "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*")==false)
						throw new Exception("EMail is not valid");

				OnChange();
				_email=value;
			}
		}


		public DistributionFormatEnum DistributionFormat
		{
			get{return _distributionFormat;}
			set
			{ 
				OnChange();
				_distributionFormat=value;
			}
		}


		public void Fetch()
		{
			this.Validate(true);

			FI.DataAccess.Contacts dacObj=DataAccessFactory.Instance.GetContactsDA();
			System.Data.DataRow row=dacObj.ReadContact(this._owner.ID , this.ID).Rows[0];
			this._name=(string)row["Name"];
			this._email=(string)row["EMail"];
			this._distributionFormat=(Contact.DistributionFormatEnum)System.Enum.Parse(typeof(Contact.DistributionFormatEnum) , (string)row["DistributionFormat"]);

			_isDirty=false;
			_isProxy=false;
		}

		public void Save()
		{
			this.Validate(true);
			if(this.IsProxy)
				throw new Exception("Cannot save proxy");

			FI.DataAccess.Contacts dacObj=DataAccessFactory.Instance.GetContactsDA();
			dacObj.UpdateContact(this.Owner.ID , this.ID , this.Name , this.EMail , this.DistributionFormat.ToString());

			_isDirty=false;
		}

	}
}
