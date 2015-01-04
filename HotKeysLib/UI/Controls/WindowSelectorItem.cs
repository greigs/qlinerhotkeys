using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace HotKeysLib
{
	/// <summary>
	/// Summary description for HotKeySelectorItem.
	/// </summary>
	/// 

	public enum WindowSelectorItemType
	{
		Window, MinimizeAll, RestoreAll, CloseAll
	}
	public class WindowSelectorItem : System.Windows.Forms.UserControl
	{
		public new System.Windows.Forms.Label Text;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label symbol;

		public WindowSelectorItemType itemType = WindowSelectorItemType.Window;
		public WindowSelectorItemType Type 
		{
			get
			{
				return itemType;
			}
			set
			{
				itemType = value;
				switch(itemType)
				{
					case WindowSelectorItemType.CloseAll:
						this.symbol.Text = "\x72";
						this.Title = "Close Group";
						this.symbol.Visible = true;
						break;
					case WindowSelectorItemType.MinimizeAll:
						this.symbol.Text = "\x30";
						this.Title = "Minimize Group";
						this.symbol.Visible = true;
						break;
					case WindowSelectorItemType.RestoreAll:
						this.symbol.Text = "\x32";
						this.Title = "Restore Group";
						this.symbol.Visible = true;
						break;
					case WindowSelectorItemType.Window:
						this.symbol.Text = "";
						this.symbol.Visible = false;
						break;
				}
			}
		}

		private Icon spareIcon = null;
		public Icon SpareIcon
		{
			get
			{
				return spareIcon;
			}
			set
			{
				spareIcon = value;
			}
		}

		public WindowSelectorItem()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		private Window window = null;
		public Window Window
		{
			get
			{
				return window;
			}
			set
			{
				window = value;
			}
		}

		private bool active = false;
		public bool Active
		{
			get
			{
				return active;
			}
			set
			{
				active = value;
				if(active)
				{
					this.BackColor = SystemColors.Highlight;
					this.Text.ForeColor = SystemColors.HighlightText;
					this.Text.BackColor = SystemColors.Highlight;
					this.symbol.ForeColor = SystemColors.HighlightText;
					this.symbol.BackColor = SystemColors.Highlight;
				}
				else
				{
					this.BackColor = SystemColors.Control;
					this.Text.ForeColor = SystemColors.ControlText;
					this.Text.BackColor = SystemColors.Control;
					this.symbol.ForeColor = SystemColors.ControlText;
					this.symbol.BackColor = SystemColors.Control;
				}
			}
		}

		public string Title
		{
			get
			{
				return this.Text.Text;
			}
			set
			{
				this.Text.Text = value;
				this.Text.Left = 34;
				this.Text.Top = (this.Height - this.Text.Height) / 2;
				this.Width = this.Text.Left + this.Text.Width;
			}
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Text = new System.Windows.Forms.Label();
			this.symbol = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Text
			// 
			this.Text.AutoSize = true;
			this.Text.Location = new System.Drawing.Point(56, 8);
			this.Text.Name = "Text";
			this.Text.Size = new System.Drawing.Size(26, 16);
			this.Text.TabIndex = 0;
			this.Text.Text = "Text";
			this.Text.Click += new System.EventHandler(this.symbol_Click);
			// 
			// symbol
			// 
			this.symbol.Font = new System.Drawing.Font("Marlett", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((System.Byte)(2)));
			this.symbol.Location = new System.Drawing.Point(1, 1);
			this.symbol.Name = "symbol";
			this.symbol.Size = new System.Drawing.Size(32, 32);
			this.symbol.TabIndex = 1;
			this.symbol.Text = "-";
			this.symbol.Visible = false;
			this.symbol.Click += new System.EventHandler(this.symbol_Click);
			// 
			// WindowSelectorItem
			// 
			this.Controls.Add(this.symbol);
			this.Controls.Add(this.Text);
			this.Name = "WindowSelectorItem";
			this.Size = new System.Drawing.Size(272, 34);
			this.Resize += new System.EventHandler(this.HotKeySelectorItem_Resize);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.HotKeySelectorItem_Paint);
			this.ResumeLayout(false);

		}
		#endregion

		private void HotKeySelectorItem_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if((window!=null) && (this.Type==WindowSelectorItemType.Window))
			{
				Icon instanceIcon = window.InstanceIcon;
				if(instanceIcon!=null)
					e.Graphics.DrawIcon(window.InstanceIcon, new Rectangle(1,1,33,33));	
				else
					if(this.SpareIcon!=null)
						e.Graphics.DrawIcon(this.SpareIcon, new Rectangle(1,1,33,33));	
			}
		}

		private void HotKeySelectorItem_Resize(object sender, System.EventArgs e)
		{
			this.Height = 34;
		}

		private void symbol_Click(object sender, System.EventArgs e)
		{
			this.OnClick(e);
		}
	}
}
