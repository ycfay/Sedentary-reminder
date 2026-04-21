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

            ToolStripMenuItem MainToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem AboutToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem Exit_ToolStripMenuItem = new ToolStripMenuItem();

            // 
            // contextMenuStrip
            // 
            GlobalContextMenu.Items.AddRange(new ToolStripItem[] {
            MainToolStripMenuItem,
            AboutToolStripMenuItem,
            Exit_ToolStripMenuItem});
            GlobalContextMenu.Name = "contextMenuStrip";
            GlobalContextMenu.Size = new Size(181, 92);
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
    }
}
