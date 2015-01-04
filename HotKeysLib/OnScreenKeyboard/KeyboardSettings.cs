using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HotKeysLib.OnScreenKeyboard
{
	/// <summary>
	/// Summary description for KeyboardSettings.
	/// </summary>
	[Serializable]
	public class KeyboardSettings
	{
		public KeyboardSettings()
		{
		}

		public const string CONFIGFILENAME = "KeyboardSettings.bin";

		private static KeyboardSettings currentSettings = null;
		public static KeyboardSettings Current
		{
			get
			{
				if(currentSettings==null)
				{
					// Try to open the file
					try
					{
						FileStream file = new FileStream(HotKeyHelperFunctions.ApplicationDataPath + "\\" + KeyboardSettings.CONFIGFILENAME,FileMode.Open);
						BinaryFormatter formatter = new BinaryFormatter();
						currentSettings = (KeyboardSettings)formatter.Deserialize(file);
						file.Close();
					}
					catch
					{	
						// This means the file does not exist or is in a wrong format
						// just create a new object as we cannot do anything about it anyway
						currentSettings = new KeyboardSettings();
					}
				}
				return currentSettings;
			}
		}

		public static void Persist()
		{
			try
			{
				FileStream file = new FileStream(HotKeyHelperFunctions.ApplicationDataPath + "\\" + KeyboardSettings.CONFIGFILENAME,FileMode.Create);
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(file,currentSettings);
				file.Close();
			}
			catch
			{	
				// Nothing we can do about this...
			}
		}

		private bool fade = true;
		public bool Fade
		{
			get{return this.fade;}
			set{this.fade = value;}
		}

		private int secondsBeforeDisplay = 1;
		public int SecondsBeforeDisplay
		{
			get{return this.secondsBeforeDisplay;}
			set{this.secondsBeforeDisplay = value;}
		}

		private string style = "Silver";
		public string Style
		{
			get{return this.style;}
			set{this.style = value;}
		}

		private string layout = "Standard";
		public string Layout
		{
			get{return this.layout;}
			set{this.layout = value;}
		}

		private string language = "US";
		public string Language
		{
			get{return this.language;}
			set{this.language = value;}
		}
	}
}
