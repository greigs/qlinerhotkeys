using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Kennedy.ManagedHooks;
using HotKeysLib.UI.Forms;
using HotKeysLib.OnScreenKeyboard;

namespace HotKeysLib
{
	/// <summary>
	/// Summary description for HotKeyListner.
	/// </summary>
	public class HotKeyListner : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public delegate string CommandLineArgumentsHandlerDelegate(object[] args);

		public string HandleCommandLineArguments(object[] args)
		{
			if(args.Length > 0)
			{
				try
				{
					string[] arguments = (string[])args;
					return CommandLineInterface.HandleCliArguments(arguments);
				}
				catch(Exception exception)
				{
					return String.Format("Unable to complete your request because: {0}", exception.Message);
				}
			}
			return "";
		}

		public HotKeyListner()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
//			this.shellExtensionMessage = Win32Interop.RegisterWindowMessage(HotKeyHelperFunctions.SHELLEXTENSIONMESSAGE);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// HotKeyListner
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(0, 0);
			this.ControlBox = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			this.Name = "HotKeyListner";
			this.ShowInTaskbar = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HotKeyListner";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.HotKeyListner_Closing);
			this.Load += new System.EventHandler(this.HotKeyListner_Load);

		}
		#endregion

		WindowSelectionForm selectionForm = null;
		int previousKey = 0;
