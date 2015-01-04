using System;
using System.Collections;
using System.Windows.Forms;
using HotKeysLib.OnScreenKeyboard;

namespace HotKeysLib
{
	/// <summary>
	/// Summary description for EnhancedKey.
	/// </summary>
	public class EnhancedKey
	{
		public override string ToString()
		{
			if(description!="")
				return description;
			else
				return languageBasedKeyText( key ); // .ToString();
				//return key.ToString();
		}

		private Keys key;
		public Keys Key
		{
			get
			{
				return key;
			}
		}

		private string description = "";
		public string Description
		{
			get
			{
				if(description!="")
					return description;
				else
					return key.ToString();
			}
		}

		private bool usableAsHotKey = true;
		public bool UsableAsHotKey
		{
			get{return usableAsHotKey;}
			set{usableAsHotKey=value;}
		}

		public EnhancedKey(Keys newKey)
		{
			this.key = newKey;
		}
			
		public EnhancedKey(Keys newKey, string newDescription)
		{
			this.key = newKey;
			this.description = newDescription;
		}

		public EnhancedKey(Keys newKey, string newDescription, bool usable)
		{
			this.key = newKey;
			this.description = newDescription;
			this.usableAsHotKey = false;
		}

		public static bool IsKeyUsable(Keys key)
		{
			return GetEnhancedKey(key).UsableAsHotKey;
		}

		public static EnhancedKey GetEnhancedKey(Keys key)
		{
			foreach(EnhancedKey currentKey in EnhancedKey.GetEnhancedKeys())
			{
				if(currentKey.Key==key)
					return currentKey;				
			}
			return new EnhancedKey(Keys.None, "Key not in enhanced key collection");
		}

		private static Hashtable currentLanguageData = null;
		private static string currentLanguage = "";
		private static Hashtable keyScanCodePairs = null;
		private static string languageBasedKeyText(Keys key)
		{
			if(currentLanguageData == null || currentLanguage != KeyboardSettings.Current.Language)
			{
				// Load language data
				currentLanguage = KeyboardSettings.Current.Language;
				currentLanguageData = HotKeyHelperFunctions.GetLanguageData(currentLanguage);
				// reverse scanCodeKeyPairs
				Hashtable scanCodeKeyPairs = (Hashtable)currentLanguageData["scanCodeKeyPairs"];
				keyScanCodePairs = new Hashtable();
				foreach(int scanCode in scanCodeKeyPairs.Keys)
					keyScanCodePairs.Add(scanCodeKeyPairs[scanCode], scanCode);
			}
			if(currentLanguageData!=null)
			{
				if(currentLanguageData.ContainsKey(ShiftState.None))
				{
					Hashtable shiftStateNoneTable = (Hashtable)currentLanguageData[ShiftState.None]; 
					if(keyScanCodePairs.Contains(key))
					{
						int scancode = (int)keyScanCodePairs[key];
						if(shiftStateNoneTable.ContainsKey(scancode))
							return (string)(shiftStateNoneTable[scancode]);
					}
				}
			}
			return key.ToString();
		}

