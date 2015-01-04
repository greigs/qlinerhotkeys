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
	/// Summary description for LayoutKeysEditor.
	/// </summary>
	public class LayoutKeysEditor : System.ComponentModel.Design.CollectionEditor
	{
		public LayoutKeysEditor(Type type) : base(type){}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			object[] attributes;
			XmlArrayItemAttribute xmlAttr;
			PropertyInfo propertyInfo = null;

			Type contextType = context.Instance.GetType();

			PropertyInfo[] propertyInfoInfo = contextType.GetProperties();
			for (int i = 0; i < propertyInfoInfo.Length; i++)
			{
				propertyInfo = propertyInfoInfo[i];

				if (context.PropertyDescriptor.Name == propertyInfo.Name)
				{
					attributes =
						propertyInfo.GetCustomAttributes(typeof(XmlArrayItemAttribute),false);
					if (attributes.Length > 0)
					{
						xmlAttr = attributes[0] as XmlArrayItemAttribute;
					}
				}
			}
			return base.EditValue (context, provider, value);
		}


		protected override System.Type[] CreateNewItemTypes() 
		{
			return new Type[] {typeof(KeyboardLayoutKey)};
		}

		protected override Type CreateCollectionItemType()
		{
			return typeof(KeyboardLayoutKey);
		}

	}
}
