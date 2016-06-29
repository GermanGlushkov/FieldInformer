using System;

namespace FI.BusinessObjects
{

	public class User:MarshalByRefObject, IDisposable
	{

		protected FI.BusinessObjects.ContactSystem _contactSystem=null;
		protected FI.BusinessObjects.DistributionSystem _distributionSystem=null;
		protected FI.BusinessObjects.ReportSystem _reportSystem=null;
		protected decimal _id=0;
		protected string _name="";
		protected string _email="";
		protected string _logon="";
		protected string _password="";
		protected DateTime _passwordTimestamp=DateTime.Now;
		protected decimal _companyId=0;
		protected string _companyNameShort="";
		protected string _companyNameLong="";
		protected string _oltpDatabase="";

		protected byte _cssStyle=0;

		protected string _ipAddress="";
		protected string _sessionId="";

		protected bool _isAdmin=false;
		protected bool _isAdminAudit=false;
		protected bool _isLoggedIn=false;
		protected bool _isProxy=true;


		public User(decimal ID , bool AsProxy)
		{	
			if(AsProxy==false)
			{
				FI.Common.DataAccess.IUsersDA dacObj=DataAccessFactory.Instance.GetUsersDA();
				FI.Common.Data.FIDataTable table=dacObj.ReadUser(ID);
				if(table==null || table.Rows.Count==0)
					throw new Exception("Cannot authenticate by id:" + ID.ToString());

				LoadData(table);

				this._isProxy=false;
			}

			this._id=ID;

			if(this.IsNew==false)
			{
				_contactSystem=new ContactSystem(this);
				_reportSystem=new ReportSystem(this);
				_distributionSystem=new DistributionSystem(this); // after Contacts and Reports
			}
		}

		public User(string CompanyNameShort, string Logon, string Password)
		{
			FI.Common.DataAccess.IUsersDA dacObj=DataAccessFactory.Instance.GetUsersDA();			

			// read
			FI.Common.Data.FIDataTable table=dacObj.ReadUser(CompanyNameShort , Logon, Password);
			if(table.Rows.Count==0)
				throw new Exception("Incorrect login information");

			System.Data.DataRow row=table.Rows[0];
			this._id=(decimal)row["id"];
			LoadData(table);

			
			this._isProxy=false;

			_contactSystem=new ContactSystem(this);
			_reportSystem=new ReportSystem(this);
			_distributionSystem=new DistributionSystem(this); // after Contacts and Reports
		}


		public void Dispose()
		{
			try
			{
				if(this.ReportSystem!=null)
					this.ReportSystem.CancelExecutingReports();
			}
			catch
			{
				throw;
			}
		}


		public override string ToString()
		{
			return string.Format("User id: {0}, logon: {1}, company: {2}, ip: {3}", this.ID, this.Logon, this.CompanyNameShort, this.IPAddress);
		}

		private void LoadData(FI.Common.Data.FIDataTable table)
		{
			System.Data.DataRow row=table.Rows[0];
			if(table.Rows.Count==0)
				throw new Exception("Cannot load data");

			this._logon=(string)row["Logon"];
			this._password=(string)row["Password"];
			this._passwordTimestamp=(DateTime)row["PasswordTimestamp"];
			this._name=(string)row["Name"];
			this._email=(string)row["Email"];
			this._ipAddress=(string)row["ConnectionAddress"];
			this._sessionId=(string)row["SessionId"];
			this._companyId=(decimal)row["CompanyId"];
			this._isLoggedIn=(bool)row["IsLoggedIn"];
			this._companyNameShort=(string)row["CompanyNameShort"];
			this._companyNameLong=(string)row["CompanyNameLong"];
			this._isAdmin=(bool)row["IsAdmin"];
			this._oltpDatabase=(string)row["OltpDatabase"];
			this._oltpDatabase=(string)row["OltpDatabase"];
			this.CssStyle=(byte)(row["CssStyle"]==DBNull.Value ? (byte)0 : row["CssStyle"]);

			this._isAdminAudit=this._isAdmin;
		}


		
		private bool IsValidIp(string IpAddress)
		{
			if(IpAddress=="127.0.0.1")
				return true; //allow if localhost

			string[] ipAddressRanges=FI.Common.AppConfig.ReadSetting(this.CompanyNameShort , "" , "IPAccess").Split(new char[]{';'});
			if(ipAddressRanges.Length==1 && ipAddressRanges[0].Trim()=="")
				return true; // if empty, allow all

			for(int i=0;i<ipAddressRanges.Length;i++)
			{
				if(ipAddressRanges[i]=="")
					continue;

				string[] ipAddressRange=ipAddressRanges[i].Split(new char[]{'-'});
				if(ipAddressRange.Length>2)
					throw new Exception("Invalid ip range");

				string ipStart=ipAddressRange[0].Trim();
				if(ipAddressRange.Length==1)
					if(FI.Common.Net.CompareIpAddresses(IpAddress , ipStart)==0) //if not range and equal
						return true;
				else
					continue;

				string ipEnd=ipAddressRange[1].Trim();
				if(FI.Common.Net.CompareIpAddresses(IpAddress , ipStart)>=0 && FI.Common.Net.CompareIpAddresses(IpAddress , ipEnd)<=0) 
					return true;
			}

			return false;
		}
		


