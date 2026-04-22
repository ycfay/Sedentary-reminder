using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace Reminder
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool ExisFlag = false;
            System.Diagnostics.Process currentProccess = System.Diagnostics.Process.GetCurrentProcess();
            System.Diagnostics.Process[] currentProccessArray = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process p in currentProccessArray)
            {
                if (p.ProcessName == currentProccess.ProcessName && p.Id != currentProccess.Id)
                {
                    ExisFlag = true;
                }
            }
            var serialNumber = OperateIniFileHelper.ReadIniData("system", "serial", "", OperateIniFileHelper.localPath + "\\reminder_config.ini");
            if (String.IsNullOrEmpty(serialNumber))
            {
                OperateIniFileHelper.WriteIniData("system", "serial", GlobalMenu.GetRandomSerial(), Path.Combine(OperateIniFileHelper.localPath, "reminder_config.ini"));
            }

            if (ExisFlag)//防止程序二次运行
            {
                MessageBox.Show("程序运行中，见右下角系统托盘");
                return;
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new TestFrm());
            }
           
        }
    }
}
