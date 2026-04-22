using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reminder
{
    public static class GlobalMenu
    {
        public static ContextMenuStrip GlobalContextMenu = new ContextMenuStrip();
        public static event EventHandler OnClick;

        static GlobalMenu()
        {
            GlobalContextMenu.BackgroundImageLayout = ImageLayout.Center;

            ToolStripMenuItem UploadToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem MainToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem AboutToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem Exit_ToolStripMenuItem = new ToolStripMenuItem();

            // 
            // contextMenuStrip
            // 
            GlobalContextMenu.Items.AddRange(new ToolStripItem[] {
            MainToolStripMenuItem,
            UploadToolStripMenuItem,
            AboutToolStripMenuItem,
            Exit_ToolStripMenuItem});
            GlobalContextMenu.Name = "contextMenuStrip";
            GlobalContextMenu.Size = new Size(181, 92);
            // 
            // 主窗体ToolStripMenuItem
            // 
            UploadToolStripMenuItem.Name = "MainToolStripMenuItem";
            UploadToolStripMenuItem.Size = new Size(180, 22);
            UploadToolStripMenuItem.Text = "上传图片";
            UploadToolStripMenuItem.Click += UploadToolStripMenuItem_Click;
            // 
            // 主窗体ToolStripMenuItem
            // 
            MainToolStripMenuItem.Name = "MainToolStripMenuItem";
            MainToolStripMenuItem.Size = new Size(180, 22);
            MainToolStripMenuItem.Text = "首选项";
            MainToolStripMenuItem.Click += MainToolStripMenuItem_Click;
            // 
            // 关于ToolStripMenuItem
            // 
            AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            AboutToolStripMenuItem.Size = new Size(180, 22);
            AboutToolStripMenuItem.Text = "关于";
            AboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            // 
            // exit_ToolStripMenuItem
            // 
            Exit_ToolStripMenuItem.Name = "exit_ToolStripMenuItem";
            Exit_ToolStripMenuItem.Size = new Size(180, 22);
            Exit_ToolStripMenuItem.Text = "退出";
            Exit_ToolStripMenuItem.Click += new EventHandler(ExitToolStripMenuItem_Click);
        }
        private static void UploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadFrm uploadBox = new UploadFrm();
            uploadBox.ShowDialog();
        }
        private static void MainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnClick?.Invoke(null, EventArgs.Empty);
        }
        private static void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private static void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }
        public static string GetRandomSerial(int length = 5)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789"; // 排除易混淆字符如 0, O, 1, I
            var random = new Random();
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            return new string(result);
        }
    }
}