		private static ArrayList keys = null;
		public static ArrayList GetAvailableEnhancedKeys(Keys Exception)
		{
			ArrayList result = new ArrayList();
			foreach(EnhancedKey enhancedKey in EnhancedKey.GetEnhancedKeys())
			{
				if(enhancedKey.UsableAsHotKey)
					result.Add(enhancedKey);
			}
			foreach(HotKey hotKey in HotKey.GetAllHotKeys())
			{
				foreach(EnhancedKey key in result)
				{
					if(hotKey.Key == key.Key && key.Key!=Exception)
					{
						result.Remove(key);
						break;
					}
				}
			}
			return result;
		}
		public static ArrayList GetAvailableEnhancedKeys()
		{
			ArrayList result = new ArrayList(EnhancedKey.GetEnhancedKeys());
			foreach(HotKey hotKey in HotKey.GetAllHotKeys())
			{
				foreach(EnhancedKey key in result)
				{
					if(hotKey.Key == key.Key)
					{
						result.Remove(key);
						break;
					}
				}
			}
			return result;
		}
		public static ArrayList GetEnhancedKeys()
		{
			if(keys!=null)
				return keys;
			keys = new ArrayList();
			keys.Add(new EnhancedKey(Keys.A));
			keys.Add(new EnhancedKey(Keys.B, "" , false)); // For some very odd reason the B key seems unavailable?!?!
			keys.Add(new EnhancedKey(Keys.C));
			keys.Add(new EnhancedKey(Keys.D));
			keys.Add(new EnhancedKey(Keys.E));
			keys.Add(new EnhancedKey(Keys.F));
			keys.Add(new EnhancedKey(Keys.G));
			keys.Add(new EnhancedKey(Keys.H));
			keys.Add(new EnhancedKey(Keys.I));
			keys.Add(new EnhancedKey(Keys.J));
			keys.Add(new EnhancedKey(Keys.K));
			keys.Add(new EnhancedKey(Keys.L));
			keys.Add(new EnhancedKey(Keys.M));
			keys.Add(new EnhancedKey(Keys.N));
			keys.Add(new EnhancedKey(Keys.O));
			keys.Add(new EnhancedKey(Keys.P));
			keys.Add(new EnhancedKey(Keys.Q));
			keys.Add(new EnhancedKey(Keys.R));
			keys.Add(new EnhancedKey(Keys.S));
			keys.Add(new EnhancedKey(Keys.T));
			keys.Add(new EnhancedKey(Keys.U));
			keys.Add(new EnhancedKey(Keys.V));
			keys.Add(new EnhancedKey(Keys.W));
			keys.Add(new EnhancedKey(Keys.X));
			keys.Add(new EnhancedKey(Keys.Y));
			keys.Add(new EnhancedKey(Keys.Z));
			keys.Add(new EnhancedKey(Keys.D1, "1"));
			keys.Add(new EnhancedKey(Keys.D2, "2"));
			keys.Add(new EnhancedKey(Keys.D3, "3"));
			keys.Add(new EnhancedKey(Keys.D4, "4"));
			keys.Add(new EnhancedKey(Keys.D5, "5"));
			keys.Add(new EnhancedKey(Keys.D6, "6"));
			keys.Add(new EnhancedKey(Keys.D7, "7"));
			keys.Add(new EnhancedKey(Keys.D8, "8"));
			keys.Add(new EnhancedKey(Keys.D9, "9"));
			keys.Add(new EnhancedKey(Keys.D0, "0"));
			keys.Add(new EnhancedKey(Keys.NumPad1, "1 (numerical keypad)"));
			keys.Add(new EnhancedKey(Keys.NumPad2, "2 (numerical keypad)"));
			keys.Add(new EnhancedKey(Keys.NumPad3, "3 (numerical keypad)"));
			keys.Add(new EnhancedKey(Keys.NumPad4, "4 (numerical keypad)"));
			keys.Add(new EnhancedKey(Keys.NumPad5, "5 (numerical keypad)"));
			keys.Add(new EnhancedKey(Keys.NumPad6, "6 (numerical keypad)"));
			keys.Add(new EnhancedKey(Keys.NumPad7, "7 (numerical keypad)"));
			keys.Add(new EnhancedKey(Keys.NumPad8, "8 (numerical keypad)"));
			keys.Add(new EnhancedKey(Keys.NumPad9, "9 (numerical keypad)"));
			keys.Add(new EnhancedKey(Keys.NumPad0, "0 (numerical keypad)"));
			keys.Add(new EnhancedKey(Keys.F1));
			keys.Add(new EnhancedKey(Keys.F2));
			keys.Add(new EnhancedKey(Keys.F3));
			keys.Add(new EnhancedKey(Keys.F4));
			keys.Add(new EnhancedKey(Keys.F5));
			keys.Add(new EnhancedKey(Keys.F6));
			keys.Add(new EnhancedKey(Keys.F7));
			keys.Add(new EnhancedKey(Keys.F8));
			keys.Add(new EnhancedKey(Keys.F9));
			keys.Add(new EnhancedKey(Keys.F10));
			keys.Add(new EnhancedKey(Keys.F11));
			keys.Add(new EnhancedKey(Keys.F12));
			keys.Add(new EnhancedKey(Keys.F13));
			keys.Add(new EnhancedKey(Keys.F14));
			keys.Add(new EnhancedKey(Keys.F15));
			keys.Add(new EnhancedKey(Keys.F16));
			keys.Add(new EnhancedKey(Keys.F17));
			keys.Add(new EnhancedKey(Keys.F18));
			keys.Add(new EnhancedKey(Keys.F19));
			keys.Add(new EnhancedKey(Keys.F20));
			keys.Add(new EnhancedKey(Keys.F21));
			keys.Add(new EnhancedKey(Keys.F22));
			keys.Add(new EnhancedKey(Keys.F23));
			keys.Add(new EnhancedKey(Keys.F24));
			keys.Add(new EnhancedKey(Keys.Delete));
			keys.Add(new EnhancedKey(Keys.End));
			keys.Add(new EnhancedKey(Keys.Home));
			keys.Add(new EnhancedKey(Keys.Insert));
			keys.Add(new EnhancedKey(Keys.PageUp, "Page Up"));
			keys.Add(new EnhancedKey(Keys.PageDown, "Page Down"));
			keys.Add(new EnhancedKey(Keys.PrintScreen, "Print Screen"));
			keys.Add(new EnhancedKey(Keys.Pause));
			keys.Add(new EnhancedKey(Keys.NumLock, "Num Lock"));
			keys.Add(new EnhancedKey(Keys.Escape));
			keys.Add(new EnhancedKey(Keys.OemMinus, "-"));
			keys.Add(new EnhancedKey(Keys.Oemplus, "+"));
			keys.Add(new EnhancedKey(Keys.Oemcomma,","));
			keys.Add(new EnhancedKey(Keys.OemSemicolon,","));
			keys.Add(new EnhancedKey(Keys.Oemtilde,"`"));
			keys.Add(new EnhancedKey(Keys.OemOpenBrackets,"["));
			keys.Add(new EnhancedKey(Keys.OemCloseBrackets,"]"));
			keys.Add(new EnhancedKey(Keys.OemBackslash,@"\",false));
			keys.Add(new EnhancedKey(Keys.OemClear,"Backspace", false));
			keys.Add(new EnhancedKey(Keys.OemPeriod,"."));
			keys.Add(new EnhancedKey(Keys.OemSemicolon,":"));
			keys.Add(new EnhancedKey(Keys.OemQuotes,"'"));
			keys.Add(new EnhancedKey(Keys.OemQuestion,"?"));
			keys.Add(new EnhancedKey(Keys.Tab,"Tab",false));
			keys.Add(new EnhancedKey(Keys.Enter,"Enter"));
			keys.Add(new EnhancedKey(Keys.Space,"Space"));
			keys.Add(new EnhancedKey(Keys.Left));
			keys.Add(new EnhancedKey(Keys.Right));
			keys.Add(new EnhancedKey(Keys.Up));
			keys.Add(new EnhancedKey(Keys.Down));
			keys.Add(new EnhancedKey(Keys.Divide));
			keys.Add(new EnhancedKey(Keys.Multiply));
			keys.Add(new EnhancedKey(Keys.Decimal));
			keys.Add(new EnhancedKey(Keys.Menu,"",false));
			keys.Add(new EnhancedKey(Keys.Scroll));
			// Unusable
			keys.Add(new EnhancedKey(Keys.CapsLock, "Caps Lock" , false));
			keys.Add(new EnhancedKey(Keys.LShiftKey, "Left Shift" , false));
			keys.Add(new EnhancedKey(Keys.RShiftKey, "Right Shift" , false));
			keys.Add(new EnhancedKey(Keys.LControlKey, "Left Ctrl" , false));
			keys.Add(new EnhancedKey(Keys.RControlKey, "Right Ctrl" , false));
			keys.Add(new EnhancedKey(Keys.Alt, "Alt" , false));
			keys.Add(new EnhancedKey(Keys.LWin, "Left Windows" , false));
			keys.Add(new EnhancedKey(Keys.RWin, "Right Windows" , false));
			keys.Add(new EnhancedKey(Keys.None, "" , false));
			return keys;
		}
	}
}
