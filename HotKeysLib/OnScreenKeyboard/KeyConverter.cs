using System;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;

namespace HotKeysLib.OnScreenKeyboard
{
	public class KeyConverter : StringConverter 
	{
		private static StandardValuesCollection keyStrings;

		public KeyConverter()
		{
			ArrayList arrayList = new ArrayList();
			foreach(EnhancedKey enhancedKey in EnhancedKey.GetEnhancedKeys())
			{
				arrayList.Add(enhancedKey.ToString());
			}
			keyStrings = new StandardValuesCollection(arrayList);
		}

		public override bool GetStandardValuesSupported(
			ITypeDescriptorContext context) 
		{
			return true;
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if(sourceType == typeof(string))
                return true;
			else if(sourceType == typeof(Keys))
				return true;
			else
				return false;
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if(destinationType == typeof(string))
				return true;
			else if(destinationType == typeof(Keys))
				return true;
			else
				return false;
		}


		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object someValue)
		{
			if(someValue.GetType() == typeof(string))
			{
				foreach(EnhancedKey enhancedKey in EnhancedKey.GetEnhancedKeys())
				{
					if(enhancedKey.ToString() == someValue.ToString())
						return enhancedKey.Key;
				}
				return Keys.None;
			}
			else if(someValue.GetType() == typeof(Keys))
				return EnhancedKey.GetEnhancedKey((Keys)someValue).ToString();
			else
				return base.ConvertFrom(context, culture, someValue);
		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object someValue, Type destinationType)
		{
			if((someValue.GetType() == typeof(string)) && (destinationType==(typeof(Keys))))
			{
				foreach(EnhancedKey enhancedKey in EnhancedKey.GetEnhancedKeys())
				{
					if(enhancedKey.ToString() == someValue.ToString())
						return enhancedKey.Key;
				}
				return Keys.None;
			}
			else if((someValue.GetType() == typeof(Keys)) &&  (destinationType==(typeof(string))))
				return EnhancedKey.GetEnhancedKey((Keys)someValue).ToString();
			else
				return base.ConvertTo(context, culture, someValue, destinationType);
		}



		public override bool GetStandardValuesExclusive(
			ITypeDescriptorContext context) 
		{
			// returning false here means the property will
			// have a drop down and a value that can be manually
			// entered.      
			return false;
		}

		public override StandardValuesCollection GetStandardValues(
			ITypeDescriptorContext context) 
		{
			return keyStrings;
		}
	}

}
