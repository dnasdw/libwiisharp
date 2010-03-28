/* This file is part of the libWiiSharp - TPL Converter Example
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
 
namespace TPL_Converter_Example
{
    partial class TPL_Converter_Example
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabImageToTpl = new System.Windows.Forms.TabPage();
            this.btnToTpl = new System.Windows.Forms.Button();
            this.btnToTplOutputBrowse = new System.Windows.Forms.Button();
            this.btnToTplInputBrowse = new System.Windows.Forms.Button();
            this.tbToTplOutput = new System.Windows.Forms.TextBox();
            this.tbToTplInput = new System.Windows.Forms.TextBox();
            this.lbToTplOutput = new System.Windows.Forms.Label();
            this.lbToTplInput = new System.Windows.Forms.Label();
            this.tabTplToImage = new System.Windows.Forms.TabPage();
            this.btnFromTpl = new System.Windows.Forms.Button();
            this.btnFromTplOutputBrowse = new System.Windows.Forms.Button();
            this.btnFromTplInputBrowse = new System.Windows.Forms.Button();
            this.tbFromTplOutput = new System.Windows.Forms.TextBox();
            this.tbFromTplInput = new System.Windows.Forms.TextBox();
            this.lbFromTplOutput = new System.Windows.Forms.Label();
            this.lbFromTplInput = new System.Windows.Forms.Label();
            this.cmbToTplFormat = new System.Windows.Forms.ComboBox();
            this.lbToTplFormat = new System.Windows.Forms.Label();
            this.cmbFromTplTexture = new System.Windows.Forms.ComboBox();
            this.lbFromTplTexture = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabImageToTpl.SuspendLayout();
            this.tabTplToImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabImageToTpl);
            this.tabControl.Controls.Add(this.tabTplToImage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(338, 190);
            this.tabControl.TabIndex = 2;
            // 
            // tabImageToTpl
            // 
            this.tabImageToTpl.Controls.Add(this.cmbToTplFormat);
            this.tabImageToTpl.Controls.Add(this.btnToTpl);
            this.tabImageToTpl.Controls.Add(this.btnToTplOutputBrowse);
            this.tabImageToTpl.Controls.Add(this.btnToTplInputBrowse);
            this.tabImageToTpl.Controls.Add(this.tbToTplOutput);
            this.tabImageToTpl.Controls.Add(this.tbToTplInput);
            this.tabImageToTpl.Controls.Add(this.lbToTplFormat);
            this.tabImageToTpl.Controls.Add(this.lbToTplOutput);
            this.tabImageToTpl.Controls.Add(this.lbToTplInput);
            this.tabImageToTpl.Location = new System.Drawing.Point(4, 22);
            this.tabImageToTpl.Name = "tabImageToTpl";
            this.tabImageToTpl.Padding = new System.Windows.Forms.Padding(3);
            this.tabImageToTpl.Size = new System.Drawing.Size(330, 164);
            this.tabImageToTpl.TabIndex = 0;
            this.tabImageToTpl.Text = "Image to TPL";
            this.tabImageToTpl.UseVisualStyleBackColor = true;
            // 
            // btnToTpl
            // 
            this.btnToTpl.Location = new System.Drawing.Point(11, 123);
            this.btnToTpl.Name = "btnToTpl";
            this.btnToTpl.Size = new System.Drawing.Size(311, 28);
            this.btnToTpl.TabIndex = 4;
            this.btnToTpl.Text = "Convert";
            this.btnToTpl.UseVisualStyleBackColor = true;
            this.btnToTpl.Click += new System.EventHandler(this.btnToTpl_Click);
            // 
            // btnToTplOutputBrowse
            // 
            this.btnToTplOutputBrowse.Location = new System.Drawing.Point(297, 49);
            this.btnToTplOutputBrowse.Name = "btnToTplOutputBrowse";
            this.btnToTplOutputBrowse.Size = new System.Drawing.Size(25, 20);
            this.btnToTplOutputBrowse.TabIndex = 2;
            this.btnToTplOutputBrowse.Text = "...";
            this.btnToTplOutputBrowse.UseVisualStyleBackColor = true;
            this.btnToTplOutputBrowse.Click += new System.EventHandler(this.btnToTplOutputBrowse_Click);
            // 
            // btnToTplInputBrowse
            // 
            this.btnToTplInputBrowse.Location = new System.Drawing.Point(297, 16);
            this.btnToTplInputBrowse.Name = "btnToTplInputBrowse";
            this.btnToTplInputBrowse.Size = new System.Drawing.Size(25, 20);
            this.btnToTplInputBrowse.TabIndex = 2;
            this.btnToTplInputBrowse.Text = "...";
            this.btnToTplInputBrowse.UseVisualStyleBackColor = true;
            this.btnToTplInputBrowse.Click += new System.EventHandler(this.btnToTplInputBrowse_Click);
            // 
            // tbToTplOutput
            // 
            this.tbToTplOutput.Location = new System.Drawing.Point(85, 49);
            this.tbToTplOutput.Name = "tbToTplOutput";
            this.tbToTplOutput.ReadOnly = true;
            this.tbToTplOutput.Size = new System.Drawing.Size(206, 20);
            this.tbToTplOutput.TabIndex = 1;
            // 
            // tbToTplInput
            // 
            this.tbToTplInput.Location = new System.Drawing.Point(85, 16);
            this.tbToTplInput.Name = "tbToTplInput";
            this.tbToTplInput.ReadOnly = true;
            this.tbToTplInput.Size = new System.Drawing.Size(206, 20);
            this.tbToTplInput.TabIndex = 1;
            // 
            // lbToTplOutput
            // 
            this.lbToTplOutput.AutoSize = true;
            this.lbToTplOutput.Location = new System.Drawing.Point(8, 53);
            this.lbToTplOutput.Name = "lbToTplOutput";
            this.lbToTplOutput.Size = new System.Drawing.Size(65, 13);
            this.lbToTplOutput.TabIndex = 0;
            this.lbToTplOutput.Text = "Output TPL:";
            // 
            // lbToTplInput
            // 
            this.lbToTplInput.AutoSize = true;
            this.lbToTplInput.Location = new System.Drawing.Point(8, 20);
            this.lbToTplInput.Name = "lbToTplInput";
            this.lbToTplInput.Size = new System.Drawing.Size(66, 13);
            this.lbToTplInput.TabIndex = 0;
            this.lbToTplInput.Text = "Input Image:";
            // 
            // tabTplToImage
            // 
            this.tabTplToImage.Controls.Add(this.cmbFromTplTexture);
            this.tabTplToImage.Controls.Add(this.btnFromTpl);
            this.tabTplToImage.Controls.Add(this.btnFromTplOutputBrowse);
            this.tabTplToImage.Controls.Add(this.btnFromTplInputBrowse);
            this.tabTplToImage.Controls.Add(this.tbFromTplOutput);
            this.tabTplToImage.Controls.Add(this.tbFromTplInput);
            this.tabTplToImage.Controls.Add(this.lbFromTplTexture);
            this.tabTplToImage.Controls.Add(this.lbFromTplOutput);
            this.tabTplToImage.Controls.Add(this.lbFromTplInput);
            this.tabTplToImage.Location = new System.Drawing.Point(4, 22);
            this.tabTplToImage.Name = "tabTplToImage";
            this.tabTplToImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabTplToImage.Size = new System.Drawing.Size(330, 164);
            this.tabTplToImage.TabIndex = 1;
            this.tabTplToImage.Text = "TPL to Image";
            this.tabTplToImage.UseVisualStyleBackColor = true;
            // 
            // btnFromTpl
            // 
            this.btnFromTpl.Location = new System.Drawing.Point(11, 123);
            this.btnFromTpl.Name = "btnFromTpl";
            this.btnFromTpl.Size = new System.Drawing.Size(311, 28);
            this.btnFromTpl.TabIndex = 12;
            this.btnFromTpl.Text = "Convert";
            this.btnFromTpl.UseVisualStyleBackColor = true;
            this.btnFromTpl.Click += new System.EventHandler(this.btnFromTpl_Click);
            // 
            // btnFromTplOutputBrowse
            // 
            this.btnFromTplOutputBrowse.Location = new System.Drawing.Point(297, 49);
            this.btnFromTplOutputBrowse.Name = "btnFromTplOutputBrowse";
            this.btnFromTplOutputBrowse.Size = new System.Drawing.Size(25, 20);
            this.btnFromTplOutputBrowse.TabIndex = 9;
            this.btnFromTplOutputBrowse.Text = "...";
            this.btnFromTplOutputBrowse.UseVisualStyleBackColor = true;
            this.btnFromTplOutputBrowse.Click += new System.EventHandler(this.btnFromTplOutputBrowse_Click);
            // 
            // btnFromTplInputBrowse
            // 
            this.btnFromTplInputBrowse.Location = new System.Drawing.Point(297, 16);
            this.btnFromTplInputBrowse.Name = "btnFromTplInputBrowse";
            this.btnFromTplInputBrowse.Size = new System.Drawing.Size(25, 20);
            this.btnFromTplInputBrowse.TabIndex = 10;
            this.btnFromTplInputBrowse.Text = "...";
            this.btnFromTplInputBrowse.UseVisualStyleBackColor = true;
            this.btnFromTplInputBrowse.Click += new System.EventHandler(this.btnFromTplInputBrowse_Click);
            // 
            // tbFromTplOutput
            // 
            this.tbFromTplOutput.Location = new System.Drawing.Point(85, 49);
            this.tbFromTplOutput.Name = "tbFromTplOutput";
            this.tbFromTplOutput.ReadOnly = true;
            this.tbFromTplOutput.Size = new System.Drawing.Size(206, 20);
            this.tbFromTplOutput.TabIndex = 7;
            // 
            // tbFromTplInput
            // 
            this.tbFromTplInput.Location = new System.Drawing.Point(85, 16);
            this.tbFromTplInput.Name = "tbFromTplInput";
            this.tbFromTplInput.ReadOnly = true;
            this.tbFromTplInput.Size = new System.Drawing.Size(206, 20);
            this.tbFromTplInput.TabIndex = 8;
            // 
            // lbFromTplOutput
            // 
            this.lbFromTplOutput.AutoSize = true;
            this.lbFromTplOutput.Location = new System.Drawing.Point(8, 53);
            this.lbFromTplOutput.Name = "lbFromTplOutput";
            this.lbFromTplOutput.Size = new System.Drawing.Size(74, 13);
            this.lbFromTplOutput.TabIndex = 5;
            this.lbFromTplOutput.Text = "Output Image:";
            // 
            // lbFromTplInput
            // 
            this.lbFromTplInput.AutoSize = true;
            this.lbFromTplInput.Location = new System.Drawing.Point(8, 20);
            this.lbFromTplInput.Name = "lbFromTplInput";
            this.lbFromTplInput.Size = new System.Drawing.Size(57, 13);
            this.lbFromTplInput.TabIndex = 6;
            this.lbFromTplInput.Text = "Input TPL:";
            // 
            // cmbToTplFormat
            // 
            this.cmbToTplFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToTplFormat.FormattingEnabled = true;
            this.cmbToTplFormat.Items.AddRange(new object[] {
            "RGBA8",
            "RGB565",
            "RGB5A3",
            "IA8",
            "IA4",
            "I8",
            "I4"});
            this.cmbToTplFormat.Location = new System.Drawing.Point(85, 86);
            this.cmbToTplFormat.Name = "cmbToTplFormat";
            this.cmbToTplFormat.Size = new System.Drawing.Size(237, 21);
            this.cmbToTplFormat.TabIndex = 6;
            // 
            // lbToTplFormat
            // 
            this.lbToTplFormat.AutoSize = true;
            this.lbToTplFormat.Location = new System.Drawing.Point(8, 89);
            this.lbToTplFormat.Name = "lbToTplFormat";
            this.lbToTplFormat.Size = new System.Drawing.Size(42, 13);
            this.lbToTplFormat.TabIndex = 0;
            this.lbToTplFormat.Text = "Format:";
            // 
            // cmbFromTplTexture
            // 
            this.cmbFromTplTexture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromTplTexture.FormattingEnabled = true;
            this.cmbFromTplTexture.Location = new System.Drawing.Point(85, 85);
            this.cmbFromTplTexture.Name = "cmbFromTplTexture";
            this.cmbFromTplTexture.Size = new System.Drawing.Size(237, 21);
            this.cmbFromTplTexture.TabIndex = 14;
            this.cmbFromTplTexture.Visible = false;
            // 
            // lbFromTplTexture
            // 
            this.lbFromTplTexture.AutoSize = true;
            this.lbFromTplTexture.Location = new System.Drawing.Point(8, 88);
            this.lbFromTplTexture.Name = "lbFromTplTexture";
            this.lbFromTplTexture.Size = new System.Drawing.Size(46, 13);
            this.lbFromTplTexture.TabIndex = 5;
            this.lbFromTplTexture.Text = "Texture:";
            this.lbFromTplTexture.Visible = false;
            // 
            // TPL_Converter_Example
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 190);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TPL_Converter_Example";
            this.Text = "libWiiSharp - TPL Converter Example";
            this.Load += new System.EventHandler(this.TPL_Converter_Example_Load);
            this.tabControl.ResumeLayout(false);
            this.tabImageToTpl.ResumeLayout(false);
            this.tabImageToTpl.PerformLayout();
            this.tabTplToImage.ResumeLayout(false);
            this.tabTplToImage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabImageToTpl;
        private System.Windows.Forms.Button btnToTpl;
        private System.Windows.Forms.Button btnToTplOutputBrowse;
        private System.Windows.Forms.Button btnToTplInputBrowse;
        private System.Windows.Forms.TextBox tbToTplOutput;
        private System.Windows.Forms.TextBox tbToTplInput;
        private System.Windows.Forms.Label lbToTplOutput;
        private System.Windows.Forms.Label lbToTplInput;
        private System.Windows.Forms.TabPage tabTplToImage;
        private System.Windows.Forms.Button btnFromTpl;
        private System.Windows.Forms.Button btnFromTplOutputBrowse;
        private System.Windows.Forms.Button btnFromTplInputBrowse;
        private System.Windows.Forms.TextBox tbFromTplOutput;
        private System.Windows.Forms.TextBox tbFromTplInput;
        private System.Windows.Forms.Label lbFromTplOutput;
        private System.Windows.Forms.Label lbFromTplInput;
        private System.Windows.Forms.ComboBox cmbToTplFormat;
        private System.Windows.Forms.Label lbToTplFormat;
        private System.Windows.Forms.ComboBox cmbFromTplTexture;
        private System.Windows.Forms.Label lbFromTplTexture;
    }
}

