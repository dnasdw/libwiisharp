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
 
using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using libWiiSharp;
using System.Text.RegularExpressions;

namespace HbcTransmitter_Example
{
    public partial class HbcTransmitter_Example : Form
    {
        private delegate void objectInvoker(object obj);

        public HbcTransmitter_Example()
        {
            InitializeComponent();
        }

        private void HbcTransmitter_Example_Load(object sender, System.EventArgs e)
        {
            cmbProtocol.SelectedIndex = 0;
        }

        private void HbcTransmitter_Example_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnFileBrowse_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Wii Executables|*.dol;*.elf";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbFile.Text = ofd.FileName;
        }

        private void btnTransmit_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(tbIpAddress.Text) || string.IsNullOrEmpty(tbFile.Text)) return;

            Regex ipRegex = new Regex(@"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                                 @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                                 @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                                 @"([01]?\d\d?|2[0-4]\d|25[0-5])");

            if (!ipRegex.IsMatch(tbIpAddress.Text))
            { MessageBox.Show("Please enter a valid IP Address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            Thread workThread = new Thread(new ParameterizedThreadStart(transmitFile));
            workThread.Start(new string[] { tbIpAddress.Text, tbFile.Text, cmbProtocol.SelectedItem.ToString() });
        }

        private void transmitFile(object obj)
        {
            string ipAddress = ((string[])obj)[0];
            string file = ((string[])obj)[1];
            Protocol prot = ((string[])obj)[2].Contains("JODI") ? Protocol.JODI : Protocol.HAXX;

            setControls(false);

            HbcTransmitter transmitter = new HbcTransmitter(prot, ipAddress);
            transmitter.Progress += new System.EventHandler<System.ComponentModel.ProgressChangedEventArgs>(transmitter_Progress);

            if (!transmitter.TransmitFile(file))
            { MessageBox.Show(transmitter.LastError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            { MessageBox.Show(string.Format("Successfully transmitted file!{0}", (transmitter.CompressionRatio > 0) ? "\nCompression Ratio: " + transmitter.CompressionRatio + "%" : string.Empty), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            setControls(true);
        }

        void transmitter_Progress(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.Invoke(new objectInvoker(updateProgress), e.ProgressPercentage);
        }

        void updateProgress(object progressPercentage)
        {
            pbProgress.Value = (int)progressPercentage;
        }

        private void setControls(object enabled)
        {
            if (this.InvokeRequired)
            { this.Invoke(new objectInvoker(setControls), enabled); return; }

            bool state = (bool)enabled;

            btnFileBrowse.Enabled = state;
            cmbProtocol.Enabled = state;
            tbIpAddress.Enabled = state;
            tbFile.Enabled = state;
            pbProgress.Visible = !state;
        }
    }
}
