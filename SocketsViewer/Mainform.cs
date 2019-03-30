﻿using SocketsViewer.Libs;
using SocketsViewer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SocketsViewer
{
    public partial class Mainform : Form
    {

        List<RowInfo> _rowInfos = new List<RowInfo>();

        public Mainform()
        {
            InitializeComponent();
        }

        public delegate void Action();

        private void Mainform_Load(object sender, EventArgs e)
        {
            var td = new Thread(new ThreadStart(new Action(() =>
            {
                while (true)
                {
                    _rowInfos.Clear();

                    var tps = NetProcessAPI.GetAllTcpConnections();

                    var ups = NetProcessAPI.GetAllUdpConnections();

                    foreach (var p in tps)
                    {
                        _rowInfos.Add(new RowInfo()
                        {

                            PIcon = ProcessAPI.GetIcon(p.owningPid, true),
                            PName = ProcessAPI.GetProcessNameByPID(p.owningPid),
                            PID = p.owningPid.ToString(),
                            Type = "TCP",
                            LocalAddress = p.LocalAddress.ToString(),
                            LocalPort = p.LocalPort.ToString(),
                            RemoteAddress = p.RemoteAddress.ToString(),
                            RemotePort = p.RemotePort.ToString()
                        });
                    }

                    foreach (var p in ups)
                    {
                        _rowInfos.Add(new RowInfo()
                        {
                            PIcon = ProcessAPI.GetIcon(p.owningPid, true),
                            PName = ProcessAPI.GetProcessNameByPID(p.owningPid),
                            PID = p.owningPid.ToString(),
                            Type = "UDP",
                            LocalAddress = p.LocalAddress.ToString(),
                            LocalPort = p.LocalPort.ToString(),
                            RemoteAddress = "",
                            RemotePort = ""
                        });
                    }

                    Thread.Sleep(1000);

                    dataGridView1.Invoke(new Action(() =>
                    {


                    }));
                    break;
                }
            })));

            td.IsBackground = true;
            //td.Start();





        }

        private void button1_Click(object sender, EventArgs e)
        {
            var pName = textBox1.Text;

            var lIp = textBox2.Text;

            var lPort = textBox3.Text;

            var rIp = textBox4.Text;

            var rPort = textBox5.Text;

            dataGridView1.Rows.Clear();


            var tps = NetProcessAPI.GetAllTcpConnections();


            foreach (var p in tps)
            {
                if (!string.IsNullOrEmpty(pName) && ProcessAPI.GetProcessNameByPID(p.owningPid).IndexOf(pName, StringComparison.OrdinalIgnoreCase) == -1)
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(lIp) && p.LocalAddress.ToString() != lIp)
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(lPort) && p.LocalPort.ToString() != lPort)
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(rIp) && p.RemoteAddress.ToString() != rIp)
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(rPort) && p.RemotePort.ToString() != rPort)
                {
                    continue;
                }

                dataGridView1.Rows.Add(new object[] { p.owningPid.ToString(), ProcessAPI.GetIcon(p.owningPid, true), ProcessAPI.GetProcessNameByPID(p.owningPid), "TCP", p.LocalAddress.ToString(), p.LocalPort.ToString(), p.RemoteAddress.ToString(), p.RemotePort.ToString() });
            }

            var ups = NetProcessAPI.GetAllUdpConnections();

            if (!string.IsNullOrEmpty(rIp) || !string.IsNullOrEmpty(rPort))
            {
                return;
            }

            foreach (var p in ups)
            {
                if (!string.IsNullOrEmpty(pName) && ProcessAPI.GetProcessNameByPID(p.owningPid).IndexOf(pName, StringComparison.OrdinalIgnoreCase) == -1)
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(lIp) && p.LocalAddress.ToString() != lIp)
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(lPort) && p.LocalPort.ToString() != lPort)
                {
                    continue;
                }

                dataGridView1.Rows.Add(new object[] { p.owningPid.ToString(), ProcessAPI.GetIcon(p.owningPid, true), ProcessAPI.GetProcessNameByPID(p.owningPid), "UDP", p.LocalAddress.ToString(), p.LocalPort.ToString(), "", "" });
            }

            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = this.dataGridView1.Rows[i];
                r.HeaderCell.Value = string.Format("{0}", i + 1);
            }
            this.dataGridView1.Refresh();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(button1, null);
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            textBox1_KeyUp(sender, e);
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            textBox1_KeyUp(sender, e);
        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            textBox1_KeyUp(sender, e);
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            textBox1_KeyUp(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1_Click(button1, null);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = checkBox1.Checked;
        }
    }
}
