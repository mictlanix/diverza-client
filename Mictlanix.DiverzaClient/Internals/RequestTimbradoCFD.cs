//
// RequestTimbradoCFD.cs
//
// Author:
//       Eddy Zavaleta <eddy@mictlanix.com>
//
// Copyright (c) 2013 Eddy Zavaleta, Mictlanix, and contributors.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Xml;
using System.Xml.Serialization;

namespace Mictlanix.Diverza.Client.Internals
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
		public XmlSerializerNamespaces Xmlns {
			get {
				if (xmlns == null) {
					xmlns = new XmlSerializerNamespaces (new XmlQualifiedName[] {
						new XmlQualifiedName("t", "http://www.buzonfiscal.com/ns/xsd/bf/TimbradoCFD")
					});
				}
				
				return xmlns;
			}
			set { xmlns = value; }
		}
	}
}

