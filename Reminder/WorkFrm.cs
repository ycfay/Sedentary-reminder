using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Reminder
{
    public partial class WorkFrm : Form
    {
        private Point mPoint;//定义一个位置信息Point用于存储鼠标位置
        private int wrk_minutes;//工作时间(分)
        private int wrk_seconds;//工作时间(秒)
        private int wrk_m;
        private int rst_minutes;//休息时间(分)
        private bool input_flag;//是否选中锁定键盘
        private bool left_flag;//鼠标左键是否点击
        private Point mouseoff;
        public WorkFrm()
        {
            InitializeComponent();
        }
        //定义一个构造函数，接受前一个窗体传来的参数
        public WorkFrm(int wrk_minutes, int rst_minutes,bool input_flag)
        {
            InitializeComponent();           
            this.wrk_minutes = wrk_minutes;
            this.rst_minutes = rst_minutes;
            //this.input_flag = input_flag;
            this.wrk_m = wrk_minutes;
            this.input_flag = input_flag;

            int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - 160;
            int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - 90;
            Point p = new Point(x, y);
            this.PointToScreen(p);
            this.Location = p;

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //读取位置
            if (File.Exists(OperateIniFileHelper.localPath+ "\\config.ini"))    
            {
                var screenLocation = OperateIniFileHelper.ReadIniData("system", "location", "0,0", OperateIniFileHelper.localPath + "\\reminder_config.ini").Split(',');
                if (!screenLocation[0].Equals("0") && !screenLocation[1].Equals("0"))
                    this.Location = new Point(Convert.ToInt32(screenLocation[0]), Convert.ToInt32(screenLocation[1]));
            }


            wrk_seconds = 0; 

            if (wrk_seconds >= 10)
            {
                lblSecond.Text = wrk_seconds.ToString();
            }
            else
            {
                lblSecond.Text = "0" + wrk_seconds.ToString();
            }

            if (wrk_minutes>=10) {
                lblMin.Text = wrk_minutes.ToString();
            }
            else
            {
                lblMin.Text = "0"+wrk_minutes.ToString();
            }

            this.Opacity = 0.8;
            
        }

        private void TimerWrk_Tick(object sender, EventArgs e)
        {
            // 1. 获取空闲时间（毫秒）
            //uint idleTime = UserActivityMonitor.GetIdleTime();
   
            Timing();
            // 关键检查：如果窗体已经销毁、正在销毁、或句柄尚未创建，则直接返回
            if (this.IsDisposed || this.Disposing || !this.IsHandleCreated)
            {
                timerWrk.Stop(); // 安全起见，停止计时器
                return;
            }
            try
            {
                //设置窗口置顶
                SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
            }
            catch (ObjectDisposedException)
            {
                timerWrk.Stop();
            }
        }


        // 在你的窗体类中添加
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        // 定义常量
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOMOVE = 0x0002;
        const uint SWP_NOACTIVATE = 0x0010;

        /// <summary>
        /// 递归的方式倒计时
        /// </summary>
        public  void Timing()
        {
            Warn();

            if (wrk_seconds > 0)
            {
                wrk_seconds = wrk_seconds - 1;
                if (wrk_seconds >= 10)
                {
                    lblSecond.Text = wrk_seconds.ToString();
                }
                else
                {
                    lblSecond.Text = "0"+wrk_seconds.ToString();
                }
               
            }
            else //秒=0时，分钟-1
            {
                timerWrk.Enabled = false;
                wrk_minutes--;
                if (wrk_minutes >= 10)
                {
                    lblMin.Text = wrk_minutes.ToString();
                }
                else
                {
                    lblMin.Text = "0"+wrk_minutes.ToString();
                }
                
                if (wrk_minutes > -1) //若分钟不为0，秒回到60，继续递归
                {
                    timerWrk.Enabled = true;
                    wrk_seconds = 60;

                    Timing();
                }
                else
                {

                    this.Close();
                    RestFrm restFrm = new RestFrm(rst_minutes, wrk_m, input_flag);
                    restFrm.ShowDialog();                   
                }
            }
        }

        /// <summary>
        /// 工作的最后15秒提醒
        /// </summary>
        private void Warn()
        {
            if (wrk_minutes==0&&wrk_seconds<=16)
            {
                this.BackColor = Color.Red;
                lblWarn.ForeColor = Color.Yellow;
                lblWarn.Text = "该休息了！";
                int x = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width) / 2 - this.Width/2;
                int y = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height) / 2 - this.Height/2;
                Point p = new Point(x, y);
                this.PointToScreen(p);
                this.Location = p;
            }         
            
        }

        /// <summary>
        /// 让程序不显示在alt+Tab视图窗体中
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_APPWINDOW = 0x40000;
                const int WS_EX_TOOLWINDOW = 0x80;
                CreateParams cp = base.CreateParams;
                cp.ExStyle &= (~WS_EX_APPWINDOW);    // 不显示在TaskBar
                cp.ExStyle |= WS_EX_TOOLWINDOW;      // 不显示在Alt+Tab
                return cp;
            }
        }


        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerWrk.Stop();
            timerWrk.Enabled = false;
        }
        private void mouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseoff = new Point(e.X, e.Y);
                left_flag = true;

            }
        }
        private void mouseMove()
        {
            if (left_flag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(-mouseoff.X, -mouseoff.Y);//这里注意下-的用意，offset
                Location = mouseSet;
            }

        }
        private void mouseUp()
        {
            if (left_flag)
            {
                left_flag = false;
            }
        }
        private void WorkFrm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown(e);
        }

        private void WorkFrm_MouseMove(object sender, MouseEventArgs e)
        {
            mouseMove();
        }
        private void WorkFrm_Move(object sender, EventArgs e)
        {
            mPoint = new Point(this.Left, this.Top);
        }
        private void WorkFrm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseUp();
            // 用户松开鼠标时，保存最终位置
            SaveLocation();
        }
        private void SaveLocation()
        {
            string locStr = string.Format("{0},{1}", this.Location.X, this.Location.Y);
            string path = Path.Combine(OperateIniFileHelper.localPath, "reminder_config.ini");
            OperateIniFileHelper.WriteIniData("system", "location", locStr, path);
        }
    }
}
