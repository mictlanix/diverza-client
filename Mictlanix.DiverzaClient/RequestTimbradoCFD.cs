using System;
using System.Xml;
using System.Xml.Serialization;

namespace Mictlanix.DiverzaClient
{
	[Serializable]
	[XmlType(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/TimbradoCFD")]
	[XmlRoot("RequestTimbradoCFD", Namespace="http://www.buzonfiscal.com/ns/xsd/bf/TimbradoCFD", IsNullable=false)]
	public partial class RequestTimbradoCFD
	{
		private string refIDField;
		private Documento documentoField;
		private InfoBasica infoBasicaField;
		private InfoAdicional[] infoAdicionalField;
		
		[XmlAttribute(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
		public string RefID {
			get {
				return this.refIDField;
			}
			set {
				this.refIDField = value;
			}
		}
		
		[XmlElement(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
		public Documento Documento {
			get {
				return this.documentoField;
			}
			set {
				this.documentoField = value;
			}
		}
		
		[XmlElement(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
		public InfoBasica InfoBasica {
			get {
				return this.infoBasicaField;
			}
			set {
				this.infoBasicaField = value;
			}
		}
		
		[XmlElement("InfoAdicional", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public InfoAdicional[] InfoAdicional {
			get {
				return this.infoAdicionalField;
			}
			set {
				this.infoAdicionalField = value;
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
						new XmlQualifiedName("t", "http://www.buzonfiscal.com/ns/xsd/bf/TimbradoCFD")
					});
				}
				
				return xmlns;
			}
			set { xmlns = value; }
		}
	}
}

