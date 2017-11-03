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
            FileStream inStream, outStream;
            filesNamesStrArray = Directory.GetFiles(this.TargetPathTxtBox.Text, "*", SearchOption.TopDirectoryOnly).OrderBy(m => new FileInfo(m).CreationTime).ToArray<string>();

                await Task.Run(() =>
                {
                    foreach (var fileName in filesNamesStrArray) FileList.Add(fileName);
                    while (Start)
                    {
                        filesNamesStrArray = Directory.GetFiles(this.TargetPathTxtBox.Text, "*", SearchOption.TopDirectoryOnly).OrderBy(m => new FileInfo(m).CreationTime).ToArray<string>();
                        if (filesNamesStrArray.Count() > FileList.Count)  

                        //Is not safecheck to prove only the filenumber but the filenames
                        //find a way to return a array of filename differences - Compare 2 array of  and return A - B

                        {
                            var dif = filesNamesStrArray.Except(FileList).First().ToString();
                            var sourceFile = Path.Combine(sourceDir, Path.GetFileName(dif));
                            var targetFile = Path.Combine(targetDir, Path.GetFileName(dif));
                            var btArr = new byte[BufferSize];
                            inStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
                            outStream = new FileStream(targetFile, FileMode.OpenOrCreate, FileAccess.Write);
                            try
                            {
                                //File.Copy(sourceFile, targetFile , true);
                                var readRes = inStream.ReadAsync(btArr, offset: 0, count: BufferSize);
                                while (readRes.Result > 0)
                                {
                                    int counter = 0;
                                    while ( (btArr[counter] != 0) & (counter <= BufferSize ) )
                                    {
                                        outStream.WriteByte(btArr[counter]);
                                        counter++;
                                    }
                                    //outStream.WriteAsync(btArr, offset: 0, count: BufferSize);
                                    readRes = inStream.ReadAsync(btArr, offset: 0, count: BufferSize);
                                }
                                FileList.Add(filesNamesStrArray.Last());
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                inStream.Close();
                                outStream.Close();
                            }
                        }
                        //else if (filesNamesStrArray.Count() < FileList.Count)
                        //{
                        //    //reinitialize the list of files stored in FileList
                        //    FileList.Clear();
                        //    foreach (var fileName in filesNamesStrArray) FileList.Add(fileName);
                        //}

                    }
                });
        }


        
    }
}
