using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using HotKeysLib;

namespace HotKeys
{
	/// <summary>
	/// Summary description for StartUp.
	/// </summary>
	public class StartUp
	{
		private StartUp()
		{
		}

		static private HotKeyListner hotKeyListner = null;
		static private SingleInstanceHandler application = new SingleInstanceHandler();

		static string commandLineArgumentsHandler(object sender, MyEventArgs eventArgs)
		{
			try
			{
				if(eventArgs.bNewInstance)
				{
					hotKeyListner = new HotKeyListner();
					hotKeyListner.HandleCommandLineArguments(eventArgs.strArgs);
					Application.Run(hotKeyListner);
					return "";
				}
				else
				{
					object[] parameters = new object[1]{ eventArgs.strArgs };
					if(parameters.Length == 0)
					{
						parameters = new object[1]{"Instructions"};
					}
					return (string)hotKeyListner.Invoke(new HotKeyListner.CommandLineArgumentsHandlerDelegate(hotKeyListner.HandleCommandLineArguments),parameters );
				}
			}
			catch(Exception exception)
			{
				return "Exception in MyEventHandlerFunction" + exception.Message;
			}
		}

//		private static Mutex appMutex = null;
		[STAThread]
		static void Main(string[] args) 
		{
			try
			{
				Application.EnableVisualStyles();
				application.MyEvent += new MyEventHandler(commandLineArgumentsHandler);			
				application.Run(args);
			}
			catch(Exception exp)
			{
				MessageBox.Show("Exception in Main : " + exp.Message);
			}
//			// The can be only one!! (Instance)
//			bool instanceFound = false;
//			appMutex = new Mutex(false, "HotKeyApp::" + Environment.UserName + "@" + Environment.UserDomainName);
//			try
//			{
//				if (!appMutex.WaitOne(0,false))
//				{
//					instanceFound = true;
//				}
//			}
//			catch(Exception e)
//			{
//				// TODO: remove lteral
//				MessageBox.Show("Unable to search for previous instances: " + e.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
//			}
//			if(instanceFound)
//			{
//				try
//				{
//					InstructionsForm instructionForm = new InstructionsForm();
//					instructionForm.ShowDialog();
//				}
//				catch(Exception e)
//				{
//					// ignore
//				}
//			}
//			else
//			{
//				if(args.Length>0)
//				{
//					InstructionsForm instructionForm = new InstructionsForm();
//					instructionForm.Show();
//				}
//				// HotKeyHelperFunctions.SetAutoStart(); // Replaced by startup menu item
//				Application.EnableVisualStyles(); // (Does not seem to work anyway :( )
//				Application.Run(new HotKeyListner());
//				HotKeyHelperFunctions.ClearListner();
//			}
		}
	}
}