		public virtual decimal ID
		{	
			get 
			{ 
				return _id;
			} 
		}

		public virtual string Name
		{	
			get 
			{ 
				return _name;
			}
			set 
			{ 
				if(value.Trim()=="")
					throw new Exception("Name is not valid");

				_name=value; 
			}	
		}


		public virtual string Email
		{	
			get 
			{ 
				return _email;
			}
			set 
			{ 
				if(value.Trim()=="" || System.Text.RegularExpressions.Regex.IsMatch(value , "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*")==false)
					throw new Exception("EMail is not valid");

				_email=value; 
			}	
		}

		public virtual string IPAddress
		{	
			get 
			{ 
				return _ipAddress;
			}
		}

		public virtual string SessionId
		{	
			get 
			{ 
				return _sessionId;
			}
		}

		public virtual string Logon
		{	
			get 
			{ 
				return _logon;
			}
			set 
			{ 
				if(value.Trim()=="")
					throw new Exception("Logon is not valid");

				_logon=value;
			}	
		}

		public virtual string Password
		{	
			get 
			{ 
				return _password;
			}
			set 
			{ 
				if(value.Trim()=="")
					throw new Exception("Password is not valid");

				this._passwordTimestamp=DateTime.Now;

				_password=value;
			}	
		}


		public virtual DateTime PasswordTimestamp
		{	
			get 
			{ 
				return _passwordTimestamp;
			}
		}


		public bool PasswordExpired
		{
			get
			{
				if(_passwordTimestamp.AddMonths(1).CompareTo(DateTime.Now)<=0)
					return true;

				return false;
			}
		}

		public decimal CompanyId
		{	
			get 
			{ 
				return _companyId;
			}
		}


		public virtual string CompanyNameShort
		{	
			get 
			{ 
				return _companyNameShort;
			}
		}


		public virtual string CompanyNameLong
		{	
			get 
			{ 
				return _companyNameLong; 
			}
		}


		public virtual string OltpDatabase
		{	
			get 
			{ 
				return _oltpDatabase; 
			}
			set 
			{ 
				_oltpDatabase=value; 
			}	
		}

		public virtual byte CssStyle
		{	
			get 
			{ 
				return _cssStyle; 
			}
			set 
			{ 
				if(value<1 || value>3)
					value=3;
				_cssStyle=value; 
			}	
		}

		public virtual bool IsLoggedIn
		{	
			get 
			{ 
				return _isLoggedIn; 
			}
		}

		public virtual bool IsProxy
		{	
			get 
			{ 
				return _isProxy; 
			}
		}


		public virtual bool IsNew
		{	
			get 
			{ 
				return (this.ID==0?true:false); 
			}
		}


		public virtual bool IsAdmin
		{	
			get 
			{ 
				return this._isAdmin; 
			}
			set 
			{ 
				_isAdmin=value; 
			}	
		}


		public FI.BusinessObjects.ContactSystem ContactSystem
		{
			get { return _contactSystem; }
		}


		public FI.BusinessObjects.DistributionSystem DistributionSystem
		{
			get { return _distributionSystem; }
		}


		public FI.BusinessObjects.ReportSystem ReportSystem
		{
			get { return _reportSystem; }
		}


		public virtual void Login(bool Force, string IpAddress , string SessionId)
		{
			if(this.IsValidIp(IpAddress)==false)
				throw new Exception("Permission denied from address " + IpAddress);

			if(this.IsLoggedIn && this.SessionId!=SessionId && Force==false)
			{
				this._isLoggedIn=false;
				throw new Exception("User is already logged in");
			}

			// login
			this._isLoggedIn=true;
			this._ipAddress=IpAddress;
			this._sessionId=SessionId;
			FI.Common.DataAccess.IUsersDA dacObj=DataAccessFactory.Instance.GetUsersDA();
			dacObj.UpdateUserSession(this.ID , this.IPAddress , this.SessionId, this.IsLoggedIn); 
		}



