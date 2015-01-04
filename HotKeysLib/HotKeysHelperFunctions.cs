using System;
using System.Collections;
using System.Windows;
using System.Drawing;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;

namespace HotKeysLib
{
	/// <summary>
	/// Summary description for HotKeysHelperFunctions.
	/// </summary>
	public class HotKeyHelperFunctions
	{	
		//public static string UPDATECONFIG = "HotKeyUpdateConfig";
		public static string SHELLEXTENSIONMESSAGE = "HotKeyShowDialog";
		public static string CONFIGFILENAME = "hotkeysconfig.kbc";

		public static string ApplicationDataPath
		{
			// suggested location for app data. If doesn't exist it will be created
			get
			{
				string applicationDataPath = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) + "\\qliner\\hotkeys";
				// If does not exist try to create
				if(!Directory.Exists(applicationDataPath))
				{
					try
					{
						Directory.CreateDirectory(applicationDataPath);
					}
					catch(Exception e)
					{
						// rethrow
						throw new Exception("Unable to create or obtain application data path: " + applicationDataPath, e);
					}
				}
				return applicationDataPath;
			}
		}

		private static string completeConfigFilePath = HotKeyHelperFunctions.ApplicationDataPath + "\\" + HotKeyHelperFunctions.CONFIGFILENAME;
		public static FileSystemWatcher ConfigFileWatcher = new FileSystemWatcher();
		
		public static void PersistHotKeyConfig()
		{
			if(allHotKeys==null)
				return;
			FileStream stream = new FileStream(completeConfigFilePath,FileMode.Create);
			new BinaryFormatter().Serialize(stream,allHotKeys);
			stream.Close();
		}

		public static void CreateCreationScript(string filename)
		{
			StreamWriter writer = new StreamWriter(filename,false);
			foreach(HotKey hotkey in HotKey.GetAllHotKeys())
			{
				if(hotkey.TargetType == HotKeyTargetType.File || hotkey.TargetType == HotKeyTargetType.Folder)
					writer.WriteLine(
						string.Format("hotkeys.exe AddNewHotkey name=\"{0}\" key=\"{1}\" path=\"{2}\" arguments=\"{3}\" workingDirectory=\"{4}\" overwrite=\"{5}\"",
							hotkey.Name, hotkey.Key, hotkey.Target, hotkey.Arguments, hotkey.WorkingDir, true)
						);
			}
			writer.Close();
		}

		public static ArrayList GetAllHotKeys(bool forceUpdate)
		{
			if(forceUpdate)
				allHotKeys = null;
			return GetAllHotKeys();
		}

