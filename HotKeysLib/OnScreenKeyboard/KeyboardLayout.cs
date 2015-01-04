using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HotKeysLib.OnScreenKeyboard
{
	/// <summary>
	/// Summary description for KeyboardLayout.
	/// </summary>
	[Serializable]	
	public class KeyboardLayout
	{
		public KeyboardLayout()
		{
		}

		private ArrayList rows = new ArrayList();

		[Editor(typeof(LayoutRowsEditor), typeof(UITypeEditor))]
		public ArrayList Rows
		{
			get{return this.rows;}
			set{this.rows=value;}
		}

		public void Save(string filename)
		{
			FileStream file = null;
			try
			{
				file = new FileStream(filename, FileMode.Create);
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(file,this);
				file.Close();
			}
			catch(Exception e)
			{
				file.Close();
				throw e;
			}
		}

		public static KeyboardLayout Open(string filename)
		{
			FileStream file = null;
			try
			{
				file = new FileStream(filename, FileMode.Open,FileAccess.Read);
				BinaryFormatter formatter = new BinaryFormatter();
				KeyboardLayout layout = (KeyboardLayout)formatter.Deserialize(file);
				file.Close();
				return layout;
			}
			catch(Exception e)
			{
				if(file!=null)
					file.Close();
				throw e;
			}
		}
	}
}
