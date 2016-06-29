using System;

namespace FI.BusinessObjects
{
	[Serializable]
	public class Distribution
	{
		public enum FrequencyTypeEnum
		{
			Weekdays,
			Weeks,
			Monthly
		}

		[Flags]
		public enum FrequencyValueEnum
		{
			Mon=1,
			Tue=2,
			Wed=4,
			Thu=8,
			Fri=16,
			Sat=32,
			Sun=64,

			Week1=128,
			Week2=256,
			Week3=512,
			Week4=1024,

			Monthly=2048
		}

		protected internal decimal _id=0;
		protected internal bool _isDirty=false;
		protected internal bool _isProxy=false;
		protected internal FrequencyTypeEnum _frequencyType=FrequencyTypeEnum.Monthly;
		protected internal FrequencyValueEnum _frequencyValue=FrequencyValueEnum.Monthly;
		protected internal Report.ExportFormat _format=Report.ExportFormat.HTML;
		protected internal User _owner=null;
		protected internal Contact _contact=null;
		protected internal Report _report=null;

		internal Distribution(decimal ID , User Owner)
		{
			_id=ID;
			_owner=Owner;
			_isProxy=true;
			_isDirty=false;
		}

		internal Distribution(User Owner , Report report , Contact contact, Report.ExportFormat format)
		{			
			_owner=Owner;
			this.Report=report;
			this.Contact=contact;
			this.Format=format;

			FI.Common.DataAccess.IDistributionsDA dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			_id=dacObj.InsertDistribution(_owner.ID, report.ID , contact.ID , report.GetTypeCode() , this.FrequencyType.ToString() , this.FrequencyValue.ToString(), (int)this.Format);

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

		public Contact Contact
		{
			get{return _contact;}
			set
			{
				if(this.Owner.ID!=value.Owner.ID)
					throw new Exception("Owner mismatch");

				OnChange();
				_contact=value;
			}
		}

		public Report Report
		{
			get{return _report;}
			set
			{
				if(this.Owner.ID!=value.Owner.ID)
					throw new Exception("Owner mismatch");

				OnChange();
				_report=value;
			}
		}

		public Report.ExportFormat Format
		{
			get{return _format;}
			set
			{ 
				if(_format==value)
					return;
				OnChange();
				_format=value;
			}
		}

		public FrequencyTypeEnum FrequencyType
		{
			get{return _frequencyType;}
			set
			{ 
				OnChange();
				_frequencyType=value;

				if(_frequencyType==FrequencyTypeEnum.Weekdays)
				{
					_frequencyValue=_frequencyValue & (FrequencyValueEnum.Mon | FrequencyValueEnum.Tue | FrequencyValueEnum.Wed | FrequencyValueEnum.Thu | FrequencyValueEnum.Fri | FrequencyValueEnum.Sat | FrequencyValueEnum.Sun);
					if(_frequencyValue==0)
						_frequencyValue=FrequencyValueEnum.Mon;
				}

				if(_frequencyType==FrequencyTypeEnum.Weeks)
				{
					_frequencyValue=_frequencyValue & (FrequencyValueEnum.Week1 | FrequencyValueEnum.Week2 | FrequencyValueEnum.Week3 | FrequencyValueEnum.Week4);
					if(_frequencyValue==0)
						_frequencyValue=FrequencyValueEnum.Week1;
				}

				if(_frequencyType==FrequencyTypeEnum.Monthly)
				{
					_frequencyValue=FrequencyValueEnum.Monthly;
				}

				_isDirty=true;
			}
		}

		public FrequencyValueEnum FrequencyValue
		{
			get{return _frequencyValue;}
			set
			{ 
				OnChange();
				_frequencyValue=value;

				if(_frequencyType==FrequencyTypeEnum.Weekdays)
					_frequencyValue=_frequencyValue & (FrequencyValueEnum.Mon | FrequencyValueEnum.Tue | FrequencyValueEnum.Wed | FrequencyValueEnum.Thu | FrequencyValueEnum.Fri | FrequencyValueEnum.Sat | FrequencyValueEnum.Sun);

				if(_frequencyType==FrequencyTypeEnum.Weeks)
					_frequencyValue=_frequencyValue & (FrequencyValueEnum.Week1 | FrequencyValueEnum.Week2 | FrequencyValueEnum.Week3 | FrequencyValueEnum.Week4);

				if(_frequencyType==FrequencyTypeEnum.Monthly)
					_frequencyValue=FrequencyValueEnum.Monthly;

				if(_frequencyValue==0)
					throw new Exception("Invalid Frequency Value");

				_isDirty=true;
			}
		}


		public void Fetch()
		{
			this.Validate(true);

			FI.Common.DataAccess.IDistributionsDA dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			System.Data.DataRow row=dacObj.ReadDistribution(this._owner.ID , this.ID).Rows[0];
			this._contact=_owner.ContactSystem.GetContact(int.Parse(row["ContactId"].ToString()), false);
			this._report=_owner.ReportSystem.GetReport(int.Parse(row["ReportId"].ToString()) , _owner.ReportSystem.GetReportType(int.Parse(row["ReportType"].ToString())) , false);
			this.FrequencyType=(Distribution.FrequencyTypeEnum)System.Enum.Parse(typeof(Distribution.FrequencyTypeEnum) , row["FrequencyType"].ToString());
			this.FrequencyValue=(Distribution.FrequencyValueEnum)System.Enum.Parse(typeof(Distribution.FrequencyValueEnum) , row["FrequencyValue"].ToString());
			this.Format=(Report.ExportFormat)((int)(byte)row["Format"]);

			this._isProxy=false;			
			this._isDirty=false;
		}

		public void Save()
		{
			this.Validate(true);
			if(this.IsProxy)
				throw new Exception("Cannot save proxy");

			FI.Common.DataAccess.IDistributionsDA dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			dacObj.UpdateDistribution(this.Owner.ID, this.ID , this.Report.ID , this.Contact.ID , this.Report.GetTypeCode() , this.FrequencyType.ToString() , this.FrequencyValue.ToString(), (int)this.Format ); 

			this._isDirty=false;
		}


		public static bool IsScheduledForDate(string freqTypeString, string freqValueString, System.DateTime date)
		{
			FrequencyTypeEnum freqType=(FrequencyTypeEnum)System.Enum.Parse(typeof(FrequencyTypeEnum) , freqTypeString);
			FrequencyValueEnum freqValue=(FrequencyValueEnum)System.Enum.Parse(typeof(FrequencyValueEnum) , freqValueString);
			
			return IsScheduledForDate(freqType, freqValue, date);
		}

		public static bool IsScheduledForDate(FrequencyTypeEnum freqType, FrequencyValueEnum freqValue, System.DateTime date)
		{
			if(freqType==FrequencyTypeEnum.Weekdays)
			{
				if(date.DayOfWeek==System.DayOfWeek.Monday && (freqValue & FrequencyValueEnum.Mon)==FrequencyValueEnum.Mon)
					return true;
				else if(date.DayOfWeek==System.DayOfWeek.Tuesday && (freqValue & FrequencyValueEnum.Tue)==FrequencyValueEnum.Tue)
					return true;
				else if(date.DayOfWeek==System.DayOfWeek.Wednesday && (freqValue & FrequencyValueEnum.Wed)==FrequencyValueEnum.Wed)
					return true;
				else if(date.DayOfWeek==System.DayOfWeek.Thursday && (freqValue & FrequencyValueEnum.Thu)==FrequencyValueEnum.Thu)
					return true;
				else if(date.DayOfWeek==System.DayOfWeek.Friday && (freqValue & FrequencyValueEnum.Fri)==FrequencyValueEnum.Fri)
					return true;
				else if(date.DayOfWeek==System.DayOfWeek.Saturday && (freqValue & FrequencyValueEnum.Sat)==FrequencyValueEnum.Sat)
					return true;
				else if(date.DayOfWeek==System.DayOfWeek.Sunday && (freqValue & FrequencyValueEnum.Sun)==FrequencyValueEnum.Sun)
					return true;
			}
			else if(freqType==FrequencyTypeEnum.Weeks)
			{
				
				if(date.DayOfWeek==System.DayOfWeek.Monday && date.Day>=1 && date.Day<=7 && (freqValue & FrequencyValueEnum.Week1)==FrequencyValueEnum.Week1)
					return true;
				else if(date.DayOfWeek==System.DayOfWeek.Monday && date.Day>=8 && date.Day<=14 && (freqValue & FrequencyValueEnum.Week2)==FrequencyValueEnum.Week2)
					return true;
				else if(date.DayOfWeek==System.DayOfWeek.Monday && date.Day>=15 && date.Day<=21 && (freqValue & FrequencyValueEnum.Week3)==FrequencyValueEnum.Week3)
					return true;
				else if(date.DayOfWeek==System.DayOfWeek.Monday && date.Day>=22 && date.Day<=28 && (freqValue & FrequencyValueEnum.Week4)==FrequencyValueEnum.Week4)
					return true;
			}
			else if(freqType==FrequencyTypeEnum.Monthly)
			{
				if(date.Day==1 && (freqValue & FrequencyValueEnum.Monthly)==FrequencyValueEnum.Monthly)
					return true;
			}

			return false;
		}

		public bool IsScheduledFor(System.DateTime date)
		{
			return IsScheduledForDate(this.FrequencyType, this.FrequencyValue, date);
		}

	}
}
