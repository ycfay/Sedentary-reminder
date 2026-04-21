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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Reminder
{
    public partial class UploadFrm : Form
    {
        string filePath=String.Empty;
        public UploadFrm()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.webp";

                    if (dialog.ShowDialog() != DialogResult.OK)
                        return;

                    filePath = dialog.FileName;
                    picBox.Image = new Bitmap(filePath);
                }
            }
            catch {
                MessageBox.Show("该图片格式不支持，请重新选择");
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                //string url = "http://127.0.0.1:5180/upload";
                string url = "http://43.156.184.19/upload";


                // 1. 创建请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";

                string boundary = "----WebKitFormBoundary" + DateTime.Now.Ticks.ToString("x");
                request.ContentType = "multipart/form-data; boundary=" + boundary;

                // 2. 组装请求体
                Stream requestStream = request.GetRequestStream();

                byte[] boundaryBytes = Encoding.UTF8.GetBytes("--" + boundary + "\r\n");

                // 文件头
                string header =
                    "Content-Disposition: form-data; name=\"file\"; filename=\"" + Path.GetFileName(filePath) + "\"\r\n" +
                    "Content-Type: application/octet-stream\r\n\r\n";

                byte[] headerBytes = Encoding.UTF8.GetBytes(header);

                // 写入 boundary + header
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                requestStream.Write(headerBytes, 0, headerBytes.Length);

                // 写入文件内容
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }
                }

                // 结束 boundary
                byte[] endBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                requestStream.Write(endBytes, 0, endBytes.Length);

                requestStream.Close();

                // 3. 获取响应
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string result = reader.ReadToEnd();
                    if(result.Contains("true"))
                        MessageBox.Show("上传成功,等待在屏幕锁定期间播放图片！");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("上传失败：" + ex.Message);
            }
        }
    }
}
