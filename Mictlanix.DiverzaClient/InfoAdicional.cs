using System;
using System.Xml;
using System.Xml.Serialization;

namespace Mictlanix.Diverza.Client
{
	[Serializable]
	[XmlType(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
	[XmlRoot("InfoAdicional", Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
	internal partial class InfoAdicional
	{
		private string atributoField;
		private string valorField;
		
		[XmlAttribute(Namespace="")]
		public string Atributo {
			get {
				return this.atributoField;
			}
			set {
				this.atributoField = value;
			}
		}
		
		[XmlAttribute(Namespace="")]
		public string Valor {
			get {
				return this.valorField;
			}
			set {
				this.valorField = value;
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
						new XmlQualifiedName("r", "http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")
					});
				}
				
				return xmlns;
			}
			set { xmlns = value; }
		}
	}
}

