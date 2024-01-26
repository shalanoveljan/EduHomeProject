using System;
namespace Karma.Service.Exceptions
{
	public class ItemNotFoundException:Exception
	{
		public ItemNotFoundException(string msg):base(msg)
		{

		}
	}
}

