using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;

namespace MyBot.Run_Updater
{
    public partial class MainForm : Form
    {

        public RegistryKey MyBot_set = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MyBot_Updater");
        public RegistryKey MyBot_get = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MyBot_Updater");
        public string myBotNewVersionURL = "https://raw.githubusercontent.com/MyBotRun/MyBot/master/LastVersion.txt";
        public string myBotDownloadURL = null;
        public string userDownloadFolder = personalFolders.GetPath(KnownFolder.Downloads);
        public string newMyBotVersion = null;
        public string currentMyBotVersion = null;
        public string currentMyBotFileName = null;
        public string currentMyBotPath = null;
        public Thread downloadThread;
        public Thread unzipThread;
        public MainForm()
        {
            InitializeComponent();
            Logger("Hey, I'm Hendrik. Your MyBot Updater!", Color.Blue);
        }

        public void Btn_checkUpdate_Click(object sender, EventArgs e)
        {
            Logger("Open editor!", Color.Black);
            Logger("Please go in your MyBot folder and select the Application .exe (MyBot.run)", Color.Black);
            OpenFileDialog openCurrentMyBot = new OpenFileDialog();
            openCurrentMyBot.Title = "Choose MyBot.run.exe";
            openCurrentMyBot.Filter = "Application file|*.exe";
            openCurrentMyBot.InitialDirectory = userDownloadFolder;
            if (openCurrentMyBot.ShowDialog() == DialogResult.OK)
            {
                Logger("You chose the application = " + openCurrentMyBot.FileName, Color.Black);

                MyBot_set.SetValue("mybot_path", Path.GetDirectoryName(openCurrentMyBot.FileName));
                Logger("MyBot path = " + MyBot_set.GetValue("mybot_path").ToString(), Color.Red, 3);

                MyBot_set.SetValue("mybot_exe", Path.GetFullPath(openCurrentMyBot.FileName));
                Logger("MyBot xxe path = " + MyBot_set.GetValue("mybot_exe").ToString(), Color.Red, 3);

                string latestMyBotPath = Path.GetFullPath(openCurrentMyBot.FileName);
                var latestMyBotVersionInfo = FileVersionInfo.GetVersionInfo(latestMyBotPath);
                currentMyBotVersion = "v" + latestMyBotVersionInfo.FileVersion;

                MyBot_set.SetValue("mybot_version", currentMyBotVersion);
                Logger("MyBot current version = " + MyBot_set.GetValue("mybot_version"), Color.Red, 3);

                WebClient myBotNewVersionClient = new WebClient();
                Stream stream = myBotNewVersionClient.OpenRead(myBotNewVersionURL);
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();

                var sb = new StringBuilder(content.Length);
                foreach (char i in content)
                {
                    if (i == '\n')
                    {
                        sb.Append(Environment.NewLine);
                    }
                    else if (i != '\r' && i != '\t')
                        sb.Append(i);
                }

                content = sb.ToString();

                var vals = content.Split(
                                            new[] { Environment.NewLine },
                                            StringSplitOptions.None
                                        )
                            .SkipWhile(line => !line.StartsWith("[general]"))
                            .Skip(1)
                            .Take(1)
                            .Select(line => new
                            {
                                Key = line.Substring(0, line.IndexOf('=')),
                                Value = line.Substring(line.IndexOf('=') + 1).Replace("\"", "").Replace(" ", "")

                            });
                /*
                 * Complete output: vals.FirstOrDefault().Key  + vals.FirstOrDefault().Value;
                 * To get the first key + value thy this code line
                 * Only the first value: vals.FirstOrDefault().Value;
                 **/
                newMyBotVersion = vals.FirstOrDefault().Value;
                MyBot_set.SetValue("mybot_version", newMyBotVersion);
                Logger("MyBot new version = " + MyBot_set.GetValue("mybot_version").ToString(), Color.Red);
                myBotDownloadURL = "https://github.com/MyBotRun/MyBot/releases/download/MBR_" + newMyBotVersion + "/MyBot-MBR_" + newMyBotVersion + ".zip";
                if (currentMyBotVersion != newMyBotVersion)
                {
                    Logger("Wait for download...", Color.Black);
                    DialogResult wantUpdateYoN = MessageBox.Show("New version " + newMyBotVersion + " is available. Would you like to install it?", "New version", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (wantUpdateYoN == DialogResult.Yes)
                    {
                        Logger("The bot will be updated.", Color.Blue);
                        if (File.Exists(userDownloadFolder + "/MyBot-MBR_" + newMyBotVersion + ".zip"))
                        {
                            Logger("Zip is allready exists!", Color.Black);
                            UnZip_MyBot();
                        }
                        else
                        {
                            Logger("Start Download...", Color.Blue);
                            startDownload();
                        }
                    }
                    else if (wantUpdateYoN == DialogResult.No)
                    {
                        Logger("Canceled...", Color.Red);
                    }
                }
                else
                {
                    Logger("Your version is the newest!", Color.Green);
                }
            }
            else
            {
                Logger("No file selected!", Color.Red);
            }
        }

        #region MainForm Load
        private void MainForm_Load(object sender, EventArgs e)
        {
            //getRegistryValue();
        }
        #endregion

        #region Unzip
        public void UnZip_MyBot()
        {
            if (!File.Exists(userDownloadFolder + "/MyBot-MBR_" + newMyBotVersion))
            {
                BackgroundWorker unzipBackgroundWorker = new BackgroundWorker();
                unzipThread = new Thread(() =>
                {
                    // To report progress from the background worker we need to set this property
                    unzipBackgroundWorker.WorkerReportsProgress = true;
                    unzipBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(unzipBackgroundWorker_ProgressChanged);
                    // This event will be raised on the worker thread when the worker starts
                    unzipBackgroundWorker.DoWork += new DoWorkEventHandler(unzipBackgroundWorker_DoWork);
                    // This event will be raised when we call ReportProgress
                    unzipBackgroundWorker.RunWorkerAsync();
                });
                unzipThread.Start();
                //using (var unzip = new unzip(userDownloadFolder + "/MyBot-MBR_" + newMyBotVersion + ".zip"))
                //{

                //    Logger("Unzip the MyBot-MBR_" + newMyBotVersion + ".zip", Color.Black);
                //    unzip.ExtractToDirectory(userDownloadFolder);
                //}
                unzipBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(unzipBackgroundWorker_ProgressChanged);
                Logger("Unzip the MyBot-MBR_" + newMyBotVersion + ".zip", Color.Black);
            }
            else
            {
                Logger("Folder allready exists!", Color.Blue);
            }
        }
        void unzipBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Your background task goes here           
            ZipFile.ExtractToDirectory(userDownloadFolder + "/MyBot-MBR_" + newMyBotVersion + ".zip", userDownloadFolder);
        }
        // Back on the 'UI' thread so we can update the progress bar
        void unzipBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // The progress percentage is a property of e
            progressBarMain.Value = e.ProgressPercentage;
        }
        #endregion

