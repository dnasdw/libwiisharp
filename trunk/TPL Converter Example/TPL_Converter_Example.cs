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

using System;
using System.Drawing;
using System.Windows.Forms;
using libWiiSharp;

namespace TPL_Converter_Example
{
    public partial class TPL_Converter_Example : Form
    {
        private TPL inputTpl;
        private Image inputImage;

        public TPL_Converter_Example()
        {
            InitializeComponent();
        }

        private void TPL_Converter_Example_Load(object sender, EventArgs e)
        {
            cmbToTplFormat.SelectedIndex = 0;
        }

        private void btnFromTplInputBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TPL|*.tpl";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try { inputTpl = TPL.Load(ofd.FileName); }
                catch { MessageBox.Show("The selected file is not a valid TPL!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                tbFromTplInput.Text = ofd.FileName;

                if (inputTpl.NumOfTextures > 1)
                {
                    cmbFromTplTexture.Items.Clear();

                    for (int i = 0; i < inputTpl.NumOfTextures; i++)
                        cmbFromTplTexture.Items.Add(i + 1);

                    cmbFromTplTexture.SelectedIndex = 0;

                    cmbFromTplTexture.Visible = true;
                    lbFromTplTexture.Visible = true;
                }
                else
                {
                    cmbFromTplTexture.Visible = false;
                    lbFromTplTexture.Visible = false;
                }
            }
        }

        private void btnFromTplOutputBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG|*.png|JPG|*.jpg|GIF|*.gif|BMP|*.bmp|All|*.png;*.jpg;*.gif;*.bmp";
            sfd.FilterIndex = 5;

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbFromTplOutput.Text = sfd.FileName;
        }

        private void btnToTplInputBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG|*.png|JPG|*.jpg|GIF|*.gif|BMP|*.bmp|All|*.png;*.jpg;*.gif;*.bmp";
            ofd.FilterIndex = 5;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try { inputImage = Image.FromFile(ofd.FileName); }
                catch { MessageBox.Show("The selected file is not a valid Image!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                tbToTplInput.Text = ofd.FileName;
            }
        }

        private void btnToTplOutputBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "TPL|*.tpl";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbToTplOutput.Text = sfd.FileName;
        }

        private void btnFromTpl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbFromTplInput.Text) && !string.IsNullOrEmpty(tbFromTplOutput.Text))
            {
                try
                {
                    int texture = 0;

                    if (inputTpl.NumOfTextures > 1)
                        texture = int.Parse(cmbFromTplTexture.SelectedItem.ToString()) - 1;

                    inputTpl.ExtractTexture(texture, tbFromTplOutput.Text);

                    MessageBox.Show("Successfully extracted image to:\n" + tbFromTplOutput.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnToTpl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbToTplInput.Text) && !string.IsNullOrEmpty(tbToTplOutput.Text))
            {
                try
                {
                    TPL_TextureFormat tplFormat = TPL_TextureFormat.RGBA8;

                    switch (cmbToTplFormat.SelectedIndex)
                    {
                        case 0:
                            tplFormat = TPL_TextureFormat.RGBA8; break;
                        case 1:
                            tplFormat = TPL_TextureFormat.RGB565; break;
                        case 2:
                            tplFormat = TPL_TextureFormat.RGB5A3; break;
                        case 3:
                            tplFormat = TPL_TextureFormat.IA8; break;
                        case 4:
                            tplFormat = TPL_TextureFormat.IA4; break;
                        case 5:
                            tplFormat = TPL_TextureFormat.I8; break;
                        case 6:
                            tplFormat = TPL_TextureFormat.I4; break;
                    }

                    TPL t = TPL.FromImage(inputImage, tplFormat);
                    t.Save(tbToTplOutput.Text);

                    MessageBox.Show("Successfully saved TPL to:\n" + tbToTplOutput.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
