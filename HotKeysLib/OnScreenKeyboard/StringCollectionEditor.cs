using System;
using System.Xml;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.Design;

namespace HotKeysLib.OnScreenKeyboard
{
	/// <summary>
	/// Summary description for StringCollectionEditor.
	/// </summary>
	public class StringCollectionEditor : System.ComponentModel.Design.CollectionEditor
	{
		public StringCollectionEditor() : base(typeof(ArrayList)) 
		{
		}

		protected override object CreateInstance(System.Type itemType)
		{
			return "" ;
		}


		protected override Type CreateCollectionItemType()
		{
			return typeof(string);
		}

	}
}
