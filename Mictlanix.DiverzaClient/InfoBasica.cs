using System;
using System.Xml;
using System.Xml.Serialization;

namespace Mictlanix.Diverza.Client
{
	[Serializable]
	[XmlType(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
	[XmlRoot("InfoBasica", Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
	internal partial class InfoBasica
	{
		private string rfcEmisorField;
		private string rfcReceptorField;
		private string serieField;
		
		[XmlAttribute(Namespace="")]
		public string RfcEmisor {
			get {
				return this.rfcEmisorField;
			}
			set {
				this.rfcEmisorField = value;
			}
		}
		
		[XmlAttribute(Namespace="")]
		public string RfcReceptor {
			get {
				return this.rfcReceptorField;
			}
			set {
				this.rfcReceptorField = value;
			}
		}
		
		[XmlAttribute(Namespace="")]
		public string Serie {
			get {
				return this.serieField;
			}
			set {
				this.serieField = value;
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