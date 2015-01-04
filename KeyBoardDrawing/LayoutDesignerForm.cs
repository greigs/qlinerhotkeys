using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using HotKeysLib.OnScreenKeyboard;

namespace KeyboardDrawing
{
	/// <summary>
	/// Summary description for LayoutDesignerForm.
	/// </summary>
	public class LayoutDesignerForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.PropertyGrid layoutPropertyGrid;
		private System.Windows.Forms.Splitter splitter3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.PropertyGrid stylePropertyGrid;
		private System.Windows.Forms.Splitter splitter4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.PropertyGrid rowPropertyGrid;
		private System.Windows.Forms.Splitter splitter5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.PropertyGrid keyPropertyGrid;
		private System.Windows.Forms.MenuItem styleOpenMenuItem;
		private System.Windows.Forms.MenuItem saveStyleAsMenuItem;
		private System.Windows.Forms.MenuItem saveStyleMenuItem;
		private System.Windows.Forms.MenuItem closeMenuItem;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.MenuItem openLayoutMenuItem;
		private System.Windows.Forms.MenuItem saveLayoutmenuItem;
		private System.Windows.Forms.MenuItem saveLayoutAsMenuItem;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.MenuItem VGTestmenuItem;
		private System.Windows.Forms.MenuItem importKLCFileMenuItem;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem openSafeMenuItem;
		private System.Windows.Forms.MenuItem saveSaveMenuItem;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem bindScanCodesMenuItem;
		private System.Windows.Forms.MenuItem convertFilesMenuItem;
		private System.ComponentModel.IContainer components;

