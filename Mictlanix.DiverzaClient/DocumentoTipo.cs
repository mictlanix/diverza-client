using System;
using System.Xml;
using System.Xml.Serialization;

namespace Mictlanix.DiverzaClient
{
	[Serializable]
	[XmlType(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
	public enum DocumentoTipo
	{
		ZIP,
		XML
	}
}