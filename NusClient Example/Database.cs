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
 
using System.Windows.Forms;
using System.Xml;

namespace NusClient_Example
{
    partial class NusClient_Example
    {
        private void LoadDatabase()
        {
            TreeNode sysNode = new TreeNode("System");
            TreeNode iosNode = new TreeNode("IOS");

            XmlDocument db = new XmlDocument();
            db.Load("database.xml");

            XmlNodeList sysNodes = db.GetElementsByTagName("SYS");

            for (int i = 0; i < sysNodes.Count; i++)
            {
                string thisName = string.Empty;
                string thisId = string.Empty;
                string[] versions = new string[0];
                string toolTipText = string.Empty;

                for (int y = 0; y < sysNodes[i].ChildNodes.Count; y++)
                {
                    switch (sysNodes[i].ChildNodes[y].Name.ToLower())
                    {
                        case "name":
                            thisName = sysNodes[i].ChildNodes[y].InnerText;
                            break;
                        case "titleid":
                            thisId = sysNodes[i].ChildNodes[y].InnerText;
                            break;
                        case "version":
                            versions = sysNodes[i].ChildNodes[y].InnerText.Split(',');
                            break;
                        case "danger":
                            toolTipText = sysNodes[i].ChildNodes[y].InnerText;
                            break;
                    }
                }

                TreeNode titleNode = new TreeNode(string.Format("{0} ({1})", thisName, thisId));
                titleNode.ToolTipText = toolTipText;

                titleNode.Nodes.Add("Latest");
                foreach (string thisVersion in versions)
                    titleNode.Nodes.Add("v" + thisVersion);

                sysNode.Nodes.Add(titleNode);
            }

            XmlNodeList iosNodes = db.GetElementsByTagName("IOS");

            for (int i = 0; i < iosNodes.Count; i++)
            {
                string thisName = string.Empty;
                string thisId = string.Empty;
                string[] versions = new string[0];
                string toolTipText = string.Empty;

                for (int y = 0; y < iosNodes[i].ChildNodes.Count; y++)
                {
                    switch (iosNodes[i].ChildNodes[y].Name.ToLower())
                    {
                        case "name":
                            thisName = iosNodes[i].ChildNodes[y].InnerText;
                            break;
                        case "titleid":
                            thisId = iosNodes[i].ChildNodes[y].InnerText;
                            break;
                        case "version":
                            versions = iosNodes[i].ChildNodes[y].InnerText.Split(',');
                            break;
                        case "danger":
                            toolTipText = iosNodes[i].ChildNodes[y].InnerText;
                            break;
                    }
                }

                TreeNode titleNode = new TreeNode(string.Format("{0} ({1})", thisName, thisId));
                titleNode.ToolTipText = toolTipText;

                titleNode.Nodes.Add("Latest");
                foreach (string thisVersion in versions)
                    titleNode.Nodes.Add("v" + thisVersion);

                iosNode.Nodes.Add(titleNode);
            }

            tvTitles.Nodes.Clear();
            tvTitles.Nodes.Add(sysNode);
            tvTitles.Nodes.Add(iosNode);
        }
    }
}
