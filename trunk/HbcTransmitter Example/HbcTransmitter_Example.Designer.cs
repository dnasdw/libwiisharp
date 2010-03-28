/* This file is part of the libWiiSharp - HbcTransmitter Example
 * Copyright (C) 2009 Leathl
 * 
 * libWiiSharp is free software: you can redistribute it and/or
 * modify it under the terms of the GNU General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * libWiiSharp is distributed in the hope that it will be
 * useful, but WITHOUT ANY WARRANTY; without even the implied warranty
 * of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
 
namespace HbcTransmitter_Example
{
    partial class HbcTransmitter_Example
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbIpAddress = new System.Windows.Forms.Label();
            this.tbIpAddress = new System.Windows.Forms.TextBox();
            this.btnFileBrowse = new System.Windows.Forms.Button();
            this.lbFile = new System.Windows.Forms.Label();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.lbProtocol = new System.Windows.Forms.Label();
            this.cmbProtocol = new System.Windows.Forms.ComboBox();
            this.btnTransmit = new System.Windows.Forms.Button();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lbIpAddress
            // 
            this.lbIpAddress.AutoSize = true;
            this.lbIpAddress.Location = new System.Drawing.Point(12, 23);
            this.lbIpAddress.Name = "lbIpAddress";
            this.lbIpAddress.Size = new System.Drawing.Size(61, 13);
            this.lbIpAddress.TabIndex = 0;
            this.lbIpAddress.Text = "IP Address:";
            // 
            // tbIpAddress
            // 
            this.tbIpAddress.Location = new System.Drawing.Point(88, 20);
            this.tbIpAddress.Name = "tbIpAddress";
            this.tbIpAddress.Size = new System.Drawing.Size(235, 20);
            this.tbIpAddress.TabIndex = 1;
            // 
            // btnFileBrowse
            // 
            this.btnFileBrowse.Location = new System.Drawing.Point(296, 49);
            this.btnFileBrowse.Name = "btnFileBrowse";
            this.btnFileBrowse.Size = new System.Drawing.Size(27, 20);
            this.btnFileBrowse.TabIndex = 2;
            this.btnFileBrowse.Text = "...";
            this.btnFileBrowse.UseVisualStyleBackColor = true;
            this.btnFileBrowse.Click += new System.EventHandler(this.btnFileBrowse_Click);
            // 
            // lbFile
            // 
            this.lbFile.AutoSize = true;
            this.lbFile.Location = new System.Drawing.Point(12, 52);
            this.lbFile.Name = "lbFile";
            this.lbFile.Size = new System.Drawing.Size(26, 13);
            this.lbFile.TabIndex = 0;
            this.lbFile.Text = "File:";
            // 
            // tbFile
            // 
            this.tbFile.BackColor = System.Drawing.SystemColors.Window;
            this.tbFile.Location = new System.Drawing.Point(88, 49);
            this.tbFile.Name = "tbFile";
            this.tbFile.ReadOnly = true;
            this.tbFile.Size = new System.Drawing.Size(202, 20);
            this.tbFile.TabIndex = 1;
            // 
            // lbProtocol
            // 
            this.lbProtocol.AutoSize = true;
            this.lbProtocol.Location = new System.Drawing.Point(12, 81);
            this.lbProtocol.Name = "lbProtocol";
            this.lbProtocol.Size = new System.Drawing.Size(70, 13);
            this.lbProtocol.TabIndex = 0;
            this.lbProtocol.Text = "HBC Version:";
            // 
            // cmbProtocol
            // 
            this.cmbProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProtocol.FormattingEnabled = true;
            this.cmbProtocol.Items.AddRange(new object[] {
            "HBC >= 1.0.5 (JODI)",
            "HBC < 1.0.5 (HAXX)"});
            this.cmbProtocol.Location = new System.Drawing.Point(88, 78);
            this.cmbProtocol.Name = "cmbProtocol";
            this.cmbProtocol.Size = new System.Drawing.Size(235, 21);
            this.cmbProtocol.TabIndex = 3;
            // 
            // btnTransmit
            // 
            this.btnTransmit.Location = new System.Drawing.Point(15, 114);
            this.btnTransmit.Name = "btnTransmit";
            this.btnTransmit.Size = new System.Drawing.Size(308, 27);
            this.btnTransmit.TabIndex = 4;
            this.btnTransmit.Text = "Transmit";
            this.btnTransmit.UseVisualStyleBackColor = true;
            this.btnTransmit.Click += new System.EventHandler(this.btnTransmit_Click);
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(15, 114);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(308, 27);
            this.pbProgress.TabIndex = 5;
            this.pbProgress.Visible = false;
            // 
            // HbcTransmitter_Example
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 153);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.btnTransmit);
            this.Controls.Add(this.cmbProtocol);
            this.Controls.Add(this.btnFileBrowse);
            this.Controls.Add(this.tbFile);
            this.Controls.Add(this.tbIpAddress);
            this.Controls.Add(this.lbProtocol);
            this.Controls.Add(this.lbFile);
            this.Controls.Add(this.lbIpAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "HbcTransmitter_Example";
            this.Text = "libWiiSharp - HbcTransmitter Example";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HbcTransmitter_Example_FormClosing);
            this.Load += new System.EventHandler(this.HbcTransmitter_Example_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbIpAddress;
        private System.Windows.Forms.TextBox tbIpAddress;
        private System.Windows.Forms.Button btnFileBrowse;
        private System.Windows.Forms.Label lbFile;
        private System.Windows.Forms.TextBox tbFile;
        private System.Windows.Forms.Label lbProtocol;
        private System.Windows.Forms.ComboBox cmbProtocol;
        private System.Windows.Forms.Button btnTransmit;
        private System.Windows.Forms.ProgressBar pbProgress;
    }
}

