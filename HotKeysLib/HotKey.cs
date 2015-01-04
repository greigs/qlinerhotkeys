using System;
using System.Drawing;
using System.Collections;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace HotKeysLib
{
	/// <summary>
	/// Summary description for HotKey.
	/// </summary>
	[Serializable()]
	public class HotKey : IDeserializationCallback 
	{
		public HotKey()
		{
			try
			{
				this.Name = "New HotKey";
			}
			catch
			{
				// make sure the name is unique
				this.Name = HotKey.CreateUniqueName("New HotKey");
			}
		}

		public static string CreateUniqueName(string baseName)
		{
			bool validNameFound = false;
			string newName = baseName.Trim();
			int counter = 0;
			while(validNameFound==false)
			{
				counter++;
				validNameFound = true;
				foreach(HotKey hotKey in HotKeyHelperFunctions.GetAllHotKeys())
				{
					if(hotKey.Name == newName.Trim())
					{
						validNameFound = false;
						break;
					}
				}
				if(!validNameFound)
					newName = baseName.Trim() + " (" + counter.ToString() + ")";
			}
			return newName;
		}
		
		private string name = "";
		public string Name
		{
			get{return name;}
			set
			{
				HotKeyHelperFunctions.ValidateHotKeyName(value, this);
				name = value.Trim();
			}
		}

		private HotKeyTargetType targetType = HotKeyTargetType.File;
		public HotKeyTargetType TargetType
		{
			get{return targetType;}
			set
			{
				this.icon = null;
				targetType = value;
			}
		}

		public bool Executable
		{
			get
			{
				return HotKeyHelperFunctions.IsExecutable(this.Target.ToString());
			}
		}

		[NonSerialized]
		private Icon icon = null;
		public Icon Icon
		{
			get
			{
				if(icon==null)
				{
					//icon = HotKeyHelperFunctions.GetIconForHotKeyTarget(this.Target);
				}
				return icon;
			}
			set
			{
				icon = value;
			}
		}

		private string arguments = "";
		public string Arguments
		{
			get{return this.arguments;}
			set{this.arguments = value;}
		}

		private string workingDir = "";
		public string WorkingDir
		{
			get{return this.workingDir;}
			set{this.workingDir = value;}
		}
		

		private string fileTarget = "";
		private string folderTarget = "";
		private int systemTarget = 0;
		private HotKeyConfiguredAddInAction addInTarget;
		public object Target
		{
			get
			{
				switch(this.TargetType)
				{
					case HotKeyTargetType.File :
						return fileTarget;
					case HotKeyTargetType.Folder :
						return folderTarget;
					case HotKeyTargetType.AddIn :
						return addInTarget;
					case HotKeyTargetType.System :
						return systemTarget;
					default :
						return "";
				}
			}
			set
			{
				this.icon = null;
				switch(this.TargetType)
				{
					case HotKeyTargetType.File :
						fileTarget = value.ToString();
						break;
					case HotKeyTargetType.Folder :
						folderTarget = value.ToString();
						break;
					case HotKeyTargetType.AddIn :
						addInTarget = (HotKeyConfiguredAddInAction)value;
						break;
					case HotKeyTargetType.System :
						systemTarget = (int) value;
						break;
				}
			}
		}
		
		private Keys key = Keys.A;
		public Keys Key
		{
			get{return key;}
			set
			{
				key = value;
				if(this.Enabled)this.AttachKey();
			}
		}

		public bool Enabled
		{
			get
			{
				return true;
			}
			set
			{
				if(value)
					this.AttachKey();
				else
					this.ReleaseKey();
			}

		}
		private int keyAttached = 0;
		private int altKeyAttached = 0;
		private void AttachKey()
		{
			this.ReleaseKey();
			keyAttached = Win32Interop.RegisterHotKey(HotKeyHelperFunctions.ListnerWindowHandle.ToInt32(),Win32Interop.GlobalAddAtom(Guid.NewGuid().ToString()),Win32Interop.MOD_WIN,(int)this.Key);
			altKeyAttached = Win32Interop.RegisterHotKey(HotKeyHelperFunctions.ListnerWindowHandle.ToInt32(),Win32Interop.GlobalAddAtom(Guid.NewGuid().ToString()),Win32Interop.MOD_WIN | Win32Interop.MOD_ALT,(int)this.Key);
		}

		private Guid id = Guid.NewGuid();
		public Guid ID
		{
			get
			{
				return id;
			}
			set
			{
				this.id = value;
			}
		}

		public static HotKey GetHotKeyByID(Guid ID)
		{
			foreach(HotKey hotKey in HotKey.GetAllHotKeys())
			{
				if(hotKey.ID==ID)
					return hotKey;
			}
			return null;
		}

		public static HotKey GetHotKeyByKey(Keys key)
		{
			foreach(HotKey hotKey in HotKey.GetAllHotKeys())
			{
				if(hotKey.Key==key)
					return hotKey;
			}
			return null;
		}

		private void ReleaseKey()
		{
			Win32Interop.UnregisterHotKey(HotKeyHelperFunctions.ListnerWindowHandle.ToInt32(), keyAttached);
			Win32Interop.UnregisterHotKey(HotKeyHelperFunctions.ListnerWindowHandle.ToInt32(), altKeyAttached);
			keyAttached = 0;
		}

		public static ArrayList GetAllHotKeys(bool forceUpdate)
		{
			return HotKeyHelperFunctions.GetAllHotKeys(forceUpdate);
		}

		public static ArrayList GetAllHotKeys()
		{
			return HotKeyHelperFunctions.GetAllHotKeys();
		}

		public static void Persist()
		{
			HotKeyHelperFunctions.PersistHotKeyConfig();
		}
		#region IDeserializationCallback Members

		public void OnDeserialization(object sender)
		{
			//if(this.Enabled)
				this.AttachKey();
		}

		#endregion

		public ArrayList GetWindows()
		{
			if(!this.Executable)
				throw new Exception("Cannot enumerate windows for non executables");
			return HotKeyHelperFunctions.GetWindows(this.Target.ToString(),this.Icon);
		}

	}	

}
