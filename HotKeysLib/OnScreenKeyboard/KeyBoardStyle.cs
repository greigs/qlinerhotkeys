using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HotKeysLib.OnScreenKeyboard
{
	/// <summary>
	/// Summary description for KeyboardStyle.
	/// </summary>
	/// 
	[Serializable]
	public class KeyboardStyle
	{
		public KeyboardStyle()
		{
			this.keyboardLayoutKeyTextElements.Add(new KeyboardLayoutKeyTextElement());	
		}

		private ArrayList keyboardLayoutKeyTextElements = new ArrayList();
		[Editor(typeof(TextElementEditor), typeof(UITypeEditor))]
		public ArrayList KeyTextElements
		{
			get{return this.keyboardLayoutKeyTextElements;}
			set{this.keyboardLayoutKeyTextElements=value;}
		}

//		//		Pen outerPen = new Pen(Brushes.DarkGray,4);
//		//		Color darkColor = Color.FromArgb(225,225,212);
//		//		Color lightColor = Color.White;
//		//		Color fontColor = Color.Black;
//		//		Color backColor = Color.White;
//
//		Pen outerPen = new Pen(Brushes.Black,4);
//		Color darkColor = Color.Black;
//		Color lightColor = Color.Gray;
//		Color fontColor = Color.White;
//		Color backColor = Color.FromArgb(15,15,15);

		private int outerPenWidth = 4;
		public int OuterPenWidth
		{
			get{return outerPenWidth;}
			set{this.outerPenWidth = value;}
		}

		private Color outerPenColor = Color.DarkGray;
		public Color OuterPenColor
		{
			get{return outerPenColor;}
			set{this.outerPenColor = value;}
		}

		private Color darkColor = Color.FromArgb(225,225,212);
		public Color DarkColor
		{
			get{return darkColor;}
			set{this.darkColor = value;}
		}

		private Color lightColor = Color.White;
		public Color LightColor
		{
			get{return lightColor;}
			set{this.lightColor = value;}
		}

		private Color fontColor = Color.Black;
		public Color FontColor
		{
			get{return fontColor;}
			set{this.fontColor = value;}
		}

		private Color backColor = Color.White;
		public Color BackColor
		{
			get{return backColor;}
			set{this.backColor = value;}
		}

//		private Color keyBackColor = Color.White;
//		public Color KeyBackColor
//		{
//			get
//			{
//				return keyBackColor;
//			}
//			set
//			{
//				this.keyBackColor = value;
//			}
//		}

//		private int edgeWidth = 4;
//		public int EdgeWidth
//		{
//			get
//			{
//				return edgeWidth;
//			}
//			set
//			{
//				this.edgeWidth = value;
//			}
//		}

		private PointF offsetIcon = new PointF(0,0);
		[TypeConverter(typeof(PointFTypeConverter))]
		public PointF OffsetIcon
		{
			get{return this.offsetIcon;}
			set{this.offsetIcon=value;}
		}

//		private int iconSize = 50;
//		public int IconSize
//		{
//			get{return this.iconSize;}
//			set{this.iconSize=value;}
//		}

		private int edgeExternalWidth = 20;
		public int EdgeExternalWidth
		{
			get{return edgeExternalWidth;}
			set{this.edgeExternalWidth = value;}
		}

		private int edgeInternalWidth = 20;
		public int EdgeInternalWidth
		{
			get{return edgeInternalWidth;}
			set{this.edgeInternalWidth = value;}
		}

//		private Color edgeExternalColor = Color.Black;
//		public Color EdgeExternalColor
//		{
//			get{return edgeExternalColor;}
//			set{this.edgeExternalColor = value;}
//		}

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

		public static KeyboardStyle Open(string filename)
		{
			FileStream file = null;
			try
			{
				file = new FileStream(filename, FileMode.Open,FileAccess.Read);
				BinaryFormatter formatter = new BinaryFormatter();
				KeyboardStyle style = (KeyboardStyle)formatter.Deserialize(file);
				file.Close();
				return style;
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
