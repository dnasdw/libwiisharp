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
 
using System;
using System.Threading;
using System.Windows.Forms;
using libWiiSharp;
using System.IO;

namespace IosPatcher_Example
{
    public partial class IosPatcher_Example : Form
    {
        private IosPatcher iosPatcher = new IosPatcher();
        private bool[] patches = new bool[] { true, true, true };
        private delegate void objectInvoker(object obj);

        public IosPatcher_Example()
        {
            InitializeComponent();
        }

        private void IosPatcher_Example_Load(object sender, System.EventArgs e)
        {
            iosPatcher.Debug += new System.EventHandler<MessageEventArgs>(iosPatcher_Debug);
        }

        void iosPatcher_Debug(object sender, MessageEventArgs e)
        {
            objectInvoker v = new objectInvoker(this.updateLog);
            this.Invoke(v, e.Message);
        }

        void updateLog(object mes)
        {
            rtbLog.Text += mes + "\n";

            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.ScrollToCaret();
        }

        private void cb_CheckedChanged(object sender, EventArgs e)
        {
            patches[0] = cbFakesigning.Checked;
            patches[1] = cbEsIdentify.Checked;
            patches[2] = cbNandPermissions.Checked;
        }

        private void btnIosWadBrowse_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "WAD|*.wad";

            if (ofd.ShowDialog() == DialogResult.OK)
                tbIosWad.Text = ofd.FileName;
        }

        private void btnPatch_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbIosWad.Text) && (patches[0] || patches[1] || patches[2]))
            {
                rtbLog.Clear();

                Thread workThread = new Thread(new ParameterizedThreadStart(this.startPatching));
                workThread.Start(tbIosWad.Text);
            }
        }

        private void startPatching(object wadPath)
        {
            try
            {
                int patchCount = 0;

                WAD w = WAD.Load((string)wadPath);
                iosPatcher.LoadIOS(ref w);

                if (patches[0] && patches[1] && patches[2])
                {
                    patchCount = iosPatcher.PatchAll();
                }
                else
                {
                    if (patches[0]) patchCount += iosPatcher.PatchFakeSigning();
                    if (patches[1]) patchCount += iosPatcher.PatchEsIdentify();
                    if (patches[2]) patchCount += iosPatcher.PatchNandPermissions();
                }
                
                if (patchCount > 0)
                {
                    string newPath = Path.GetDirectoryName((string)wadPath) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension((string)wadPath) + "_patched.wad";
                    w.Save(newPath);
                    iosPatcher_Debug(null, new MessageEventArgs("\nPatched WAD was saved to: " + newPath));
                }
                else
                    iosPatcher_Debug(null, new MessageEventArgs("\nNo patches were made!"));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