		private static ArrayList allHotKeys = null;
		public static ArrayList GetAllHotKeys()
		{
			if(HotKeyHelperFunctions.allHotKeys==null)
			{
				FileStream stream = null;
				try
				{
					// Check if file exists
					if(!File.Exists(completeConfigFilePath))
						throw new MissingHotKeyConfigFileException();
					// load & deserialize...
					stream = new FileStream(completeConfigFilePath,FileMode.Open);
					allHotKeys = (ArrayList)(new BinaryFormatter().Deserialize(stream));
				}
				catch(MissingHotKeyConfigFileException err)
				{
					// We can safely ignore this exception as a new file is created automaitcally later
					// We catch it so we can differentiate it from a corrupt/locked/ file
				}
				catch(Exception e)
				{
					// TODO: ugly solution + literal:
					// we just create a new one - we should rename it or something
					// might result in user data loss (corrupt but repearable config file. FIX!)
					// MessageBox.Show("Hotkeys applies it's default configuration." + Environment.NewLine + Environment.NewLine + "You should see this message after a (new) installation of the product. If this is not the case, please contact your system administrator.", "Applying default configuration" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				finally{if(stream!=null)stream.Close();}
			}
			if(allHotKeys==null)
			{
				// There really was no hotkey config available... create a new one
				allHotKeys = new ArrayList();
				
				// And add system defined Hotkeys
				// Pause: System Properties
				addSystemHotKey(allHotKeys, "System Properties", Keys.Pause, 163);
				// D: Show Desktop
				addSystemHotKey(allHotKeys, "Show Desktop", Keys.D, 34);
				// M: Minimize all Windows
				addSystemHotKey(allHotKeys, "Minimize all Windows", Keys.M, 34);
				// E: Explorer
				addSystemHotKey(allHotKeys, "Explorer (E)", Keys.E, 45);
				// F: Search
				addSystemHotKey(allHotKeys, "Search", Keys.F, 22);
				// F1: System help
				addSystemHotKey(allHotKeys, "Windows Help", Keys.F1, 23);
				// L: Lock desktop
				addSystemHotKey(allHotKeys, "Lock Desktop", Keys.L, 47);
				// R: Run dialog
				addSystemHotKey(allHotKeys, "Run", Keys.R, 24);
				// U: Utility manager
				addSystemHotKey(allHotKeys, "Utility Manager", Keys.U, 159);
				
				// Try to create some entries to start with...
				string programFilesPath = Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles);
				string windowsSystem32Path = Environment.GetFolderPath(System.Environment.SpecialFolder.System);
				string windowsPath = Environment.GetFolderPath(System.Environment.SpecialFolder.System).ToLower().Replace("\\system32","");
				string myDocuments = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
				
				// TODO: !!! Add code to check if files actually exist!!!!!!
				// N: Notepad
				addAppHotKey(allHotKeys, "Notepad", windowsSystem32Path + "\\notepad.exe", Keys.N);
				// Space: Explorer
				addAppHotKey(allHotKeys, "Explorer (Space)", windowsPath + "\\explorer.exe", Keys.Space);
				// I: Internet Explorer
				addAppHotKey(allHotKeys, "Internet Explorer", programFilesPath + "\\Internet Explorer\\iexplore.exe", Keys.I);
				// C: Cmd.exe
				addAppHotKey(allHotKeys, "Command Prompt", windowsSystem32Path + "\\cmd.exe", Keys.C);
				// J: Jaggle (marketing effort!)
				// addAppHotKey(allHotKeys, "Jaggle - It's time to Jaggle.", @"http://www.jaggle.nl/english.htm",Keys.J);
				// Home: My Documents
				addFolderHotKey(allHotKeys, "My Documents", myDocuments, Keys.Home);
				
				// PageUp: Volume Up
				addAddInHotKey(allHotKeys, "Volume Up", VolumeAddIn.ADDINID, VolumeAddIn.VOLUPACTIONID, "", Keys.PageUp); 
				// PageDown: Volume Down
				addAddInHotKey(allHotKeys, "Volume Down", VolumeAddIn.ADDINID, VolumeAddIn.VOLDOWNACTIONID, "", Keys.PageDown);
				// End: Volume Toggle
				addAddInHotKey(allHotKeys, "Volume On/Off", VolumeAddIn.ADDINID, VolumeAddIn.VOLTOGGLEACTIONID, "", Keys.End);
				// T: Show Clock
				addAddInHotKey(allHotKeys, "Show Clock", ClockAddIn.ADDINID, Guid.NewGuid(), "", Keys.T);

				// O: Outlook, if exists, else outlook express);
				string oPath = GetOfficeAppPath("OUTLOOK.EXE");
				if(File.Exists(oPath))
					addAppHotKey(allHotKeys, "Microsoft Outlook", oPath , Keys.O);
				else
				{
					oPath = programFilesPath + "\\Outlook Express\\msimn.exe";
					if(File.Exists(oPath))
						addAppHotKey(allHotKeys, "Outlook Express", oPath, Keys.O);
				}
				// P: Powerpoint
				string pPath = GetOfficeAppPath("POWERPNT.EXE");
				if(File.Exists(pPath))
					addAppHotKey(allHotKeys, "Microsoft Powerpoint", pPath , Keys.P);
				else
				{
					pPath = windowsSystem32Path + "\\mspaint.exe";
					if(File.Exists(pPath))
						addAppHotKey(allHotKeys, "Paint", pPath, Keys.P);
				}
				// W: Word
				string wPath = GetOfficeAppPath("WINWORD.EXE");
				if(File.Exists(wPath))
					addAppHotKey(allHotKeys, "Microsoft Word", wPath , Keys.W);
				else
				{
					wPath = programFilesPath + "\\Windows NT\\Accessories\\wordpad.exe";
					if(File.Exists(wPath))
						addAppHotKey(allHotKeys, "Wordpad", wPath, Keys.W);
				}
				// S: Excel (S)pread(s)heet
				string sPath = GetOfficeAppPath("EXCEL.EXE");
				if(File.Exists(sPath))
					addAppHotKey(allHotKeys, "Microsoft Excel", sPath , Keys.S);

				// `: devenv, if exits ;)
				if(File.Exists(programFilesPath + @"\Microsoft Visual Studio .NET 2003\Common7\IDE\devenv.exe"))
					addAppHotKey(allHotKeys, "Visual Studio .NET 2003", programFilesPath + @"\Microsoft Visual Studio .NET 2003\Common7\IDE\devenv.exe" , Keys.Oemtilde);

				// TODO: evaluate NS extension GUID
				// TODO: literal path to control panel item!
				// Esc: HotKeys (::{20D04FE0-3AEA-1069-A2D8-08002B30309D}\::{21EC2020-3AEA-1069-A2DD-08002B30309D}\::{4DE81366-C7E6-44E8-84F7-372E8BC81A70})
				// addAppHotKey(allHotKeys, "HotKeys Settings", @"::{20D04FE0-3AEA-1069-A2D8-08002B30309D}\::{21EC2020-3AEA-1069-A2DD-08002B30309D}\::{4DE81366-C7E6-44E8-84F7-372E8BC81A70}", Keys.Escape);
				// New GUID!!! : {FA925FB5-4F7E-4611-9AF1-425523B7F778}
				// addAppHotKey(allHotKeys, "HotKeys Settings", @"::{20D04FE0-3AEA-1069-A2D8-08002B30309D}\::{21EC2020-3AEA-1069-A2DD-08002B30309D}\::{FA925FB5-4F7E-4611-9AF1-425523B7F778}", Keys.Escape);

				// F2: Control Panel (::{20D04FE0-3AEA-1069-A2D8-08002B30309D}\::{21EC2020-3AEA-1069-A2DD-08002B30309D})
				addAppHotKey(allHotKeys, "Control Panel", @"::{20D04FE0-3AEA-1069-A2D8-08002B30309D}\::{21EC2020-3AEA-1069-A2DD-08002B30309D}", Keys.F2);

				// F3: Network Settings (::{20D04FE0-3AEA-1069-A2D8-08002B30309D}\::{21EC2020-3AEA-1069-A2DD-08002B30309D}\::{7007ACC7-3202-11D1-AAD2-00805FC1270E})
				addAppHotKey(allHotKeys, "Network Settings", @"::{20D04FE0-3AEA-1069-A2D8-08002B30309D}\::{21EC2020-3AEA-1069-A2DD-08002B30309D}\::{7007ACC7-3202-11D1-AAD2-00805FC1270E}", Keys.F3);

				PersistHotKeyConfig();

			}
			ConfigFileWatcher.Path = ApplicationDataPath;
			ConfigFileWatcher.Filter = CONFIGFILENAME;
			ConfigFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
			ConfigFileWatcher.EnableRaisingEvents = true;
			return allHotKeys;
		}

