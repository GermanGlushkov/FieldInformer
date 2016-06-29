using System;

namespace FI.BusinessObjects
{
	/// <summary>
	/// Summary description for ContactSystem.
	/// </summary>
	public class ContactSystem:MarshalByRefObject
	{
		protected User _owner=null;
		
		public event EventHandler BeforeDeleteContact;

		internal ContactSystem(User Owner)
		{
			_owner=Owner;
		}

		public User Owner
		{
			get { return _owner; }
		}


		public Contact GetContact(decimal ID , bool Fetch)
		{
			Contact contact=new Contact(ID , this._owner);
			if(Fetch)
				contact.Fetch();
			return contact;
		}


		public Contact NewContact()
		{
			return new Contact(this.Owner);
		}


		public void DeleteContact(Contact contact)
		{
			contact.Validate(true);

			if (BeforeDeleteContact != null)
				BeforeDeleteContact(contact, EventArgs.Empty);

			FI.DataAccess.Contacts dacObj=DataAccessFactory.Instance.GetContactsDA();
			dacObj.DeleteContact(contact.Owner.ID , contact.ID);

			contact=null;
		}


		public void DeleteAll()
		{
			FI.Common.Data.FIDataTable table=GetContactsPage(1, 100000, "", "");
			while(table!=null && table.Rows.Count>0)
			{
				foreach(System.Data.DataRow row in table.Rows)
				{
					Contact cnt=this.GetContact((decimal)row["id"], false);
					this.DeleteContact(cnt);
				}
				table=GetContactsPage(1, 100000, "", "");
			}
		}



		public FI.Common.Data.FIDataTable GetContactsPage(int CurrentPage , int RowCount , string FilterExpression , string SortExpression)
		{
			int StartIndex=(CurrentPage-1)*RowCount;

			FI.DataAccess.Contacts dacObj=DataAccessFactory.Instance.GetContactsDA();
            FI.Common.Data.FIDataTable table=dacObj.ReadContactsPage(_owner.ID, StartIndex , RowCount , FilterExpression , SortExpression);

			return table;
		}
		
	}
}
