using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using HotKeysLib.OnScreenKeyboard;

namespace KeyboardDrawing
{
	/// <summary>
	/// Summary description for StyleDesignerForm.
	/// </summary>
	public class StyleDesignerForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.PropertyGrid propertyGrid2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StyleDesignerForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.panel2 = new System.Windows.Forms.Panel();
			this.propertyGrid2 = new System.Windows.Forms.PropertyGrid();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(200, 538);
			this.propertyGrid1.TabIndex = 1;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.propertyGrid2);
			this.panel2.Controls.Add(this.splitter2);
			this.panel2.Controls.Add(this.propertyGrid1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(200, 538);
			this.panel2.TabIndex = 4;
			// 
			// propertyGrid2
			// 
			this.propertyGrid2.CommandsVisibleIfAvailable = true;
			this.propertyGrid2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.propertyGrid2.LargeButtons = false;
			this.propertyGrid2.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid2.Location = new System.Drawing.Point(0, 322);
			this.propertyGrid2.Name = "propertyGrid2";
			this.propertyGrid2.Size = new System.Drawing.Size(200, 216);
			this.propertyGrid2.TabIndex = 3;
			this.propertyGrid2.Text = "propertyGrid2";
			this.propertyGrid2.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid2.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.propertyGrid2.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
			// 
			// splitter2
			// 
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter2.Location = new System.Drawing.Point(0, 0);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(200, 3);
			this.splitter2.TabIndex = 2;
			this.splitter2.TabStop = false;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(200, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 538);
			this.splitter1.TabIndex = 5;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.pictureBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(203, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(485, 538);
			this.panel1.TabIndex = 6;
			// 
			// pictureBox
			// 
			this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(485, 538);
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.Resize += new System.EventHandler(this.pictureBox_Resize);
			this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
			// 
			// StyleDesignerForm
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(688, 538);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panel2);
			this.Name = "StyleDesignerForm";
			this.Text = "StyleDesignerForm";
			this.Load += new System.EventHandler(this.StyleDesignerForm_Load);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void pictureBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			this.render(e.Graphics, new Rectangle(0,0,this.pictureBox.Width,this.pictureBox.Height));
		}

		private void render(Graphics g, Rectangle clientRectangle)
		{
			Bitmap bitmapForScreen = new Bitmap(clientRectangle.Width, clientRectangle.Height);
			Graphics graphicsForScreen = Graphics.FromImage(bitmapForScreen);

			graphicsForScreen.SmoothingMode = SmoothingMode.HighQuality;

			this.initializeCoordinates(graphicsForScreen, clientRectangle);

			// graphicsForScreen.Clear(this.keyBoardStyle.KeyboardBackColor);
			
			this.renderKey(graphicsForScreen, this.keyBoardLayoutKey,this.keyBoardStyle,new PointF(-((100 * this.keyBoardLayoutKey.Width)/2) ,-50));

			g.DrawImageUnscaled(bitmapForScreen,0,0);
			graphicsForScreen.Dispose();
		}

		private void renderKey(Graphics g, KeyboardLayoutKey key, KeyboardStyle style, PointF position)
		{
			g.FillRectangle(new SolidBrush(style.KeyBackColor),position.X , position.Y , 100 * key.Width,100);
			// g.DrawRectangle(new Pen(style.KeyEdgeColor, style.EdgeWidth),position.X , position.Y,100 * key.Width,100);
//			if(key.Text.Length<=1)
//				g.DrawString(key.Text,style.LargeFont, new SolidBrush(style.KeyFontColor) , position.X + style.OffsetLargeFont.X , position.Y + style.OffsetLargeFont.Y);
//			else
//				g.DrawString(key.Text,style.SmallFont, new SolidBrush(style.KeyFontColor) , position.X + style.OffsetSmallFont.X , position.Y + style.OffsetSmallFont.Y);

		}

		private void initializeCoordinates(Graphics g, Rectangle clientRectangle)
		{
			if (clientRectangle.Width == 0 || clientRectangle.Height == 0)
				return;

			g.TranslateTransform(clientRectangle.Width / 2, clientRectangle.Height / 2);
			float finches = Math.Min(clientRectangle.Width / g.DpiX, clientRectangle.Height / g.DpiY);

			g.ScaleTransform(finches * g.DpiX / 200, finches * g.DpiY / 200);
		}

		private KeyboardStyle keyBoardStyle = new KeyboardStyle();
		private KeyboardLayoutKey keyBoardLayoutKey = new KeyboardLayoutKey(Keys.W);
		private void StyleDesignerForm_Load(object sender, System.EventArgs e)
		{
			this.propertyGrid1.SelectedObject = this.keyBoardStyle;
			this.propertyGrid2.SelectedObject = this.keyBoardLayoutKey;
		}

		private void propertyGrid1_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			this.render(Graphics.FromHwnd(this.pictureBox.Handle),this.pictureBox.ClientRectangle);
		}

		private void pictureBox_Resize(object sender, System.EventArgs e)
		{
			this.render(Graphics.FromHwnd(this.pictureBox.Handle),this.pictureBox.ClientRectangle);
		}
	}
}