		private static void addSystemHotKey(ArrayList arrayList, string name, Keys key, int imageIndex)
		{
			HotKey newHotKey = new HotKey();
			newHotKey.Name = name;
			newHotKey.TargetType = HotKeyTargetType.System;
			// TODO: dirty trick - put icon index in target (from the other side: the System Hotkeys are a 'dead area' anyway
			newHotKey.Target = imageIndex;
			newHotKey.Key = key;
			arrayList.Add(newHotKey);
		} 

		private static void addAppHotKey(ArrayList arrayList, string name, string target, Keys key)
		{
			HotKey newHotKey = new HotKey();
			newHotKey.Name = name;
			newHotKey.TargetType = HotKeyTargetType.File;
			newHotKey.Target = target;
			newHotKey.Key = key;
			newHotKey.Enabled = true;
			arrayList.Add(newHotKey);
		}

		private static void addFolderHotKey(ArrayList arrayList, string name, string target, Keys key)
		{
			HotKey newHotKey = new HotKey();
			newHotKey.Name = name;
			newHotKey.TargetType = HotKeyTargetType.Folder;
			newHotKey.Target = target;
			newHotKey.Key = key;
			newHotKey.Enabled = true;
			arrayList.Add(newHotKey);
		}

		private static void addAddInHotKey(ArrayList arrayList, string name, Guid AddInID, Guid ActionID, string Config, Keys key)
		{			
			HotKey newHotKey = new HotKey();
			newHotKey.Name = name;
			newHotKey.TargetType = HotKeyTargetType.AddIn;
			newHotKey.Key = key;
			newHotKey.Enabled = true;

			HotKeyConfiguredAddInAction action = new HotKeyConfiguredAddInAction();
			action.AddInID = AddInID;
			action.ActionID = ActionID;
			action.Config = Config;

			newHotKey.Target = action;

			arrayList.Add(newHotKey);
		}

