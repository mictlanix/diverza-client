// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.17020
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

// 
//This source code was auto-generated by MonoXSD
//
namespace Schemas {
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/TimbradoCFD")]
    [System.Xml.Serialization.XmlRootAttribute("RequestTimbradoCFD", Namespace="http://www.buzonfiscal.com/ns/xsd/bf/TimbradoCFD", IsNullable=false)]
    public partial class RequestTimbradoCFDType {
        
        private string refIDField;
        
        private DocumentoType documentoField;
        
        private InfoBasicaType infoBasicaField;
        
        private InfoAdicionalType[] infoAdicionalField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
        public string RefID {
            get {
                return this.refIDField;
            }
            set {
                this.refIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DocumentoType Documento {
            get {
                return this.documentoField;
            }
            set {
                this.documentoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public InfoBasicaType InfoBasica {
            get {
                return this.infoBasicaField;
            }
            set {
                this.infoBasicaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("InfoAdicional")]
        public InfoAdicionalType[] InfoAdicional {
            get {
                return this.infoAdicionalField;
            }
            set {
                this.infoAdicionalField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
    [System.Xml.Serialization.XmlRootAttribute("Documento", Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI", IsNullable=true)]
    public partial class DocumentoType {
        
        private byte[] archivoField;
        
        private string nombreArchivoField;
        
        private DocumentoTypeTipo tipoField;
        
        private string versionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public byte[] Archivo {
            get {
                return this.archivoField;
            }
            set {
                this.archivoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public string NombreArchivo {
            get {
                return this.nombreArchivoField;
            }
            set {
                this.nombreArchivoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public DocumentoTypeTipo Tipo {
            get {
                return this.tipoField;
            }
            set {
                this.tipoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public string Version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
    public enum DocumentoTypeTipo {
        
        /// <remarks/>
        ZIP,
        
        /// <remarks/>
        XML,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
    [System.Xml.Serialization.XmlRootAttribute("InfoBasica", Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI", IsNullable=true)]
    public partial class InfoBasicaType {
        
        private string rfcEmisorField;
        
        private string rfcReceptorField;
        
        private string serieField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public string RfcEmisor {
            get {
                return this.rfcEmisorField;
            }
            set {
                this.rfcEmisorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public string RfcReceptor {
            get {
                return this.rfcReceptorField;
            }
            set {
                this.rfcReceptorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public string Serie {
            get {
                return this.serieField;
            }
            set {
                this.serieField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.buzonfiscal.com/ns/xsd/bf/RequestTimbraCFDI")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=true)]
    public partial class InfoAdicionalType {
        
        private string atributoField;
        
        private string valorField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public string Atributo {
            get {
                return this.atributoField;
            }
            set {
                this.atributoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public string Valor {
            get {
                return this.valorField;
            }
            set {
                this.valorField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.sat.gob.mx/TimbreFiscalDigital")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.sat.gob.mx/TimbreFiscalDigital", IsNullable=false)]
    public partial class TimbreFiscalDigital {
        
        private string versionField1;
        
        private string uUIDField;
        
        private System.DateTime fechaTimbradoField;
        
        private string selloCFDField;
        
        private string noCertificadoSATField;
        
        private string selloSATField;
        
        /// <remarks>
///Atributo requerido para la expresión de la versión del estándar del Timbre Fiscal Digital
///</remarks>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public string version {
            get {
                return this.versionField1;
            }
            set {
                this.versionField1 = value;
            }
        }
        
        /// <remarks>
///Atributo requerido para expresar los 36 caracteres del UUID de la transacción de timbrado
///</remarks>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public string UUID {
            get {
                return this.uUIDField;
            }
            set {
                this.uUIDField = value;
            }
        }
        
        /// <remarks>
///Atributo requerido para expresar la fecha y hora de la generación del timbre 
///</remarks>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public System.DateTime FechaTimbrado {
            get {
                return this.fechaTimbradoField;
            }
            set {
                this.fechaTimbradoField = value;
            }
        }
        
        /// <remarks>
///Atributo requerido para contener el sello digital del comprobante fiscal, que será timbrado. El sello deberá ser expresado cómo una cadena de texto en formato Base 64.
///</remarks>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public string selloCFD {
            get {
                return this.selloCFDField;
            }
            set {
                this.selloCFDField = value;
            }
        }
        
        /// <remarks>
///Atributo requerido para expresar el número de serie del certificado del SAT usado para el Timbre
///</remarks>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public string noCertificadoSAT {
            get {
                return this.noCertificadoSATField;
            }
            set {
                this.noCertificadoSATField = value;
            }
        }
        
        /// <remarks>
///Atributo requerido para contener el sello digital del Timbre Fiscal Digital, al que hacen referencia las reglas de resolución miscelánea aplicable. El sello deberá ser expresado cómo una cadena de texto en formato Base 64.
///</remarks>
        [System.Xml.Serialization.XmlAttributeAttribute(Namespace="")]
        public string selloSAT {
            get {
                return this.selloSATField;
            }
            set {
                this.selloSATField = value;
            }
        }
    }
}