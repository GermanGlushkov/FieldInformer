using System;

namespace FI.BusinessObjects
{
	public abstract class Report:MarshalByRefObject
	{
		public enum ExportFormat
		{
			HTML,
			CSV
		}
		
		public enum SharingEnum
		{
			None=0,
			SnapshotSubscriber=3,
			InheriteSubscriber=4
		}

		public enum StateEnum
		{
			Closed=0,
			Open=1,
			Executing=2,
			Executed=3
		}

		public readonly byte MaxStateCount=20;

		protected internal decimal _id=0;
		protected internal bool _isProxy=false;
		protected internal bool _isDirty=false;
		protected internal bool _isSaved=false;
		protected internal bool _isLoaded=false;
		protected internal bool _isSelected=false;
		protected internal StateEnum _state=StateEnum.Closed;
		protected internal SharingEnum _sharing=SharingEnum.None;
		protected internal SharingEnum _maxSubscriberSharing=SharingEnum.None;
		protected internal string _name="";
		protected internal string _description="";
		protected internal User _owner=null;
		protected internal byte _undoStateCount=0;
		protected internal byte _redoStateCount=0;
		protected internal decimal _parentReportId=0;
		protected internal bool _lastExecCanceled;


		internal Report(decimal ID , User Owner)
		{
			_id=ID;
			_owner=Owner;
			_isProxy=true;
		}

		// ---- abstract members -----
		public abstract string Export(ExportFormat Format);
		protected abstract internal void _SaveHeader();
		public abstract void LoadHeader();
		protected abstract internal Report _Clone(string Name , string Description);
		protected abstract internal void _Open();
		protected abstract internal void _Close(bool SaveFromState);
		protected abstract internal void _Delete(bool DenyShared);
		protected abstract internal void _SaveState();
		protected abstract internal void _LoadState(short StateCode);
		protected abstract internal void _DeleteStates();
		protected abstract internal void _Execute();
		protected abstract internal void _ClearResult();

		protected abstract internal void _BeginExecute();
		protected abstract internal void _EndExecute();
		protected abstract internal void _CancelExecute();
		// ---- abstract members -----


		// ---- events -----
		public event EventHandler StartExecuteEvent;
		public event EventHandler EndExecuteEvent;



		protected virtual void OnChangeReportHeader()
		{
			if(this.State==StateEnum.Closed)
				return; // it's ok, report's being opened

			if(this.SharingStatus==SharingEnum.InheriteSubscriber || this.SharingStatus==SharingEnum.SnapshotSubscriber)
				throw new Exception("Cannot change shared report");
		}
		
		protected virtual void OnChangeReport(bool ClearResult)
		{
			if(this.State==StateEnum.Closed)
				return; // it's ok, report's being opened
			
			if(this._state==Report.StateEnum.Executing)
				throw new Exception("Cannot change report properties while executing");

			// Allow changes to shared report. Saving is not allowed
//			if(this.SharingStatus==SharingEnum.InheriteSubscriber || this.SharingStatus==SharingEnum.SnapshotSubscriber)
//				throw new Exception("Cannot change shared report");

			if(ClearResult)
				this.ClearResult();

			this._isDirty=true;
			this._isSaved=false;
		}
		

