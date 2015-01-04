using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace HotKeysLib.OnScreenKeyboard
{
	/// <summary>
	/// Summary description for KeyboardLayoutKeyTextElement.
	/// </summary>
	/// 
	[Serializable]
	public class KeyboardLayoutKeyTextElement
	{
		public KeyboardLayoutKeyTextElement()
		{
			this.largeFont = new Font("Arial Narrow", 50, FontStyle.Italic, GraphicsUnit.World);
			this.smallFont = new Font("Arial Narrow", 30, FontStyle.Italic, GraphicsUnit.World);
		}

		public KeyboardLayoutKeyTextElement(Font newLargeFont, Font newSmallFont, PointF newOffsetLargeFont, PointF newOffsetSmallFont, Color newColor)
		{
			this.fontColor = newColor;
			this.largeFont = newLargeFont;
			this.smallFont = newSmallFont;
			this.offsetLargeFont = newOffsetLargeFont;
			this.offsetSmallFont = newOffsetSmallFont;
		}

		private Color fontColor = Color.Black;
		public Color FontColor
		{
			get
			{
				return fontColor;
			}
			set
			{
				this.fontColor = value;
			}
		}

		private Font largeFont = null; // new Font("Arial", 20); // See constructor
		public Font LargeFont
		{
			get
			{
				return largeFont;
			}
			set
			{
				this.largeFont = value;
			}
		}

		private Font smallFont = null; // new Font("Arial", 20); // See constructor
		public Font SmallFont
		{
			get
			{
				return smallFont;
			}
			set
			{
				this.smallFont = value;
			}
		}

		private PointF offsetSmallFont = new PointF(2,5);
		private PointF offsetLargeFont = new PointF(0,0);
		[TypeConverter(typeof(PointFTypeConverter))]
		public PointF OffsetSmallFont
		{
			get{return this.offsetSmallFont;}
			set{this.offsetSmallFont=value;}
		}
		[TypeConverter(typeof(PointFTypeConverter))]
		public PointF OffsetLargeFont
		{
			get{return this.offsetLargeFont;}
			set{this.offsetLargeFont=value;}
		}
	}

	internal class PointFTypeConverter : TypeConverter
	{
		public PointFTypeConverter()
		{

		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType)
		{
			if (destinationType == typeof(string))
				return ((PointF)value).X.ToString() + " , " + ((PointF)value).Y.ToString();
			if (destinationType == typeof(PointF))
				return value;
			return "0 , 0";
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			try
			{
				string[] result = ((string)value).Split(",".ToCharArray());
				return new PointF(Convert.ToSingle(result[0]) , Convert.ToSingle(result[1]));
			}
			catch
			{
			}
			return new PointF(0,0);
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if(sourceType==typeof(string))
				return true;
			if(sourceType==typeof(PointF))
				return true;
			return base.CanConvertFrom (context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if(destinationType==typeof(string))
				return true;
			if(destinationType==typeof(PointF))
				return true;
			return base.CanConvertTo (context, destinationType);
		}



	}

}
