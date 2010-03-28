/* This file is part of the libWiiSharp - NusClient Example
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
 
namespace NusClient_Example
{
    partial class NusClient_Example
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
            this.tvTitles = new System.Windows.Forms.TreeView();
            this.btnStart = new System.Windows.Forms.Button();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.panStoreTypes = new System.Windows.Forms.Panel();
            this.cbStoreWad = new System.Windows.Forms.CheckBox();
            this.cbStoreDecrypted = new System.Windows.Forms.CheckBox();
            this.cbStoreEncrypted = new System.Windows.Forms.CheckBox();
            this.panStoreTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvTitles
            // 
            this.tvTitles.CheckBoxes = true;
            this.tvTitles.Dock = System.Windows.Forms.DockStyle.Top;
            this.tvTitles.Location = new System.Drawing.Point(0, 26);
            this.tvTitles.Name = "tvTitles";
            this.tvTitles.ShowNodeToolTips = true;
            this.tvTitles.Size = new System.Drawing.Size(450, 213);
            this.tvTitles.TabIndex = 0;
            this.tvTitles.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvTitles_AfterCheck);
            // 
            // btnStart
            // 
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnStart.Location = new System.Drawing.Point(0, 493);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(450, 37);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start Download";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pbProgress
            // 
            this.pbProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbProgress.Location = new System.Drawing.Point(0, 470);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(450, 23);
            this.pbProgress.TabIndex = 2;
            this.pbProgress.Value = 100;
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.SystemColors.Window;
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.rtbLog.Location = new System.Drawing.Point(0, 239);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(450, 230);
            this.rtbLog.TabIndex = 3;
            this.rtbLog.Text = "";
            // 
            // panStoreTypes
            // 
            this.panStoreTypes.Controls.Add(this.cbStoreWad);
            this.panStoreTypes.Controls.Add(this.cbStoreDecrypted);
            this.panStoreTypes.Controls.Add(this.cbStoreEncrypted);
            this.panStoreTypes.Dock = System.Windows.Forms.DockStyle.Top;
            this.panStoreTypes.Location = new System.Drawing.Point(0, 0);
            this.panStoreTypes.Name = "panStoreTypes";
            this.panStoreTypes.Size = new System.Drawing.Size(450, 26);
            this.panStoreTypes.TabIndex = 4;
            // 
            // cbStoreWad
            // 
            this.cbStoreWad.AutoSize = true;
            this.cbStoreWad.Checked = true;
            this.cbStoreWad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStoreWad.Location = new System.Drawing.Point(358, 6);
            this.cbStoreWad.Name = "cbStoreWad";
            this.cbStoreWad.Size = new System.Drawing.Size(80, 17);
            this.cbStoreWad.TabIndex = 0;
            this.cbStoreWad.Text = "Store WAD";
            this.cbStoreWad.UseVisualStyleBackColor = true;
            this.cbStoreWad.CheckedChanged += new System.EventHandler(this.cbStore_CheckedChanged);
            // 
            // cbStoreDecrypted
            // 
            this.cbStoreDecrypted.AutoSize = true;
            this.cbStoreDecrypted.Checked = true;
            this.cbStoreDecrypted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStoreDecrypted.Location = new System.Drawing.Point(179, 6);
            this.cbStoreDecrypted.Name = "cbStoreDecrypted";
            this.cbStoreDecrypted.Size = new System.Drawing.Size(143, 17);
            this.cbStoreDecrypted.TabIndex = 0;
            this.cbStoreDecrypted.Text = "Store Decrypted Content";
            this.cbStoreDecrypted.UseVisualStyleBackColor = true;
            this.cbStoreDecrypted.CheckedChanged += new System.EventHandler(this.cbStore_CheckedChanged);
            // 
            // cbStoreEncrypted
            // 
            this.cbStoreEncrypted.AutoSize = true;
            this.cbStoreEncrypted.Checked = true;
            this.cbStoreEncrypted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStoreEncrypted.Location = new System.Drawing.Point(12, 6);
            this.cbStoreEncrypted.Name = "cbStoreEncrypted";
            this.cbStoreEncrypted.Size = new System.Drawing.Size(142, 17);
            this.cbStoreEncrypted.TabIndex = 0;
            this.cbStoreEncrypted.Text = "Store Encrypted Content";
            this.cbStoreEncrypted.UseVisualStyleBackColor = true;
            this.cbStoreEncrypted.CheckedChanged += new System.EventHandler(this.cbStore_CheckedChanged);
            // 
            // NusClient_Example
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 530);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tvTitles);
            this.Controls.Add(this.panStoreTypes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NusClient_Example";
            this.Text = "libWiiSharp - NusClient Example";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NusClient_Example_FormClosing);
            this.Load += new System.EventHandler(this.NusClient_Example_Load);
            this.panStoreTypes.ResumeLayout(false);
            this.panStoreTypes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvTitles;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Panel panStoreTypes;
        private System.Windows.Forms.CheckBox cbStoreWad;
        private System.Windows.Forms.CheckBox cbStoreDecrypted;
        private System.Windows.Forms.CheckBox cbStoreEncrypted;
    }
}

