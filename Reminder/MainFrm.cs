using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            if (wrkFrm!=null)
            {
                wrkFrm.Close();
            }
        }
         //保存配置
        private void SaveConfig()
        {
            string cstr = string.Format("{0},{1},{2}", wrkTime,rstTime, input_flag);
            string path = Path.Combine(OperateIniFileHelper.localPath, "reminder_config.ini");
            OperateIniFileHelper.WriteIniData("system", "timeconfig", cstr, path);
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
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            System.Environment.Exit(0);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }
    }
}
