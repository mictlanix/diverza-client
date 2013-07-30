using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Security;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Text;
using Mictlanix.CFDv32;
using Mictlanix.DiverzaClient;

namespace Mictlanix.DiverzaClient.Test
{
	class Program
    {
		const string CSD_CERTIFICATE_FILE = "CSD01_AAA010101AAA.cer";
		const string CSD_PRIVATE_KEY_FILE = "CSD01_AAA010101AAA.key";
		const string CSD_PRIVATE_KEY_PWD = "12345678a";

		static DateTime TEST_DATE = new DateTime (2013, 07, 30, 14, 15, 16, DateTimeKind.Unspecified);
		
		static void Main(string[] args)
		{
			var url = "https://demotf.buzonfiscal.com/timbrado";
			var cert = new X509Certificate2 ("TestCert.pfx", "ZARE860422FD7");
			
			DummyTest (url, cert);
			//Test01 (url, cert);
			//Test02 (url, cert);
			//Test03 (url, cert);
			//Test04 (url, cert);
			//Test05 (url, cert);
			//Test06 (url, cert);
			//Test07 (url, cert);
			//Test08 (url, cert);
			//Test09 (url, cert);
		}

		static Comprobante CreateCFD()
		{
			var cfd = new Comprobante
			{
				tipoDeComprobante = ComprobanteTipoDeComprobante.ingreso,
				serie = "A",
				folio = "1",
				fecha = TEST_DATE,
				LugarExpedicion = "México, DF",
				metodoDePago = "Efectivo",
				formaDePago = "PAGO EN UNA SOLA EXHIBICION",
				TipoCambio = (1m).ToString(),
				Moneda = "MXN",
				noCertificado = "00001000000203341766",
				certificado = Convert.ToBase64String (File.ReadAllBytes(CSD_CERTIFICATE_FILE)),
				Emisor = new ComprobanteEmisor
				{
					rfc = "ZARE860422FD7",
					nombre = "Eddy Ezequiel Zavaleta Ruíz",
					RegimenFiscal = new ComprobanteEmisorRegimenFiscal[] {
						new ComprobanteEmisorRegimenFiscal {
							Regimen = "Régimen de las Personas Físicas con Actividades Empresariales y Profesionales"
						}
					},
				},
				Receptor = new ComprobanteReceptor
				{
					rfc = "AAA010101AAA",
					nombre = "Empresa Demo"
				},
				Impuestos = new ComprobanteImpuestos()
			};

			return cfd;
		}

		static void AddItem (Comprobante cfd, string code, string name, decimal qty, decimal amount)
		{
			int count = 1;

			if (cfd.Conceptos == null) {
				cfd.Conceptos = new ComprobanteConcepto [count];
			} else {
				count = cfd.Conceptos.Length + 1;
				var items = cfd.Conceptos;
				Array.Resize (ref items, count);
				cfd.Conceptos = items;
			}

			cfd.Conceptos[count-1] = new ComprobanteConcepto {
				cantidad = qty,
				unidad = "No Aplica",
				noIdentificacion = code,
				descripcion = name,
				valorUnitario = amount,
				importe = Math.Round(qty * amount, 2)
			};

			cfd.subTotal = cfd.Conceptos.Sum (x => x.importe);
			cfd.total = Math.Round (cfd.subTotal * 1.16m, 2);

			cfd.Impuestos.Traslados = new ComprobanteImpuestosTraslado[] {
				new ComprobanteImpuestosTraslado {
					impuesto = ComprobanteImpuestosTrasladoImpuesto.IVA,
					importe = cfd.total - cfd.subTotal,
					tasa = 16
				}
			};
		}

		static void AddItems (Comprobante cfd, string prefix, int count)
		{
			var sum = 0m;
			
			cfd.Conceptos = new ComprobanteConcepto[count];
			
			for(int i = 1; i <= count; i++) {
				cfd.Conceptos[i-1] = new ComprobanteConcepto {
					cantidad = i,
					unidad = "No Aplica",
					noIdentificacion = string.Format("P{0:D4}", i),
					descripcion = string.Format("{0} {1:D4}", prefix, i),
					valorUnitario = 5m * i,
					importe = 5m * i * i
				};
				sum += 5m * i * i;
			}
			
			cfd.subTotal = sum;
			cfd.total = Math.Round(sum * 1.16m, 2);
			
			cfd.Impuestos.Traslados = new ComprobanteImpuestosTraslado[] {
				new ComprobanteImpuestosTraslado {
					impuesto = ComprobanteImpuestosTrasladoImpuesto.IVA,
					importe = cfd.total - cfd.subTotal,
					tasa = 16
				}
			};
		}

		static void DummyTest (string url, X509Certificate2 cert)
		{
			var cfd = CreateCFD ();
			var cli = new DiverzaClient (url, cert);

			AddItems (cfd, "Producto", 3);
			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes(CSD_PRIVATE_KEY_PWD));

