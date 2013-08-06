using System;
using System.Xml;
using System.Xml.Serialization;

namespace Mictlanix.Diverza.Client
{
	[Serializable]
	[XmlType(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
	internal enum DocumentoTipo
	{
		ZIP,
		XML
	}
}