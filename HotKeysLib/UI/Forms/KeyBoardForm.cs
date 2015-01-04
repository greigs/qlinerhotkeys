using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using HotKeysLib.OnScreenKeyboard;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace HotKeysLib.UI.Forms
{
	/// <summary>
	/// Summary description for KeyBoardForm.
	/// </summary>
	public class KeyBoardForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;

		private KeyBoardForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.Opacity = 0;
		}

		private static KeyBoardForm keyBoardForm = null;
		public static KeyBoardForm GetKeyBoardForm()
		{
			if(keyBoardForm==null)
			{
				GC.Collect();
				keyBoardForm = new KeyBoardForm();
			}
			return keyBoardForm;
		} 

		public static void DropKeyBoardForm()
		{
			keyBoardForm.Close();
			keyBoardForm.Dispose();
			keyBoardForm = null;
			GC.Collect();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			try
			{
				if(this.render!=null)
				{
					this.render.CleanUp();
					this.render = null;
				}
			}
			catch{}
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
            this.components = new System.ComponentModel.Container();
            this.keyContextMenu = new System.Windows.Forms.ContextMenu();
            this.newHotKeyMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.cutMenuItem = new System.Windows.Forms.MenuItem();
            this.copyMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteMenuItem = new System.Windows.Forms.MenuItem();
            this.deleteMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.propertiesMenuItem = new System.Windows.Forms.MenuItem();
            this.keyboardContextMenu = new System.Windows.Forms.ContextMenu();
            this.colorsMenuItem = new System.Windows.Forms.MenuItem();
            this.layoutMenuItem = new System.Windows.Forms.MenuItem();
            this.languageMenuItem = new System.Windows.Forms.MenuItem();
            this.displayAfterMenuItem = new System.Windows.Forms.MenuItem();
            this.fadeMenuItem = new System.Windows.Forms.MenuItem();
            this.sizeMenuItem = new System.Windows.Forms.MenuItem();
            this.positionMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.seperatorMenuItem = new System.Windows.Forms.MenuItem();
            this.quitHotKeysMenuItem = new System.Windows.Forms.MenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // keyContextMenu
            // 
            this.keyContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.newHotKeyMenuItem,
            this.menuItem10,
            this.cutMenuItem,
            this.copyMenuItem,
            this.pasteMenuItem,
            this.deleteMenuItem,
            this.menuItem7,
            this.propertiesMenuItem});
            // 
            // newHotKeyMenuItem
            // 
            this.newHotKeyMenuItem.Index = 0;
            this.newHotKeyMenuItem.Text = "New Hotkey";
            this.newHotKeyMenuItem.Click += new System.EventHandler(this.newHotKeyMenuItem_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 1;
            this.menuItem10.Text = "-";
            // 
            // cutMenuItem
            // 
            this.cutMenuItem.Index = 2;
            this.cutMenuItem.Text = "Cut";
            this.cutMenuItem.Click += new System.EventHandler(this.cutMenuItem_Click);
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Index = 3;
            this.copyMenuItem.Text = "Copy";
            this.copyMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
            // 
            // pasteMenuItem
            // 
            this.pasteMenuItem.Index = 4;
            this.pasteMenuItem.Text = "Paste";
            this.pasteMenuItem.Click += new System.EventHandler(this.pasteMenuItem_Click);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Index = 5;
            this.deleteMenuItem.Text = "Delete";
            this.deleteMenuItem.Click += new System.EventHandler(this.deleteMenuItem_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 6;
            this.menuItem7.Text = "-";
            // 
            // propertiesMenuItem
            // 
            this.propertiesMenuItem.Index = 7;
            this.propertiesMenuItem.Text = "Properties";
            this.propertiesMenuItem.Click += new System.EventHandler(this.propertiesMenuItem_Click);
            // 
            // keyboardContextMenu
            // 
            this.keyboardContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.colorsMenuItem,
            this.layoutMenuItem,
            this.languageMenuItem,
            this.displayAfterMenuItem,
            this.fadeMenuItem,
            this.sizeMenuItem,
            this.positionMenuItem,
            this.menuItem2,
            this.aboutMenuItem,
            this.seperatorMenuItem,
            this.quitHotKeysMenuItem});
            // 
            // colorsMenuItem
            // 
            this.colorsMenuItem.Index = 0;
            this.colorsMenuItem.Text = "Colors";
            // 
            // layoutMenuItem
            // 
            this.layoutMenuItem.Index = 1;
            this.layoutMenuItem.Text = "Layout";
            // 
            // languageMenuItem
            // 
            this.languageMenuItem.Index = 2;
            this.languageMenuItem.Text = "Language";
            // 
            // displayAfterMenuItem
            // 
            this.displayAfterMenuItem.Index = 3;
            this.displayAfterMenuItem.Text = "Show after";
            // 
            // fadeMenuItem
            // 
            this.fadeMenuItem.Index = 4;
            this.fadeMenuItem.Text = "Fade in / out";
            this.fadeMenuItem.Click += new System.EventHandler(this.fadeMenuItem_Click);
            // 
            // sizeMenuItem
            // 
            this.sizeMenuItem.Index = 5;
            this.sizeMenuItem.Text = "Size";
            this.sizeMenuItem.Visible = false;
            // 
            // positionMenuItem
            // 
            this.positionMenuItem.Index = 6;
            this.positionMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3,
            this.menuItem4});
            this.positionMenuItem.Text = "Position";
            this.positionMenuItem.Visible = false;
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.Text = "Custom";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "Center";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 7;
            this.menuItem2.Text = "-";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 8;
            this.aboutMenuItem.Text = "About...";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // seperatorMenuItem
            // 
            this.seperatorMenuItem.Index = 9;
            this.seperatorMenuItem.Text = "-";
            // 
            // quitHotKeysMenuItem
            // 
            this.quitHotKeysMenuItem.Index = 10;
            this.quitHotKeysMenuItem.Text = "Quit HotKeys";
            this.quitHotKeysMenuItem.Click += new System.EventHandler(this.quitHotKeysMenuItem_Click);
            // 
            // KeyBoardForm
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 20);
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1032, 456);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.;
		    this.ShowInTaskbar = true;
		    this.ControlBox = true;
            this.Name = "KeyBoardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TransparencyKey = System.Drawing.Color.Silver;
            this.Load += new System.EventHandler(this.KeyBoardForm_Load);
            this.Click += new System.EventHandler(this.KeyBoardForm_Click);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.KeyBoardForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.KeyBoardForm_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.KeyBoardForm_DragOver);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.KeyBoardForm_Paint);
            this.DoubleClick += new System.EventHandler(this.KeyBoardForm_DoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyBoardForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyBoardForm_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KeyBoardForm_MouseDown);
            this.MouseEnter += new System.EventHandler(this.KeyBoardForm_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.KeyBoardForm_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.KeyBoardForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KeyBoardForm_MouseUp);
            this.ResumeLayout(false);

		}
		#endregion

		private KeyboardStyle style = null;
		private KeyboardLayout layout = null;
		private Hashtable language = null;
		private ShiftState shiftState = ShiftState.None;
		private System.Windows.Forms.ContextMenu keyContextMenu;
		private System.Windows.Forms.ContextMenu keyboardContextMenu;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem sizeMenuItem;
		private System.Windows.Forms.MenuItem positionMenuItem;
		private System.Windows.Forms.MenuItem colorsMenuItem;
		private System.Windows.Forms.MenuItem layoutMenuItem;
		private KeyboardRenderer render = new KeyboardRenderer();

		private static string LAYOUTPATH = "c:\\";
		private static string LAYOUTSUFFIX = ".layout.bin";
