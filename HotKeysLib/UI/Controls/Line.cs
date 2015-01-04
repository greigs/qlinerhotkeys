using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace HotKeysLib
{
	/// <summary>
	/// Summary description for Line.
	/// </summary>
	public class Line : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Line()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
			// 
			// Line
			// 
			this.Name = "Line";
			this.Size = new System.Drawing.Size(150, 2);
			this.Resize += new System.EventHandler(this.Line_Resize);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Line_Paint);

		}
		#endregion

		private void Line_Resize(object sender, System.EventArgs e)
		{
			this.Height = 2;
		}

		private void Line_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawLine(new Pen(SystemColors.ControlDark),0,0,this.Width,0);
			e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight),0,1,this.Width,1);
		}
	}
}
