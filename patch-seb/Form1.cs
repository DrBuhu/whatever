using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using patch_seb.Properties;

namespace patch_seb
{
    public partial class Form1 : Form
    {
		public static bool isBackup;
		public static bool started = false;
		public static string SEBPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\SafeExamBrowser\Application\";

		public Form1()
        {
            InitializeComponent();
        }
		public void AddLog(string log)
		{
			this.textBox1.Text += log + Environment.NewLine;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			AddLog("Safe Exam Browser Patch v" + Application.ProductVersion);
			
			if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\SafeExamBrowser\Application\SafeExamBrowser.exe")) {
				AddLog("Safe Exam Browser not found.");
				button1.Enabled = false;
			}
			else
			{
				FileVersionInfo SEBVersion = FileVersionInfo.GetVersionInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\SafeExamBrowser\Application\SafeExamBrowser.exe");
				if (SEBVersion.FileVersion != "3.8.0.742")
				{
					AddLog("Found unsupported Safe Exam Browser version.");
					button1.Enabled = false;
				}
				else
				{
					AddLog("Supported Safe Exam Browser version found.");
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			isBackup = checkBox1.Checked;
			if (!started)
			{
				started = true;
				Thread thr = new Thread(PatchThread);
				thr.Start();
			}
		}
		private void PatchThread()
		{
			if (isBackup)
			{
				try
				{
					File.Copy(SEBPath + @"SafeExamBrowser.exe", SEBPath + @"SafeExamBrowser.exe.backup");
					File.Copy(SEBPath + @"SafeExamBrowser.Client.exe", SEBPath + @"SafeExamBrowser.Client.exe.backup");
					File.Copy(SEBPath + @"SafeExamBrowser.Configuration.dll", SEBPath + @"SafeExamBrowser.Configuration.dll.backup");
					File.Copy(SEBPath + @"SafeExamBrowser.Monitoring.dll", SEBPath + @"SafeExamBrowser.Monitoring.dll.backup");
					File.Copy(SEBPath + @"SafeExamBrowser.UserInterface.Desktop.dll", SEBPath + @"SafeExamBrowser.UserInterface.Desktop.dll.backup");
					File.Copy(SEBPath + @"SafeExamBrowser.UserInterface.Shared.dll", SEBPath + @"SafeExamBrowser.UserInterface.Shared.dll.backup");
					File.Copy(SEBPath + @"SafeExamBrowser.WindowsApi.dll", SEBPath + @"SafeExamBrowser.WindowsApi.dll.backup");
				}
				catch (Exception ex)
				{
					AddLog(ex.Message);
				}
			}
			try
			{
				File.Delete(SEBPath + @"SafeExamBrowser.exe");
				File.Delete(SEBPath + @"SafeExamBrowser.Client.exe");
				File.Delete(SEBPath + @"SafeExamBrowser.Configuration.dll");
				File.Delete(SEBPath + @"SafeExamBrowser.Monitoring.dll");
				File.Delete(SEBPath + @"SafeExamBrowser.UserInterface.Desktop.dll");
				File.Delete(SEBPath + @"SafeExamBrowser.UserInterface.Shared.dll");
				File.Delete(SEBPath + @"SafeExamBrowser.WindowsApi.dll");
				File.WriteAllBytes(SEBPath + @"SafeExamBrowser.exe",Resources.SafeExamBrowser);
				File.WriteAllBytes(SEBPath + @"SafeExamBrowser.Client.exe", Resources.SafeExamBrowser_Client);
				File.WriteAllBytes(SEBPath + @"SafeExamBrowser.Configuration.dll", Resources.SafeExamBrowser_Configuration);
				File.WriteAllBytes(SEBPath + @"SafeExamBrowser.Monitoring.dll", Resources.SafeExamBrowser_Monitoring);
				File.WriteAllBytes(SEBPath + @"SafeExamBrowser.UserInterface.Desktop.dll", Resources.SafeExamBrowser_UserInterface_Desktop);
				File.WriteAllBytes(SEBPath + @"SafeExamBrowser.UserInterface.Shared.dll", Resources.SafeExamBrowser_UserInterface_Shared);
				File.WriteAllBytes(SEBPath + @"SafeExamBrowser.WindowsApi.dll", Resources.SafeExamBrowser_WindowsApi);
				AddLog("Patching done.");
				button1.Text = "PATCH DONE";
			}
			catch (Exception ex)
			{
				AddLog(ex.Message);
			}
		}
	}
}
