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

        private void ChooseDirBtn_Click(object sender, EventArgs e)
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
            FileStream inStream, outStream;
            filesNamesStrArray = Directory.GetFiles(this.TargetPathTxtBox.Text, "*", SearchOption.TopDirectoryOnly).OrderBy(m => new FileInfo(m).CreationTime).ToArray<string>();

            await Task.Run(() =>
            {
                foreach (var fileName in filesNamesStrArray) FileList.Add(fileName);
                while (Start)
                {
                    filesNamesStrArray = Directory.GetFiles(this.TargetPathTxtBox.Text, "*", SearchOption.TopDirectoryOnly).OrderBy(m => new FileInfo(m).CreationTime).ToArray<string>();
                    foreach (var file in filesNamesStrArray)
                    {
                        var sourceFile = Path.Combine(sourceDir, Path.GetFileName(file));
                        var targetFile = Path.Combine(targetDir, Path.GetFileName(file));
                        if (!File.Exists(targetFile)){

                            var btArr = new byte[BufferSize];

                            try
                            {
                                inStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
                                outStream = new FileStream(targetFile, FileMode.OpenOrCreate, FileAccess.Write);
                                //File.Copy(sourceFile, targetFile , true);
                                var readRes = inStream.ReadAsync(btArr, offset: 0, count: BufferSize);
                                while (readRes.Result > 0)
                                {
                                    int counter = 0;
                                    while ((btArr[counter] != 0) & (counter <= BufferSize))
                                    {
                                        outStream.WriteByte(btArr[counter]);
                                        counter++;
                                    }
                                    //outStream.WriteAsync(btArr, offset: 0, count: BufferSize);
                                    readRes = inStream.ReadAsync(btArr, offset: 0, count: BufferSize);
                                }
                                FileList.Add(filesNamesStrArray.Last());
                                inStream.Close();
                                outStream.Close();
                            }
                            catch (Exception)
                            {
                                //throw;
                            }

                        }
                    }
                }
            });
        }


        
    }
}
