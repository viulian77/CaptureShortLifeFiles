using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortLifeFileCapture.Forms
{
    public partial class MonitorDir : Form
    {
        bool Start = false;

        List<String> FileList = new List<string>();
        
        public MonitorDir()
        {
            InitializeComponent();
            textBox1.Text = "C:\\temp";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var curDir = textBox1.Text;
            if (Start == false)
                Start = true;
            else
                Start = false;
            var files = Directory.GetFiles(this.textBox1.Text, "*", SearchOption.TopDirectoryOnly).OrderBy(f => new FileInfo(f).CreationTime);
            foreach (var file in files ) { FileList.Add(file); }
            while (Start)
            {
                files = Directory.GetFiles(this.textBox1.Text, "*", SearchOption.TopDirectoryOnly).OrderBy(f => new FileInfo(f).CreationTime);
                if ( files.Count() != FileList.Count)
                {
                    // Copy file in temp
                    File.Copy(Path.Combine(curDir, files.Last()), Path.Combine("C:\\temp", files.Last()),true);
                    FileList.Add(files.Last());
                }
                    
            }

        }
    }
}
