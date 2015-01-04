using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace HotKeysLib
{
	/// <summary>
	/// InstanceSelectionForm provides user with a GUI for selecting an instance 
	/// of an application associated with a HotKeyItem
	/// </summary>
	public class WindowSelectionForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		// Reference to HotKeyItem for current form
		// private Window currentWindow;
		private HotKey currentKey;
		// Holds refences to all instance of the application associated with
		// the current Window
		private ArrayList windows;
		// Holds references to all WindowSelector controls on the form
		private ArrayList windowSelectorItems;
		private const int BORDERWIDTH = 2;
		private const int MINIMUMFORMWIDTH = 200;

		public WindowSelectionForm(ArrayList Windows, HotKey key)
		{
			// Required for Windows Form Designer support
			InitializeComponent();
			// Set and initialize members
			currentKey = key;
			windows = Windows;
			windowSelectorItems = new ArrayList(windows.Count);
		
            // Calculate and set form height:
			// 3 is Minimize, Maximize and Close all items
			this.Height = ((3 + windows.Count) * 34) + (2 * BORDERWIDTH);
			// Counter 
			int i = 0;
			foreach(Window window in windows)
			{
				// Create a selector item for each process
				addWindowSelectorItem(WindowSelectorItemType.Window, i, window);
				i++;
			}
			// Minimize all item
			addWindowSelectorItem(WindowSelectorItemType.MinimizeAll,windows.Count,null);
			// Maximize all item
			addWindowSelectorItem(WindowSelectorItemType.RestoreAll,windows.Count + 1,null);
			// Close all item
			addWindowSelectorItem(WindowSelectorItemType.CloseAll,windows.Count + 2,null);
			// Calculate the max width for the form
			int widestItem = 0;
			foreach(WindowSelectorItem item in windowSelectorItems)
			{
				if(item.Width > widestItem)widestItem=item.Width;
			}
			if (widestItem > ((int)(Screen.PrimaryScreen.Bounds.Width * 0.7)))
				widestItem = (int)(Screen.PrimaryScreen.Bounds.Width * 0.7);
			if (widestItem < MINIMUMFORMWIDTH) widestItem = MINIMUMFORMWIDTH;
			this.Width = widestItem;
			// Set the proper width on all HotKeyItemSelector controls
			foreach(WindowSelectorItem item in windowSelectorItems)
			{
				item.Width = this.Width - (2 * BORDERWIDTH);
			}
		}

		private void addWindowSelectorItem(WindowSelectorItemType itemType, int position, Window window)
		{
			// Create a selector item and set its values
			WindowSelectorItem windowSelectorItem = new WindowSelectorItem();
			this.Controls.Add(windowSelectorItem);
			windowSelectorItems.Add(windowSelectorItem);
			windowSelectorItem.Anchor = ((System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			windowSelectorItem.Location = new System.Drawing.Point(BORDERWIDTH, (position * 34) + BORDERWIDTH);
			windowSelectorItem.Type = itemType;
			if(position==0)windowSelectorItem.Active = true;
			windowSelectorItem.Window = window;
			windowSelectorItem.SpareIcon = currentKey.Icon;
			windowSelectorItem.MouseMove += new MouseEventHandler(windowSelectorItem_MouseMove);
			windowSelectorItem.Click += new EventHandler(windowSelectorItem_Click);
			windowSelectorItem.Name = new Guid().ToString() + "Button";
			if(window!=null)
				windowSelectorItem.Title = window.Title;
		}

		
		// Clean up any resources being used.
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
			// 
			// WindowSelectionForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(288, 336);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Name = "WindowSelectionForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "InstanceSelectionForm";
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InstanceSelectionForm_KeyUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.InstanceSelectionForm_Paint);
			this.Deactivate += new System.EventHandler(this.InstanceSelectionForm_Deactivate);

		}
		#endregion

		private void InstanceSelectionForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode != currentKey.Key)
			{
				formExit();
			}
		}

		private WindowSelectorItem activeItem
		{
			get
			{
				foreach(WindowSelectorItem item in windowSelectorItems)
				{
					if(item.Active)
					{
						return item;
					}
				}
				return null;
			}
			set
			{
				foreach(WindowSelectorItem item in windowSelectorItems)
				{
					if(item.Active)
					{
						item.Active = false;
						break;
					}
				}
				foreach(WindowSelectorItem item in windowSelectorItems)
				{
					if(item == value)
					{
						item.Active = true;
						break;
					}
				}
			}
		}

		private bool runningFormExit = false;
		private void formExit()
		{
			// Make sure the exit process is run only once
			if(runningFormExit)return;
			runningFormExit = true;
			// Hide the form while we perform the user selected action
			this.Visible = false;
			WindowSelectorItem activeItem = null;
			foreach(WindowSelectorItem item in windowSelectorItems)
			{
				if(item.Active)
				{
					activeItem = item;
					break;
				}
			}
			switch(activeItem.Type)
			{
				case WindowSelectorItemType.Window:
					activeItem.Window.RestoreAndBringToFront();
					break;
				case WindowSelectorItemType.CloseAll:
					foreach(Window window in windows)
					{
						window.Close();
					}
					break;
				case WindowSelectorItemType.MinimizeAll:
					foreach(Window window in windows)
					{
						window.Minimize();
					}
					break;
				case WindowSelectorItemType.RestoreAll:
					foreach(Window window in windows)
					{
						window.Restore();
					}
					break;
			}
			this.Close();
		}

		private void InstanceSelectionForm_Deactivate(object sender, System.EventArgs e)
		{
			formExit();
		}

		public void SelectNextItem()
		{
			this.Activate();
			for(int i = 0; i < windowSelectorItems.Count; i++)
			{
				if(((WindowSelectorItem)windowSelectorItems[i]).Active)
				{
					((WindowSelectorItem)windowSelectorItems[i]).Active = false;
					if(i<(windowSelectorItems.Count - 1))
					{
						((WindowSelectorItem)windowSelectorItems[i + 1]).Active = true;
						//((WindowSelectorItem)windowSelectorItems[i + 1]).Focus();
					}
					else
					{
						((WindowSelectorItem)windowSelectorItems[0]).Active = true;
						//((WindowSelectorItem)windowSelectorItems[0]).Focus();
					}
					break;
				}
			}
		}

		private void InstanceSelectionForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			// Draw edge around form....
			// (White outer top an left)
			e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight,1),0,0,this.Width,0);
			e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight,1),0,0,0,this.Height);
			// (White inner top and left)
			e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight,1),1,1,this.Width - 1,1);
			e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight,1),1,1,1,this.Height - 1);
			// (Dark outer bottom and right0
			e.Graphics.DrawLine(new Pen(SystemColors.ControlDarkDark,1),1,this.Height-1,this.Width-1,this.Height-1);
			e.Graphics.DrawLine(new Pen(SystemColors.ControlDarkDark,1),this.Width-1,this.Height-1,this.Width - 1,1);
			// (Somewhat less dark inner bottom and right)
			e.Graphics.DrawLine(new Pen(SystemColors.ControlDark,1),2,this.Height - 2,this.Width - 2 ,this.Height - 2);
			e.Graphics.DrawLine(new Pen(SystemColors.ControlDark,1),this.Width - 2,this.Height - 2 ,this.Width - 2,2);
		}

		private void windowSelectorItem_MouseMove(object sender, MouseEventArgs e)
		{
			this.activeItem = (WindowSelectorItem)sender;
		}

		private void windowSelectorItem_Click(object sender, EventArgs e)
		{
			this.activeItem = (WindowSelectorItem)sender;
			this.formExit();
		}
	}
}
