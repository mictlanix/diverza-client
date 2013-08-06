using System;
using System.Xml;
using System.Xml.Serialization;

namespace Mictlanix.Diverza.Client
{
	[Serializable]
	[XmlType(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
	internal partial class Documento
	{
		private byte[] archivoField;
		private string nombreArchivoField;
		private DocumentoTipo tipoField;
		private string versionField;
		
		public Documento()
		{
			this.versionField = "3.2";
			this.tipoField = DocumentoTipo.XML;
		}
		
		[XmlAttribute(DataType="base64Binary")]
		public byte[] Archivo
		{
			get
			{
				return this.archivoField;
			}
			set
			{
				this.archivoField = value;
			}
		}
		
		[XmlAttribute()]
		public string NombreArchivo
		{
			get
			{
				return this.nombreArchivoField;
			}
			set
			{
				this.nombreArchivoField = value;
			}
		}
		
		[XmlAttribute()]
		public DocumentoTipo Tipo
		{
			get
			{
				return this.tipoField;
			}
			set
			{
				this.tipoField = value;
			}
		}
		
		[XmlAttribute()]
		public string Version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
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

