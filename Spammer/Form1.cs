using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Reflection;

using MetroFramework.Forms;
using MetroFramework;

namespace Spamer
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Standart();
        }

        private void startTile_Click(object sender, EventArgs e)
        {
            if (metroToggle1.Checked)
            {
                if (TextListView.Items.Count == 0)
                    MetroMessageBox.Show(this, "Введите хоть одну строку в список", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (startTile.Text == "Старт")
                    {
                        TimerPro();

                        this.currentIndex = -1;
                        startTile.Text = "Стоп";
                        metroTextBox1.Enabled = metroToggle1.Enabled = metroRadioButton1.Enabled = metroRadioButton2.Enabled = metroTextBox2.Enabled = metroTextBox3.Enabled =  metroCheckBox1.Enabled = metroLink1.Enabled = metroLink4.Enabled = metroLink2.Enabled = TextListView.Enabled= metroToggle1.Enabled = false;
                    }
                    else
                    {
                        counter = 0;
                        this.currentIndex = -1;
                        timer.Stop();
                        startTile.Text = "Старт";
                        metroTextBox1.Enabled = metroToggle1.Enabled = metroRadioButton1.Enabled = metroRadioButton2.Enabled = metroTextBox2.Enabled = metroTextBox3.Enabled = metroCheckBox1.Enabled = metroLink1.Enabled = metroLink4.Enabled = metroLink2.Enabled = TextListView.Enabled = metroToggle1.Enabled = true;
                    }
                }
            }
            else
            {
                if (metroTextBox1.Text == "")
                    MetroMessageBox.Show(this, "Введите текст", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (startTile.Text == "Старт")
                    {
                        timer = new Timer();
                        timer.Interval = 1000;
                        counter = 0;
                        this.currentIndex = -1;
                        timer.Start();
                        timer.Tick += Timer_Tick;


                        startTile.Text = "Стоп";
                        metroTextBox1.Enabled = false;
                        metroToggle1.Enabled = false;
                    }
                    else
                    {
                        timer.Stop();
                        MetroMessageBox.Show(this, "Было отправлено сообщений: " + counter, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        counter = 0;
                        this.currentIndex = -1;
                        startTile.Text = "Старт";
                        metroTextBox1.Enabled = true;
                        metroToggle1.Enabled = true;
                    }
                }
            }
            this.ActiveControl = null;
        }

        private void TimerPro()
        {
            timer = new Timer();

            if (metroTextBox2.Text == "")
                timer.Interval = 5000;
            else
                timer.Interval = Convert.ToInt32(metroTextBox2.Text) * 1000;
            if (metroCheckBox1.Checked)
                counter = 0;
            else if (metroTextBox3.Text == "")
                counter = 10;
            else
                counter = Convert.ToInt32(metroTextBox3.Text);
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (metroToggle1.Checked)
            {
                /*
                if (metroLink3.Text != "Выбрать")
                {

                        Process[] targetProcess = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(metroLink3.Text));
                        MessageBox.Show(targetProcess.ToString());
                    if (targetProcess.hasExited)
                        timer.Stop();
                        startTile_Click(sender, e);
                        MetroMessageBox.Show(this, "Выбранный процесс процесс был закрыт. Спаммер остановлен.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //MessageBox.Show(Process.GetProcessesByName(metroLink3.Text)[0].MainWindowHandle.ToString());
                }
                */
                
                this.currentIndex = this.currentIndex + 1;
                this.TextListView.Items[this.currentIndex].Selected = false;
                if (metroRadioButton2.Checked)
                {
                    SendKeys.SendWait(this.TextListView.Items[this.currentIndex].Text + "^{ENTER}");
                }
                else
                {
                    SendKeys.SendWait(this.TextListView.Items[this.currentIndex].Text + "{ENTER}");
                }
                
                if (this.currentIndex < this.TextListView.Items.Count)
                {
                    this.TextListView.Items[this.currentIndex].Selected = true;
                    timer.Stop();
                    TimerPro();
                }
                else
                {
                    this.currentIndex = -1;
                    if (metroCheckBox1.Checked)
                    {
                        counter++;
                        metroTextBox3.Text = counter.ToString();
                    }
                    else
                    {
                        counter--;
                        metroTextBox3.Text = counter.ToString();
                        if (counter == 0)
                        {
                            timer.Stop();
                            startTile_Click(sender, e);
                        }
                    }
                }
            }
            else
            {
                SendKeys.SendWait(metroTextBox1.Text + "{ENTER}");
                counter++;
            }
        }

        private void metroTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void metroTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroCheckBox1.Checked)
            {
                metroTextBox3.Enabled = false;
                metroTextBox3.Text = "";
            }
            else
            {
                metroTextBox3.Enabled = true;
            }
        }

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroToggle1.Checked)
            {
                Form1_Activated(sender, e);
                Pro();
            }
            else
            {
                Standart();
            }
        }

        private void Standart()
        {
            this.ClientSize = new System.Drawing.Size(380, 165);

            metroTextBox1.Multiline = false;
            this.metroTextBox1.Size = new System.Drawing.Size(334, 23);
            this.metroTextBox1.Location = new System.Drawing.Point(23, 63);

            this.startTile.Location = new System.Drawing.Point(282, 106);

            metroLabel4.Visible = true;
            metroLabel2.Visible = metroTextBox2.Visible = metroLabel3.Visible = metroTextBox3.Visible = metroCheckBox1.Visible = metroLabel1.Visible = metroRadioButton1.Visible = metroRadioButton2.Visible= TextListView.Visible= metroLink1.Visible = metroLink4.Visible = metroLink2.Visible =  false;
            //metroLabel6.Visible = metroLink3.Visible = false;
        }
        
        private void Pro()
        {
            this.ClientSize = new System.Drawing.Size(524, 412);

            metroTextBox1.Multiline = true;
            this.metroTextBox1.Size = new System.Drawing.Size(202, 185);
            this.metroTextBox1.Location = new System.Drawing.Point(23, 63);

            this.startTile.Location = new System.Drawing.Point(417, 355);

            metroLabel4.Visible = false;
            metroLabel2.Visible = metroTextBox2.Visible = metroLabel3.Visible = metroTextBox3.Visible = metroCheckBox1.Visible = metroLabel1.Visible = metroRadioButton1.Visible = metroRadioButton2.Visible = TextListView.Visible = metroLink1.Visible = metroLink4.Visible = metroLink2.Visible = true;
            //metroLabel6.Visible = metroLink3.Visible = true;
        }

        private void metroLink4_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("TextforSpam.txt");
            }
            catch
            {
                File.WriteAllText(Directory.GetCurrentDirectory() + @"\TextforSpam.txt", "");
                Process.Start("TextforSpam.txt");
            }
            
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            TextListView.Items.Clear();
            
            try
            {
                File.Exists(Directory.GetCurrentDirectory() + @"\TextforSpam.txt");

                string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\TextforSpam.txt");
                foreach (string line in lines)
                {
                    TextListView.Items.Add(line);
                }
            }
            catch
            {
                File.WriteAllText(Directory.GetCurrentDirectory() + @"\TextforSpam.txt", "");
            }
            
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            if (metroTextBox1.Text != "")
            {
                try
                {
                    File.Exists(Directory.GetCurrentDirectory() + @"\TextforSpam.txt");


                    TextListView.Items.Add(metroTextBox1.Text);

                    FileStream file = new FileStream(Directory.GetCurrentDirectory() + @"\TextforSpam.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(file);

                    foreach (ListViewItem item in TextListView.Items)
                    {
                        sw.WriteLine(item.Text);
                    }
                    sw.Close();
                    file.Close();

                }
                catch
                {
                    File.WriteAllText(Directory.GetCurrentDirectory() + @"\TextforSpam.txt", metroTextBox1.Text);
                }
            }
        }

        private void metroLink2_Click(object sender, EventArgs e)
        {
            var selectedItems = TextListView.SelectedItems;
            if (selectedItems.Count > 0)
            {
                TextListView.Items.Remove(selectedItems[0]);

                FileStream file = new FileStream(Directory.GetCurrentDirectory() + @"\TextforSpam.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(file);

                foreach (ListViewItem item in TextListView.Items)
                {
                    sw.WriteLine(item.Text);
                }
                sw.Close();
                file.Close();
            }
        }

        private void metroLink3_Click(object sender, EventArgs e)
        {
            form = new Processes();
            form.Tag = this;
            form.Show();
        }
    }

    
}