//		private int updateMessage = 0;
//		private int shellExtensionMessage = 0;
		protected override void WndProc(ref Message m)
		{
			
//			if (m.Msg == this.updateMessage)
//			{
//				HotKey.GetAllHotKeys(true);
//				if(keyBoardForm!=null)
//				{
//					this.keyBoardForm.UpdateScreen();
//				}
//			}
//			if (m.Msg == this.shellExtensionMessage)
//			{
//				Keys key = (Keys)(m.WParam.ToInt32());
//				HotKey hotkey = HotKey.GetHotKeyByKey(key);
//				if(this.keyBoardForm==null)
//					this.keyBoardForm = new KeyBoardForm();
//				switch(m.LParam.ToInt32())
//				{
//					case (int)ShellRequestType.Properties :
//						this.keyBoardForm.OpenPropertyDialog(hotkey);
//						break;
//					case (int)ShellRequestType.Delete :
//						this.keyBoardForm.DeleteHotKey(hotkey);
//						break;
//					case (int)ShellRequestType.New :
//						this.keyBoardForm.OpenNewHotKeyWizard(Keys.None);
//						break;
//					case (int)ShellRequestType.Rename :
//						// This is the only change to the config file that happens outside the keyboard process!
//						this.keyBoardForm.ReloadConfigFile();
//					
////						// If by coincidence any property dialogs are open make sure they get updated! First obtain a reference to an existing dialog
////						PropertiesForm propertiesForm = this.keyBoardForm.GetExistingPropertyDialog(hotkey);
////						// So reload config file:
////						HotKey.GetAllHotKeys(true);
////						// And update the keyboard
////						this.keyBoardForm.UpdateScreen();
////						// Because we reloaded the config file
//						break;
//				}
//			}
			if (m.Msg == (int)Win32Interop.WM_HOTKEY)
			{
				// If there is an invisible selection form KILL it by releasing the reference
				if(selectionForm!=null)
				{
					if(!selectionForm.Visible)selectionForm=null;
				}
				int lParam = m.LParam.ToInt32();
				int lParamHighWord = lParam >> 16;
				int lParamLowWord = lParam & 0x00FF;
				if (previousKey==lParamHighWord && selectionForm!=null)
				{
					selectionForm.SelectNextItem();
				}
				else
				{
					foreach(HotKey hotKey in HotKey.GetAllHotKeys())
					{
						if((int)hotKey.Key==lParamHighWord)
						{
							if(hotKey.TargetType==HotKeyTargetType.File&&hotKey.Executable)
							{
								ArrayList windows = hotKey.GetWindows();
								if((windows.Count == 0) || (lParamLowWord == 9))
								{
									this.startHotKey(hotKey);
								}
								else
								{
									// If there is MORE the 1 running instance show....
									if(windows.Count > 1)
									{	
										// Multiple running instances: show selection form
										previousKey = lParamHighWord;
										selectionForm = new WindowSelectionForm(windows, hotKey);
										selectionForm.TopMost = true;
										selectionForm.Show();
										selectionForm.Activate();						
									}
									else
									{
										// A single instance is running: bring to foreground
										((Window)windows[0]).RestoreAndBringToFront();
									}
								}
							}
							else
								startHotKey(hotKey);
						}
					}
				}
			}
			//Console.WriteLine("Message: " + m.ToString() + " " + DateTime.Now.ToString() );
			base.WndProc (ref m);
		}

		private void startHotKey(HotKey hotKey)
		{
			try
			{
				if(hotKey.TargetType==HotKeyTargetType.AddIn)
				{
					HotKeyAddInManager.InvokeAction((HotKeyConfiguredAddInAction)hotKey.Target);
				}
				else
				{
					ProcessStartInfo startinfo = new ProcessStartInfo(hotKey.Target.ToString());
					startinfo.Arguments = hotKey.Arguments;
					startinfo.WorkingDirectory = hotKey.WorkingDir;
					startinfo.UseShellExecute = true;
					Process.Start(startinfo);
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private void HotKeyListner_Load(object sender, System.EventArgs e)
		{
			this.Top = Screen.PrimaryScreen.Bounds.Height;
			this.Icon = null;
			this.Text = "";
			this.Show();
			//this.Visible = false;
			//this.Hide();

			// Set hook for continous winkey down detection
			IntPtr appInstance = GetApplicationInstance();
			keyboardHook = new Kennedy.ManagedHooks.KeyboardHook();
			keyboardHook.KeyboardEvent += new Kennedy.ManagedHooks.KeyboardHook.KeyboardEventHandler(keyboardHook_KeyboardEvent);
			
			keyboardHook.InstallHook();

			// Register system even to monitor for resolution or orientation changes
			Microsoft.Win32.SystemEvents.DisplaySettingsChanged +=new EventHandler(SystemEvents_DisplaySettingsChanged);

			HotKeyHelperFunctions.ListnerWindowHandle = this.Handle;
			HotKeyHelperFunctions.SetListner(this.Handle.ToInt32());
			ArrayList allHotKeys = HotKey.GetAllHotKeys();
			// Do some silly thing to make sure all icons are cached, resulting in faster first loadtime for the keyboardform
			int iconcount = 0;
			foreach(HotKey hotkey in HotKey.GetAllHotKeys())
			{
				if(hotkey.Icon!=null)
					iconcount++;
			}
			GC.Collect();

            KeyBoardForm.GetKeyBoardForm().Visible = true;
            KeyBoardForm.GetKeyBoardForm().Opacity = 1;
            KeyBoardForm.GetKeyBoardForm().Show();
            this.Show();
            this.Visible = true;
		}

		private IntPtr GetApplicationInstance()
		{
			System.Reflection.Assembly A = this.GetType().Assembly;
			System.Reflection.Module module = A.GetModules(false)[0];
			IntPtr appInstance = System.Runtime.InteropServices.Marshal.GetHINSTANCE(module);

			return appInstance;
		}

		private bool keyBoardFullyVisible()
		{
			return (KeyBoardForm.GetKeyBoardForm().Visible == true && KeyBoardForm.GetKeyBoardForm().Opacity == 1);
		}

		private Kennedy.ManagedHooks.KeyboardHook keyboardHook = null;
		private bool winKeyDown = false;
		private DateTime winKeyDownTime;
		// private KeyBoardForm keyBoardForm = KeyBoardForm.GetKeyBoardForm();
		private bool dropForm = false;
		private void keyboardHook_KeyboardEvent(Kennedy.ManagedHooks.KeyboardEvents kEvent, Keys key)
		{
			if(key == Keys.LWin || key == Keys.RWin)
			{
				KeyBoardForm keyBoardForm = KeyBoardForm.GetKeyBoardForm();
				if(kEvent == KeyboardEvents.KeyDown)
				{
					if(!this.keyBoardFullyVisible())
					{
						if(!winKeyDown)
						{
							winKeyDown = true;
							winKeyDownTime = DateTime.Now;
						}
						else 
						{
							if(((TimeSpan)DateTime.Now.Subtract(winKeyDownTime)).Seconds > KeyboardSettings.Current.SecondsBeforeDisplay)
							{
								if(keyBoardForm==null || this.dropForm )
								{
									if(this.dropForm)
									{
										keyBoardForm = null;
										KeyBoardForm.DropKeyBoardForm();
									}
									keyBoardForm = KeyBoardForm.GetKeyBoardForm();
									keyBoardForm.Show();
									this.dropForm = false;
								}
								else 
								{
									if(!keyBoardForm.Visible)
										keyBoardForm.Visible = true;
									keyBoardForm.TopMost = true;
									System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
									if(KeyboardSettings.Current.Fade)
									{
										while(keyBoardForm.Opacity < 1)
											keyBoardForm.Opacity += 0.2;
									}
									else
										keyBoardForm.Opacity = 1;
									keyBoardForm.Activate();
									keyBoardForm.Focus();
									System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
								}
							}
						}
					}
				}
				else // if(kEvent == KeyboardEvents.KeyUp)
				{
					if(keyBoardForm!=null)
					{
						//Win32Interop.SendMessage(Win32Interop.HWND_BROADCAST,Win32Interop.WM_KEYDOWN,Win32Interop.VK_SHIFT,0);
						Application.DoEvents();
						System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
						if(KeyboardSettings.Current.Fade)
						{
							while(keyBoardForm.Opacity > 0)
								keyBoardForm.Opacity -= 0.2;				
						}
						else
						{
							keyBoardForm.Opacity = 0;
						}
						keyBoardForm.Visible = false;
						System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
					}
					winKeyDown = false;
				}
			}
			GC.Collect();
			
		}

		private void HotKeyListner_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			keyboardHook.UninstallHook();
		}

		private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
		{
			this.dropForm = true;
		}
	}
}
