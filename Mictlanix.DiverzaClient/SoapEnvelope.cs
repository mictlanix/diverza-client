using System;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Services.Protocols;
using Mictlanix.CFDv32;

namespace Mictlanix.Diverza.Client
{
	[Serializable]
	[XmlType(Namespace="http://schemas.xmlsoap.org/soap/envelope/")]
	[XmlRoot("Envelope", Namespace="http://schemas.xmlsoap.org/soap/envelope/", IsNullable=false)]
	internal partial class SoapEnvelope
	{
		private object[] bodyField;
		
		[XmlArray("Body")]
		[XmlArrayItem(typeof(RequestTimbradoCFD), Namespace="http://www.buzonfiscal.com/ns/xsd/bf/TimbradoCFD")]
		[XmlArrayItem(typeof(TimbreFiscalDigital), Namespace="http://www.sat.gob.mx/TimbreFiscalDigital")]
		[XmlArrayItem(typeof(SoapFault), Namespace="http://schemas.xmlsoap.org/soap/envelope/")]
		public object[] Body {
			get {
				return this.bodyField;
			}
			set {
				this.bodyField = value;
			}
		}
		
		XmlSerializerNamespaces xmlns;
		
		[XmlNamespaceDeclarations]
		public XmlSerializerNamespaces Xmlns
		{
			get
			{
				if (xmlns == null)
				{
					xmlns = new XmlSerializerNamespaces(new XmlQualifiedName[] {
						new XmlQualifiedName("s", "http://schemas.xmlsoap.org/soap/envelope/"),
						new XmlQualifiedName("r", "http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI"),
						new XmlQualifiedName("t", "http://www.buzonfiscal.com/ns/xsd/bf/TimbradoCFD")
					});
				}
				
				return xmlns;
			}
			set { xmlns = value; }
		}

		public MemoryStream ToXmlStream()
		{
			return CFDLib.Utils.SerializeToXmlStream(this, Xmlns);
		}
		
		public byte[] ToXmlBytes()
		{
			using (var ms = ToXmlStream()) {
				return ms.ToArray ();
			}
		}

		public string ToXmlString()
		{
			using (var ms = ToXmlStream()) {
				return Encoding.UTF8.GetString (ms.ToArray ());
			}
		}
		
		public static SoapEnvelope FromXml (string xml)
		{
			using(var ms = new MemoryStream (Encoding.UTF8.GetBytes(xml))) {
				return FromXml (ms);
			}
		}

		public static SoapEnvelope FromXml (Stream xml)
		{
			var xs = new XmlSerializer (typeof(SoapEnvelope));
			object obj = xs.Deserialize(xml);
			return obj as SoapEnvelope;
		}
	}
}

