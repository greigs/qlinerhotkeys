using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace HotKeysLib.UI.Dialogs
{
	/// <summary>
	/// Summary description for AboutDialog.
	/// </summary>
	public class AboutDialog : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public static void ShowAbout(string imageResourceString)
		{
			ShowAbout(imageResourceString, null);
		}
		//private static AboutDialog aboutDialogInstance = null;
		private static Hashtable aboutDialogs = new Hashtable();
		public static void ShowAbout(string imageResourceString, ArrayList textElements)
		{
			AboutDialog aboutDialogInstance = (AboutDialog)aboutDialogs[imageResourceString]; 
			if(aboutDialogInstance!=null)
			{
				Window.RestoreAndBringToFront(aboutDialogInstance.Handle.ToInt32());
			}
			else
			{
				aboutDialogInstance = new HotKeysLib.UI.Dialogs.AboutDialog(imageResourceString, textElements);
				aboutDialogs.Add(imageResourceString, aboutDialogInstance);
				aboutDialogInstance.Closing +=new CancelEventHandler(aboutDialogInstance_Closing);
				aboutDialogInstance.Show();
			}
		}

		private string imageResourceString = "";
		public string ImageResourceString
		{
			get{return this.imageResourceString;}
		}

		private AboutDialog(string resourceString, ArrayList textElements)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(textElements!=null)
				foreach(AboutDialog.TextElement textElement in textElements)
					this.textElements.Add(textElement);
			this.imageResourceString = resourceString;
			StreamReader streamReader = new StreamReader(Assembly.GetAssembly(typeof(HotKey)).GetManifestResourceStream(this.imageResourceString));
			image = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
            this.SetBitmap(image,255);
			this.TopMost = true;
			this.CenterToScreen();
		}

		protected override CreateParams CreateParams
		{
			get 
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x80000;
				return cp;
			}
		}

		private Bitmap image = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private ArrayList textElements = new ArrayList();
		public ArrayList TextElements
		{
			get{return this.textElements;}
		}

		private void SetBitmap(Bitmap bitmap, byte opacity)
		{
			// Draw Text on bitmap
			Graphics graphics = Graphics.FromImage((Image)bitmap);
			graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
			graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			foreach(TextElement t in this.textElements)
				graphics.DrawString(t.Text,t.Font,t.Brush,t.Rectangle,t.Format);
			IntPtr bitmapHandle = IntPtr.Zero;
			IntPtr oldBitmapHandle = IntPtr.Zero;
			IntPtr screenDC = Win32Interop.GetDC(IntPtr.Zero);
			IntPtr memoryDC = Win32Interop.CreateCompatibleDC(screenDC);
			try 
			{
				bitmapHandle = bitmap.GetHbitmap(Color.FromArgb(0));
				oldBitmapHandle = Win32Interop.SelectObject(memoryDC, bitmapHandle); 
				Win32Interop.BLENDFUNCTION blendFunction = new Win32Interop.BLENDFUNCTION();
				blendFunction.BlendOp = Win32Interop.AC_SRC_OVER;
				blendFunction.BlendFlags = 0;
				blendFunction.SourceConstantAlpha = opacity;
				blendFunction.AlphaFormat = Win32Interop.AC_SRC_ALPHA;
				Win32Interop.Size size = new Win32Interop.Size(bitmap.Width, bitmap.Height);
				Win32Interop.Point pointSource = new Win32Interop.Point(0, 0);
				Win32Interop.Point pointDestination = new Win32Interop.Point(Left, Top);
				Win32Interop.UpdateLayeredWindow(
					Handle, 
					screenDC, 
					ref pointDestination, 
					ref size, 
					memoryDC, 
					ref pointSource, 
					0, 
					ref blendFunction, 
					Win32Interop.ULW_ALPHA);
			}
			catch(Exception exception)
			{
				Console.WriteLine(exception.Message);
			}
			finally 
			{
				Win32Interop.ReleaseDC(IntPtr.Zero, screenDC);
				if (bitmapHandle != IntPtr.Zero) 
				{
					Win32Interop.SelectObject(memoryDC, oldBitmapHandle);
					Win32Interop.DeleteObject(bitmapHandle);
				}
				Win32Interop.DeleteDC(memoryDC);
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// AboutDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AboutDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AboutDialog";
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AboutDialog_MouseUp);

		}
		#endregion

		private void AboutDialog_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.Close();
			//aboutDialogInstance = null;
		}

		private static void aboutDialogInstance_Closing(object sender, CancelEventArgs e)
		{
			aboutDialogs.Remove(((AboutDialog)sender).ImageResourceString);
		}

		public class TextElement
		{
			public TextElement()
			{
			}

			public TextElement(string text, Font font, Brush brush, RectangleF rectangle, StringFormat format)
			{
				this.text = text;
				this.font = font;
				this.brush = brush;
				this.rectangle = rectangle;
				this.format = format;
			}

			private string text = "";
			public string Text
			{
				get{return this.text;}
				set{this.text = value;}
			}

			private Font font = new Font("Arial",8);
			public Font Font
			{
				get{return this.font;}
				set{this.font = value;}
			}

			private Brush brush = Brushes.Black;
			public Brush Brush
			{
				get{return this.brush;}
				set{this.brush = value;}
			}

			private RectangleF rectangle = new RectangleF(0,0,100,100);
			public RectangleF Rectangle
			{
				get{return this.rectangle;}
				set{this.rectangle = value;}
			}

			private StringFormat format = StringFormat.GenericDefault;
			public StringFormat Format
			{
				get{return this.format;}
				set{this.format = value;}
			}

		}
	}
}
