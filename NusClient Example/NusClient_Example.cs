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
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using libWiiSharp;

namespace NusClient_Example
{
    public partial class NusClient_Example : Form
    {
        private int checkedBoxes = 0;
        private NusClient nusClient = new NusClient();
        Thread dlThread;
        private bool[] storeTypes = new bool[] { true, true, true };
        private delegate void objectInvoker(object obj);

        public NusClient_Example()
        {
            InitializeComponent();
        }

        private void NusClient_Example_Load(object sender, EventArgs e)
        {
            LoadDatabase();
            nusClient.Debug += new EventHandler<MessageEventArgs>(nusClient_Debug);
            nusClient.Progress += new EventHandler<ProgressChangedEventArgs>(nusClient_Progress);
        }

        private void NusClient_Example_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void cbStore_CheckedChanged(object sender, EventArgs e)
        {
            storeTypes[0] = cbStoreEncrypted.Checked;
            storeTypes[1] = cbStoreDecrypted.Checked;
            storeTypes[2] = cbStoreWad.Checked;
        }

        void nusClient_Progress(object sender, ProgressChangedEventArgs e)
        {
            objectInvoker v = new objectInvoker(this.updateProgress);
            this.Invoke(v, e.ProgressPercentage);
        }

        void updateProgress(object newValue)
        {
            pbProgress.Value = (int)newValue;
        }

        void nusClient_Debug(object sender, MessageEventArgs e)
        {
            objectInvoker v = new objectInvoker(this.updateLog);
            this.Invoke(v, e.Message);
        }

        void updateLog(object mes)
        {
            rtbLog.Text += mes + "\n";

            if (((string)mes).ToLower().Contains("finished"))
                rtbLog.Text += "\n";

            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.ScrollToCaret();
        }

        void setControls(bool enabled)
        {
            objectInvoker v = new objectInvoker(this.updateControls);
            this.Invoke(v, enabled);
        }

        void updateControls(object enabled)
        {
            tvTitles.Enabled = (bool)enabled;
            cbStoreEncrypted.Enabled = (bool)enabled;
            cbStoreDecrypted.Enabled = (bool)enabled;
            cbStoreWad.Enabled = (bool)enabled;
            btnStart.Enabled = (bool)enabled;
        }

        private void uncheckNodes()
        {
            foreach (TreeNode thisNode in tvTitles.Nodes)
                uncheckNodes(thisNode);
        }

        private void uncheckNodes(TreeNode parentNode)
        {
            parentNode.Checked = false;

            foreach (TreeNode childNode in parentNode.Nodes)
                uncheckNodes(childNode);
        }

        private void tvTitles_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
                if (e.Node.Checked) checkedBoxes++;
                else checkedBoxes--;
            }
            else
            {
                foreach (TreeNode childNode in e.Node.Nodes)
                    childNode.Checked = e.Node.Checked;
            }
        }

        private void getList(TreeNode parentNode, List<string[]> titles)
        {
            if (parentNode.Nodes.Count > 0)
            {
                foreach (TreeNode childNode in parentNode.Nodes)
                    getList(childNode, titles);
            }
            else if (parentNode.Checked)
            {
                string titleId = parentNode.Parent.Text.Substring(parentNode.Parent.Text.IndexOf('(') + 1).Replace(")", string.Empty);
                string titleVersion = parentNode.Text.ToLower().StartsWith("v") ? parentNode.Text.Substring(1) : string.Empty;

                if (titleVersion.Contains(" ")) titleVersion = titleVersion.Remove(titleVersion.IndexOf(' '));

                titles.Add(new string[] { titleId, titleVersion });
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (checkedBoxes > 0 && (cbStoreEncrypted.Checked || cbStoreDecrypted.Checked || cbStoreWad.Checked))
            {
                rtbLog.Clear();
                List<string[]> titles = new List<string[]>();

                foreach (TreeNode thisNode in tvTitles.Nodes)
                    getList(thisNode, titles);

                dlThread = new Thread(new ParameterizedThreadStart(this.startDownload));
                dlThread.Start(titles);
            }
        }

        private void startDownload(object titleList)
        {
            try
            {
                setControls(false);
                List<string[]> titles = (List<string[]>)titleList;
                List<StoreType> storeList = new List<StoreType>();

                if (storeTypes[0]) storeList.Add(StoreType.EncryptedContent);
                if (storeTypes[1]) storeList.Add(StoreType.DecryptedContent);
                if (storeTypes[2]) storeList.Add(StoreType.WAD);

                foreach (string[] thisTitle in titles)
                {
                    try { nusClient.DownloadTitle(thisTitle[0], thisTitle[1], Application.StartupPath + Path.DirectorySeparatorChar + thisTitle[0] + (string.IsNullOrEmpty(thisTitle[1]) ? string.Empty : "v" + thisTitle[1]), storeList.ToArray()); }
                    catch { }
                }
            }
            finally
            {
                this.Invoke(new MethodInvoker(this.uncheckNodes));
                setControls(true);
            }
        }
    }
}
