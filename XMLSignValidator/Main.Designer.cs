﻿namespace XMLSignValidator {
	partial class Main {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.DropArea = new System.Windows.Forms.Panel();
			this.Result = new System.Windows.Forms.Label();
			this.validateCertCfg = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// DropArea
			// 
			this.DropArea.AllowDrop = true;
			this.DropArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DropArea.Location = new System.Drawing.Point(12, 12);
			this.DropArea.Name = "DropArea";
			this.DropArea.Size = new System.Drawing.Size(324, 86);
			this.DropArea.TabIndex = 0;
			this.DropArea.DragDrop += new System.Windows.Forms.DragEventHandler(this.XmlDrop);
			this.DropArea.DragOver += new System.Windows.Forms.DragEventHandler(this.XmlDragging);
			// 
			// Result
			// 
			this.Result.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Result.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Result.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Result.Location = new System.Drawing.Point(12, 127);
			this.Result.Name = "Result";
			this.Result.Size = new System.Drawing.Size(324, 25);
			this.Result.TabIndex = 1;
			this.Result.Text = "Drag && Drop an XML above!";
			this.Result.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// validateCertCfg
			// 
			this.validateCertCfg.AutoSize = true;
			this.validateCertCfg.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.validateCertCfg.Location = new System.Drawing.Point(82, 104);
			this.validateCertCfg.Name = "validateCertCfg";
			this.validateCertCfg.Size = new System.Drawing.Size(187, 16);
			this.validateCertCfg.TabIndex = 2;
			this.validateCertCfg.Text = "Validate Certificate as well as Signature";
			this.validateCertCfg.UseVisualStyleBackColor = true;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(348, 162);
			this.Controls.Add(this.validateCertCfg);
			this.Controls.Add(this.Result);
			this.Controls.Add(this.DropArea);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Main";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "XML Signature Validator";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel DropArea;
		private System.Windows.Forms.Label Result;
		private System.Windows.Forms.CheckBox validateCertCfg;
	}
}

