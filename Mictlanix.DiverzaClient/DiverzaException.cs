using System;

namespace Mictlanix.DiverzaClient
{
	[Serializable]
	public class DiverzaClientException : Exception
    {
		public DiverzaClientException ()
        {
        }

		public DiverzaClientException (string message) : base (message)
        {
        }

		public DiverzaClientException (string code, string message) : base (message)
		{
			Code = code;
        }

		public string Code { get; private set; }
    }
}
