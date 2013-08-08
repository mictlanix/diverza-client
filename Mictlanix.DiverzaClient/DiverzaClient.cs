//
// DiverzaClient.cs
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
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Mictlanix.CFDv32;
using Mictlanix.Diverza.Client.Internals;

namespace Mictlanix.Diverza.Client
{
	public class DiverzaClient
	{
		public static string DEVELOPMENT_URL = @"https://demotf.buzonfiscal.com/timbrado";
		public static string PRODUCTION_URL = @"https://tf.buzonfiscal.com/timbrado";

		public DiverzaClient (string url, X509Certificate2 cert)
		{
			TimeOut = 15 * 1000;
			Url = url;
			Certificate = cert;

			ServicePointManager.ServerCertificateValidationCallback = 
				(object sp, X509Certificate c, X509Chain r, SslPolicyErrors e) => true;
		}

		public DiverzaClient (string url) : this(url, null)
		{
		}

		public int TimeOut { get; set; }
		public string Url { get; set; }
		public X509Certificate2 Certificate { get; set; }

		/// Request a stamp for cfd to the PAC and returns it.
		/// <exception cref="DiverzaClientException">Unexpected results when requesting stamp.</exception>
		public TimbreFiscalDigital Stamp (string id, Comprobante cfd)
		{
			var env = CreateEnvelope (id, cfd);
			string response = TryRequest (env);
			
			if(response.Length == 0) {
				throw new DiverzaClientException ("Bad response format.");
			}

			var doc = SoapEnvelope.FromXml (response);

			if(doc.Body.Length == 0) {
				throw new DiverzaClientException ("Bad response format.");
			}

			if(doc.Body[0] is TimbreFiscalDigital) {
				var tfd = doc.Body[0] as TimbreFiscalDigital;
				return new TimbreFiscalDigital {
					UUID = tfd.UUID,
					FechaTimbrado = tfd.FechaTimbrado,
					selloCFD = tfd.selloCFD,
					noCertificadoSAT = tfd.noCertificadoSAT,
					selloSAT = tfd.selloSAT
				};
			}
			
			if(doc.Body[0] is SoapFault) {
				var fault = doc.Body[0] as SoapFault;
				throw new DiverzaClientException (fault.FaultCode, fault.FaultString);
			}

			return null;
		}

		SoapEnvelope CreateEnvelope(string id, Comprobante doc)
		{
			var request = new SoapEnvelope {
				Body = new RequestTimbradoCFD[] {
					new RequestTimbradoCFD {
						RefID = id,
						Documento = new Documento {
							Archivo = doc.ToXmlBytes (),
							Tipo = DocumentoTipo.XML,
							Version = doc.version
						},
						InfoBasica = new InfoBasica {
							RfcEmisor = doc.Emisor.rfc,
							RfcReceptor = doc.Receptor.rfc,
							Serie = doc.serie
						}
					}
				}
			};

			return request;
		}

		void DoGetRequest (string url, X509Certificate2 cert)
		{
			HttpWebRequest req = (HttpWebRequest) WebRequest.Create (url);

			if (cert != null) {
				req.ClientCertificates.Add (cert);
			}

			using(var resp = req.GetResponse ()) {
				using(var stream = resp.GetResponseStream ()) {
/*#if DEBUG
					using(var sr = new StreamReader (stream, Encoding.UTF8)) {
						System.Diagnostics.Debug.WriteLine (sr.ReadToEnd ());
					}
#endif*/
				}
			}
		}
		
		string DoPostRequest (string url, X509Certificate2 cert, byte[] data)
		{
			var req = (HttpWebRequest)WebRequest.Create(url);
			req.ContentType = "text/xml; charset=utf-8";
			req.Method = "POST";
			req.ContentLength = data.Length;
			
			if (cert != null) {
				req.ClientCertificates.Add (cert);
			}

			using(var stream = req.GetRequestStream ()) {
				stream.Write (data, 0, data.Length);
			}
			
			using(var resp = req.GetResponse ()) {
				using(var stream = resp.GetResponseStream ()) {
					using(var sr = new StreamReader (stream, Encoding.UTF8)) {
						return sr.ReadToEnd ();
					}
				}
			}
		}

		string TryRequest (SoapEnvelope env)
		{
			var bytes = env.ToXmlBytes ();
			int time_out = TimeOut > 1000 ? TimeOut : 1000;
			string response = string.Empty;
			var dt = DateTime.Now;

#if DEBUG
			System.Diagnostics.Debug.WriteLine (env.ToXmlString ());
#endif

			while((DateTime.Now - dt).TotalMilliseconds < time_out) {
				try {
					DoGetRequest (Url, Certificate);
					response = DoPostRequest (Url, Certificate, bytes);
					break;
				} catch(WebException ex) {
#if DEBUG
					//System.Diagnostics.Debug.WriteLine (ex);
#endif
					var hwr = ex.Response as HttpWebResponse;
					if (hwr != null && hwr.StatusCode == HttpStatusCode.InternalServerError) {
						using (var sr = new StreamReader(hwr.GetResponseStream())) {
							response = sr.ReadToEnd ();
							break;
						}
					}
				}
			}
			
#if DEBUG
			System.Diagnostics.Debug.WriteLine (response);
#endif

			return response;
		}
	}
}