		public virtual void Export(string Path, ExportFormat Format)
		{
			System.IO.FileStream fs=null;
			System.IO.StreamWriter sw=null; 

			try
			{
				fs = new System.IO.FileStream(Path, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
				sw = new System.IO.StreamWriter(fs , System.Text.Encoding.Unicode);
				sw.BaseStream.Seek(0, System.IO.SeekOrigin.End);
				sw.Write(this.Export(Format));
				sw.Flush();
			}
			finally
			{
				if(sw!=null)
					sw.Close();

				if(fs!=null)
					fs.Close();
			}
		}


		public virtual void SaveHeader()
		{
			if(this.IsProxy)
				throw new Exception("Cannot save proxy header");

			this._SaveHeader();	
		}

		public virtual void Clone(string Name , string Description)
		{
			this.Validate(true);
			if(this.IsProxy)
				throw new Exception("Cannot clone proxy");

			this._Clone(Name, Description);
		}

		public virtual void Open()
		{
			if(this.State==StateEnum.Open)
				return;

			this.Validate(true);

			/*
			if(this.IsNew)
				throw new Exception("Cannot fetch new object");
				*/

			// cleanup results
			this.ClearResult();	
			//_isSelected=true;

			this._Open();

			_isProxy=false;
			_isDirty=false;

			if(UndoStateCount>0 || RedoStateCount>0)
				_isSaved=false;
			else
				_isSaved=true;

			if(UndoStateCount<=0)
				this.SaveState(true);

			_state=StateEnum.Open;
		}


		public virtual void Close(bool SaveFromState)
		{
			if(this.State==StateEnum.Closed)
				return;

			this.Validate(true);

			//if(this.State==StateEnum.Closed)
			//throw new Exception("Already closed");

			//_isSelected=false;

			if(SaveFromState)
			{
				if(this.SharingStatus==SharingEnum.InheriteSubscriber || this.SharingStatus==SharingEnum.SnapshotSubscriber)
					throw new Exception("Cannot save shared report");
			}

			this._Close(SaveFromState);
			this._DeleteStates();

			_isSaved=true;
			this._state=StateEnum.Closed;
			this._undoStateCount=0;
			this._redoStateCount=0;
		}




		protected virtual void LoadState(short StateCode)
		{
			this.Validate(true);
			if(this.IsProxy)
				throw new Exception("Cannot load state of proxy");

			/*
			if(this.IsNew)
				throw new Exception("Cannot load state of new");
				*/

			if(StateCode<0 && this.UndoStateCount<=0)
				throw new Exception("No state available");
			else if(StateCode>0 && this.RedoStateCount<=0)
				throw new Exception("No state available");

			this.OnChangeReport(true);

			this._LoadState(StateCode);

			this._isDirty=false;
		}

		public void SaveState()
		{
			SaveState(false);
		}

		public void SaveState(bool Force)
		{
			this.Validate(true);
			if(this.IsProxy)
				throw new Exception("Cannot save state of proxy");

			/*
			if(this.IsNew)
				throw new Exception("Cannot save state of new");
				*/

			if(this.IsDirty==false && Force==false)
				return;

			_SaveState();

			this._redoStateCount=0;
			this._isDirty=false;
		}


		protected void OnStartExecute()
		{
			if (StartExecuteEvent != null)
				StartExecuteEvent(this, EventArgs.Empty);
		}

		protected void OnEndExecute()
		{
			if (EndExecuteEvent != null)
				EndExecuteEvent(this, EventArgs.Empty);
		}



		public virtual void Execute()
		{
			if(this.IsProxy)
				throw new Exception("Cannot execute Proxy");

			this.ClearResult();

			this.OnStartExecute();

			try
			{
				_lastExecCanceled=false;
				this._Execute();
			}
			catch
			{
				this.OnEndExecute();
				throw;
			}

			_state=StateEnum.Executed;
			this.OnEndExecute();
		}


		// async execute 
		public virtual void BeginExecute()
		{
			if(_state==Report.StateEnum.Closed)
				throw new Exception("Report is not open");
			else if(_state==Report.StateEnum.Executing)
				throw new Exception("Report is still executing");

			this.OnStartExecute();

			try
			{
				_lastExecCanceled=false;
				this._BeginExecute();
			}
			catch
			{
				this.OnEndExecute();
				throw;
			}

			_state=Report.StateEnum.Executing;
		}

		public virtual void EndExecute()
		{
			Report.StateEnum prevState=this._state;

			this._EndExecute();

			if(prevState==Report.StateEnum.Executing && this._state!=Report.StateEnum.Executing)
				this.OnEndExecute();
		}

		public virtual void CancelExecute()
		{
			this._CancelExecute();
			_state=Report.StateEnum.Open;
			_lastExecCanceled=true;

			this.OnEndExecute();
		}




		public virtual void ClearResult()
		{
			if(_state==StateEnum.Executing)
				throw new Exception("Cannot clear result while executing");

			this._ClearResult();

			if(_state==StateEnum.Executed)
				_state=StateEnum.Open;
		}

		public bool IsSelected
		{
			get{ return _isSelected;}
			set{ _isSelected=value;}
		}

		public bool IsProxy
		{
			get{ return _isProxy;}
		}

		public bool IsDirty
		{
			get{ return _isDirty;}
		}

		public bool IsSaved
		{
			get{ return _isSaved;}
		}

		public bool IsValid
		{
			get{ return Validate(false);}
		}

		public bool IsLastExecutionCanceled
		{
			get{ return _lastExecCanceled;}
		}


		public virtual StateEnum State
		{
			get{ return _state; }
		}

		public virtual SharingEnum SharingStatus
		{
			get{ return _sharing; }
		}

		public virtual SharingEnum MaxSubscriberSharingStatus
		{
			get{ return _maxSubscriberSharing; }
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
				if(_name==value)
					return;

				OnChangeReportHeader();
				_name=value;
			}
		}

		public string Description
		{
			get{return _description;}
			set
			{ 
				if(_description==value)
					return;

				OnChangeReportHeader();
				_description=value;
			}
		}



		public byte UndoStateCount
		{
			get{return _undoStateCount;}
		}

		public byte RedoStateCount
		{
			get{return _redoStateCount;}
		}


		public void Undo()
		{
			LoadState(-1);
		}

		public void Redo()
		{
			LoadState(1);
		}


		public int GetTypeCode()
		{
			return this.Owner.ReportSystem.GetReportTypeCode(this.GetType());
		}
	}
}