		private static string GetOfficeAppPath(string appExeName)
		{ 
			try
			{

				string officeFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Microsoft Office";
				foreach(string folder in Directory.GetDirectories(officeFolder, "office*"))
				{
					foreach(string fileName in Directory.GetFiles(folder, appExeName))
					{
						return fileName;
					}
				}
			}
			catch{}
			return "";
		}


		internal static void ValidateHotKeyName(string newName, HotKey currentHotKey)
		{
			// Just for fun we will forbid empty names. :))) Technically they are no problem though, it's just that
			// the user might be confused. (should we enforce the user experience at this level?!?! hmmm.
			// Stuff should be named!!
			if(newName.Trim() == "")
				throw new EmptyHotKeyNameException();
			// The name MUST be unique. Not because we want to but because the Shell Integration stuff
			// seems to freak out on duplicate names. So check for duplicity and throw an exception.
			foreach(HotKey hotKey in HotKeyHelperFunctions.GetAllHotKeys())
			{
				if(hotKey.ID!=currentHotKey.ID && hotKey.Name == newName.Trim())
					throw new DuplicateHotKeyNameException();
			}
		}
	
		public static Icon GetIconForHotKeyTarget(object target)
		{
			// Gets default icon (shell icon sized or resized 48 * 48)
			return GetIconForHotKeyTarget(target, HotkeyIconSize.sizeShell32x32Or48x38);
		}

		public static int GetShellIconSize()
		{
			// Retrieve actual size from registry:
			// HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics\Shell Icon Size
			RegistryKey registryKey = Registry.CurrentUser;
			RegistryKey runKey = registryKey.OpenSubKey(@"Control Panel\Desktop\WindowMetrics",false);
			return Convert.ToInt32(runKey.GetValue("Shell Icon Size"));  
		}

		public static Icon GetSystemIcon(int index)
		{
			IntPtr iconPtr = new IntPtr( Win32Interop.ExtractIcon( System.Runtime.InteropServices.Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(),Environment.SystemDirectory + "\\" + "Shell32.dll", index ) );
			if(!iconPtr.Equals(IntPtr.Zero))
			{
				return Icon.FromHandle(iconPtr);
			}
			else
				return null;
		}

