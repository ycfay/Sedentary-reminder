using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;

namespace Reminder
{

    public partial class TestFrm : Form
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool PeekMessage(out PeekMsg msg, IntPtr hWnd, uint messageFilterMin,
                                       uint messageFilterMax, uint flags);

        [StructLayout(LayoutKind.Sequential)]
        private struct PeekMsg
        {
            private readonly IntPtr hWnd;
            private readonly Message msg;
            private readonly IntPtr wParam;
            private readonly IntPtr lParam;
            private readonly uint time;
            private readonly Point p;
        }

        private static bool AppStillIdle
        {
            get
            {
                PeekMsg msg;
                return !PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
            }
        }
        public TestFrm()
        {
            InitializeComponent();
            Application.Idle += Application_Idle;

        }
        private static void Application_Idle(object sender, EventArgs e)
        {
            try
            {
                while (AppStillIdle)
                {
                    File.AppendAllText($"Log-{DateTime.Now.ToString("yyyyMM")}.txt", $"Application_Idle:{DateTime.Now.Ticks}" + Environment.NewLine);
                }

            }
            catch (Exception ex)
            {
                File.AppendAllText($"Log-{DateTime.Now.ToString("yyyyMM")}.txt", $"LoadImage:{ex.Message}" + Environment.NewLine);
            }
        }
        public void LoadImage()
        {
            WebClient client = new WebClient();

            var serialNumber = OperateIniFileHelper.ReadIniData("system", "serial", "", OperateIniFileHelper.localPath + "\\reminder_config.ini");

            string finalUrl = "http://43.156.184.19/image?sn=" + Uri.EscapeDataString(serialNumber);

            // 注册下载完成事件
            client.DownloadDataCompleted += (s, e) =>
            {
                try
                {
                    if (e.Error == null)
                    {
                        byte[] data = e.Result;
                        using (MemoryStream ms = new MemoryStream(data))
                        {
                            picBox.Image = ImageHelper.ConvertWebpToDisplayImage(data);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("图片解析失败: " + ex.Message);
                }
            };
            // 开始异步下载
            client.DownloadDataAsync(new Uri(finalUrl));
        }

        private void TestFrm_Load(object sender, EventArgs e)
        {
            LoadImage();
        }
    }
}
