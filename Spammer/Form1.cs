using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
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
            if (metroTextBox1.Text == "")
                MetroMessageBox.Show(this, "Введите текст", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (metroToggle1.Checked)
                {
                    if (startTile.Text == "Старт")
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


                        startTile.Text = "Стоп";
                        metroTextBox1.Enabled = false;
                        metroToggle1.Enabled = false;
                        metroRadioButton1.Enabled = false;
                        metroRadioButton2.Enabled = false;
                        metroTextBox2.Enabled = false;
                        metroTextBox3.Enabled = false;
                        metroCheckBox1.Enabled = false;
                    }
                    else
                    {
                        counter = 0;
                        timer.Stop();
                        startTile.Text = "Старт";
                        metroTextBox1.Enabled = true;
                        metroToggle1.Enabled = true;
                        metroRadioButton1.Enabled = true;
                        metroRadioButton2.Enabled = true;
                        metroTextBox2.Enabled = true;
                        metroTextBox3.Enabled = true;
                        metroCheckBox1.Enabled = true;
                    }
                }
                else
                {
                    if (startTile.Text == "Старт")
                    {
                        timer = new Timer();
                        timer.Interval = 1000;
                        counter = 0;
                        timer.Start();
                        timer.Tick += Timer_Tick;


                        startTile.Text = "Стоп";
                        metroTextBox1.Enabled = false;
                        metroToggle1.Enabled = false;
                    }
                    else
                    {
                        timer.Stop();
                        MetroMessageBox.Show(this,"Было отправлено сообщений: " + counter, "",MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        counter = 0;
                        startTile.Text = "Старт";
                        metroTextBox1.Enabled = true;
                        metroToggle1.Enabled = true;
                    }
                }
            }
            this.ActiveControl = null;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (metroToggle1.Checked)
            {
                if (metroRadioButton1.Checked)
                {
                    SendKeys.SendWait(metroTextBox1.Text + "{ENTER}");
                }
                else
                {
                    SendKeys.SendWait(metroTextBox1.Text + "^{ENTER}");

                }

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
            this.metroTextBox1.Size = new System.Drawing.Size(334, 23);
            metroTextBox1.Multiline = false;
            this.metroTextBox1.Location = new System.Drawing.Point(23, 63);
            this.startTile.Location = new System.Drawing.Point(282, 106);

            metroLabel4.Visible = true;
            metroLabel2.Visible = metroTextBox2.Visible = metroLabel3.Visible = metroTextBox3.Visible = metroCheckBox1.Visible = metroLabel1.Visible = metroRadioButton1.Visible = metroRadioButton2.Visible = false;
        }
        
        private void Pro()
        {
            metroTextBox1.Multiline = true;

            metroLabel4.Visible = false;
            metroLabel2.Visible = metroTextBox2.Visible = metroLabel3.Visible = metroTextBox3.Visible = metroCheckBox1.Visible = metroLabel1.Visible = metroRadioButton1.Visible = metroRadioButton2.Visible = true;
        }
    }
}
