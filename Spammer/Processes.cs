using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Security.Permissions;
using System.Runtime.InteropServices;


namespace Spammer
{

    public partial class Processes : Form
    {
        public Processes()
        {

            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedItems = listView1.SelectedItems;
            if (selectedItems.Count > 0)
            {
                ((Form1)this.Tag).metroLink3.Text = listView1.SelectedItems[0].SubItems[0].Text;
                this.Close();
            }
        }

        private void Processes_Load(object sender, EventArgs e)
        {
            Process[] process = Process.GetProcesses();

            foreach (Process p in process)
            {
                //ListViewItem item = new ListViewItem();
                //ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem();
                //item.Tag = p;
                //item.SubItems.Add(p.ProcessName);
                //item.SubItems.Add(p.Id.ToString());
                //item.SubItems.Add(p.HandleCount.ToString());
                //item.SubItems.Add(p.Threads.Count.ToString());
                //item.SubItems.Add(p.MainWindowTitle);
               // item.SubItems.Add(p.Responding.ToString());
                listView1.Items.Add(p.ProcessName);
            }
        }
    }

}
