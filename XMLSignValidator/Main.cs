using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;

namespace XMLSignValidator {
	public partial class Main : Form {
		public Main() {
			InitializeComponent();
		}

		private void ValidaXml(string arquivo) {
			try {
				XmlDocument x = new XmlDocument();
				x.Load(arquivo);

				bool valid = SignVerify(x);

				if (valid) {
					Result.Text = "Válido";
					Result.ForeColor = Color.Green;
				} else {
					Result.Text = "Inválido";
					Result.ForeColor = Color.Red;
				}
			} catch (Exception e) {
				if (e is XmlException) {
					MessageBox.Show("Erro ao parsear o arquivo como XML.\n\nEste arquivo é um XML válido?", "Erro!");
				} else {
					MessageBox.Show(e.GetBaseException().Message, "Erro!");
				}
			}
		}

		private void XmlDrop(object sender, DragEventArgs e) {
			var dropped = ((string[])e.Data.GetData(DataFormats.FileDrop));
			var files = dropped.ToList();

			if (!files.Any())
				return;

			if (files.Count > 1) {
				MessageBox.Show("Arraste apenas 1 arquivo!", "Atenção");
				return;
			}

			FileAttributes attr = File.GetAttributes(files[0]);

			if (attr.HasFlag(FileAttributes.Directory)) {
				MessageBox.Show("Esta aplicação aceita apenas arquivos XML", "Atenção");
				return;
			}

			if (Path.GetExtension(files[0]).ToLower() != ".xml") {
				MessageBox.Show("Esta aplicação aceita apenas arquivos XML", "Atenção");
				return;
			}

			ValidaXml(files[0]);
		}

		private void XmlDragging(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
		}

		private static bool SignVerify(XmlDocument document) {
			if (document == null) throw new ArgumentNullException(nameof(document), "XML document is null.");

			SignedXml signed = new SignedXml(document);
			XmlNodeList list = document.GetElementsByTagName("Signature");
			if (list == null)
				throw new CryptographicException($"O XML não possui uma assinatura.");
			if (list.Count > 1)
				throw new CryptographicException($"O XML contém mais de uma assinatura.");

			signed.LoadXml((XmlElement)list[0]);

			List<X509Certificate2> certs = new List<X509Certificate2>();

			foreach (KeyInfoX509Data clause in signed.KeyInfo) {
				foreach (X509Certificate2 cert in clause.Certificates) {
					certs.Add(cert);
				}
			}

			if (certs.Count == 0) throw new CryptographicException($"O XML não contém um certificado.");

			if (certs.Count > 1) throw new CryptographicException($"O XML contém mais de um certificado.");

			return signed.CheckSignature(certs[0], true);
		}
	}
}
