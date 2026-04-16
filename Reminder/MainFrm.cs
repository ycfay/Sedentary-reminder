using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reminder
{
    public partial class MainFrm : Form
    {
        int wrkTime, rstTime;
        bool input_flag;
        WorkFrm wrkFrm;
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            //读取位置
            if (File.Exists(OperateIniFileHelper.localPath+ "\\reminder_config.ini"))
            {
                var time_config = OperateIniFileHelper.ReadIniData("system", "timeconfig", "0,0", OperateIniFileHelper.localPath + "\\reminder_config.ini").Split(',');
                if (time_config.Length>2&& !time_config[0].Equals("0") && !time_config[1].Equals("0"))
                {
                    this.numWrkTime.Value = Convert.ToInt32(time_config[0]);
                    this.numRstTime.Value = Convert.ToInt32(time_config[1]);
                    this.ckBoxInput.Checked = Convert.ToBoolean(time_config[2]);    
                }
                var opacity = OperateIniFileHelper.ReadIniData("system", "opacity", "7", OperateIniFileHelper.localPath + "\\reminder_config.ini");
                if (!String.IsNullOrEmpty(opacity))
                {
                    this.trackBar.Value = Convert.ToInt32(opacity);
                }
                var lockWindow = OperateIniFileHelper.ReadIniData("system", "lockwindow", "False", OperateIniFileHelper.localPath + "\\reminder_config.ini");
                if (!String.IsNullOrEmpty(opacity))
                {
                    this.ckBoxLockWindow.Checked = Convert.ToBoolean(lockWindow);
                }

            }
            ckBoxAutoStart.Checked = AutoStartHelper.IsAutoStartEnabled();

        }

        private void Btn_start_Click(object sender, EventArgs e)
        {
            //设置开机自启
            AutoStartHelper.SetAutoStart(ckBoxAutoStart.Checked);

            if (this.ckBoxInput.Checked)
            {
                input_flag = true;
            }
            else {
                input_flag = false;
            }

            wrkTime = (int)this.numWrkTime.Value;
            rstTime = (int)this.numRstTime.Value;
            //时间检测
            if (rstTime > wrkTime)
            {
                MessageBox.Show("我真是奇了怪了,到底是什么公司,请你们这些天天带薪摸鱼的家伙!\r\n休息时间不能大于工作时间!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (wrkFrm != null)
            {
                wrkFrm.Close();
            }
            wrkFrm = new WorkFrm(wrkTime,rstTime,input_flag);
            wrkFrm.Show();
            //MainFrm.Visible = false;
            this.Visible = false;
            SaveConfig();
        }

        private void MainToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            if (wrkFrm != null)
            {
                wrkFrm.Visible=false;
            }
        }
         //保存配置
        private void SaveConfig()
        {
            string cstr = string.Format("{0},{1},{2}", wrkTime,rstTime, input_flag);
            string path = Path.Combine(OperateIniFileHelper.localPath, "reminder_config.ini");
            OperateIniFileHelper.WriteIniData("system", "timeconfig", cstr, path);
            OperateIniFileHelper.WriteIniData("system", "opacity", this.trackBar.Value.ToString(), path);
            OperateIniFileHelper.WriteIniData("system", "lockwindow", this.ckBoxLockWindow.Checked.ToString(), path);

        }
        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {            
            //取消关闭窗口
            e.Cancel = true;
            //最小化主窗口
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            //不在系统任务栏显示主窗口图标
            this.ShowInTaskbar = false;
            if (wrkFrm != null)
            {
                wrkFrm.Visible = true;
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            System.Environment.Exit(0);
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            this.Opacity = Convert.ToInt32(trackBar.Value) / 10.0;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }
    }
}
