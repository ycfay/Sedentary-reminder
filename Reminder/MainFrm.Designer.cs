namespace Reminder
{
    partial class MainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_start = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numWrkTime = new System.Windows.Forms.NumericUpDown();
            this.numRstTime = new System.Windows.Forms.NumericUpDown();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.ckBoxInput = new System.Windows.Forms.CheckBox();
            this.ckBoxAutoStart = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.ckBoxLockWindow = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numWrkTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRstTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "工作时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "计时器：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "休息时间：";
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.Transparent;
            this.btn_start.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btn_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_start.Location = new System.Drawing.Point(62, 234);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 11;
            this.btn_start.Text = "开始";
            this.btn_start.UseVisualStyleBackColor = false;
            this.btn_start.Click += new System.EventHandler(this.Btn_start_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(147, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "（分钟）";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(147, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "（分钟）";
            // 
            // numWrkTime
            // 
            this.numWrkTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numWrkTime.Location = new System.Drawing.Point(97, 39);
            this.numWrkTime.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numWrkTime.Name = "numWrkTime";
            this.numWrkTime.Size = new System.Drawing.Size(51, 21);
            this.numWrkTime.TabIndex = 16;
            this.numWrkTime.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // numRstTime
            // 
            this.numRstTime.BackColor = System.Drawing.Color.White;
            this.numRstTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numRstTime.Location = new System.Drawing.Point(96, 66);
            this.numRstTime.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numRstTime.Name = "numRstTime";
            this.numRstTime.Size = new System.Drawing.Size(51, 21);
            this.numRstTime.TabIndex = 17;
            this.numRstTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Reminder";
            this.notifyIcon.Visible = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(12, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 14);
            this.label4.TabIndex = 18;
            this.label4.Text = "选项：";
            // 
            // ckBoxInput
            // 
            this.ckBoxInput.AutoSize = true;
            this.ckBoxInput.Checked = true;
            this.ckBoxInput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckBoxInput.Location = new System.Drawing.Point(29, 125);
            this.ckBoxInput.Name = "ckBoxInput";
            this.ckBoxInput.Size = new System.Drawing.Size(144, 16);
            this.ckBoxInput.TabIndex = 19;
            this.ckBoxInput.Text = "休息时屏蔽键盘和鼠标";
            this.ckBoxInput.UseVisualStyleBackColor = true;
            // 
            // ckBoxAutoStart
            // 
            this.ckBoxAutoStart.Location = new System.Drawing.Point(29, 169);
            this.ckBoxAutoStart.Name = "ckBoxAutoStart";
            this.ckBoxAutoStart.Size = new System.Drawing.Size(144, 16);
            this.ckBoxAutoStart.TabIndex = 20;
            this.ckBoxAutoStart.Text = "开机自启动";
            this.ckBoxAutoStart.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 196);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "锁屏透明度：";
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(99, 192);
            this.trackBar.Minimum = 1;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(104, 45);
            this.trackBar.TabIndex = 22;
            this.trackBar.Value = 1;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // ckBoxLockWindow
            // 
            this.ckBoxLockWindow.Location = new System.Drawing.Point(29, 147);
            this.ckBoxLockWindow.Name = "ckBoxLockWindow";
            this.ckBoxLockWindow.Size = new System.Drawing.Size(144, 16);
            this.ckBoxLockWindow.TabIndex = 23;
            this.ckBoxLockWindow.Text = "休息时自动锁定屏幕";
            this.ckBoxLockWindow.UseVisualStyleBackColor = true;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(213, 265);
            this.Controls.Add(this.ckBoxLockWindow);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ckBoxAutoStart);
            this.Controls.Add(this.ckBoxInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numRstTime);
            this.Controls.Add(this.numWrkTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainFrm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sedentary Reminder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numWrkTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRstTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_start;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numWrkTime;
        private System.Windows.Forms.NumericUpDown numRstTime;
        protected System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ckBoxInput;
        private System.Windows.Forms.CheckBox ckBoxAutoStart;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.CheckBox ckBoxLockWindow;
    }
}