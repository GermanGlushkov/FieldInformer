using System;

namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for Exceptions.
	/// </summary>
	public class BrokenDependencyException:System.Exception
	{
		public BrokenDependencyException(string message):base(message)
		{}
	}


	public class InvalidMemberException:System.Exception
	{
		public InvalidMemberException(string message):base(message)
		{}
	}
}