			var tfd = cli.Stamp ("WS00000002", cfd);
			Console.WriteLine(tfd.ToXmlString ());
			
			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (tfd);

			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());
		}

		static void Test01 (string url, X509Certificate2 cert)
		{
			var cfd = CreateCFD ();
			var cli = new DiverzaClient (url, cert);

			Console.WriteLine (" =============================== ");
			Console.WriteLine (" TEST 01 ");
			Console.WriteLine (" =============================== ");

			AddItems (cfd, "Producto", 3);
			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));

			var tfd = cli.Stamp ("TEST_01", cfd);

			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (tfd);

			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());

			Console.WriteLine (tfd.ToXmlString ());
			Console.WriteLine (tfd.ToString ());
		}
		
		static void Test02 (string url, X509Certificate2 cert)
		{
			var cfd = CreateCFD ();
			var cli = new DiverzaClient (url, cert);
			
			Console.WriteLine (" =============================== ");
			Console.WriteLine (" TEST 02 ");
			Console.WriteLine (" =============================== ");
			
			AddItems (cfd, "Producto & \" “ ' ‘ < >", 3);
			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));
			
			var tfd = cli.Stamp ("TEST_02", cfd);
			
			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (tfd);

			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());

			Console.WriteLine (tfd.ToXmlString ());
			Console.WriteLine (tfd.ToString ());
		}
		
		static void Test03 (string url, X509Certificate2 cert)
		{
			var cfd = CreateCFD ();
			var cli = new DiverzaClient (url, cert);
			
			Console.WriteLine (" =============================== ");
			Console.WriteLine (" TEST 03 ");
			Console.WriteLine (" =============================== ");
			
			AddItems (cfd, "Producto", 3);

			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (new LeyendasFiscales {
				Leyenda = new LeyendasFiscalesLeyenda[] {
					new LeyendasFiscalesLeyenda {
						disposicionFiscal="ISR",
						norma="ARTICULO 133",
						textoLeyenda="Efectos fiscales al pago"
					}
				}
			});

			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));
			
			var tfd = cli.Stamp ("TEST_03", cfd);
			
			cfd.Complemento.Add (tfd);

			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());

			Console.WriteLine (tfd.ToXmlString ());
			Console.WriteLine (tfd.ToString ());
		}

		static void Test04 (string url, X509Certificate2 cert)
		{
			var cfd = CreateCFD ();
			var cli = new DiverzaClient (url, cert);
			
			Console.WriteLine (" =============================== ");
			Console.WriteLine (" TEST 04 ");
			Console.WriteLine (" =============================== ");


			AddItems (cfd, "Producto", 1);
			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));
			
			var tfd = cli.Stamp ("TEST_04i", cfd);
			
			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (tfd);
			
			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());
			
			Console.WriteLine (tfd.ToXmlString ());
			Console.WriteLine (tfd.ToString ());

			cfd = CreateCFD ();
			cfd.serie = "NC";
			cfd.tipoDeComprobante = ComprobanteTipoDeComprobante.egreso;

			AddItems (cfd, "Producto", 1);
			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));
			
			tfd = cli.Stamp ("TEST_04e", cfd);
			
			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (tfd);
			
			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());
			
			Console.WriteLine (tfd.ToXmlString ());
			Console.WriteLine (tfd.ToString ());
			
			cfd = CreateCFD ();
			cfd.serie = "CP";
			cfd.tipoDeComprobante = ComprobanteTipoDeComprobante.traslado;
			
			AddItems (cfd, "Producto", 1);
			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));

			tfd = cli.Stamp ("TEST_04t", cfd);
			
			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (tfd);
			
			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());
			
			Console.WriteLine (tfd.ToXmlString ());
			Console.WriteLine (tfd.ToString ());
		}
		
		static void Test05 (string url, X509Certificate2 cert)
		{
			var cfd = CreateCFD ();
			var cli = new DiverzaClient (url, cert);
			
			Console.WriteLine (" =============================== ");
			Console.WriteLine (" TEST 05 ");
			Console.WriteLine (" =============================== ");
			
			AddItems (cfd, "Producto", 3);

			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (new LeyendasFiscales {
				Leyenda = new LeyendasFiscalesLeyenda[] {
					new LeyendasFiscalesLeyenda {
						disposicionFiscal="ISR",
						norma="ARTICULO 133",
						textoLeyenda="Efectos fiscales al pago"
					}
				}
			});
			
			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));

			var tfd = cli.Stamp ("TEST_05a", cfd);
			
			cfd.Complemento.Add (tfd);
			
			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());
			
			Console.WriteLine (tfd.ToXmlString ());
			Console.WriteLine (tfd.ToString ());

			cfd = CreateCFD ();
			cfd.fecha = cfd.fecha.AddDays(-3);
			AddItems (cfd, "Producto", 3);
			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));

			try {
				tfd = cli.Stamp ("TEST_05b", cfd);
			} catch(DiverzaClientException ex) {
				Console.WriteLine (ex);
			}

			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());
		}

		static void Test06 (string url, X509Certificate2 cert)
		{
			var cfd = CreateCFD ();
			var cli = new DiverzaClient (url, cert);
			
			Console.WriteLine (" =============================== ");
			Console.WriteLine (" TEST 06 ");
			Console.WriteLine (" =============================== ");
			
			AddItems (cfd, "Producto", 3);
			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));
			
			var tfd = cli.Stamp ("TEST_06", cfd);
			
			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (tfd);
			
			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());
			
			Console.WriteLine (tfd.ToXmlString ());
			Console.WriteLine (tfd.ToString ());
		}
		
		static void Test07 (string url, X509Certificate2 cert)
		{
			var cfd = CreateCFD ();
			var cli = new DiverzaClient (url, cert);
			
			Console.WriteLine (" =============================== ");
			Console.WriteLine (" TEST 07 ");
			Console.WriteLine (" =============================== ");
			
			AddItems (cfd, "Producto", 3);
			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));
			
			var tfd = cli.Stamp ("54xdfrt63", cfd);
			
			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (tfd);
			
			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());
			
			Console.WriteLine (tfd.ToXmlString ());
			Console.WriteLine (tfd.ToString ());

			cfd = CreateCFD ();
			AddItems (cfd, "Producto X", 3);
			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));
			
			tfd = cli.Stamp ("54xdfrt63", cfd);
			
			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (tfd);
			
			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());
			
			Console.WriteLine (tfd.ToXmlString ());
			Console.WriteLine (tfd.ToString ());
		}
		
		static void Test08 (string url, X509Certificate2 cert)
		{
			var cfd = CreateCFD ();
			var cli = new DiverzaClient (url, cert);
			
			Console.WriteLine (" =============================== ");
			Console.WriteLine (" TEST 08 ");
			Console.WriteLine (" =============================== ");
			
			AddItems (cfd, "Producto", 160);
			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));
			
			var tfd = cli.Stamp ("TEST_08", cfd);
			
			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (tfd);
			
			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());
			
			Console.WriteLine (tfd.ToXmlString ());
			Console.WriteLine (tfd.ToString ());
		}
		
		static void Test09 (string url, X509Certificate2 cert)
		{
			var cfd = CreateCFD ();
			var cli = new DiverzaClient (url, cert);
			
			Console.WriteLine (" =============================== ");
			Console.WriteLine (" TEST 09 ");
			Console.WriteLine (" =============================== ");

			AddItem (cfd, "P0001", "Producto", 1, 6000);
			cfd.formaDePago = "PAGO EN PARCIALIDADES";
			cfd.metodoDePago = "No Identificado";
			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));
			
			var tfd = cli.Stamp ("TEST_09", cfd);
			
			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (tfd);
			
			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());
			
			Console.WriteLine (tfd.ToXmlString ());
			Console.WriteLine (tfd.ToString ());

			cfd = CreateCFD ();
			cfd.serie = "P";
			cfd.folio = "1";
			cfd.fecha = TEST_DATE.AddHours (1);
			cfd.formaDePago = "PARCIALIDAD 1 de 2";
			cfd.metodoDePago = "No Identificado";
			cfd.SerieFolioFiscalOrig = "A";
			cfd.FolioFiscalOrig = "1";
			cfd.FechaFolioFiscalOrig = TEST_DATE;
			cfd.MontoFolioFiscalOrig = 6000;
			AddItem (cfd, "PA0001", "Pago Parcial", 1, 3000);

			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));
			
			tfd = cli.Stamp ("TEST_09a", cfd);
			
			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (tfd);
			
			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());
			
			Console.WriteLine (tfd.ToXmlString ());
			Console.WriteLine (tfd.ToString ());
			
			cfd = CreateCFD ();
			cfd.serie = "P";
			cfd.folio = "2";
			cfd.fecha = TEST_DATE.AddHours (2);
			cfd.formaDePago = "PARCIALIDAD 2 de 2";
			cfd.metodoDePago = "No Identificado";
			cfd.SerieFolioFiscalOrig = "A";
			cfd.FolioFiscalOrig = "1";
			cfd.FechaFolioFiscalOrig = TEST_DATE;
			cfd.MontoFolioFiscalOrig = 6000;
			AddItem (cfd, "PA0001", "Pago Parcial", 1, 3000);
			
			cfd.Sign (File.ReadAllBytes (CSD_PRIVATE_KEY_FILE), Encoding.UTF8.GetBytes (CSD_PRIVATE_KEY_PWD));
			
			tfd = cli.Stamp ("TEST_09b", cfd);
			
			cfd.Complemento = new List<object>();
			cfd.Complemento.Add (tfd);
			
			Console.WriteLine (cfd.ToXmlString ());
			Console.WriteLine (cfd.ToString ());
			
			Console.WriteLine (tfd.ToXmlString ());
			Console.WriteLine (tfd.ToString ());
		}

    }
}