//		private static string LANGUAGEPATH = "c:\\";
//		private static string LANGUAGESUFFIX = ".language.bin";
		private static string STYLEPATH = "c:\\";
		private static string STYLESUFFIX = ".style.bin";

		private System.Windows.Forms.MenuItem cutMenuItem;
		private System.Windows.Forms.MenuItem copyMenuItem;
		private System.Windows.Forms.MenuItem pasteMenuItem;
		private System.Windows.Forms.MenuItem deleteMenuItem;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem propertiesMenuItem;
		private System.Windows.Forms.MenuItem newHotKeyMenuItem;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem seperatorMenuItem;
		private System.Windows.Forms.MenuItem quitHotKeysMenuItem;
		private System.Windows.Forms.MenuItem displayAfterMenuItem;
		private System.Windows.Forms.MenuItem fadeMenuItem;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.MenuItem aboutMenuItem;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem languageMenuItem;

		private void KeyBoardForm_Load(object sender, System.EventArgs e)
		{
			Microsoft.Win32.SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);
			LAYOUTPATH = Path.GetDirectoryName( Application.ExecutablePath ) + "\\Keyboard Data\\";
			STYLEPATH = Path.GetDirectoryName( Application.ExecutablePath ) + "\\Keyboard Data\\";
//			LANGUAGEPATH = Path.GetDirectoryName( Application.ExecutablePath ) + "\\Keyboard Data\\";
			this.CreateLayoutMenuItems();
			this.CreateStyleMenuItems();
			this.CreateLanguageMenuItems();
			this.CreateSecondsMenuItems();
			this.fadeMenuItem.Checked = KeyboardSettings.Current.Fade;
			try
			{
				layout = KeyboardLayout.Open(LAYOUTPATH + KeyboardSettings.Current.Layout + LAYOUTSUFFIX);
				style = KeyboardStyle.Open(STYLEPATH + KeyboardSettings.Current.Style + STYLESUFFIX);
				this.openLanguageFile(/*LANGUAGEPATH +*/ KeyboardSettings.Current.Language /*+ LANGUAGESUFFIX*/);
				this.initForm(layout,style);
			}

			catch(Exception exception)
			{
				MessageBox.Show(exception.Message);
				MessageBox.Show(STYLEPATH + KeyboardSettings.Current.Style + STYLESUFFIX);
				MessageBox.Show(exception.InnerException.Message);
				MessageBox.Show(exception.StackTrace);
				MessageBox.Show(Application.ExecutablePath);
			}
		}

		private void openLanguageFile(string language)
		{
			// Load the file
//			FileStream stream = new FileStream(filename,FileMode.Open,FileAccess.Read);
//			BinaryFormatter formatter = new BinaryFormatter();
			this.language = HotKeyHelperFunctions.GetLanguageData(language); //(Hashtable)formatter.Deserialize(stream);
//			stream.Close();
			this.bindKeysToScanCodes();
		}

		private void bindKeysToScanCodes()
		{
			// Bind the the virtual keys to the keyboard (using scancodes)
			Hashtable scanCodeKeyPairs = (Hashtable)this.language["scanCodeKeyPairs"];
			foreach(KeyboardLayoutRow row in this.layout.Rows)
				foreach(KeyboardLayoutKey key in row.Keys)
					if(scanCodeKeyPairs[key.ScanCode]!=null)
						key.Key = (Keys)scanCodeKeyPairs[key.ScanCode];
		}

		private void initForm(KeyboardLayout newLayout, KeyboardStyle newStyle)
		{
			layout = newLayout;
			style = newStyle;
			
			// Add Icons to layout
			foreach(HotKey hotKey in HotKey.GetAllHotKeys())
			{
				foreach(KeyboardLayoutRow row in layout.Rows)
				{
					foreach(KeyboardLayoutKey key in row.Keys)
					{
						if(key.Key == hotKey.Key)
							key.Icon = hotKey.Icon;
					}
				}
			}
			
			Screen currentScreen = Screen.PrimaryScreen;
			SizeF size = new SizeF((float)(currentScreen.Bounds.Width * 0.95), (float)(currentScreen.Bounds.Height * 1));
			Rectangle availableScreenRect = new Rectangle(new Point(0,0),new Size((int)size.Width, (int)size.Height));
			// Create initial back buffer by doing a render on a null, this also gets us the size of the image
			render.Invalidate();
			render.RenderAll(null, IntPtr.Zero, layout, style, language, this.shiftState, availableScreenRect);
			this.Size = render.ActualSize;
			this.Left = (int)(currentScreen.Bounds.Width - this.Width )/2;
			this.Top = (int)(currentScreen.Bounds.Height - this.Height)/2;
		}

		public void CreateLayoutMenuItems()
		{
			string[] files = Directory.GetFiles(LAYOUTPATH, "*" + LAYOUTSUFFIX);
			foreach(string file in files)
			{
				string menuText = file.Trim().Replace(LAYOUTPATH,"").Replace(LAYOUTSUFFIX,"");
				MenuItem newItem = new MenuItem(menuText,new EventHandler(changeLayout_MenuClick));
				newItem.RadioCheck = true;
				if(menuText==KeyboardSettings.Current.Layout)
					newItem.Checked = true;
				layoutMenuItem.MenuItems.Add(newItem);
			}
		}

		public void CreateSecondsMenuItems()
		{
			for(int i = 0; i <= 10; i++)
			{
				string menuText = i.ToString() + (i == 1 ? " second" : " seconds");
				MenuItem newItem = new MenuItem(menuText,new EventHandler(changeDelay_MenuClick));
				newItem.RadioCheck = true;
				if(i == KeyboardSettings.Current.SecondsBeforeDisplay)
					newItem.Checked = true;
				this.displayAfterMenuItem.MenuItems.Add(newItem);
			}
		}

		public void changeDelay_MenuClick(object sender, EventArgs e)
		{
			KeyboardSettings.Current.SecondsBeforeDisplay = int.Parse( ((MenuItem)sender).Text.Replace(" second", "" ).Replace("s",""));
			foreach(MenuItem menuItem in this.displayAfterMenuItem.MenuItems)
			{
				menuItem.Checked=(menuItem==((MenuItem)sender));
			}
			KeyboardSettings.Persist();
		}

		public void CreateStyleMenuItems()
		{
			string[] files = Directory.GetFiles(STYLEPATH, "*" + STYLESUFFIX);
			foreach(string file in files)
			{
				string menuText = file.Trim().Replace(STYLEPATH,"").Replace(STYLESUFFIX,"");
				MenuItem newItem = new MenuItem(menuText,new EventHandler(changeStyle_MenuClick));
				newItem.RadioCheck = true;
				if(menuText==KeyboardSettings.Current.Style)
					newItem.Checked = true;
				colorsMenuItem.MenuItems.Add(newItem);
			}
		}

		public void CreateLanguageMenuItems()
		{
			string[] files = HotKeyHelperFunctions.GetAvailableLanguages();//Directory.GetFiles(LANGUAGEPATH, "*" + LANGUAGESUFFIX);
			foreach(string file in files)
			{
				string menuText = file; //file.Trim().Replace(LANGUAGEPATH,"").Replace(LANGUAGESUFFIX,"");
				MenuItem newItem = new MenuItem(menuText,new EventHandler(changeLanguage_MenuClick));
				newItem.RadioCheck = true;
				if(menuText==KeyboardSettings.Current.Language)
					newItem.Checked = true;
				languageMenuItem.MenuItems.Add(newItem);
			}
		}

		private void changeStyle_MenuClick(object sender, EventArgs e)
		{
			MenuItem clickedMenuItem = (MenuItem)sender;
			foreach(MenuItem menuItem in this.colorsMenuItem.MenuItems)
			{
				menuItem.Checked=(menuItem==clickedMenuItem);
			}
			style = KeyboardStyle.Open(STYLEPATH + clickedMenuItem.Text + STYLESUFFIX);
			this.initForm(layout,style);
			this.Refresh();
			KeyboardSettings.Current.Style = clickedMenuItem.Text;
			KeyboardSettings.Persist();
		}

		private void changeLanguage_MenuClick(object sender, EventArgs e)
		{
			MenuItem clickedMenuItem = (MenuItem)sender;
			foreach(MenuItem menuItem in this.languageMenuItem.MenuItems)
			{
				menuItem.Checked=(menuItem==clickedMenuItem);
			}
			this.openLanguageFile(/*LANGUAGEPATH +*/ clickedMenuItem.Text /*+ LANGUAGESUFFIX*/);
			this.initForm(layout,style);
			this.UpdateScreen();
			KeyboardSettings.Current.Language = clickedMenuItem.Text;
			KeyboardSettings.Persist();
		}

		private void changeLayout_MenuClick(object sender, EventArgs e)
		{
			MenuItem clickedMenuItem = (MenuItem)sender;
			foreach(MenuItem menuItem in this.layoutMenuItem.MenuItems)
			{
				menuItem.Checked=(menuItem==clickedMenuItem);
			}
			layout = KeyboardLayout.Open(LAYOUTPATH + clickedMenuItem.Text + LAYOUTSUFFIX);
			this.bindKeysToScanCodes();
			this.initForm(layout,style);
			this.Refresh();
			KeyboardSettings.Current.Layout = clickedMenuItem.Text;
			KeyboardSettings.Persist();
		}

		private void KeyBoardForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			render.RenderAll(e.Graphics, this.Handle, layout, style, language, this.shiftState, this.ClientRectangle);
		}
		
		private void KeyBoardForm_MouseEnter(object sender, System.EventArgs e)
		{
			// this.Capture = true;
		}

		private void KeyBoardForm_MouseLeave(object sender, System.EventArgs e)
		{
			// this.Capture = false;
		}

		KeyboardLayoutKey currentKey = null;
		KeyboardLayoutKey previousIconKey = null;
		Point previousMousePos;
		private void KeyBoardForm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			currentKey = render.KeyFromPoint(new PointF(e.X,e.Y));
			if(currentKey!=null)
			{
				if(!currentKey.IsPlaceHolder)
				{
					Color highlightColor = Color.Blue;
					if(selectedIconKey!=null || draggedIcon!=null)
					{
						if(isKeyValidDropTarget(currentKey))
							highlightColor = Color.LightGreen;
						else
							highlightColor = Color.Red;
					}
					if(previousIconKey!=null)
					{
						if(previousIconKey!=currentKey)
						{
							render.RestoreKey(Graphics.FromHwnd(this.Handle) ,previousIconKey, (selectedIconKey!=previousIconKey));
							render.HighlightKey(currentKey, highlightColor, (selectedIconKey!=currentKey));
							render.RestoreKey(Graphics.FromHwnd(this.Handle),currentKey,false);
							render.UnHighlightKey(previousIconKey, (selectedIconKey!=previousIconKey));
							render.RestoreKey(Graphics.FromHwnd(this.Handle),previousIconKey,false);
							previousIconKey = currentKey;
						}
					}
					else
					{
						render.HighlightKey(currentKey, highlightColor, (selectedIconKey!=currentKey));
						render.RestoreKey(Graphics.FromHwnd(this.Handle),currentKey,false);
						previousIconKey = currentKey;
					}
				}
				else
				{
					if(previousIconKey!=null)
					{
						render.UnHighlightKey(previousIconKey, (selectedIconKey!=previousIconKey));
						render.RestoreKey(Graphics.FromHwnd(this.Handle),previousIconKey,false);
						previousIconKey = currentKey;
					}
				}
				try
				{
					// Set tooltip
					HotKey hotkey = HotKey.GetHotKeyByKey(currentKey.Key);
					RectangleF iconRect = render.IconRectFromPoint(new PointF((float)e.X, (float)e.Y));
					if(hotkey!=null && iconRect.Width!=0)
						this.showToolTip(hotkey.Name, Rectangle.Truncate( iconRect ));
					else
						this.hideTooltip();
				}
				catch(Exception exception){Console.WriteLine(exception.Message);}
			}
			else
			{
				// Un set tooltip
				this.hideTooltip();
				if(previousIconKey!=null)
				{
					render.UnHighlightKey(previousIconKey, (selectedIconKey!=previousIconKey));
					render.RestoreKey(Graphics.FromHwnd(this.Handle),previousIconKey,false);
					previousIconKey = currentKey;
				}
			}
			
			if(e.Button==MouseButtons.Left && /*selectedIconKey*/ this.draggedIcon!=null)
			{
				// Restore graphics underneath previous icon position
				// TODO: Icon issue
				render.RestoreRect(Graphics.FromHwnd(this.Handle),new Rectangle(previousMousePos, new Size(32,32)));
				// Draw the icon at the new position
				Graphics.FromHwnd(this.Handle).DrawIcon(draggedIcon, e.X - pointerIconOffset.X, e.Y - pointerIconOffset.Y);
				previousMousePos = new Point(e.X - pointerIconOffset.X,e.Y - pointerIconOffset.Y);
			}
		}

		private Rectangle tooltipRectangle;
		private string tooltipText = "";
		private void showToolTip(string text, Rectangle targetItemRectangle)
		{
			if(text == "" || this.tooltipText == text)
				return;
			if(this.tooltipText!="")
			{
				this.hideTooltip();
			}
			this.tooltipText = text;
			this.tooltipRectangle = this.drawTooltipImage(text,targetItemRectangle);
		}

		private void hideTooltip()
		{
			this.render.RestoreRect(Graphics.FromHwnd(this.Handle), tooltipRectangle);
			this.tooltipText = "";
			this.tooltipRectangle = new Rectangle(0,0,0,0);
		}

		public Rectangle drawTooltipImage(string tooltipText, Rectangle targetItemRectangle)
		{
			string text = tooltipText;
//			if(text.Length>155)
//				text = text.Substring(0,155) + "...";
			int textMargin = 2;
			SizeF stringSize = Graphics.FromHwnd(this.Handle).MeasureString(text, this.Font,960);
			// in the current case the shadow is goes round the corner
			int shadowWidth = 15;
			// so we need an inset
			int inSet = 5;
			Size tooltipSize = new Size((int)stringSize.Width + (2 * (textMargin - inSet + 2)),(int)stringSize.Height + (2 * (textMargin - inSet + 1)));
			Size resultSize = new Size(tooltipSize.Width + (2 * shadowWidth), tooltipSize.Height + (2 * shadowWidth));
			StreamReader streamReader = new StreamReader(Assembly.GetAssembly(typeof(HotKey)).GetManifestResourceStream("HotKeysLib.UI.Images.tooltip.png"));
			Bitmap shadowBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
			Bitmap result = new Bitmap(resultSize.Width,resultSize.Height);
			Graphics graphics = Graphics.FromImage(result);
			graphics.PageUnit = GraphicsUnit.Pixel;
			// Copy corners: top left
			Rectangle sourceRectangle = new Rectangle(0,0,shadowWidth,shadowWidth);
			graphics.DrawImage(shadowBitmap,0,0,sourceRectangle,GraphicsUnit.Pixel);
			// top right
			sourceRectangle = new Rectangle(shadowBitmap.Width - shadowWidth,0,shadowWidth,shadowWidth);
			graphics.DrawImage(shadowBitmap,result.Width - shadowWidth,0,sourceRectangle,GraphicsUnit.Pixel);
			// bottom left
			sourceRectangle = new Rectangle(0,shadowBitmap.Height - shadowWidth,shadowWidth,shadowWidth);
			graphics.DrawImage(shadowBitmap,0,result.Height - shadowWidth,sourceRectangle,GraphicsUnit.Pixel);
			// bottom right
			sourceRectangle = new Rectangle(shadowBitmap.Width - shadowWidth,shadowBitmap.Height - shadowWidth,shadowWidth,shadowWidth);
			graphics.DrawImage(shadowBitmap,result.Width - shadowWidth,result.Height - shadowWidth,sourceRectangle,GraphicsUnit.Pixel);
			// draw top and bottom edge
			int chunkSize = 50;
			if(chunkSize > tooltipSize.Width)
				chunkSize = tooltipSize.Width;
			for(int i = 0; tooltipSize.Width > i; i += chunkSize)
			{
				int width = chunkSize;
				if(i > tooltipSize.Width - chunkSize)
					width = tooltipSize.Width - i;
				Rectangle sourceRectangleTop = new Rectangle(shadowWidth + 5 ,0,width,shadowWidth);
				Rectangle sourceRectangleBottom = new Rectangle(shadowWidth + 5 ,shadowBitmap.Height - shadowWidth,width,shadowWidth);
				graphics.DrawImage(shadowBitmap,i + shadowWidth,0,sourceRectangleTop,GraphicsUnit.Pixel);
				graphics.DrawImage(shadowBitmap,i + shadowWidth,result.Height - shadowWidth,sourceRectangleBottom,GraphicsUnit.Pixel);
			}
			chunkSize = 10;
			if(chunkSize > tooltipSize.Height)
				chunkSize = tooltipSize.Height;
			for(int i = 0; tooltipSize.Height > i; i += chunkSize)
			{
				int height = chunkSize;
				if(i > tooltipSize.Height - chunkSize)
					height = tooltipSize.Height - i;
				Rectangle sourceRectangleLeft = new Rectangle(0 ,shadowWidth + 5,shadowWidth,height);
				Rectangle sourceRectangleRight = new Rectangle( shadowBitmap.Width - shadowWidth,shadowWidth + 5,shadowWidth,height);
				graphics.DrawImage(shadowBitmap,0,i + shadowWidth,sourceRectangleLeft,GraphicsUnit.Pixel);
				graphics.DrawImage(shadowBitmap,result.Width - shadowWidth,i + shadowWidth,sourceRectangleRight,GraphicsUnit.Pixel);
			}
			// draw inner rectangle
			graphics.FillRectangle(Brushes.White,shadowWidth,shadowWidth,tooltipSize.Width,tooltipSize.Height);
			// draw text
			graphics.DrawString(text,this.Font,Brushes.Black, new RectangleF(new PointF((float)(textMargin + shadowWidth) - inSet,(float)( 2 + shadowWidth) - inSet),stringSize));
			// determine position on keyboard
			Point position = new Point( (int)targetItemRectangle.Left - 10 ,(int)targetItemRectangle.Top + (int)targetItemRectangle.Height - 5);
			if(position.X + resultSize.Width > this.Width)
				position.X = this.Width - resultSize.Width;
			if(position.Y + resultSize.Height > this.Height)
				position.Y = targetItemRectangle.Y - resultSize.Height;
			// draw tooltip on keyboard
			Graphics.FromHwnd(this.Handle).DrawImage(result,position);
			// return rectangle (used to later restore the bitmap)
			return new Rectangle(position,resultSize);
		}

		private bool isKeyValidDropTarget(KeyboardLayoutKey keyboardKey)
		{
			if(keyboardKey==null)return false;
			ArrayList availableKeys = EnhancedKey.GetAvailableEnhancedKeys();
			if(keyboardKey==selectedIconKey)return true;
			foreach(EnhancedKey key in availableKeys)
			{
				if(keyboardKey.Key==key.Key && key.UsableAsHotKey)return true;
			}
			return false;
		}

		private KeyboardLayoutKey selectedIconKey = null;
		private Icon draggedIcon = null;
		private Point pointerIconOffset = new Point(0,0);
		private void KeyBoardForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button==MouseButtons.Left)
			{
				selectedIconKey = render.IconKeyFromPoint(new PointF(e.X,e.Y));
				if(selectedIconKey!=null)
				{
					HotKey hotKey = HotKey.GetHotKeyByKey(selectedIconKey.Key);
					if(hotKey==null)
					{
						// Should not be 
						this.Refresh();
						this.draggedIcon = null;
						return;
					}
					if(hotKey.TargetType != HotKeyTargetType.System)
					{
						draggedIcon = selectedIconKey.Icon;
						RectangleF iconRect = render.IconRectFromPoint(new PointF(e.X,e.Y));
						pointerIconOffset.X = e.X - (int)iconRect.Left;
						pointerIconOffset.Y = e.Y - (int)iconRect.Top;
						previousMousePos = new Point(e.X - pointerIconOffset.X,e.Y - pointerIconOffset.Y);
						render.HighlightKey(selectedIconKey, Color.Blue, false);
					}
					else
					{
						draggedIcon = null;
						selectedIconKey = null;
					}
				}
				else
				{
					draggedIcon = null;
				}
			}
		}

		private string droppedFileName = "";
		private void KeyBoardForm_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if(e.Data.GetDataPresent("FileDrop") && ((string[])e.Data.GetData("FileDrop",true)).Length==1)
			{
				e.Effect = DragDropEffects.Copy;
				droppedFileName = ((string[])e.Data.GetData("FileDrop",true))[0].Trim();
				if(droppedFileName.EndsWith(".lnk"))
				{
					ShellLink link = new ShellLink(droppedFileName);
					draggedIcon = link.LargeIcon;
				}
				else
				{
					FileIcon fileIcon = new FileIcon(droppedFileName);
					draggedIcon = fileIcon.ShellIcon;
				}
				pointerIconOffset = new Point(draggedIcon.Width/2,draggedIcon.Height/2);
				return;
			}
			if(e.Data.GetDataPresent(DataFormats.Text,true))
			{
				droppedFileName = e.Data.GetData(DataFormats.Text,true).ToString().Trim();
				// check to see if it is an e-mail address
				if(HotKeyHelperFunctions.IsValidEmailAddress(droppedFileName) && !droppedFileName.StartsWith("mailto:w")) droppedFileName = "mailto:" + droppedFileName;
				// check to see if it is a valid uri
				draggedIcon = HotKeyHelperFunctions.IconForUri(droppedFileName);
				if(draggedIcon!=null)
				{
					pointerIconOffset = new Point(draggedIcon.Width/2,draggedIcon.Height/2);
					e.Effect = DragDropEffects.Link;
				}
				else
				{
					droppedFileName = "";
					e.Effect = DragDropEffects.None;
				}
				return;
			}
		}

		private void KeyBoardForm_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			Point location = this.PointToClient(new Point(e.X,e.Y));
			MouseEventArgs mouseEventArgs = new MouseEventArgs(MouseButtons.Left,0,location.X,location.Y,0);
			this.KeyBoardForm_MouseUp(this,mouseEventArgs);
		}

		private void KeyBoardForm_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button==MouseButtons.Left && (selectedIconKey!=null || draggedIcon!=null))
			{
				if(isKeyValidDropTarget(currentKey))
				{   
					Graphics g = Graphics.FromHwnd(this.Handle);
					// Unhighlight the drop target
					this.render.UnHighlightKey(currentKey,false);
					this.render.RestoreKey(g, currentKey,false);
					// Animate icon to final position
					if(selectedIconKey!=null)
						this.snapBackIcon(previousMousePos,render.GetIconPositionForKey(currentKey),selectedIconKey.Icon);
					else
						this.snapBackIcon(previousMousePos,render.GetIconPositionForKey(currentKey),draggedIcon);
					if(currentKey!=selectedIconKey)
					{
						if(selectedIconKey!=null)
						{
							// Swap icons
							currentKey.Icon = selectedIconKey.Icon;
							selectedIconKey.Icon = null;
							// Make sure hotkeys config is updated and listner gets notified
							this.changeKey(selectedIconKey,currentKey);
						}
						else
						{
							currentKey.Icon = draggedIcon;
							HotKeyHelperFunctions.CreateNewHotkey(droppedFileName, currentKey.Key);
						}
						// TODO: this is rude and causes the keyboard to dissapaer for a sec investigate methods above
						this.UpdateScreen();
					}
					if(currentKey!=null)
					{
						// Mouse should be above the target so lets highlight it now
						this.render.HighlightKey(currentKey, Color.Blue, true);
						this.render.RestoreKey(g, currentKey,true);
					}
				}
				else
				{
					if(selectedIconKey!=null)
						this.snapBackIcon(previousMousePos,render.GetIconPositionForKey(selectedIconKey),selectedIconKey.Icon);
					// Mouse should be above the target so lets highlight it now
					if(currentKey!=null)
					{
						Graphics g = Graphics.FromHwnd(this.Handle);
						if(!currentKey.IsPlaceHolder)
							this.render.HighlightKey(currentKey, Color.Blue, true);
						this.render.RestoreKey(g, currentKey,true);
					}
					if(selectedIconKey==null)
						this.Refresh();
				}
			}
			if(e.Button==MouseButtons.Right)
			{
				KeyboardLayoutKey clickedKey = render.KeyFromPoint(new PointF(e.X,e.Y));
				if(clickedKey==null)
				{
					keyboardContextMenu.Show(this,new Point(e.X,e.Y));
				}
				else
				{
					if(clickedKey.IsPlaceHolder)
						keyboardContextMenu.Show(this,new Point(e.X,e.Y));
					else
					{
						if(EnhancedKey.IsKeyUsable(clickedKey.Key))
						{
							keyForContextMenu = clickedKey;
							prepareContextMenuForKey(clickedKey.Key);
							keyContextMenu.Show(this,new Point(e.X,e.Y));
						}
					}
				}
			}
			this.selectedIconKey=null;
			this.draggedIcon = null;
		}

		private KeyboardLayoutKey keyForContextMenu = null;
		private HotKey hotKeyOnClipboard = null;

		private void prepareContextMenuForKey(Keys key)
		{
			HotKey hotKey = HotKey.GetHotKeyByKey(key);
			if(hotKey!=null)
			{
				this.newHotKeyMenuItem.Enabled = false;
				this.copyMenuItem.Enabled = (hotKey.TargetType!=HotKeyTargetType.System);
				this.cutMenuItem.Enabled = (hotKey.TargetType!=HotKeyTargetType.System);
				this.deleteMenuItem.Enabled = (hotKey.TargetType!=HotKeyTargetType.System);
				this.pasteMenuItem.Enabled = false;
				this.propertiesMenuItem.Enabled = true;
			}
			else
			{
				this.newHotKeyMenuItem.Enabled = true;
				this.copyMenuItem.Enabled = false;
				this.cutMenuItem.Enabled = false;
				this.deleteMenuItem.Enabled = false;
				this.pasteMenuItem.Enabled = (hotKeyOnClipboard!=null);
				this.propertiesMenuItem.Enabled = false;
			}
		}

		private void changeKey(KeyboardLayoutKey currentKey, KeyboardLayoutKey newKey)
		{
			// Update the Hotkey
			HotKey currentHotKey = HotKey.GetHotKeyByKey(currentKey.Key);
			currentHotKey.Key = newKey.Key;
			// If any dialogs are open: update
			this.UpdatePropertyDialog(currentHotKey);
			HotKey.Persist();
		}

		private void snapBackIcon(PointF startPos, PointF endPos, Icon icon)
		{
			try // this might fail for several reason, but is never relevant
			{
				// Calculate vector
				PointF vector = new PointF((endPos.X - startPos.X)/10, (endPos.Y - startPos.Y)/10);
				PointF currentPos = startPos;
				Graphics g = Graphics.FromHwnd(this.Handle);
				for(int i=0; i<10;i++)
				{					
					// TODO: IconIssue
					render.RestoreRect(Graphics.FromHwnd(this.Handle),new Rectangle(new Point((int)currentPos.X,(int)currentPos.Y), new Size(32,32)));
					// Draw the icon at the new position
					currentPos.X += vector.X;
					currentPos.Y += vector.Y;
					g.DrawIcon(icon, (int)currentPos.X, (int)currentPos.Y);
					System.Threading.Thread.Sleep(27);
				}
			}
			catch{}
		}

		private void KeyBoardForm_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			Point location = this.PointToClient(new Point(e.X,e.Y));
				MouseEventArgs mouseEventArgs = new MouseEventArgs(MouseButtons.Left,0,location.X,location.Y,0);
			this.KeyBoardForm_MouseMove(this,mouseEventArgs);
		}

		public void OpenPropertyDialog(HotKey hotkey)
		{
			if(hotkey==null)
				return;
			PropertiesForm newForm = (PropertiesForm)propertyFormsHashTable[hotkey];
			if(newForm!=null)
				Window.RestoreAndBringToFront(newForm.Handle.ToInt32());
			else
			{
				newForm = new PropertiesForm();
				propertyFormsHashTable.Add(hotkey, newForm);
				this.TopMost = false;
				newForm.HotKey = hotkey;
				newForm.Apply += new EventHandler(propertiesFormApplyHandler);
				newForm.Closed += new EventHandler(propertiesFormClosedHandler);
				newForm.Show();
			}
			newForm.TopMost = true;
		}

		public void UpdatePropertyDialog(HotKey hotkey)
		{
			PropertiesForm newForm = (PropertiesForm)propertyFormsHashTable[hotkey];
			if(newForm!=null)
			{
				newForm.HotKey = hotkey;
			}
		}

		public void ClosePropertyDialog(HotKey hotkey)
		{
			PropertiesForm newForm = (PropertiesForm)propertyFormsHashTable[hotkey];
			if(newForm!=null)
			{
				newForm.Close();
			}
		}

		Hashtable propertyFormsHashTable = new Hashtable();
		private void propertiesMenuItem_Click(object sender, System.EventArgs e)
		{
			// Check to see what key's properties we need to show
			HotKey hotkey = HotKey.GetHotKeyByKey(keyForContextMenu.Key);
			OpenPropertyDialog(hotkey);
		}

		private void propertiesFormApplyHandler(object sender, EventArgs e)
		{
			HotKey.Persist();
			this.UpdateScreen();
		}

		private void propertiesFormClosedHandler(object sender, EventArgs e)
		{
			if(((PropertiesForm)sender).DialogResult == DialogResult.OK)
			{
				HotKey.Persist();
				this.UpdateScreen();
			}
			propertyFormsHashTable.Remove(((PropertiesForm)sender).HotKey);
			((PropertiesForm)sender).Dispose();
		}

		public void UpdateScreen()
		{
			if(this.layout == null || this.style == null)
				return;
			// Notify screen of update
			this.attachIconsToKeyboard();
			this.render.Invalidate();
			this.Refresh();
		}

		private void attachIconsToKeyboard()
		{
			foreach(KeyboardLayoutRow row in layout.Rows)
			{
				foreach(KeyboardLayoutKey key in row.Keys)
				{
					HotKey hotKey = HotKey.GetHotKeyByKey(key.Key);
					key.Icon = (hotKey!=null) ? hotKey.Icon : null;
				}
			}
		}

		private void deleteMenuItem_Click(object sender, System.EventArgs e)
		{
			HotKey hotKey = HotKey.GetHotKeyByKey(keyForContextMenu.Key);
			this.DeleteHotKey(hotKey);
		}

		public void DeleteHotKey(HotKey hotKey)
		{
			if(hotKey!=null)
			{
				if(hotKey.TargetType!=HotKeyTargetType.System)
				{
					if(DialogResult.Yes == MessageBox.Show("Are you sure you want to permanently delete this HotKey?","Delete HotKey",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question))
					{
						this.ClosePropertyDialog(hotKey);
						HotKey.GetAllHotKeys().Remove(hotKey);
						HotKey.Persist();
						this.UpdateScreen();
					}
				}
			}
		}

		private void cutMenuItem_Click(object sender, System.EventArgs e)
		{
			HotKey hotKey = HotKey.GetHotKeyByKey(keyForContextMenu.Key);
			hotKeyOnClipboard = hotKey;
			hotKey.Key = Keys.None;
			if(hotKey!=null)
			{
				if(hotKey.TargetType!=HotKeyTargetType.System)
				{
					this.ClosePropertyDialog(hotKey);
					HotKey.GetAllHotKeys().Remove(hotKey);
					HotKey.Persist();
					this.UpdateScreen();
				}
			}
		}

		private void copyMenuItem_Click(object sender, System.EventArgs e)
		{
			HotKey hotKey = HotKey.GetHotKeyByKey(keyForContextMenu.Key);
			hotKeyOnClipboard = hotKey;
		}

		private void pasteMenuItem_Click(object sender, System.EventArgs e)
		{
			HotKey clone = (HotKey)this.Clone( hotKeyOnClipboard );
			clone.Name = HotKey.CreateUniqueName(clone.Name);
			clone.ID = Guid.NewGuid();
			clone.Key = keyForContextMenu.Key;
			HotKey.GetAllHotKeys().Add(clone);
			HotKey.Persist();
			this.UpdateScreen();
		}

		private object Clone(object input)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream stream = new MemoryStream();
			binaryFormatter.Serialize(stream, input);
			stream.Seek(0, SeekOrigin.Begin);
			return binaryFormatter.Deserialize(stream);
		}

		private void newHotKeyMenuItem_Click(object sender, System.EventArgs e)
		{
			this.OpenNewHotKeyWizard( keyForContextMenu.Key );
		}

		public void OpenNewHotKeyWizard(Keys key)
		{
			WizardForm wizardForm = new WizardForm();
			if(key != Keys.None)
				wizardForm.SetKey( key );
			this.TopMost = false;
			wizardForm.Closed += new EventHandler(wizardForm_Closed);
			wizardForm.Show();
			wizardForm.TopMost = true;
		}

		private void wizardForm_Closed(object sender, EventArgs e)
		{
			WizardForm wizardForm = (WizardForm)sender;
			if(wizardForm.DialogResult == DialogResult.OK)
			{
				HotKey.GetAllHotKeys().Add(wizardForm.NewHotKey);
				// Persist changes
				HotKey.Persist();
				// Update screen
				this.UpdateScreen();
			}
		}

		private void KeyBoardForm_DoubleClick(object sender, System.EventArgs e)
		{
			Point mousePosition = this.PointToClient(MousePosition);
			currentKey = render.IconKeyFromPoint(new PointF(mousePosition.X,mousePosition.Y));
			if(currentKey!=null)
			{
				this.OpenPropertyDialog(HotKey.GetHotKeyByKey(currentKey.Key));
			}
		}

		private void KeyBoardForm_Click(object sender, System.EventArgs e)
		{
			this.TopMost = true;
		}

		private const int WS_BORDER = 0x00800000;

		private void quitHotKeysMenuItem_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void fadeMenuItem_Click(object sender, System.EventArgs e)
		{
			this.fadeMenuItem.Checked = !KeyboardSettings.Current.Fade;
			KeyboardSettings.Current.Fade = !KeyboardSettings.Current.Fade;
			KeyboardSettings.Persist();
		}
	
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.Style &= ~WS_BORDER;
				return cp;
			}
		}

		// For cross thread communications, will be called async
		private void onDisplaySettingsChanged(object sender, EventArgs e)
		{
			this.initForm(layout,style);
			this.Refresh();
		}

		private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
		{
			EventHandler handler = new EventHandler(onDisplaySettingsChanged);
			handler.BeginInvoke(this, new EventArgs(),null,null);
		}

		private void KeyBoardForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.handleShiftStateChange(e.Modifiers);
		}

		private void KeyBoardForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.handleShiftStateChange(e.Modifiers);
		}

		private Keys previousModifiers = Keys.None;
		private void handleShiftStateChange(Keys modifiers)
		{
			if(previousModifiers!=modifiers)
			{
				this.previousModifiers = modifiers;
				ShiftState newShiftState = this.shifStateFromModifierKeys(modifiers);
				if(this.shiftState!=newShiftState)
				{
					this.shiftState = newShiftState;
					this.initForm(layout,style);
					this.Refresh();
				}
			}
		}

		private ShiftState shifStateFromModifierKeys(Keys modifiers)
		{
			if(modifiers == Keys.Shift)
				return ShiftState.Shift;
			else if(modifiers == Keys.Control)
				return ShiftState.Ctrl;
			else if(modifiers == Keys.Alt)
				return ShiftState.Alt;
			else if(modifiers == (Keys.Control | Keys.Alt))
				return ShiftState.CtrlAlt;
			else if(modifiers == (Keys.Control | Keys.Alt | Keys.Shift))
				return ShiftState.ShiftCtrlAlt;
			else if(modifiers == (Keys.Control | Keys.Shift))
				return ShiftState.ShiftCtrl;
			else if(modifiers == (Keys.Alt | Keys.Shift))
				return ShiftState.ShiftAlt;
			return ShiftState.None;
		}

		private void aboutMenuItem_Click(object sender, System.EventArgs e)
		{
			HotKeyHelperFunctions.ShowAbout();
		}

//		private static Thread killerThread = null;
//		private static void killerMethod()
//		{
//			try
//			{
//				DateTime startMoment = DateTime.Now;
//				while(((TimeSpan)DateTime.Now.Subtract(startMoment)).TotalSeconds < 10)
//					Thread.Sleep(4000);
//				DropKeyBoardForm();
//			}
//			catch(Exception exception)
//			{
//				Console.WriteLine("KillerThread ended because: {0}", exception.Message);
//			}
//		}

//		private void KeyBoardForm_VisibleChanged(object sender, System.EventArgs e)
//		{
//			if(killerThread!=null)
//			{
//				killerThread.Abort();
//				killerThread.Join();
//			}
//			if(!this.Visible)
//			{
//				ThreadStart threadStart = new ThreadStart(killerMethod);
//				killerThread = new Thread(threadStart);
//				killerThread.Start();
//			}
//		}
	}
}