		public static Icon GetIconForHotKeyTarget(object target, HotkeyIconSize iconSizeEnum)
		{
			uint iconSize = 0;
			int iconWidthHeight = 0;
			switch(iconSizeEnum)
			{
				case HotkeyIconSize.size16x16 :
					iconSize = Win32Interop.SHGFI_SMALLICON;
					iconWidthHeight = 16;
					break;
				case HotkeyIconSize.size32x32 :
					iconSize = Win32Interop.SHGFI_LARGEICON;
					iconWidthHeight = 32;
					break;
				case HotkeyIconSize.sizeShell32x32Or48x38 :
					iconSize = Win32Interop.SHGFI_SHELLICONSIZE;
					iconWidthHeight = GetShellIconSize();
					break;
			}
			
			Icon icon = null;
			switch(HotKeyHelperFunctions.GetTargetType(target))
			{
				case HotKeyTargetType.File :
					try
					{
						string fName = target.ToString();
						Win32Interop.SHFILEINFO shinfo = new Win32Interop.SHFILEINFO();
						int result = Win32Interop.SHGetFileInfo(fName, 0, ref shinfo, (uint)System.Runtime.InteropServices.Marshal.SizeOf(shinfo), Win32Interop.SHGFI_ICON |iconSize).ToInt32();
						icon = Icon.FromHandle(shinfo.hIcon);
					}
					catch{}//(Exception err){Console.WriteLine(err.Message);}
					if(icon==null)
					{
						icon = IconForUri(target.ToString());
					}
					break;
				case HotKeyTargetType.Folder :
					StreamReader streamReader = new StreamReader(Assembly.GetAssembly(typeof(HotKey)).GetManifestResourceStream("HotKeysLib.UI.Icons.Folder.ico"));
					icon = new Icon(streamReader.BaseStream,iconWidthHeight,iconWidthHeight);
					break;
				case HotKeyTargetType.System :
					//streamReader = new StreamReader(Assembly.GetAssembly(typeof(HotKey)).GetManifestResourceStream("HotKeysLib.UI.Icons.Windows.ico"));
					//icon = new Icon(streamReader.BaseStream,iconWidthHeight,iconWidthHeight);
					try{icon = GetSystemIcon((int)target);}catch{}
					break;
				case HotKeyTargetType.AddIn :
					// Check if AddIn has Icon of its own
					HotKeyConfiguredAddInAction action = (HotKeyConfiguredAddInAction)target;
					icon = HotKeyAddInManager.GetIcon(action.AddInID);
					if(icon == null)
					{
						// If not get some default icon
						streamReader = new StreamReader(Assembly.GetAssembly(typeof(HotKey)).GetManifestResourceStream("HotKeysLib.UI.Icons.AddIn.ico"));
						icon = new Icon(streamReader.BaseStream,iconWidthHeight,iconWidthHeight);
					}
					break;
			}

			if(icon==null)
			{
				// If all else has failed retrieve the 'unknown' icon
				StreamReader streamReader = new StreamReader(Assembly.GetAssembly(typeof(HotKey)).GetManifestResourceStream("HotKeysLib.UI.Icons.Unknown.ico"));
				icon = new Icon(streamReader.BaseStream,iconWidthHeight,iconWidthHeight);
			}
			return icon;
		}

		public static Icon IconForUri(string uriText)
		{
			// Returns icon for valid uri, else return null
			try
			{
				Uri uri = new Uri(uriText);
				Win32Interop.SHFILEINFO shinfo = new Win32Interop.SHFILEINFO();
				string extension = "";
				if(uri.Scheme==Uri.UriSchemeMailto) extension = ".msg";
				else if(uri.Scheme==Uri.UriSchemeFile) extension = uriText;
				else extension = ".html";
				IntPtr result = Win32Interop.SHGetFileInfo(extension, 0, ref shinfo,(uint)System.Runtime.InteropServices.Marshal.SizeOf(shinfo),Win32Interop.SHGFI_ICON | Win32Interop.SHGFI_LARGEICON | Win32Interop.SHGFI_USEFILEATTRIBUTES);
				return System.Drawing.Icon.FromHandle(shinfo.hIcon);
			}
			catch{}
			return null;
		}

		internal static Hashtable GetLanguageData(string languageName)
		{
			string languagePath = Path.GetDirectoryName( Application.ExecutablePath ) + "\\Keyboard Data\\";
			FileStream stream = new FileStream(languagePath + languageName + ".language.bin",FileMode.Open,FileAccess.Read);
			BinaryFormatter formatter = new BinaryFormatter();
			Hashtable result = (Hashtable)formatter.Deserialize(stream);
			stream.Close();
			return result;
		}

