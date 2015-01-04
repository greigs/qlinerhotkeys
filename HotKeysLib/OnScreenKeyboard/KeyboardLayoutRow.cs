using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Runtime.Serialization;

namespace HotKeysLib.OnScreenKeyboard
{
	/// <summary>
	/// Summary description for KeyboardLayoutRow.
	/// </summary>
	[Serializable]
	public class KeyboardLayoutRow
	{
		public KeyboardLayoutRow()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		private ArrayList keys = new ArrayList();
		[Editor(typeof(LayoutKeysEditor), typeof(UITypeEditor))]
		public ArrayList Keys
		{
			get{return this.keys;}
			set{this.keys=value;}
		}

		private Single height = 1;
		public Single Height
		{
			get{return this.height;}
			set{this.height=value;}
		}

		private bool isPlaceHolder = false;
		public bool IsPlaceHolder
		{
			get{return this.isPlaceHolder;}
			set{this.isPlaceHolder = value;}
		}
	}
}
