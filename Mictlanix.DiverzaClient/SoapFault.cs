using System;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Web.Services.Protocols;
using Mictlanix.CFDv32;

namespace Mictlanix.Diverza.Client
{
	[Serializable]
	[XmlType(Namespace="http://schemas.xmlsoap.org/soap/envelope/")]
	[XmlRoot("Fault", Namespace="http://schemas.xmlsoap.org/soap/envelope/", IsNullable=false)]
	internal partial class SoapFault
	{
		string code;
		string fault_string;
		object detail;

		[XmlElement("faultcode", Form = XmlSchemaForm.Unqualified)]
		public string FaultCode {
			get { return code; }
			set { code = value; }
		}
		
		[XmlElement("faultstring", Form = XmlSchemaForm.Unqualified)]
		public string FaultString {
			get { return fault_string; }
			set { fault_string = value; }
		}
		
		[XmlElement("detail", Form = XmlSchemaForm.Unqualified)]
		public object Detail {
			get { return detail; }
			set { detail = value; }
		}
	}
}

