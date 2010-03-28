/* This file is part of the libWiiSharp - IosPatcher Example
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
 
namespace IosPatcher_Example
{
    partial class IosPatcher_Example
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbIosWad = new System.Windows.Forms.TextBox();
            this.btnIosWadBrowse = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnPatch = new System.Windows.Forms.Button();
            this.cbFakesigning = new System.Windows.Forms.CheckBox();
            this.cbEsIdentify = new System.Windows.Forms.CheckBox();
            this.cbNandPermissions = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IOS Wad:";
            // 
            // tbIosWad
            // 
            this.tbIosWad.Location = new System.Drawing.Point(72, 6);
            this.tbIosWad.Name = "tbIosWad";
            this.tbIosWad.ReadOnly = true;
            this.tbIosWad.Size = new System.Drawing.Size(258, 20);
            this.tbIosWad.TabIndex = 1;
            // 
            // btnIosWadBrowse
            // 
            this.btnIosWadBrowse.Location = new System.Drawing.Point(336, 6);
            this.btnIosWadBrowse.Name = "btnIosWadBrowse";
            this.btnIosWadBrowse.Size = new System.Drawing.Size(28, 20);
            this.btnIosWadBrowse.TabIndex = 2;
            this.btnIosWadBrowse.Text = "...";
            this.btnIosWadBrowse.UseVisualStyleBackColor = true;
            this.btnIosWadBrowse.Click += new System.EventHandler(this.btnIosWadBrowse_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.SystemColors.Window;
            this.rtbLog.Location = new System.Drawing.Point(12, 55);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(352, 120);
            this.rtbLog.TabIndex = 3;
            this.rtbLog.Text = "";
            // 
            // btnPatch
            // 
            this.btnPatch.Location = new System.Drawing.Point(12, 181);
            this.btnPatch.Name = "btnPatch";
            this.btnPatch.Size = new System.Drawing.Size(352, 23);
            this.btnPatch.TabIndex = 4;
            this.btnPatch.Text = "Patch";
            this.btnPatch.UseVisualStyleBackColor = true;
            this.btnPatch.Click += new System.EventHandler(this.btnPatch_Click);
            // 
            // cbFakesigning
            // 
            this.cbFakesigning.AutoSize = true;
            this.cbFakesigning.Checked = true;
            this.cbFakesigning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFakesigning.Location = new System.Drawing.Point(72, 32);
            this.cbFakesigning.Name = "cbFakesigning";
            this.cbFakesigning.Size = new System.Drawing.Size(83, 17);
            this.cbFakesigning.TabIndex = 5;
            this.cbFakesigning.Text = "Fakesigning";
            this.cbFakesigning.UseVisualStyleBackColor = true;
            this.cbFakesigning.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cbEsIdentify
            // 
            this.cbEsIdentify.AutoSize = true;
            this.cbEsIdentify.Checked = true;
            this.cbEsIdentify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEsIdentify.Location = new System.Drawing.Point(163, 32);
            this.cbEsIdentify.Name = "cbEsIdentify";
            this.cbEsIdentify.Size = new System.Drawing.Size(80, 17);
            this.cbEsIdentify.TabIndex = 5;
            this.cbEsIdentify.Text = "ES_Identify";
            this.cbEsIdentify.UseVisualStyleBackColor = true;
            this.cbEsIdentify.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cbNandPermissions
            // 
            this.cbNandPermissions.AutoSize = true;
            this.cbNandPermissions.Checked = true;
            this.cbNandPermissions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNandPermissions.Location = new System.Drawing.Point(249, 32);
            this.cbNandPermissions.Name = "cbNandPermissions";
            this.cbNandPermissions.Size = new System.Drawing.Size(115, 17);
            this.cbNandPermissions.TabIndex = 5;
            this.cbNandPermissions.Text = "NAND Permissions";
            this.cbNandPermissions.UseVisualStyleBackColor = true;
            this.cbNandPermissions.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Patch:";
            // 
            // IosPatcher_Example
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 216);
            this.Controls.Add(this.cbNandPermissions);
            this.Controls.Add(this.cbEsIdentify);
            this.Controls.Add(this.cbFakesigning);
            this.Controls.Add(this.btnPatch);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btnIosWadBrowse);
            this.Controls.Add(this.tbIosWad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "IosPatcher_Example";
            this.Text = "libWiiSharp - IosPatcher Example";
            this.Load += new System.EventHandler(this.IosPatcher_Example_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIosWad;
        private System.Windows.Forms.Button btnIosWadBrowse;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnPatch;
        private System.Windows.Forms.CheckBox cbFakesigning;
        private System.Windows.Forms.CheckBox cbEsIdentify;
        private System.Windows.Forms.CheckBox cbNandPermissions;
        private System.Windows.Forms.Label label2;
    }
}

