using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Reminder
{
    public partial class TestFrm : Form
    {
        public TestFrm()
        {
            InitializeComponent();
        }
        public void LoadImage()
        {
            WebClient client = new WebClient();

            // ✅ 拼接参数（注意编码）
            string finalUrl = "https://pic1.zhimg.com/80/v2-eb9325fe8f7d4314a7a4956862280d57_720w.webp?source=2c26e567";

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
