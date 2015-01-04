using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;
using System.Reflection;

namespace HotKeysLib
{
	/// <summary>
	/// Summary description for ClockForm.
	/// </summary>
	public class ClockAddIn : System.Windows.Forms.Form , IHotKeysAddIn
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem showSecondsMenuItem;
		private System.Windows.Forms.MenuItem showDayNumberMenuItem;
		private System.Windows.Forms.MenuItem stickMenuItem;
		private System.Windows.Forms.MenuItem closeMenuItem;
		private System.Windows.Forms.MenuItem revertToDefaultsMenuItem;
		private System.Windows.Forms.MenuItem showLinesMenuItem;

		private ClockSettings clockSettings = null;
		
		public ClockAddIn()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			clockSettings = ClockSettings.GetClockSettings();
			this.Location = clockSettings.Position;
			this.setSize(clockSettings.Size);
			this.setOpacity(clockSettings.Opacity);
			this.showSecondsMenuItem.Checked = this.clockSettings.ShowSeconds;
			this.showDayNumberMenuItem.Checked = this.clockSettings.ShowDayNumber;
			this.showLinesMenuItem.Checked = this.clockSettings.ShowLines;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.components = new System.ComponentModel.Container();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.sizeMenuItem = new System.Windows.Forms.MenuItem();
			this.opacityMenuItem = new System.Windows.Forms.MenuItem();
			this.foreColorMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.showSecondsMenuItem = new System.Windows.Forms.MenuItem();
			this.showDayNumberMenuItem = new System.Windows.Forms.MenuItem();
			this.showLinesMenuItem = new System.Windows.Forms.MenuItem();
			this.revertToDefaultsMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.stickMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.closeMenuItem = new System.Windows.Forms.MenuItem();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// contextMenu
			// 
			this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.sizeMenuItem,
																						this.opacityMenuItem,
																						this.foreColorMenuItem,
																						this.menuItem4,
																						this.showSecondsMenuItem,
																						this.showDayNumberMenuItem,
																						this.showLinesMenuItem,
																						this.revertToDefaultsMenuItem,
																						this.menuItem1,
																						this.stickMenuItem,
																						this.menuItem3,
																						this.closeMenuItem});
			// 
			// sizeMenuItem
			// 
			this.sizeMenuItem.Index = 0;
			this.sizeMenuItem.Text = "Size";
			// 
			// opacityMenuItem
			// 
			this.opacityMenuItem.Index = 1;
			this.opacityMenuItem.Text = "Opacity";
			// 
			// foreColorMenuItem
			// 
			this.foreColorMenuItem.Index = 2;
			this.foreColorMenuItem.Text = "Fore color";
			this.foreColorMenuItem.Click += new System.EventHandler(this.foreColorMenuItem_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "Back color";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// showSecondsMenuItem
			// 
			this.showSecondsMenuItem.Index = 4;
			this.showSecondsMenuItem.Text = "Show seconds";
			this.showSecondsMenuItem.Click += new System.EventHandler(this.showSecondsMenuItem_Click);
			// 
			// showDayNumberMenuItem
			// 
			this.showDayNumberMenuItem.Index = 5;
			this.showDayNumberMenuItem.Text = "Show day number";
			this.showDayNumberMenuItem.Click += new System.EventHandler(this.showDayNumberMenuItem_Click);
			// 
			// showLinesMenuItem
			// 
			this.showLinesMenuItem.Index = 6;
			this.showLinesMenuItem.Text = "Show lines";
			this.showLinesMenuItem.Click += new System.EventHandler(this.showLinesMenuItem_Click);
			// 
			// revertToDefaultsMenuItem
			// 
			this.revertToDefaultsMenuItem.Index = 7;
			this.revertToDefaultsMenuItem.Text = "Revert to defaults";
			this.revertToDefaultsMenuItem.Click += new System.EventHandler(this.revertToDefaultsMenuItem_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 8;
			this.menuItem1.Text = "-";
			// 
			// stickMenuItem
			// 
			this.stickMenuItem.Index = 9;
			this.stickMenuItem.Text = "Stick";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 10;
			this.menuItem3.Text = "-";
			// 
			// closeMenuItem
			// 
			this.closeMenuItem.Index = 11;
			this.closeMenuItem.Text = "Close";
			// 
			// ClockAddIn
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(406, 380);
			this.ContextMenu = this.contextMenu;
			this.Name = "ClockAddIn";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Form1";
			this.TransparencyKey = System.Drawing.Color.Silver;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClockForm_KeyDown);
			this.Resize += new System.EventHandler(this.ClockForm_Resize);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ClockForm_MouseDown);
			this.Click += new System.EventHandler(this.ClockForm_Click);
			this.Load += new System.EventHandler(this.ClockForm_Load);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ClockForm_MouseUp);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ClockForm_KeyUp);
			this.VisibleChanged += new System.EventHandler(this.ClockAddIn_VisibleChanged);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ClockForm_Paint);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ClockForm_MouseMove);

		}
		#endregion

		private void ClockForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{		
			this.Time = DateTime.Now;
		}

		private void DrawBackground(Graphics grfx)
		{
			grfx.FillEllipse(clockSettings.ForeColorBrush,-1000,-1000,2000,2000);
			grfx.FillEllipse(clockSettings.BackColorBrush,-950,-950,1900,1900);
		}

		private void InitializeCoordinates(Graphics grfx)
		{
			if (Width == 0 || Height == 0)
				return;

			grfx.TranslateTransform(this.ClientRectangle.Width / 2, this.ClientRectangle.Height / 2);
			float finches = Math.Min(this.ClientRectangle.Width / grfx.DpiX, this.ClientRectangle.Height / grfx.DpiY);

			grfx.ScaleTransform(finches * grfx.DpiX / 2000, finches * grfx.DpiY / 2000);
		}

		private void DrawLines(Graphics grfx, Brush brush)
		{
			if(!clockSettings.ShowLines)
				return;
			for(int i = 0; i < 60; i+=5)
			{
				int iSize = 50;
				if(!(i==30 && clockSettings.ShowDayNumber))
					grfx.DrawLine(new Pen(brush,iSize), 0, -925, 0,-750);
				grfx.RotateTransform(30);
			}
		}

		private void ClockForm_Load(object sender, System.EventArgs e)
		{
			// Size menu items
			for(int i = 10; i <= 100; i+=10)
			{
				MenuItem menuItem = new MenuItem(i.ToString() + "% of screen");
				menuItem.Click += new EventHandler(this.SizeMenuItemEventHandler);
				menuItem.RadioCheck = true;
				if(this.clockSettings.Size==i)
					menuItem.Checked = true;
				this.sizeMenuItem.MenuItems.Add(menuItem);
			}
			// Opacity menu items
			for(int i = 10; i <= 100; i+=10)
			{
				MenuItem menuItem = new MenuItem(i.ToString() + "%");
				menuItem.Click += new EventHandler(this.OpacityMenuItemEventHandler);
				menuItem.RadioCheck = true;
				if(this.clockSettings.Opacity==i)
					menuItem.Checked = true;
				this.opacityMenuItem.MenuItems.Add(menuItem);
			}
			this.FormBorderStyle = FormBorderStyle.None;
			this.Width = this.Height;
			Color newColor = Color.FromArgb(0,125,125,125);
			Brush newBrush = new SolidBrush(newColor);
			// Set form backcolor
			this.BackColor = Color.DarkGray;
			//Set transparancy color
			this.TransparencyKey = Color.DarkGray;
			
			this.CreateBackGroundGraphics();
		}

		public void SizeMenuItemEventHandler(object sender, EventArgs e)
		{
			clockSettings.Size = (((MenuItem)sender).Text.StartsWith("100")) ? 100 : Convert.ToInt32(((MenuItem)sender).Text.Substring(0,2));
			this.ClearChecksOnAllChildren(((MenuItem)sender).Parent);
			((MenuItem)sender).Checked = true;
			clockSettings.PersistClockSettings();
			this.setSize(clockSettings.Size);
		}

		private void ClearChecksOnAllChildren(Menu menu)
		{
			foreach(MenuItem menuItem in menu.MenuItems)
			{
				menuItem.Checked = false;
			}
		}

		private void setSize(int percentage)
		{
			Rectangle screenRect = Screen.FromHandle(this.Handle).Bounds;
			double screenSize = screenRect.Width > screenRect.Height ? screenRect.Width : screenRect.Height; 
			int size = (int)((screenSize / 100) * percentage);
			this.Size = new Size(size, size);
		}

		public void OpacityMenuItemEventHandler(object sender, EventArgs e)
		{
			clockSettings.Opacity = (((MenuItem)sender).Text.StartsWith("100")) ? 100 : Convert.ToInt32(((MenuItem)sender).Text.Substring(0,2));
			this.ClearChecksOnAllChildren(((MenuItem)sender).Parent);
			((MenuItem)sender).Checked = true;
			clockSettings.PersistClockSettings();
			this.setOpacity(clockSettings.Opacity);
		}

		private void setOpacity(int percentage)
		{
			this.Opacity = ((double)percentage) / 100;
		}

		private void CreateBackGroundGraphics()
		{
			Color newColor = Color.FromArgb(0,125,125,125);
			Brush newBrush = new SolidBrush(newColor);

			if(cachedBitmap!=null)
				cachedBitmap.Dispose();
			cachedBitmap = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
			Graphics graphicsForCache = Graphics.FromImage(cachedBitmap);
			graphicsForCache.FillRectangle(newBrush,0,0,this.ClientRectangle.Width,this.ClientRectangle.Height);
			graphicsForCache.SmoothingMode = SmoothingMode.HighQuality;
			InitializeCoordinates(graphicsForCache);
			DrawBackground(graphicsForCache);
			DrawLines(graphicsForCache,clockSettings.ForeColorBrush);
			graphicsForCache.Dispose();
		}

		private void DrawSecondHand(Graphics grfx, Brush brush)
		{
			GraphicsState gs = grfx.Save();
			grfx.RotateTransform(360f * Time.Second / 60);
			grfx.DrawLine(new Pen(brush, 10),0,100,0,-900);
			grfx.Restore(gs);
		}

		private void DrawMinuteHand(Graphics grfx, Brush brush)
		{
			GraphicsState gs = grfx.Save();
			grfx.RotateTransform(360f * Time.Minute / 60 + 
				6f * Time.Second /60);
			grfx.DrawLine(new Pen(brush, 40),0,100,0,-900);
			grfx.Restore(gs);
		}

		private void DrawHourHand(Graphics grfx, Brush brush)
		{
			GraphicsState gs = grfx.Save();
			grfx.RotateTransform(360f * Time.Hour / 12 +
				30f * Time.Minute / 60);
			grfx.DrawLine(new Pen(brush, 50),0,100,0,-725);
			grfx.Restore(gs);
		}

		private void DrawDate(Graphics grfx, Brush brush)
		{
			grfx.DrawRectangle(new Pen(brush,10),-120,695,240,220);
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			grfx.DrawString(Time.Day.ToString(),
				new Font("Arial Narrow",170),
				brush,
				new RectangleF(-145,675,300,280),
				sf
				);
		}

		private System.Windows.Forms.Timer timer;

		private DateTime previousDateTime;
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem sizeMenuItem;
		private System.Windows.Forms.MenuItem opacityMenuItem;
		private System.Windows.Forms.MenuItem foreColorMenuItem;
		private System.Windows.Forms.ColorDialog colorDialog;
		private Bitmap cachedBitmap;

		private void timer_Tick(object sender, System.EventArgs e)
		{
			this.Time = DateTime.Now;
		}

		private void ClockForm_Click(object sender, System.EventArgs e)
		{
			//this.Close();
		}

		private void ClockForm_Resize(object sender, System.EventArgs e)
		{
			this.CreateBackGroundGraphics();
		}

		private void ClockForm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
			{
				Point mousePositionScreen = this.PointToScreen(new Point(e.X,e.Y));
				this.Left = mousePositionScreen.X - mousePositionDelta.X;
				this.Top = mousePositionScreen.Y - mousePositionDelta.Y;
			}
		}

		private Point mousePositionDelta = new Point(0,0);
		private void ClockForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
				mousePositionDelta = new Point(e.X,e.Y);	
		}

		private void ClockForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.timer.Enabled = false;
			this.Visible = false;
		}

		private void ClockForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{

		}

		private void foreColorMenuItem_Click(object sender, System.EventArgs e)
		{
			this.colorDialog.Color = this.clockSettings.ForeColor;
			if(this.colorDialog.ShowDialog(this)==DialogResult.OK)
			{
				this.clockSettings.ForeColor = colorDialog.Color;
				this.CreateBackGroundGraphics();
				clockSettings.PersistClockSettings();
			}
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			this.colorDialog.Color = this.clockSettings.BackColor;
			if(this.colorDialog.ShowDialog(this)==DialogResult.OK)
			{
				this.clockSettings.BackColor = colorDialog.Color;
				this.CreateBackGroundGraphics();
				clockSettings.PersistClockSettings();
			}
		}

		private void ClockForm_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
			{
				if(clockSettings.Position!=this.Location)
				{
					clockSettings.Position=this.Location;
					clockSettings.PersistClockSettings();
				}
			}
		}

		private void showSecondsMenuItem_Click(object sender, System.EventArgs e)
		{
			this.showSecondsMenuItem.Checked = !this.showSecondsMenuItem.Checked;
			this.clockSettings.ShowSeconds = this.showSecondsMenuItem.Checked;
			clockSettings.PersistClockSettings();
		}

		private void showDayNumberMenuItem_Click(object sender, System.EventArgs e)
		{
			this.showDayNumberMenuItem.Checked = !this.showDayNumberMenuItem.Checked;
			this.clockSettings.ShowDayNumber = this.showDayNumberMenuItem.Checked;
			this.CreateBackGroundGraphics();
			clockSettings.PersistClockSettings();
		}

		private void revertToDefaultsMenuItem_Click(object sender, System.EventArgs e)
		{
			clockSettings = new ClockSettings();
			clockSettings.PersistClockSettings();
			this.CreateBackGroundGraphics();
			this.Location = clockSettings.Position;
			this.setSize(clockSettings.Size);
			this.setOpacity(clockSettings.Opacity);
		}

		private void showLinesMenuItem_Click(object sender, System.EventArgs e)
		{
			this.showLinesMenuItem.Checked = !this.showLinesMenuItem.Checked;
			this.clockSettings.ShowLines = this.showLinesMenuItem.Checked;
			this.CreateBackGroundGraphics();
			clockSettings.PersistClockSettings();
		}

		public DateTime Time
		{
			get
			{
				return previousDateTime;
			}
			set
			{
				previousDateTime = value;

				if(!this.Visible)
				{
					this.timer.Enabled = false;
					return;
				}

				Bitmap bitmapForScreen = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
				Graphics graphicsForScreen = Graphics.FromImage(bitmapForScreen);

				graphicsForScreen.SmoothingMode = SmoothingMode.HighQuality;

				graphicsForScreen.DrawImageUnscaled(cachedBitmap,0,0);
				InitializeCoordinates(graphicsForScreen);
				if(clockSettings.ShowSeconds)
					DrawSecondHand(graphicsForScreen,clockSettings.ForeColorBrush);
				DrawMinuteHand(graphicsForScreen,clockSettings.ForeColorBrush);
				DrawHourHand(graphicsForScreen,clockSettings.ForeColorBrush);
				if(clockSettings.ShowDayNumber)
					DrawDate(graphicsForScreen,clockSettings.ForeColorBrush);
				Graphics graphicsForm = CreateGraphics();
				graphicsForm.DrawImageUnscaled(bitmapForScreen,0,0);
				
				graphicsForm.Dispose();
				graphicsForScreen.Dispose();
				bitmapForScreen.Dispose();

				if(this.Left == Screen.FromControl(this).Bounds.Width)
				{
					this.Left = (Screen.FromControl(this).Bounds.Width - this.Width) / 2;
				}
				GC.Collect();
			}
		}

		#region IHotKeysAddIn Members

		public string Config
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string AddInName
		{
			get
			{
				return "Clock";
			}
		}

		public string Description
		{
			get
			{
				return "Shows time";
			}
		}

		public bool HasConfig
		{
			get
			{
				return false;
			}
		}

		public bool RequiresConfig
		{
			get
			{
				return false;
			}
		}

		public ArrayList Actions
		{
			get
			{
				HotKeyAddInAction result = new HotKeyAddInAction();
				result.Name = "Show clock";
				result.Description = "Shows the clock";
				result.HasConfig = false;
				result.RequiresConfig = false;
				ArrayList al = new ArrayList();
				al.Add(result);
				return al;
			}
		}

		public System.EventHandler ConfigChanged
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public System.Windows.Forms.DialogResult ShowAddInConfigDialog()
		{
			return new System.Windows.Forms.DialogResult ();
		}

		System.Windows.Forms.DialogResult IHotKeysAddIn.ShowAddInConfigDialog(IntPtr OwnerHandle)
		{
			return new System.Windows.Forms.DialogResult ();
		}

		public System.Windows.Forms.DialogResult ShowAddInActionConfigDialog(HotKeyAddInAction AddInAction, ref string ActionConfig)
		{
			return new System.Windows.Forms.DialogResult ();
		}

		System.Windows.Forms.DialogResult IHotKeysAddIn.ShowAddInActionConfigDialog(IntPtr OwnerHandle, HotKeyAddInAction AddInAction, ref string ActionConfig)
		{
			return new System.Windows.Forms.DialogResult ();
		}

		public void InvokeAction(HotKeyConfiguredAddInAction Action)
		{
			// Only has one:
			this.invokeAction();
		}

		private Icon icon = null;
		public Icon AddInIcon
		{
			get
			{
				if(icon!=null)
					return icon;
				icon = this.GetIcon(HotkeyIconSize.sizeShell32x32Or48x38);
				return icon;
			}
		}

		public Icon GetIcon(HotkeyIconSize size)
		{
			int intSize = 0;
			if(size==HotkeyIconSize.size16x16)intSize=16;
			if(size==HotkeyIconSize.size32x32)intSize=32;
			if(size==HotkeyIconSize.sizeShell32x32Or48x38)intSize=HotKeyHelperFunctions.GetShellIconSize();
			StreamReader streamReader = new StreamReader(Assembly.GetAssembly(typeof(VolumeAddIn)).GetManifestResourceStream("HotKeysLib.ClockAddIn.Clock.ico"));
			return new Icon(streamReader.BaseStream,intSize,intSize);
		}

		public readonly static Guid ADDINID = new Guid( "{57555FFB-523A-42db-AA50-72D6F47BFFA7}" );
		
		public Guid AddInID
		{
			get
			{
				return ADDINID;
			}
		}

		#endregion

		private void invokeAction()
		{
			this.Show();
			Window.RestoreAndBringToFront(this.Handle.ToInt32());
		}

		public override string ToString()
		{
			return this.AddInName;
		}

		private void ClockAddIn_VisibleChanged(object sender, System.EventArgs e)
		{
			this.timer.Enabled=this.Visible;
		}

	}

	[Serializable]
	public class ClockSettings
	{
		public ClockSettings()
		{
			int screenWidth = Screen.PrimaryScreen.Bounds.Width;	
			int screenHeight = Screen.PrimaryScreen.Bounds.Height;
			double clockSize = screenWidth > screenHeight ? screenWidth : screenHeight;
			clockSize = (clockSize / 100) * Size;
			Position = new Point(((screenWidth - (int)clockSize)/2) , ((screenHeight - (int)clockSize)/2));
		}

		public Color ForeColor = Color.Black;
		public Color BackColor = Color.White;
		public Point Position = new Point(0,0);
		public bool Stick = false;
		public bool ShowSeconds = true;
		public bool ShowDayNumber = false;
		public bool ShowLines = true;

		public Brush ForeColorBrush
		{
			get
			{
				return new SolidBrush(this.ForeColor);
			}
		}

		public Brush BackColorBrush
		{
			get
			{
				return new SolidBrush(this.BackColor);
			}
		}

		public int Size = 40;
		public int Opacity = 80;

		public static ClockSettings GetClockSettings()
		{
			FileStream stream = null;
			ClockSettings settings = null;
			try
			{
				stream = new FileStream(HotKeyHelperFunctions.ApplicationDataPath + "\\clocksettings.xml",FileMode.Open);
				settings = (ClockSettings)(new SoapFormatter().Deserialize(stream));
			}
			catch{}
			finally{if(stream!=null)stream.Close();}
			if(settings==null)
				settings = new ClockSettings();
			return settings;
		}

		public void PersistClockSettings()
		{
			FileStream stream = new FileStream(HotKeyHelperFunctions.ApplicationDataPath + "\\clocksettings.xml",FileMode.Create);
			new SoapFormatter().Serialize(stream,this);
			stream.Close();
		}
	}
}