		internal static string[] GetAvailableLanguages()
		{
			string languagePath = Path.GetDirectoryName( Application.ExecutablePath ) + "\\Keyboard Data\\";
			string languageSuffix = ".language.bin";
			string[] files = Directory.GetFiles(languagePath, "*" + languageSuffix);
			for(int i = 0 ; i < files.Length; i++)
				files[i] = files[i].Trim().Replace(languagePath,"").Replace(languageSuffix,"");
			return files;
		}

		internal static HotKeyTargetType GetTargetType(object target)
		{
			if(target.GetType() == typeof(string))
			{
				string path = target.ToString();
				if(path == "")
					return HotKeyTargetType.System; // TODO: crummy implementation of target type discovery... hmmm
				if(Directory.Exists(path))
					return HotKeyTargetType.Folder;
				return
					HotKeyTargetType.File;
			}
			else if (target.GetType() == typeof(HotKeyConfiguredAddInAction))
			{
				return HotKeyTargetType.AddIn;
			}
			else if (target is int)
			{
				return HotKeyTargetType.System;
			}
			throw new Exception("Uexpected HotKeyTargetType");
		}

		public static IntPtr ListnerWindowHandle = IntPtr.Zero;
		public static ArrayList GetListnerWindows()
		{
			ArrayList result = new ArrayList();
			int hwnd = Win32Interop.GetWindow(Win32Interop.GetDesktopWindow(), Win32Interop.GW_CHILD);
			while(hwnd!=0)
			{
				bool toplevel = (Win32Interop.GetWindow(hwnd, Win32Interop.GW_OWNER)==0);
				if(toplevel)
				{
					string exepath = GetExeNameForWnd(hwnd);
					if (exepath.ToLower() == HotKeyHelperFunctions.GetAutoStartExecutablePath().ToLower() )
					{
						Window newWindow = new Window(hwnd,"",null);
						result.Add(newWindow);
					}
				}
				hwnd = Win32Interop.GetWindow(hwnd, Win32Interop.GW_HWNDNEXT);
			}
			return result;
		}

		public static string NormalizePath(string path)
		{
			StringBuilder input = new StringBuilder(path);
			StringBuilder result = new StringBuilder(Win32Interop.MAX_PATH);
			Win32Interop.GetLongPathName(input, result, Win32Interop.MAX_PATH);
			return result.ToString().ToLower();
		}

		public static ArrayList GetWindows(string executablePath, Icon icon)
		{
			ArrayList result = new ArrayList();
			int hwnd = Win32Interop.GetWindow(Win32Interop.GetDesktopWindow(), Win32Interop.GW_CHILD);
			while(hwnd!=0)
			{
				string title = GetWindowTitle(hwnd);
				if(title!="")
				{
					bool visible = Win32Interop.IsWindowVisible(hwnd);
					if(visible)
					{
						bool toplevel = (Win32Interop.GetWindow(hwnd, Win32Interop.GW_OWNER)==0);
						if(toplevel)
						{
							string exepath = GetExeNameForWnd(hwnd);
							if (HotKeyHelperFunctions.NormalizePath(exepath) == HotKeyHelperFunctions.NormalizePath(executablePath) )
							{
								// Filter out the "Program Manager" for Explorer exe's
								if(title != "Program Manager" || !executablePath.ToLower().EndsWith("explorer.exe"))
								{
									Window newWindow = new Window(hwnd,title,icon);
									result.Add(newWindow);
								}
							}
						}
					}
				}
				hwnd = Win32Interop.GetWindow(hwnd, Win32Interop.GW_HWNDNEXT);
			}
			return result;
		}

		public static string GetWindowTitle(int hwnd)
		{
			StringBuilder result = new StringBuilder(1024);
			int actualLength = Win32Interop.GetWindowText(hwnd,result,1024);
			return result.ToString();
		}

