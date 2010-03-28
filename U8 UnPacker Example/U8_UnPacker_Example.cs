/* This file is part of the libWiiSharp - U8 UnPacker Example
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
using System.Threading;
using System.Windows.Forms;
using libWiiSharp;

namespace U8_UnPacker_Example
{
    public partial class Form1 : Form
    {
        private bool lz77 = false;
        private bool imd5 = false;
        private delegate void objectInvoker(object obj);

        public Form1()
        {
            InitializeComponent();
        }

        private void btnUnpackInputBrowse_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All|*.*";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                if (U8.IsU8(ofd.FileName))
                    tbUnpackInput.Text = ofd.FileName;
                else
                    MessageBox.Show("The selected file is not a U8 file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnUnpackOutputBrowse_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbUnpackOutput.Text = fbd.SelectedPath;
        }

        private void btnPackInputBrowse_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbPackInput.Text = fbd.SelectedPath;
        }

        private void btnPackOutputBrowse_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All|*.*";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbPackOutput.Text = sfd.FileName;
        }

        private void cbLz77_CheckedChanged(object sender, System.EventArgs e)
        {
            lz77 = cbLz77.Checked;
        }

        private void cbIMD5_CheckedChanged(object sender, System.EventArgs e)
        {
            imd5 = cbIMD5.Checked;
        }

        private void btnUnpack_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUnpackInput.Text) && !string.IsNullOrEmpty(tbUnpackOutput.Text))
            {
                Thread workThread = new Thread(new ParameterizedThreadStart(this.doUnpack));
                workThread.Start(new string[] { tbUnpackInput.Text, tbUnpackOutput.Text });
            }
        }

        private void doUnpack(object files)
        {
            setControls(false, false);
            string input = ((string[])files)[0];
            string output = ((string[])files)[1];

            try
            {
                U8 u = new U8();
                u.Progress += new EventHandler<System.ComponentModel.ProgressChangedEventArgs>(unpack_Progress);

                u.LoadFile(input);

                u.Extract(output);

                MessageBox.Show("Successfully unpacked U8 file to:\n" + output, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { setControls(true, false); }
        }

        private void btnPack_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbPackInput.Text) && !string.IsNullOrEmpty(tbPackOutput.Text))
            {
                Thread workThread = new Thread(new ParameterizedThreadStart(this.doPack));
                workThread.Start(new string[] { tbPackInput.Text, tbPackOutput.Text });
            }
        }

        private void doPack(object files)
        {
            setControls(false, true);
            string input = ((string[])files)[0];
            string output = ((string[])files)[1];

            try
            {
                U8 u = new U8();
                u.Progress += new EventHandler<System.ComponentModel.ProgressChangedEventArgs>(pack_Progress);

                u.CreateFromDirectory(input);

                u.Lz77Compress = lz77;
                if (imd5) u.AddHeaderImd5();

                u.Save(output);

                MessageBox.Show("Successfully packed U8 file to:\n" + output, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { setControls(true, true); }
        }

        private void setControls(bool enabled, bool pack)
        {
            this.Invoke(new objectInvoker(updateControls), enabled);

            if (pack)
                this.Invoke(new objectInvoker(updatePackProgressBar), !enabled);
            else
                this.Invoke(new objectInvoker(updateUnpackProgressBar), !enabled);
        }

        private void updateControls(object enabled)
        {
            bool state = (bool)enabled;

            btnUnpackInputBrowse.Enabled = state;
            btnUnpackOutputBrowse.Enabled = state;
            btnPackInputBrowse.Enabled = state;
            btnPackOutputBrowse.Enabled = state;
        }

        private void updateUnpackProgressBar(object visible)
        {
            pbUnpackProgress.Visible = (bool)visible;
        }

        private void updatePackProgressBar(object visible)
        {
            pbPackProgress.Visible = (bool)visible;
        }

        void unpack_Progress(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.Invoke(new objectInvoker(updateProgress), ((object)new object[] { e.ProgressPercentage, false }));
        }

        void pack_Progress(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.Invoke(new objectInvoker(updateProgress), ((object)new object[] { e.ProgressPercentage, true }));
        }

        private void updateProgress(object obj)
        {
            int pValue = (int)((object[])obj)[0];
            bool pack = (bool)((object[])obj)[1];

            if (pack)
                pbPackProgress.Value = pValue;
            else
                pbUnpackProgress.Value = pValue;
        }
    }
}