		public LayoutDesignerForm()
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
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.styleOpenMenuItem = new System.Windows.Forms.MenuItem();
			this.saveStyleMenuItem = new System.Windows.Forms.MenuItem();
			this.saveStyleAsMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.openLayoutMenuItem = new System.Windows.Forms.MenuItem();
			this.saveLayoutmenuItem = new System.Windows.Forms.MenuItem();
			this.saveLayoutAsMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.importKLCFileMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.openSafeMenuItem = new System.Windows.Forms.MenuItem();
			this.saveSaveMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.closeMenuItem = new System.Windows.Forms.MenuItem();
			this.VGTestmenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.bindScanCodesMenuItem = new System.Windows.Forms.MenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.keyPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.button4 = new System.Windows.Forms.Button();
			this.splitter5 = new System.Windows.Forms.Splitter();
			this.rowPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.button3 = new System.Windows.Forms.Button();
			this.splitter4 = new System.Windows.Forms.Splitter();
			this.stylePropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.button2 = new System.Windows.Forms.Button();
			this.splitter3 = new System.Windows.Forms.Splitter();
			this.layoutPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.button1 = new System.Windows.Forms.Button();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.panel3 = new System.Windows.Forms.Panel();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.convertFilesMenuItem = new System.Windows.Forms.MenuItem();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.BackColor = System.Drawing.Color.White;
			this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(805, 729);
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.Move += new System.EventHandler(this.pictureBox_Move);
			this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
			this.pictureBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragEnter);
			this.pictureBox.SizeChanged += new System.EventHandler(this.pictureBox_SizeChanged);
			this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
			this.pictureBox.MouseHover += new System.EventHandler(this.pictureBox_MouseHover);
			this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			this.pictureBox.DragOver += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragOver);
			this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
			this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItem1,
																					 this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.styleOpenMenuItem,
																					  this.saveStyleMenuItem,
																					  this.saveStyleAsMenuItem,
																					  this.menuItem5,
																					  this.openLayoutMenuItem,
																					  this.saveLayoutmenuItem,
																					  this.saveLayoutAsMenuItem,
																					  this.menuItem4,
																					  this.importKLCFileMenuItem,
																					  this.menuItem2,
																					  this.openSafeMenuItem,
																					  this.saveSaveMenuItem,
																					  this.menuItem7,
																					  this.closeMenuItem,
																					  this.VGTestmenuItem});
			this.menuItem1.Text = "File";
			// 
			// styleOpenMenuItem
			// 
			this.styleOpenMenuItem.Index = 0;
			this.styleOpenMenuItem.Text = "Open Style";
			this.styleOpenMenuItem.Click += new System.EventHandler(this.styleOpenMenuItem_Click);
			// 
			// saveStyleMenuItem
			// 
			this.saveStyleMenuItem.Index = 1;
			this.saveStyleMenuItem.Text = "Save Style";
			this.saveStyleMenuItem.Click += new System.EventHandler(this.saveStyleMenuItem_Click);
			// 
			// saveStyleAsMenuItem
			// 
			this.saveStyleAsMenuItem.Index = 2;
			this.saveStyleAsMenuItem.Text = "Save Style As";
			this.saveStyleAsMenuItem.Click += new System.EventHandler(this.saveStyleAsMenuItem_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "-";
			// 
			// openLayoutMenuItem
			// 
			this.openLayoutMenuItem.Index = 4;
			this.openLayoutMenuItem.Text = "Open Layout";
			this.openLayoutMenuItem.Click += new System.EventHandler(this.layoutOpenMenuItem_Click);
			// 
			// saveLayoutmenuItem
			// 
			this.saveLayoutmenuItem.Index = 5;
			this.saveLayoutmenuItem.Text = "Save Layout";
			this.saveLayoutmenuItem.Click += new System.EventHandler(this.savelayoutMenuItem_Click);
			// 
			// saveLayoutAsMenuItem
			// 
			this.saveLayoutAsMenuItem.Index = 6;
			this.saveLayoutAsMenuItem.Text = "Save Layout As";
			this.saveLayoutAsMenuItem.Click += new System.EventHandler(this.savelayoutAsMenuItem_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 7;
			this.menuItem4.Text = "-";
			// 
			// importKLCFileMenuItem
			// 
			this.importKLCFileMenuItem.Index = 8;
			this.importKLCFileMenuItem.Text = "Import KLC file";
			this.importKLCFileMenuItem.Click += new System.EventHandler(this.importKLCFileMenuItem_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 9;
			this.menuItem2.Text = "-";
			// 
			// openSafeMenuItem
			// 
			this.openSafeMenuItem.Index = 10;
			this.openSafeMenuItem.Text = "Open Safe Format";
			this.openSafeMenuItem.Click += new System.EventHandler(this.openSafeMenuItem_Click);
			// 
			// saveSaveMenuItem
			// 
			this.saveSaveMenuItem.Index = 11;
			this.saveSaveMenuItem.Text = "Save Safe Format";
			this.saveSaveMenuItem.Click += new System.EventHandler(this.saveSaveMenuItem_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 12;
			this.menuItem7.Text = "-";
			// 
			// closeMenuItem
			// 
			this.closeMenuItem.Index = 13;
			this.closeMenuItem.Text = "Close";
			this.closeMenuItem.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// VGTestmenuItem
			// 
			this.VGTestmenuItem.Index = 14;
			this.VGTestmenuItem.Text = "VGTest";
			this.VGTestmenuItem.Click += new System.EventHandler(this.VGTestmenuItem_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.bindScanCodesMenuItem,
																					  this.convertFilesMenuItem});
			this.menuItem3.Text = "Tools";
			// 
			// bindScanCodesMenuItem
			// 
			this.bindScanCodesMenuItem.Index = 0;
			this.bindScanCodesMenuItem.Text = "Bind scancodes to keys";
			this.bindScanCodesMenuItem.Click += new System.EventHandler(this.bindScanCodesMenuItem_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.splitter1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 729);
			this.panel1.TabIndex = 7;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.keyPropertyGrid);
			this.panel2.Controls.Add(this.button4);
			this.panel2.Controls.Add(this.splitter5);
			this.panel2.Controls.Add(this.rowPropertyGrid);
			this.panel2.Controls.Add(this.button3);
			this.panel2.Controls.Add(this.splitter4);
			this.panel2.Controls.Add(this.stylePropertyGrid);
			this.panel2.Controls.Add(this.button2);
			this.panel2.Controls.Add(this.splitter3);
			this.panel2.Controls.Add(this.layoutPropertyGrid);
			this.panel2.Controls.Add(this.button1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(200, 726);
			this.panel2.TabIndex = 8;
			// 
			// keyPropertyGrid
			// 
			this.keyPropertyGrid.CommandsVisibleIfAvailable = true;
			this.keyPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.keyPropertyGrid.LargeButtons = false;
			this.keyPropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.keyPropertyGrid.Location = new System.Drawing.Point(0, 570);
			this.keyPropertyGrid.Name = "keyPropertyGrid";
			this.keyPropertyGrid.Size = new System.Drawing.Size(200, 156);
			this.keyPropertyGrid.TabIndex = 10;
			this.keyPropertyGrid.Text = "propertyGrid4";
			this.keyPropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.keyPropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.keyPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.stylePropertyGrid_PropertyValueChanged);
			// 
			// button4
			// 
			this.button4.Dock = System.Windows.Forms.DockStyle.Top;
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button4.Location = new System.Drawing.Point(0, 547);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(200, 23);
			this.button4.TabIndex = 9;
			this.button4.Text = "Selected Key";
			this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// splitter5
			// 
			this.splitter5.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter5.Location = new System.Drawing.Point(0, 544);
			this.splitter5.Name = "splitter5";
			this.splitter5.Size = new System.Drawing.Size(200, 3);
			this.splitter5.TabIndex = 8;
			this.splitter5.TabStop = false;
			// 
			// rowPropertyGrid
			// 
			this.rowPropertyGrid.CommandsVisibleIfAvailable = true;
			this.rowPropertyGrid.Dock = System.Windows.Forms.DockStyle.Top;
			this.rowPropertyGrid.LargeButtons = false;
			this.rowPropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.rowPropertyGrid.Location = new System.Drawing.Point(0, 394);
			this.rowPropertyGrid.Name = "rowPropertyGrid";
			this.rowPropertyGrid.Size = new System.Drawing.Size(200, 150);
			this.rowPropertyGrid.TabIndex = 7;
			this.rowPropertyGrid.Text = "propertyGrid3";
			this.rowPropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.rowPropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.rowPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.stylePropertyGrid_PropertyValueChanged);
			// 
			// button3
			// 
			this.button3.Dock = System.Windows.Forms.DockStyle.Top;
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button3.Location = new System.Drawing.Point(0, 371);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(200, 23);
			this.button3.TabIndex = 6;
			this.button3.Text = "- Selected Row";
			this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// splitter4
			// 
			this.splitter4.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter4.Location = new System.Drawing.Point(0, 368);
			this.splitter4.Name = "splitter4";
			this.splitter4.Size = new System.Drawing.Size(200, 3);
			this.splitter4.TabIndex = 5;
			this.splitter4.TabStop = false;
			// 
			// stylePropertyGrid
			// 
			this.stylePropertyGrid.CommandsVisibleIfAvailable = true;
			this.stylePropertyGrid.Dock = System.Windows.Forms.DockStyle.Top;
			this.stylePropertyGrid.LargeButtons = false;
			this.stylePropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.stylePropertyGrid.Location = new System.Drawing.Point(0, 194);
			this.stylePropertyGrid.Name = "stylePropertyGrid";
			this.stylePropertyGrid.Size = new System.Drawing.Size(200, 174);
			this.stylePropertyGrid.TabIndex = 4;
			this.stylePropertyGrid.Text = "propertyGrid2";
			this.stylePropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.stylePropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.stylePropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.stylePropertyGrid_PropertyValueChanged);
			// 
			// button2
			// 
			this.button2.Dock = System.Windows.Forms.DockStyle.Top;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button2.Location = new System.Drawing.Point(0, 171);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(200, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "- Style";
			this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// splitter3
			// 
			this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter3.Location = new System.Drawing.Point(0, 168);
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new System.Drawing.Size(200, 3);
			this.splitter3.TabIndex = 2;
			this.splitter3.TabStop = false;
			// 
			// layoutPropertyGrid
			// 
			this.layoutPropertyGrid.CommandsVisibleIfAvailable = true;
			this.layoutPropertyGrid.Dock = System.Windows.Forms.DockStyle.Top;
			this.layoutPropertyGrid.LargeButtons = false;
			this.layoutPropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.layoutPropertyGrid.Location = new System.Drawing.Point(0, 23);
			this.layoutPropertyGrid.Name = "layoutPropertyGrid";
			this.layoutPropertyGrid.Size = new System.Drawing.Size(200, 145);
			this.layoutPropertyGrid.TabIndex = 1;
			this.layoutPropertyGrid.Text = "- Layout";
			this.layoutPropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.layoutPropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.layoutPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.layoutPropertyGrid_PropertyValueChanged);
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Top;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.Location = new System.Drawing.Point(0, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(200, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "- Layout";
			this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(200, 3);
			this.splitter1.TabIndex = 7;
			this.splitter1.TabStop = false;
			// 
			// splitter2
			// 
			this.splitter2.Location = new System.Drawing.Point(200, 0);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(3, 729);
			this.splitter2.TabIndex = 8;
			this.splitter2.TabStop = false;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.statusBar1);
			this.panel3.Controls.Add(this.pictureBox);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(203, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(805, 729);
			this.panel3.TabIndex = 9;
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 707);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(805, 22);
			this.statusBar1.TabIndex = 1;
			this.statusBar1.Text = "statusBar1";
			// 
			// convertFilesMenuItem
			// 
			this.convertFilesMenuItem.Index = 1;
			this.convertFilesMenuItem.Text = "Convert KLC Files";
			this.convertFilesMenuItem.Click += new System.EventHandler(this.convertFilesMenuItem_Click);
			// 
			// LayoutDesignerForm
			// 
			this.AllowDrop = true;
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1008, 729);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.splitter2);
			this.Controls.Add(this.panel1);
			this.Menu = this.mainMenu;
			this.Name = "LayoutDesignerForm";
			this.Text = "On Screen Keyboard Designer";
			this.Load += new System.EventHandler(this.LayoutDesignerForm_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.EnableVisualStyles();
			Application.Run(new LayoutDesignerForm());
		}

		private void LayoutDesignerForm_Load(object sender, System.EventArgs e)
		{
			for(int i = 0; i < 9; i++)
			{
				KeyboardLayoutRow newKeyboardLayoutRow = new KeyboardLayoutRow();
				layout.Rows.Add(newKeyboardLayoutRow);
				for(int j = 0; j < 26; j++)
				{
					newKeyboardLayoutRow.Keys.Add(new KeyboardLayoutKey());
				}
			}
			this.layoutPropertyGrid.SelectedObject = this.layout;
			this.stylePropertyGrid.SelectedObject = this.style;
		}

		private KeyboardRenderer renderer = new KeyboardRenderer();
		private KeyboardLayout layout = new KeyboardLayout();
		private KeyboardStyle style = new KeyboardStyle();
		private Hashtable language = null;

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// new StyleDesignerForm().ShowDialog();
		}