		public virtual void Logout()
		{					
			if(CheckSessionValidity())
			{
				FI.Common.DataAccess.IUsersDA dacObj=DataAccessFactory.Instance.GetUsersDA();
				dacObj.UpdateUserSession(this.ID , this.IPAddress , this.SessionId, false); 
			}

			// dispose
			this.Dispose();

			// logout
			this._isLoggedIn=false;
			this._id=0;
			this._logon="";
			this._password="";
			this._name="";
			this._companyId=0;
			this._companyNameShort="";
			this._companyNameLong="";
			this._oltpDatabase="";
		}


		public bool CheckSessionValidity()
		{
			if(this.IsLoggedIn==false)
				return false;

			FI.Common.DataAccess.IUsersDA dacObj=DataAccessFactory.Instance.GetUsersDA();
			string sessionId=dacObj.GetUserCurrentSession(this.ID);
			
			return (sessionId==this.SessionId);
		}


		public void AuditPageHit()
		{
			if(this.IsLoggedIn==false)
				return;

			FI.Common.DataAccess.IUsersDA dacObj=DataAccessFactory.Instance.GetUsersDA();
			dacObj.InsertPageHitAudit(this.ID , this._companyId , this.IPAddress , this.SessionId , System.DateTime.Now);
			
		}




		public FI.Common.Data.FIDataTable GetSystemUsers()
		{
			FI.Common.DataAccess.IUsersDA dacObj=DataAccessFactory.Instance.GetUsersDA();
			return dacObj.ReadUsers(this._companyId);
		}
		


		public void SaveUser(User user)
		{
			if(this.IsNew || this.IsProxy)
				throw new Exception("Cannot use new or proxy to save");
			if(user.IsNew==false && user.IsProxy)
				throw new Exception("Cannot save proxy");
			if(this.IsAdmin==false && this.ID!=user.ID)
				throw new Exception("Permission denied");
			
			if(this.ID==user.ID)
			{
				//saving itself , must not assign itself as admin if wasn't admin
				if(this._isAdminAudit==false && user.IsAdmin)
					throw new Exception("Premission denied : IsAdmin property");
			}

			FI.Common.DataAccess.IUsersDA dacObj=DataAccessFactory.Instance.GetUsersDA();
			if(user.IsNew)
				user._id=dacObj.InsertUser(user._companyId , user.Logon , user.Password, user._passwordTimestamp , user.Name, user.Email , user.IsAdmin, user.CssStyle);
			else
				dacObj.UpdateUser(user.ID , user.Logon , user.Password , user._passwordTimestamp, user.Name, user.Email , user.IsAdmin, user.CssStyle);

			user._isProxy=false;
			user._isAdminAudit=user._isAdmin;
			if(this.ID==user.ID)
			{
				//update itself
				FI.Common.Data.FIDataTable table=dacObj.ReadUser(this.ID);
				this.LoadData(table);
			}
		}




		public User NewUser()
		{
			if(this.IsProxy)
				throw new Exception("Cannot use proxy");
			if(this.IsAdmin==false)
				throw new Exception("Permission denied");

			User user=new User(0 , true);
			user._companyId=this._companyId;
			return user;
		}

		public User GetUser(decimal ID , bool AsProxy)
		{
			if(AsProxy)
			{
				User user=new User(ID , true);
				user._companyId=this._companyId;
				return user;
			}
			else
			{
				if(this.IsProxy)
					throw new Exception("Cannot use proxy");
				if(this.IsAdmin==false && this.ID!=ID)
					throw new Exception("Permission denied");

				return new User(ID , false);
			}
		}

		public void DeleteUser(User user)
		{
			if(this.IsProxy)
				throw new Exception("Cannot use proxy");
			if(this.IsAdmin==false)
				throw new Exception("Permission denied");
			if(this.ID==user.ID)
				throw new Exception("Cannot delete itself");

			user.ContactSystem.DeleteAll();
			user.ReportSystem.DeleteAll(false);

			FI.Common.DataAccess.IUsersDA dacObj=DataAccessFactory.Instance.GetUsersDA();
			dacObj.DeleteUser(user.ID);
		}

	}
}
