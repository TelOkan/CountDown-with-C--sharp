using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountDown_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int hour;
        int minute;
        int second;
        int set_times = 0;
        bool pause_counter = false;

        SoundPlayer alarm = new SoundPlayer();
        void numeric_readonly(bool boolvalue, int value)
        {
            numeric_min.ReadOnly = boolvalue;
            numeric_min.Increment = value;
            numeric_hour.ReadOnly = boolvalue;
            numeric_hour.Increment = value;

        }
        void lbl_forecolor(Color labelcolor)
        {
            lbl_dakika.ForeColor = labelcolor;
            lbl_saniye.ForeColor = labelcolor;
            lbl_saat.ForeColor = labelcolor;
        }

        void lbl_reset()
        {
            lbl_saniye.Text = "00";
            lbl_dakika.Text = "00";
            lbl_saat.Text = "00";
        }

        void variable_reset()
        {
            second = 0;
            minute = 0;
            hour = 0;
        }

        void timeinput_reset()
        {

            numeric_min.Value = 0;
            numeric_hour.Value = 0;


        }
        private void btn_start_Click(object sender, EventArgs e)
        {
            lbl_forecolor(Color.White);
            timer1.Start();

            if (pause_counter)
            {
                minute = Convert.ToInt32(lbl_dakika.Text);
                hour = Convert.ToInt32(lbl_saat.Text);
                second = Convert.ToInt32(lbl_saniye.Text);
            }
            else
            {
                minute = Convert.ToInt32(numeric_min.Value);
                hour = Convert.ToInt32(numeric_hour.Value);

                if (minute == 0 && hour == 0)
                {
                    timer1.Stop();


                }

                else
                {
                    second = 60;
                }
            }


            pause_counter = false;
            numeric_readonly(true, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Interval = 1000;
            second = second - 1;
            lbl_saniye.Text = second.ToString();

            if (minute == 0 && second == 0 && hour > 0)
            {
                hour = hour - 1;
                minute = 59;
                second = 59;
                lbl_saniye.Text = second.ToString();
                lbl_dakika.Text = minute.ToString();
                lbl_saat.Text = hour.ToString();

            }
            else if (minute == 0 && second == 59 && hour > 0)
            {
                hour = hour - 1;
                minute = 59;
                lbl_dakika.Text = minute.ToString();
                lbl_saat.Text = hour.ToString();
            }
            else if (minute > 0 && hour > 0 && second == 59)
            {
                minute = minute - 1;
                lbl_dakika.Text = minute.ToString();
                lbl_saat.Text = hour.ToString();

            }
            else if (hour == 0 && minute != 0 && second == 59)
            {
                minute = minute - 1;
                lbl_dakika.Text = minute.ToString();
                lbl_saat.Text = hour.ToString();

            }

            if (second == 0 && minute > 0)
            {
                minute = minute - 1;
                second = 59;
                lbl_saniye.Text = second.ToString();
                lbl_dakika.Text = minute.ToString();
                lbl_saat.Text = hour.ToString();

            }

            if (hour == 0 && minute == 0 && second == 0)
            {
                timer1.Stop();
                set_times++;
                lbl_set_times.Text = set_times.ToString();
                lbl_saniye.Text = "00";
                lbl_dakika.Text = "00";
                lbl_saat.Text = "00";
                hour = 0;
                minute = 0;
                second = 0;
                numeric_readonly(false, 1);
                timeinput_reset();
                lbl_forecolor(Color.Red);
                alarm.Stream = Properties.Resources.alarm;
                alarm.Play();

            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            pause_counter = true;
            timer1.Stop();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            set_times = 0;
            lbl_set_times.Text = set_times.ToString();
            lbl_reset();
            variable_reset();
            timeinput_reset();
            second = 60;
            numeric_readonly(false, 1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numeric_min.Maximum = 59;
            numeric_hour.Maximum = 23;
        }
    }
}
