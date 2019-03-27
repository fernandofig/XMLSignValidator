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
					Result.Text = "Valid";
					Result.ForeColor = Color.Green;
				} else {
					Result.Text = "Invalid";
					Result.ForeColor = Color.Red;
				}
			} catch (Exception e) {
				if (e is XmlException) {
					MessageBox.Show("Error parsing file as an XML.\n\nIs this a valid XML file?", "Error!");
				} else {
					MessageBox.Show(e.GetBaseException().Message, "Error!");
				}
			}
		}

		private void XmlDrop(object sender, DragEventArgs e) {
			var dropped = ((string[])e.Data.GetData(DataFormats.FileDrop));
			var files = dropped.ToList();

			if (!files.Any())
				return;

			if (files.Count > 1) {
				MessageBox.Show("Drop only 1 file!", "Warning");
				return;
			}

			FileAttributes attr = File.GetAttributes(files[0]);

			if (attr.HasFlag(FileAttributes.Directory) || Path.GetExtension(files[0]).ToLower() != ".xml") {
				MessageBox.Show("This application accepts only XML files", "Warning");
				return;
			}

			ValidaXml(files[0]);
		}

		private void XmlDragging(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
		}

		private bool SignVerify(XmlDocument document) {
			if (document == null) throw new ArgumentNullException(nameof(document), "XML document is null.");

			SignedXml signed = new SignedXml(document);
			XmlNodeList list = document.GetElementsByTagName("Signature");
			if (list == null)
				throw new CryptographicException($"This XML doesn't contain a signature.");
			if (list.Count > 1)
				throw new CryptographicException($"This XML contains more than one signature.");

			signed.LoadXml((XmlElement)list[0]);

			List<X509Certificate2> certs = new List<X509Certificate2>();

			foreach (KeyInfoX509Data clause in signed.KeyInfo) {
				foreach (X509Certificate2 cert in clause.Certificates) {
					certs.Add(cert);
				}
			}

			if (certs.Count == 0) throw new CryptographicException($"This XML doesn't contain a certificate.");

			if (certs.Count > 1) throw new CryptographicException($"This XML contains more than one certificate.");

			return signed.CheckSignature(certs[0], !validateCertCfg.Checked);
		}
	}
}
