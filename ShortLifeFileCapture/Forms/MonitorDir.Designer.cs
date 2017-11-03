using System;
using System.Windows.Forms;

namespace ShortLifeFileCapture.Forms
{
    partial class MonitorDir
    {
        private OpenFileDialog openFileDialog1;
        private FolderBrowserDialog folderBrowserDialog1;
        private TextBox TargetPathTxtBox;
        private BindingSource folderBrowserDialogExampleFormBindingSource;
        private Button StartStopBtn;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorDir));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.TargetPathTxtBox = new System.Windows.Forms.TextBox();
            this.StartStopBtn = new System.Windows.Forms.Button();
            this.TargetDirBtn = new System.Windows.Forms.Button();
            this.folderBrowserDialogExampleFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.folderBrowserDialogExampleFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // TargetPathTxtBox
            // 
            this.TargetPathTxtBox.Location = new System.Drawing.Point(60, 14);
            this.TargetPathTxtBox.Name = "TargetPathTxtBox";
            this.TargetPathTxtBox.Size = new System.Drawing.Size(453, 20);
            this.TargetPathTxtBox.TabIndex = 1;
            // 
            // StartStopBtn
            // 
            this.StartStopBtn.Location = new System.Drawing.Point(12, 40);
            this.StartStopBtn.Name = "StartStopBtn";
            this.StartStopBtn.Size = new System.Drawing.Size(501, 61);
            this.StartStopBtn.TabIndex = 2;
            this.StartStopBtn.Text = "Start Monitoring";
            this.StartStopBtn.UseVisualStyleBackColor = true;
            this.StartStopBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // TargetDirBtn
            // 
            this.TargetDirBtn.Image = global::ShortLifeFileCapture.Properties.Resources.OpenDir2;
            this.TargetDirBtn.Location = new System.Drawing.Point(12, 12);
            this.TargetDirBtn.Name = "TargetDirBtn";
            this.TargetDirBtn.Size = new System.Drawing.Size(32, 23);
            this.TargetDirBtn.TabIndex = 0;
            this.TargetDirBtn.UseVisualStyleBackColor = true;
            this.TargetDirBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // folderBrowserDialogExampleFormBindingSource
            // 
            this.folderBrowserDialogExampleFormBindingSource.DataSource = typeof(ShortLifeFileCapture.Classes.FolderBrowserDialogExampleForm);
            // 
            // MonitorDir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 109);
            this.Controls.Add(this.StartStopBtn);
            this.Controls.Add(this.TargetPathTxtBox);
            this.Controls.Add(this.TargetDirBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MonitorDir";
            this.Text = "MonitorDir";
            ((System.ComponentModel.ISupportInitialize)(this.folderBrowserDialogExampleFormBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private Button TargetDirBtn;
    }
}