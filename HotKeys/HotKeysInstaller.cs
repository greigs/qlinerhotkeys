using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Windows;
using System.IO;
using HotKeysLib;

namespace HotKeys
{
	/// <summary>
	/// Summary description for HotKeysInstaller.
	/// </summary>
	[RunInstaller(true)]
	public class HotKeysInstaller : System.Configuration.Install.Installer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HotKeysInstaller()
		{
			// This call is required by the Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		public override void Install(System.Collections.IDictionary
			stateSaver)
		{
			base.Install(stateSaver);
			if(Context.Parameters["UseCapsLock"]=="1")
			{
				// MessageBox.Show("You need to manually restart your computer after installation for the Caps Lock as Windows Key feature to work.","Caps Lock as Windows Key",MessageBoxButtons.OK, MessageBoxIcon.Information);
				HotKeyHelperFunctions.MakeCapsLockWin();
			}
			ProcessStartInfo startinfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().Location);
			startinfo.WorkingDirectory = Application.StartupPath;
			startinfo.Arguments = "Instructions";
			startinfo.UseShellExecute = true;
			Process.Start(startinfo);
		}


		protected override void OnBeforeUninstall(IDictionary savedState)
		{
			base.OnBeforeUninstall (savedState);
			foreach(Process process in Process.GetProcesses())
			{
				if(process.ProcessName == "HotKeys")
				{
					try
					{
						process.Kill();
					}
					catch{}
				}
			}
			HotKeyHelperFunctions.CreateCreationScript(HotKeyHelperFunctions.ApplicationDataPath + "\\restore.bat");
			HotKeyHelperFunctions.MakeCapsLockLose();
			// TODO: literal text remove
//			if(MessageBox.Show("Do you want to remove the HotKeys configuration files?", "Removing files", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
//				== System.Windows.Forms.DialogResult.Yes)
//			{
//				// Try to remove folder
//				if(System.IO.Directory.Exists(HotKeyHelperFunctions.ApplicationDataPath))
//				{
//					try
//					{
//						Directory.Delete(HotKeyHelperFunctions.ApplicationDataPath,true);
//					}
//					catch(Exception e)
//					{
//						// TODO: literal text remove
//						MessageBox.Show("Unable to remove configuration files: " + e.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
//					}
//				}
//				// TODO: delete autostart regkey!!
//			}
		}


		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