		public static string GetExeNameForWnd(int hwnd)
		{
			int procID = 0;
			Win32Interop.GetWindowThreadProcessId( hwnd, ref procID);
			int procHandle = Win32Interop.OpenProcess(Win32Interop.PROCESS_QUERY_INFORMATION + Win32Interop.PROCESS_VM_READ,false,procID);

			int[] modIDs = new int[1];
			int size = modIDs.Length;
			int needed = 0;
			bool succes = false;

			IntPtr buffer = System.Runtime.InteropServices.Marshal.AllocCoTaskMem( System.Runtime.InteropServices.Marshal.SizeOf( size ) * modIDs.Length );
			System.Runtime.InteropServices.Marshal.Copy( modIDs, 0, buffer, modIDs.Length );


			succes = Win32Interop.EnumProcessModules( procHandle, ref buffer, 1, ref needed);

			//string res = "";
			if( succes )
			{
				int[] arrayRes = new int[ size ];
				System.Runtime.InteropServices.Marshal.Copy( buffer, arrayRes, 0, size );
				System.Runtime.InteropServices.Marshal.FreeCoTaskMem( buffer );
				
				StringBuilder result2 = new StringBuilder(1024);
				Win32Interop.GetModuleFileNameEx(procHandle, arrayRes[0], result2, 1024);
				return result2.ToString();

			}
			else 
				return "";
		}

		public static bool IsExecutable(string path)
		{
			return path.ToString().TrimEnd().ToLower().EndsWith(".exe");
		}

		public static string GetAutoStartExecutablePath()
		{
			RegistryKey registryKey = Registry.CurrentUser;
			RegistryKey runKey = registryKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run",false);
			return runKey.GetValue("HotKeys").ToString();
		}

		public static void SetAutoStart()
		{
			RegistryKey registryKey = Registry.CurrentUser;
			RegistryKey runKey = registryKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run",true);
			runKey.SetValue("HotKeys",Application.ExecutablePath);
			runKey.Close();
			registryKey.Close();
		}

		public static void MakeCapsLockWin()
		{
			try
			{
				RegistryKey registryKey = Registry.LocalMachine;
				RegistryKey layoutKey = registryKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Keyboard Layout",true);
				byte[] registryValue = new byte[20]{0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x5B, 0xE0, 0x3a, 0x00, 0x00, 0x00, 0x00, 0x00};
				layoutKey.SetValue("Scancode Map",registryValue);
				layoutKey.Close();
				registryKey.Close();
			}
			catch{}
		}

		public static void MakeCapsLockLose()
		{
			try
			{
				RegistryKey registryKey = Registry.LocalMachine;
				RegistryKey layoutKey = registryKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Keyboard Layout",true);
				byte[] registryValue = new byte[20]{0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x5B, 0xE0, 0x3a, 0x00, 0x00, 0x00, 0x00, 0x00};
				object o = layoutKey.GetValue("Scancode Map");
				if(o!=null)
				{
					if(o.GetType() == registryValue.GetType())
					{
						byte[] existingValue = (byte[])o;
						if(existingValue.Length == registryValue.Length)
						{
							bool identical = true;
							for(int i = 0; i < existingValue.Length; i++)
							{
								if(existingValue[i]!=registryValue[i])
								{
									identical = false;
									break;
								}
							}
							if(identical)
								layoutKey.DeleteValue("Scancode Map",false);
						}
					}
				}
				layoutKey.Close();
				registryKey.Close();
			}
			catch{}
		}

		private static Regex emailAddressRegEx = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",RegexOptions.Compiled);
		public static bool IsValidEmailAddress(string emailAddress)
		{
			return emailAddressRegEx.IsMatch(emailAddress.Trim(), 0);
		}

		public static int GetListnerHwnd()
		{
			RegistryKey registryKey = Registry.CurrentUser;
			RegistryKey runKey = registryKey.CreateSubKey(@"Software\HotKeys");
			return (int)runKey.GetValue("ActiveListner", 0);
		}

		public static void SetListner(int hWnd)
		{
			RegistryKey registryKey = Registry.CurrentUser;
			RegistryKey runKey = registryKey.CreateSubKey(@"Software\HotKeys");
			runKey.SetValue("ActiveListner",hWnd);
			runKey.Close();
			registryKey.Close();
		}

