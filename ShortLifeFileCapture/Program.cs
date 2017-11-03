using ShortLifeFileCapture.Forms;
using System;
using System.Windows.Forms;


namespace ShortLifeFileCapture
{
    static class Program
    {
        /// <summary>
        [STAThreadAttribute]
        static void Main()
        {
            Application.Run(new MonitorDir());
        }
    }
}
