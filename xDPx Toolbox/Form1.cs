using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net;
using System.Diagnostics;
using Ionic.Zip;

namespace xDPx_Toolbox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static bool main_server_avail = false;
        WebClient webClient;               // Our WebClient that will be doing the downloading for us
        Stopwatch sw = new Stopwatch();    // The stopwatch which we will be using to calculate the download spe

        public void DownloadFile(string urlAddress, string location)
        {
            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                // The variable that will be holding the url address (making sure it starts with http://)
                Uri URL = urlAddress.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ? new Uri(urlAddress) : new Uri("http://" + urlAddress);

                // Start the stopwatch which we will be using to calculate the download speed
                sw.Start();

                try
                {
                    // Start downloading the file
                    webClient.DownloadFileAsync(URL, location);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // The event that will fire whenever the progress of the WebClient is changed
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Calculate download speed and output it to labelSpeed.
            //this.notifyIcon1.ShowBalloonTip(0x7d0, "Downloading", string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00")), ToolTipIcon.Info);

            // Update the progressbar percentage only when the value is not the same.
            progressBar1.Value = e.ProgressPercentage;

            // Show the percentage on our label.
            this.Text = e.ProgressPercentage.ToString() + "%";

            // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading
            this.notifyIcon1.ShowBalloonTip(0x7d0, "Downloading", (string.Format("{0} MB's / {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"))),ToolTipIcon.Info);
        }

        // The event that will trigger when the WebClient is completed
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            // Reset the stopwatch.
            sw.Reset();

            if (e.Cancelled == true)
            {
                this.notifyIcon1.ShowBalloonTip(0x7d0, "Failed","Download has been canceled.",ToolTipIcon.Error);
            }
            else
            {
                this.notifyIcon1.ShowBalloonTip(0x7d0,"Downloaded","Download completed!",ToolTipIcon.Info);
            }
        }

        string thisdir = Application.StartupPath;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                pictureBox2.Image = Properties.Resources.OFWMenu;
                pnlOFW.Visible = true;
                pnlCFW.Visible = false;
                pnlTest.Visible = false;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                pictureBox2.Image = Properties.Resources.CFWMenu;
                pnlOFW.Visible = false;
                pnlCFW.Visible = true;
                pnlTest.Visible = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            button1.Focus();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(thisdir + @"\\Toolbox\");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            pnlCFW.Visible = false;
            pnlTest.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlTest.Visible = false;
            pnlCFW.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            try
            {
                if(File.Exists(thisdir + @"\\Toolbox\\SFO Reader\\SFO Reader.exe"))
                {
                System.Diagnostics.Process.Start(thisdir + @"\\Toolbox\\SFO Reader\\SFO Reader.exe");
                }
                else
                {
                    DialogResult resuly = MessageBox.Show("Seems The File Is Missing \n\n Download It Seperately?", "File Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    {
                        if(resuly == DialogResult.Yes)
                        {
                            
                        }
                    }
                }
            }
            catch(Exception Ex)
            {
                
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            try
            {
                if (File.Exists(thisdir + @"\\Toolbox\\IDPS Stealer GUI\\IDPS Stealer GUI.exe"))
                {
                    this.WindowState = FormWindowState.Minimized;
                    System.Diagnostics.Process.Start(thisdir + @"\\Toolbox\\IDPS Stealer GUI\\");
                }
                else
                {
                    DialogResult resuly = MessageBox.Show("Seems The File Is Missing \n\n Download It Seperately?", "File Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    {
                        if (resuly == DialogResult.Yes)
                        {

                        }
                    }
                }
            }
            catch (Exception Ex)
            {

            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Your entering a testing phase for ps3 rif key decryption do u want to continue?", "Trail And Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                this.WindowState = FormWindowState.Minimized;
                System.Diagnostics.Process.Start(Application.StartupPath + @"\\Toolbox\\Tester Center\\KeyGen\\PS3 Key Gen.exe");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + @"\Toolbox\XMB Modder\PS3_XMB_Modder.exe"))
            {
                this.WindowState = FormWindowState.Minimized;
                System.Diagnostics.Process.Start(Application.StartupPath + @"\Toolbox\XMB Modder\PS3_XMB_Modder.exe");
            }
            else
            {
               DialogResult result = MessageBox.Show("This File Needs To Be Downloaded First", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
               if (DialogResult.Yes == result && System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true)
                {
                    DownloadFile("http://www.xxxthedartkprogramerxxx.net/Download/XMB Modder.zip", Application.StartupPath + @"\Toolbox\XMB Modder.zip");
                    this.notifyIcon1.ShowBalloonTip(0x7d0, "Extracting", "Extracting XMB Modder Please Wait",ToolTipIcon.Info);
                    Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(Application.StartupPath + @"\Toolbox\XMB Modder.zip");
                    zip.ExtractAll(Application.StartupPath + @"\Toolbox\");
                    this.notifyIcon1.ShowBalloonTip(0x7d0, "Extracted", "Opening File", ToolTipIcon.Info);
                    this.WindowState = FormWindowState.Minimized;
                    System.Diagnostics.Process.Start(Application.StartupPath + @"\Toolbox\XMB Modder\PS3_XMB_Modder.exe");
                    File.Delete(Application.StartupPath + @"\Toolbox\XMB Modder.zip");
                }
               else if(System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == false)
               {
                   MessageBox.Show("You Need An Internet Connection For This");
               }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ErrorReporter err = new ErrorReporter();
            err.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true)
            {
                this.WindowState = FormWindowState.Minimized;
                System.Diagnostics.Process.Start("http://www.ps3hax.net/showthread.php?t=76528");
            }
            else
            {
                MessageBox.Show("You Need An Active Internet Connection To Use This");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(File.Exists(Application.StartupPath +@"\\Toolbox\Dump Helper\\PlaystationHax.it.exe"))
            {
                this.WindowState = FormWindowState.Minimized;
                //System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                //{
                //    FileName = Application.StartupPath + @"\\Toolbox\Dump Helper\\PlaystationHax.it.exe",
                //    UseShellExecute = true,
                //    Verb = "open"
                //});
               System.Diagnostics.Process.Start(Application.StartupPath + @"\\Toolbox\Dump Helper\\PlaystationHax.it.exe");
            }
            else
            {
                MessageBox.Show("Could Not Load The File");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            System.Diagnostics.Process.Start(Application.StartupPath + @"\Toolbox\PeXploit\PeXploit.exe");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            System.Diagnostics.Process.Start(Application.StartupPath + @"\Toolbox\PeXploit Custom Downloader\PeXploit Downloader.exe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            System.Diagnostics.Process.Start(Application.StartupPath + @"\Toolbox\IDPS Viewer\IDPS Viewer.exe");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            System.Diagnostics.Process.Start(Application.StartupPath + @"\Toolbox\PKG Extractor\PKG Extractor.exe");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            System.Diagnostics.Process.Start(Application.StartupPath + @"\Toolbox\PS3 Creator\PS3 Creator.exe");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Will Be Included in next release");
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true)
                {
                    string islive = new WebClient().DownloadString("http://xxxthedarkprogramerxxx.net/update/test.txt");
                    if ("OK" == islive)
                    {
                        main_server_avail = true;
                    }
                }
            }
            catch
            {
                main_server_avail = false;
            }

            if(main_server_avail == true)
            {
                string version = "v1.1";
                string[] version = System.IO.File.ReadAllLines("PS3DB");
                this.notifyIcon1.ShowBalloonTip(0x7d0, "Update Available", "The Toolbox Is Being Downloaded", ToolTipIcon.Info);
            }
        }

    }
}