		public static void ClearListner()
		{
			RegistryKey registryKey = Registry.CurrentUser;
			RegistryKey runKey = registryKey.CreateSubKey(@"Software\HotKeys");
			runKey.SetValue("ActiveListner", 0);
			runKey.Close();
			registryKey.Close();
		}

		public static void CreateNewHotkey(string path, Keys key)
		{
			CreateNewHotkey("", path, key, "", "");
		}

		public static void CreateNewHotkey(string name, string path, Keys key, string arguments, string workingDirectory)
		{
			CreateNewHotkey(name,path,key,arguments,workingDirectory,false);
		}

		public static void ShowAbout()
		{
			HotKeysLib.UI.Dialogs.AboutDialog.TextElement textElement = new HotKeysLib.UI.Dialogs.AboutDialog.TextElement(
				DISPLAYVERSION,
				new Font("Trebuchet MS",20),
				Brushes.White,
				new RectangleF(170,260,80,90),
				StringFormat.GenericDefault);
			ArrayList textElements = new ArrayList();
			textElements.Add(textElement);
			HotKeysLib.UI.Dialogs.AboutDialog.ShowAbout("HotKeysLib.UI.Images.about.png",textElements);
		}

		public const string DISPLAYVERSION = "v.1.2.1";

		public static void ShowInstructions()
		{
			HotKeysLib.UI.Dialogs.AboutDialog.TextElement textElement = new HotKeysLib.UI.Dialogs.AboutDialog.TextElement(
				DISPLAYVERSION,
				new Font("Trebuchet MS",14),
				Brushes.White,
				new RectangleF(118,200,80,90),
				StringFormat.GenericDefault);
			ArrayList textElements = new ArrayList();
			textElements.Add(textElement);
			HotKeysLib.UI.Dialogs.AboutDialog.ShowAbout("HotKeysLib.UI.Images.instructions.png",textElements);
		}

		public static void CreateNewHotkey(string name, string path, Keys key, string arguments, string workingDirectory, bool overwrite)
		{
			if(path.Trim()=="")
				throw new Exception("Invalid hotkey target.");
			HotKey existingHotkey = HotKey.GetHotKeyByKey(key);
			if(existingHotkey!=null && (overwrite == false || existingHotkey.TargetType == HotKeyTargetType.System))
				throw new Exception("Key allready in use");
			if(existingHotkey!=null && overwrite == true)
			{
				HotKey.GetAllHotKeys().Remove(existingHotkey);
				HotKey.Persist();	
			}
			HotKey newHotKey = new HotKey();
			string target = "";
			if(path.EndsWith(".lnk"))
			{
				ShellLink link = new ShellLink(path);
				target = link.Target;
				// copy argument en workingdir from link
				newHotKey.Arguments = link.Arguments;
				newHotKey.WorkingDir = link.WorkingDirectory;
				if(name=="")
					name = link.Description;
			}
			else
			{
				target = path;
			}
			if(System.IO.Directory.Exists(target))
			{
				newHotKey.TargetType = HotKeyTargetType.Folder;
				newHotKey.Target = target;
				if(name!="")
					newHotKey.Name = HotKey.CreateUniqueName( name );
				else
					newHotKey.Name = HotKey.CreateUniqueName( target );
			}
			else
			{
				newHotKey.TargetType = HotKeyTargetType.File;
				newHotKey.Target = target;
				if(name!="")
					newHotKey.Name = HotKey.CreateUniqueName( name );
				else
				{
					string newName = Path.GetFileNameWithoutExtension(target);
					if(newName.Trim()=="")
						newName = "New hotkey";
					newHotKey.Name = HotKey.CreateUniqueName( newName );
				}
				newHotKey.Arguments = arguments;
				newHotKey.WorkingDir = workingDirectory;
			}
			newHotKey.Key = key;
			newHotKey.Enabled = true;
			HotKey.GetAllHotKeys().Add(newHotKey);
			HotKey.Persist();
		}
	}

	public enum HotkeyIconSize
	{
		size16x16,size32x32,sizeShell32x32Or48x38
	};
}
