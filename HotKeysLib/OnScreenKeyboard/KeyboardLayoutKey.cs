using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace HotKeysLib.OnScreenKeyboard
{
	/// <summary>
	/// Summary description for KeyboardLayoutKey.
	/// </summary>
	[Serializable]
	public class KeyboardLayoutKey
	{
		public KeyboardLayoutKey()
		{
		}

		public KeyboardLayoutKey(Keys newKey)
		{
			this.key = newKey;
		}

		private Single width = 1;
		public Single Width
		{
			get{return this.width;}
			set{this.width=value;}
		}

		private Single height = 1;
		public Single Height
		{
			get{return this.height;}
			set{this.height=value;}
		}

		
		private System.Windows.Forms.Keys key = Keys.None;
		[TypeConverter(typeof(KeyConverter))]
		public System.Windows.Forms.Keys Key
		{
			get
			{
				return this.key;
			}
			set
			{
				ArrayList newTexts = new ArrayList();
				foreach(string text in this.texts)
				{
					if(text==this.key.ToString())
						newTexts.Add(value.ToString());
					else
						newTexts.Add(text);
				}
				this.key=value;
				this.texts = newTexts;
			}
		}

		private ArrayList texts = new ArrayList();
		[Editor(typeof(StringCollectionEditor), typeof(UITypeEditor))]
		public ArrayList Texts
		{
			get
			{	
				if (this.texts.Count==0)
					this.texts.Add(this.key.ToString());
				return this.texts;
			}
			set{this.texts = value;}
		}

		private bool isPlaceHolder = false;
		public bool IsPlaceHolder
		{
			get{return this.isPlaceHolder;}
			set{this.isPlaceHolder = value;}
		}

		private Icon icon = null;
		public Icon Icon
		{
			get{return this.icon;}
			set{this.icon=value;}
		}

		private int scanCode = 0;
		public int ScanCode
		{
			get{return this.scanCode;}
			set{this.scanCode = value;}
		}

	}
}
