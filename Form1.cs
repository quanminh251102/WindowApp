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
using System.Runtime.InteropServices;

namespace WindowApp
{
   
    public partial class Form1 : Form
    {
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private int TimeLeft;
        private void btnOk_Click(object sender, EventArgs e)
        {
            string selected = cbAction.SelectedItem.ToString();
            int option;
            if(selected == "ShutDown")
            {
                option = 1;
            }
            else
            {
                if(selected == "Restart")
                {
                    option = 2;
                }
                else
                {
                    if(selected == "LogOut")
                    {
                        option = 3;
                    }
                    else
                    {
                        option = 4;
                    }
                }
            }
            switch (option)
            {
                case 1:
                    {
                        timer1.Interval = 1000;
                        timer1.Enabled = true;
                        TimeLeft = int.Parse(tbDuration.Text) * 60;
                        break;
                    }
                case 2:
                    {
                        timer2.Interval = 1000;
                        timer2.Enabled = true;
                        TimeLeft = int.Parse(tbDuration.Text) * 60;
                        break;
                    }
                case 3:
                    {
                        timer3.Interval = 1000;
                        timer3.Enabled = true;
                        TimeLeft = int.Parse(tbDuration.Text) * 60;
                        break;
                    }
                case 4:
                    {
                        timer4.Interval = 1000;
                        timer4.Enabled = true;
                        TimeLeft = int.Parse(tbDuration.Text) * 60;
                        break;
                    }
                default:
                    break;
            }
            this.tbDuration.Enabled = false;
            this.cbAction.Enabled = false;
            this.btnOk.Enabled = false;
            // hello heloo
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(TimeLeft > 0)
            {
                lbStatus.Text = "Sẽ tắt máy sau " + TimeLeft + " giây.";
                TimeLeft = TimeLeft - 1;
            }
            else
            {
                this.timer1.Stop();
                this.timer1.Enabled = false;
                System.Diagnostics.Process.Start("shutdown", "-s -f -t 0");
                lbStatus.Text = "Đã thực hiện";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn dừng chương trình không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.timer1.Stop();
                this.timer1.Enabled = false;
                lbStatus.Text = "";
                this.tbDuration.Enabled = true;
                this.cbAction.Enabled = true;
                this.btnOk.Enabled = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (TimeLeft > 0)
            {
                lbStatus.Text = "Sẽ restart máy tính sau " + TimeLeft + " giây.";
                TimeLeft = TimeLeft - 1;
            }
            else
            {
                this.timer2.Stop();
                this.timer2.Enabled = false;
                System.Diagnostics.Process.Start("shutdown", "-r -f -t 0");
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if(TimeLeft > 0)
            {
                lbStatus.Text = "Sẽ logout ra khỏi tài khoản sau " + TimeLeft + " giây.";
                TimeLeft = TimeLeft - 1;
            }
            else
            {
                this.timer3.Stop();
                this.timer3.Enabled = false;
                ExitWindowsEx(0, 0);
                lbStatus.Text = "Đã thực hiện";
            }
        }
       
        private void timer4_Tick(object sender, EventArgs e)
        {
            if (TimeLeft > 0)
            {
                lbStatus.Text = "Sẽ vào chế độ sleep sau " + TimeLeft + " giây.";
                TimeLeft = TimeLeft - 1;
            }
            else
            {
                this.timer4.Stop();
                this.timer4.Enabled = false;
                SetSuspendState(false, true, true);
                lbStatus.Text = "Đã thực hiện";
            }
        }
    }
}
