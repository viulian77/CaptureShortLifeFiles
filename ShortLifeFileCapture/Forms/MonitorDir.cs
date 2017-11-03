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
        string sourceDir = @"C:\data";
        string targetDir = @"C:\temp\copied";
        bool Start = false;
        const int BufferSize = 60 * 1024;
        

        List<String> FileList = new List<string>();
        
        public MonitorDir()
        {
            InitializeComponent();
            TargetPathTxtBox.Text = sourceDir;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.TargetPathTxtBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private async void StartStopBtn_Click(object sender, EventArgs e)
        {

            string[] filesNamesStrArray;
            sourceDir = TargetPathTxtBox.Text;
            if (Start == false)
            {
                Start = true;
                StartStopBtn.Text = "Running";
            }
            else
            {
                Start = false;
                StartStopBtn.Text = "Not Running";
            }
            FileStream f, g;
            filesNamesStrArray = Directory.GetFiles(this.TargetPathTxtBox.Text, "*", SearchOption.TopDirectoryOnly).ToArray<string>(); //.OrderBy(f => new FileInfo(f).CreationTime).ToArray<string>();

                await Task.Run(() =>
                {
                    foreach (var fileName in filesNamesStrArray) FileList.Add(fileName);
                    while (Start)
                    {
                        filesNamesStrArray = Directory.GetFiles(this.TargetPathTxtBox.Text, "*", SearchOption.TopDirectoryOnly).ToArray<string>(); //.OrderBy(f => new FileInfo(f).CreationTime).ToArray<string>();
                        if (filesNamesStrArray.Count() != FileList.Count)
                        {
                            var sourceFile = Path.Combine(sourceDir, filesNamesStrArray.Last());
                            var targetFile = Path.Combine(targetDir, Path.GetFileName(filesNamesStrArray.Last()).ToString());
                            var btArr = new byte[BufferSize];
                            f = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
                            g = new FileStream(targetFile, FileMode.Create, FileAccess.Write);
                            try
                            {
                                //File.Copy(sourceFile, targetFile , true);
                                var readRes = f.ReadAsync(btArr, offset: 0, count: BufferSize);
                                do
                                {
                                    g.WriteAsync(btArr, offset: 0, count: BufferSize);
                                } while ( readRes.Result > 0 );

                                FileList.Add(filesNamesStrArray.Last());
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                f.Close();
                                g.Close();
                            }
                        }

                    }
                });
        }
    }
}