//		bool painting = false;
		private void pictureBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
//			if(painting)
//				return;
//			painting = true;
			renderer.RenderAll(e.Graphics, this.pictureBox.Handle, this.layout, this.style, this.language, ShiftState.None, this.pictureBox.ClientRectangle);
//			painting = false;
		}

		private void stylePropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			renderer.Invalidate();
			this.pictureBox.Refresh();
		}

		private void layoutPropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			renderer.Invalidate();
			this.pictureBox.Refresh();			
		}


		private void pictureBox_SizeChanged(object sender, System.EventArgs e)
		{
			renderer.Invalidate();
			this.pictureBox.Refresh();
		}

		private void pictureBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			KeyboardLayoutKey key = renderer.KeyFromPoint(new PointF(e.X,e.Y) /*, this.layout, this.style, this.pictureBox.ClientRectangle */ );
			this.keyPropertyGrid.SelectedObject = key;
			if(iconKey!=null && key!=null)
			{
				key.Icon = iconKey.Icon;
				iconKey.Icon = null;
				this.pictureBox.Refresh();
			}
			KeyboardLayoutRow row = renderer.RowFromPoint(new PointF(e.X,e.Y) /*, this.layout, this.style, this.pictureBox.ClientRectangle */ );
			this.rowPropertyGrid.SelectedObject = row;
			iconKey = null;
		}

		int layoutPropHeight = 0;
		private void button1_Click(object sender, System.EventArgs e)
		{
			if(this.layoutPropertyGrid.Height==0)
			{
				this.layoutPropertyGrid.Height = layoutPropHeight;
				button1.Text.Replace("+", "-");
			}
			else
			{
				this.layoutPropHeight = this.layoutPropertyGrid.Height;
				this.layoutPropertyGrid.Height = 0;
				button1.Text.Replace("-", "+");
			}
		}

		int stylePropHeight = 0;
		private void button2_Click(object sender, System.EventArgs e)
		{
			if(this.stylePropertyGrid.Height==0)
			{
				this.stylePropertyGrid.Height = stylePropHeight;
				((Button)sender).Text.Replace("+", "-");
			}
			else
			{
				this.stylePropHeight = this.stylePropertyGrid.Height;
				this.stylePropertyGrid.Height = 0;
				((Button)sender).Text.Replace("-", "+");
			}
		}

		int rowPropHeight = 0;
		private void button3_Click(object sender, System.EventArgs e)
		{
			if(this.rowPropertyGrid.Height==0)
			{
				this.rowPropertyGrid.Height = rowPropHeight;
				((Button)sender).Text.Replace("+", "-");
			}
			else
			{
				this.rowPropHeight = this.rowPropertyGrid.Height;
				this.rowPropertyGrid.Height = 0;
				((Button)sender).Text.Replace("-", "+");
			}
		}

		private string styleFilename = "";
		private void styleOpenMenuItem_Click(object sender, System.EventArgs e)
		{
			switch(MessageBox.Show(this,"Save changes to current file?","Save changes?",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question))
			{
				case DialogResult.OK :
					if(this.styleFilename == "")
						this.saveStyleAsMenuItem_Click(sender, e);
					else
						this.saveStyleMenuItem_Click(sender, e);
					break;
				case DialogResult.Cancel :
					return;
			}

			if(this.openFileDialog.ShowDialog()==DialogResult.OK)
			{
				try
				{
					this.style = KeyboardStyle.Open(this.openFileDialog.FileName);
					this.stylePropertyGrid.SelectedObject = this.style;					
					this.styleFilename = this.openFileDialog.FileName;
					renderer.Invalidate();
					this.pictureBox.Refresh();
				}
				catch(Exception exception)
				{
					MessageBox.Show("Cannot open file: " + exception.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error); 
				}
			}
		}

		private void saveStyleAsMenuItem_Click(object sender, System.EventArgs e)
		{
			this.saveFileDialog.FileName = this.styleFilename;
			if(this.saveFileDialog.ShowDialog()==DialogResult.OK)
			{
				try
				{
					style.Save(this.saveFileDialog.FileName);
					this.styleFilename = this.saveFileDialog.FileName;
				}
				catch(Exception exception)
				{
					MessageBox.Show("Cannot save file: " + exception.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error); 
				}
			}
		}

		private void saveStyleMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(this.styleFilename!="")
					style.Save(this.styleFilename);
				else
					this.saveStyleAsMenuItem_Click(sender, e);
			}
			catch(Exception exception)
			{
				MessageBox.Show("Cannot save file: " + exception.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error); 
			}
		}


		private string layoutFilename = "";
		private void layoutOpenMenuItem_Click(object sender, System.EventArgs e)
		{
			switch(MessageBox.Show(this,"Save changes to current file?","Save changes?",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question))
			{
				case DialogResult.OK :
					if(this.layoutFilename == "")
						this.savelayoutAsMenuItem_Click(sender, e);
					else
						this.savelayoutMenuItem_Click(sender, e);
					break;
				case DialogResult.Cancel :
					return;
			}

			if(this.openFileDialog.ShowDialog()==DialogResult.OK)
			{
				try
				{
					this.layout = KeyboardLayout.Open(this.openFileDialog.FileName);
					this.layoutPropertyGrid.SelectedObject = this.layout;
					this.layoutFilename = this.openFileDialog.FileName;
					renderer.Invalidate();
					this.pictureBox.Refresh();
				}
				catch(Exception exception)
				{
					MessageBox.Show("Cannot open file: " + exception.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error); 
				}
			}
		}

		private void savelayoutAsMenuItem_Click(object sender, System.EventArgs e)
		{
			this.saveFileDialog.FileName = this.layoutFilename;
			if(this.saveFileDialog.ShowDialog()==DialogResult.OK)
			{
				try
				{
					layout.Save(this.saveFileDialog.FileName);
					this.layoutFilename = this.saveFileDialog.FileName;
				}
				catch(Exception exception)
				{
					MessageBox.Show("Cannot save file: " + exception.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error); 
				}
			}
		}


		private void savelayoutMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(this.layoutFilename!="")
					layout.Save(this.layoutFilename);
				else
					this.savelayoutAsMenuItem_Click(sender, e);
			}
			catch(Exception exception)
			{
				MessageBox.Show("Cannot save file: " + exception.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error); 
			}
		}

		private void pictureBox_Move(object sender, System.EventArgs e)
		{
		}

		private void pictureBox_Click(object sender, System.EventArgs e)
		{
		}

		
		private void pictureBox_MouseHover(object sender, System.EventArgs e)
		{
		}

		KeyboardLayoutKey iconKey = null;
		private void pictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			iconKey = renderer.IconKeyFromPoint(new PointF(e.X,e.Y) /*, this.layout, this.style, this.pictureBox.ClientRectangle */ );
		}

		private void pictureBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(iconKey!=null)
			{
//				Graphics g = Graphics.FromHdc(this.pictureBox.Handle);
//				lock(g)
//				{
//					g.DrawIcon(iconKey.Icon, e.X, e.Y);
//				}
				pictureBox.DoDragDrop(iconKey, DragDropEffects.Link);
			}
		}

		private void pictureBox_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			KeyboardLayoutKey key = renderer.KeyFromPoint(new PointF(e.X,e.Y) /*, this.layout, this.style, this.pictureBox.ClientRectangle*/ );			
			if(key!=null)
			{
				e.Effect = DragDropEffects.Link;
			}
			else
			{
			}
		}

		private void pictureBox_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			KeyboardLayoutKey key = renderer.KeyFromPoint(new PointF(e.X,e.Y) /*, this.layout, this.style, this.pictureBox.ClientRectangle*/ );			
			if(key!=null)
			{
				e.Effect = DragDropEffects.Link;
			}
			else
			{
			}
		}

		private void VGTestmenuItem_Click(object sender, System.EventArgs e)
		{
		}

		private Hashtable keyScanCodePairs = null;
		private Hashtable scanCodeKeyPairs = null;
		private Hashtable ligature = null;
		private void importKLCFileMenuItem_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			if(DialogResult.OK == fileDialog.ShowDialog())
			{
				importKLCFile(fileDialog.FileName);
			}
		}

		private void importKLCFile(string filename)
		{
			Stream stream = new FileStream(filename,FileMode.Open,FileAccess.Read);
			string fileString = (new StreamReader(stream)).ReadToEnd();
			stream.Close();
			string[] lines = fileString.Split(Environment.NewLine.ToCharArray());
			// read out shifstates and columms
			Hashtable languageData = new Hashtable();
			Hashtable columnShiftStatePairs = new Hashtable();
			keyScanCodePairs = new Hashtable();
			scanCodeKeyPairs = new Hashtable();
			for( int i = 24 ; i < lines.Length ; i+=2)
			{
				if(lines[i] == "")
					break;
				// First character is int corresponding with ShiftState
				ShiftState shiftState = (ShiftState)Int32.Parse(lines[i].Substring(0,1));
				languageData.Add(shiftState,new Hashtable());
				columnShiftStatePairs.Add(Int32.Parse(lines[i].Substring(11,1)) - 1,shiftState);
			}
			// read out ligature keys
			ligature = new Hashtable();
			if(fileString.IndexOf("LIGATURE") != -1)
			{
				for( int i = 24 ; i < lines.Length ; i+=2)
				{
					if(lines[i].StartsWith("LIGATURE"))
					{
						for( int j = i + 10 ; j < lines.Length ; j+=2)
						{
							if(lines[j] == "")
								break;
							lines[j] = lines[j].Replace("@", "");
							while(lines[j].IndexOf("\t\t")!=-1)
								lines[j] = lines[j].Replace("\t\t", "\t");
							string[] parts = lines[j].Split("\t".ToCharArray());
							Keys key = parseKlcKeyString(parts[0]);
							ShiftState shiftState = (ShiftState)Int32.Parse(parts[1]);
							string keyText = parceKlcCharacterString(parts[2]);
							//								for( int k = 2 ; k < parts.Length ; k++)
							//								{
							//									if(parts[k] != "" & !parts[k].StartsWith(@"//"))
							//										break;
							//									else 
							//										keyText += parceKlcCharacterString(parts[k]);
							//								}
							Hashtable keyHashtable = (Hashtable)this.ligature[key];
							if(keyHashtable==null)
							{
								keyHashtable = new Hashtable();
								this.ligature.Add(key, keyHashtable);
							}
							keyHashtable.Add(shiftState,keyText);
						}
						break;
					}
				}
			}
			// read out keys
			for( int i = 24 + (2 * columnShiftStatePairs.Count) + 12 ; i < lines.Length ; i+=2)
			{
				if(lines[i]=="")
					break;
				else
				{
					lines[i] = lines[i].Replace("@", "");
					while(lines[i].IndexOf("\t\t")!=-1)
						lines[i] = lines[i].Replace("\t\t", "\t");
					string[] parts = lines[i].Split("\t".ToCharArray());
					// Create scancode key pair
					if(parts[0]!="-1")
					{
						int scanCode = Int32.Parse(parts[0], System.Globalization.NumberStyles.HexNumber);
						Keys key = parseKlcKeyString(parts[1]);
						if(key!=Keys.None)
						{
							keyScanCodePairs.Add(key,scanCode);
							scanCodeKeyPairs.Add(scanCode,key);
						}
						foreach(int columnIndex in columnShiftStatePairs.Keys)
						{
							if(parts[columnIndex]=="%%")
							{
								((Hashtable)(languageData[(ShiftState)columnShiftStatePairs[columnIndex]])).Add(scanCode, ((Hashtable)this.ligature[key])[(ShiftState)columnShiftStatePairs[columnIndex]] );
								//MessageBox.Show(((Hashtable)this.ligature[key])[(ShiftState)columnShiftStatePairs[columnIndex]].ToString());
							}
							else
								((Hashtable)(languageData[(ShiftState)columnShiftStatePairs[columnIndex]])).Add(scanCode,parceKlcCharacterString(parts[columnIndex]));
						}
					}
				}
			}
			this.language = languageData;
		}

		private void safeSaveLayout()
		{
			SaveFileDialog dialog = new SaveFileDialog();
			if(dialog.ShowDialog(this) == DialogResult.OK)
			{
				SortedList rows = new SortedList();
				int rowIndex = 0;
				foreach(KeyboardLayoutRow row in this.layout.Rows)
				{
					// Create sorted list holding all data for keys
					SortedList keys = new SortedList();
					int keyIndex = 0;
					foreach(KeyboardLayoutKey key in row.Keys)
					{
						Hashtable keyData = new Hashtable();
						keyData.Add("key.Height", key.Height);
						keyData.Add("key.Icon", key.Icon);
						keyData.Add("key.IsPlaceHolder", key.IsPlaceHolder);
						keyData.Add("key.Key", key.Key);
						keyData.Add("key.Texts", key.Texts);
						keyData.Add("key.Width", key.Width);
						keys.Add(keyIndex, keyData);
						keyIndex++;
					}
					// Create Hashtable for rowData
					Hashtable rowData = new Hashtable();
					rowData.Add("row.Height", row.Height);
					rowData.Add("row.IsPlaceHolder", row.IsPlaceHolder);
					rowData.Add("row.Keys", keys);
					rows.Add(rowIndex, rowData);
					rowIndex++;
				}
				FileStream stream = new FileStream(dialog.FileName,FileMode.Create);
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream,rows);
				stream.Close();
			}
		}

		private void safeOpenLayout()
		{
			OpenFileDialog dialog = new OpenFileDialog();
			if(DialogResult.OK == dialog.ShowDialog(this))
			{
				KeyboardLayout layout = new KeyboardLayout();
				FileStream stream = new FileStream(dialog.FileName,FileMode.Open);
				BinaryFormatter formatter = new BinaryFormatter();
				SortedList rows = (SortedList)formatter.Deserialize(stream);
				stream.Close();
				foreach(Hashtable rowData in rows.Values)
				{
					KeyboardLayoutRow row = new KeyboardLayoutRow();
					row.Height = (float)rowData["row.Height"];
					row.IsPlaceHolder = (bool)rowData["row.IsPlaceHolder"];
					foreach(Hashtable keyData in ((SortedList)rowData["row.Keys"]).Values)
					{
						KeyboardLayoutKey key = new KeyboardLayoutKey();
						key.Height =(float)keyData["key.Height"];
						key.Icon =(Icon)keyData["key.Icon"];
						key.IsPlaceHolder =(bool)keyData["key.IsPlaceHolder"];
						key.Key =(Keys)keyData["key.Key"];
						key.Texts =(ArrayList)keyData["key.Texts"];
						key.Width =(float)keyData["key.Width"];
						row.Keys.Add(key);
					}
					layout.Rows.Add(row);
				}
				this.layout = layout;
				this.layoutPropertyGrid.SelectedObject = this.layout;
				this.layoutFilename = this.openFileDialog.FileName;
				renderer.Invalidate();
				this.pictureBox.Refresh();
			}
		}

		private void setLayoutKeyText(Keys key, string text)
		{
			foreach(KeyboardLayoutRow row in this.layout.Rows)
				foreach(KeyboardLayoutKey layoutKey in row.Keys)
					if(layoutKey.Key == key)
						layoutKey.Texts[0] = text;
		}

		private string parceKlcCharacterString(string klcString)
		{
			if(klcString.Trim().Length==1)
				return klcString;
			if(klcString.Trim()=="-1" || klcString.Trim()=="%%")
				return "";
			char unicodeChar = (char)Int32.Parse(klcString, System.Globalization.NumberStyles.HexNumber);
			return unicodeChar.ToString();
		}

		private void bindScancodesToKeys()
		{
			if(this.keyScanCodePairs!=null)
			{
				foreach(KeyboardLayoutRow row in this.layout.Rows)
					foreach(KeyboardLayoutKey key in row.Keys)
					{
						if(this.keyScanCodePairs[key.Key]!=null)
							key.ScanCode = (int)this.keyScanCodePairs[key.Key];
					}
			}
		}

		private Keys parseKlcKeyString(string klcString)
		{
			// Incompatible names
			if(klcString == "OEM_1")
				return Keys.OemSemicolon;
			if(klcString == "OEM_2")
				return Keys.OemQuestion;
			if(klcString == "OEM_3")
				return Keys.Oemtilde;
			if(klcString == "OEM_4")
				return Keys.OemOpenBrackets;
			if(klcString == "OEM_5")
				return Keys.OemBackslash;
			if(klcString == "OEM_6")
				return Keys.OemCloseBrackets;
			if(klcString == "OEM_7")
				return Keys.OemQuotes;
			// Usefull names
			string cleanString = klcString.Replace("_","").ToLower().Trim();
			System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(cleanString, @"\d");
			if(match.Success)
				cleanString = "d" + cleanString;
			// Parse enum
			try{return (Keys)Enum.Parse(typeof(Keys),cleanString,true);}
			catch{return Keys.None;}
		}

		private void openSafeMenuItem_Click(object sender, System.EventArgs e)
		{
			this.safeOpenLayout();
		}

		private void saveSaveMenuItem_Click(object sender, System.EventArgs e)
		{
			this.safeSaveLayout();
		}

		private void bindScanCodesMenuItem_Click(object sender, System.EventArgs e)
		{
			this.bindScancodesToKeys();
		}

		private void convertFilesMenuItem_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			if(dialog.ShowDialog(this) == DialogResult.OK)
			{
				foreach(string filename in Directory.GetFiles(dialog.SelectedPath, "*.klc"))
				{
					this.importKLCFile(filename);
					this.language.Add("scanCodeKeyPairs",this.scanCodeKeyPairs);
					FileStream stream = new FileStream(filename.Replace(".klc",".language.bin"),FileMode.Create,FileAccess.Write);
					BinaryFormatter formatter = new BinaryFormatter();
					formatter.Serialize(stream,this.language);
					stream.Close();
				}
			}
		}
	}
}