        #region Start Download and Progress bar
        public void startDownload()
        {
            downloadThread = new Thread(() =>
            {
                WebClient myBotDownloadNewVersion = new WebClient();
                myBotDownloadNewVersion.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                myBotDownloadNewVersion.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                myBotDownloadNewVersion.DownloadFileAsync(new Uri(myBotDownloadURL), userDownloadFolder + "/MyBot-MBR_" + newMyBotVersion + ".zip");
            });
            downloadThread.Start();
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                Logger("Downloading " + ConvertBytesToMegabytes(bytesIn) + " MB/s of " + ConvertBytesToMegabytes(totalBytes) + " MB/s", Color.Blue, 2);
                progressBarMain.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                Logger("Fully Downloaded", Color.Green);
                progressBarMain.Value = 0;
                UnZip_MyBot();
            });
        }
        #endregion

        #region Scroll Auto to Bottom
        // bind this method to its TextChanged event handler:
        // richTextBox.TextChanged += richTextBox_TextChanged;
        private void richTextBoxLog_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            richTextBoxLog.SelectionStart = richTextBoxLog.Text.Length;
            // scroll it automatically
            richTextBoxLog.ScrollToCaret();
        }
        #endregion

        #region Logger Method
        /* public void Logger(RichTextBox TextEventLog, Color TextColor, string EventText)
         * For custom RichTextBox
         **/
        public void Logger(string EventText, Color TextColor, int Option = 1)
        {
            RichTextBox TextEventLog = richTextBoxLog;
            Label status = labelstatus;
            if (Option == 1)
            {
                if (TextEventLog.InvokeRequired)
                {
                    TextEventLog.BeginInvoke(new Action(delegate
                    {
                        Logger(EventText, TextColor, Option);
                    }));
                    return;
                }
                string nDateTime = DateTime.Now.ToString("hh:mm:ss tt") + ": ";

                // color text.
                TextEventLog.SelectionStart = TextEventLog.Text.Length;
                TextEventLog.SelectionColor = TextColor;

                labelstatus.Text = EventText;

                // newline if first line, append if else.
                if (TextEventLog.Lines.Length == 0)
                {
                    TextEventLog.AppendText(nDateTime + EventText);
                    TextEventLog.ScrollToCaret();
                    TextEventLog.AppendText(Environment.NewLine);
                }
                else
                {
                    TextEventLog.AppendText(nDateTime + EventText + Environment.NewLine);
                    TextEventLog.ScrollToCaret();
                }
            }
            else if (Option == 2)
            {
                labelstatus.Text = EventText;
            }
            else if (Option == 3)
            {
                // Nothing
            }
        }
        #endregion

        #region Registry Methods
        public void getRegistryValue()
        {
            if (MyBot_get != null)
            {
                currentMyBotVersion = (string)MyBot_get.GetValue("mybot_version", null);
                currentMyBotFileName = (string)MyBot_get.GetValue("mybot_exe", null);
                currentMyBotPath = (string)MyBot_get.GetValue("mybot_path", null);
            }
            else
            {
                MyBot_set.SetValue("mybot_path", currentMyBotPath);
                MyBot_set.SetValue("mybot_exe", currentMyBotFileName);
                MyBot_set.SetValue("mybot_version", currentMyBotVersion);
            }
        }
        #endregion

        #region Convert
        public static string ConvertBytesToMegabytes(double bytes)
        {
            double result = (bytes / 1024f) / 1024f;
            return result.ToString("0.00");
        }

        public static string ConvertKilobytesToMegabytes(double kilobytes)
        {
            double result = kilobytes / 1024f;
            return result.ToString("0.00");
        }
        #endregion

        private void Btn_cancel_Click(object sender, EventArgs e)
        {

        }
    }
}

