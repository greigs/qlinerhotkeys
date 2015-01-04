using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Reflection;

namespace HotKeysLib
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class VolumeAddIn : System.Windows.Forms.Form , IHotKeysAddIn
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public VolumeAddIn()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.FormBorderStyle = FormBorderStyle.None;

			volumeSettings = VolumeSettings.GetVolumeSettings();
			this.Location = volumeSettings.Position;
			this.setSize(volumeSettings.Size);
			this.setOpacity(volumeSettings.Opacity);
			this.foreColorBrush = new SolidBrush(this.volumeSettings.ForeColor);
            this.backColorBrush = new SolidBrush(this.volumeSettings.BackColor);
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
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.sizeMenuItem = new System.Windows.Forms.MenuItem();
			this.opacityMenuItem = new System.Windows.Forms.MenuItem();
			this.foreColorMenuItem = new System.Windows.Forms.MenuItem();
			this.backColorMenuItem = new System.Windows.Forms.MenuItem();
			this.revertToDefaultsMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.stickMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.closeMenuItem = new System.Windows.Forms.MenuItem();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			// 
			// contextMenu
			// 
			this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.sizeMenuItem,
																						this.opacityMenuItem,
																						this.foreColorMenuItem,
																						this.backColorMenuItem,
																						this.revertToDefaultsMenuItem,
																						this.menuItem6,
																						this.stickMenuItem,
																						this.menuItem8,
																						this.closeMenuItem});
			// 
			// sizeMenuItem
			// 
			this.sizeMenuItem.Index = 0;
			this.sizeMenuItem.Text = "Size";
			this.sizeMenuItem.Click += new System.EventHandler(this.sizeMenuItem_Click);
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
			// backColorMenuItem
			// 
			this.backColorMenuItem.Index = 3;
			this.backColorMenuItem.Text = "Back color";
			this.backColorMenuItem.Click += new System.EventHandler(this.backColorMenuItem_Click);
			// 
			// revertToDefaultsMenuItem
			// 
			this.revertToDefaultsMenuItem.Index = 4;
			this.revertToDefaultsMenuItem.Text = "Revert to defaults";
			this.revertToDefaultsMenuItem.Click += new System.EventHandler(this.revertToDefaultsMenuItem_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 5;
			this.menuItem6.Text = "-";
			// 
			// stickMenuItem
			// 
			this.stickMenuItem.Index = 6;
			this.stickMenuItem.Text = "Stick";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 7;
			this.menuItem8.Text = "-";
			// 
			// closeMenuItem
			// 
			this.closeMenuItem.Index = 8;
			this.closeMenuItem.Text = "Close";
			// 
			// VolumeForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)));
			this.ClientSize = new System.Drawing.Size(292, 272);
			this.ContextMenu = this.contextMenu;
			this.Name = "VolumeForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "VolumeForm";
			this.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)));
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VolumeForm_MouseDown);
			this.Load += new System.EventHandler(this.VolumeForm_Load);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VolumeForm_MouseUp);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.VolumeForm_KeyUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.VolumeForm_Paint);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VolumeForm_MouseMove);

		}
		#endregion

		private void InitializeCoordinates(Graphics grfx)
		{
			if (ClientRectangle.Width== 0 || ClientRectangle.Height == 0)
				return;

			grfx.TranslateTransform(ClientRectangle.Width/ 2, ClientRectangle.Height / 2);
			float finches = Math.Min(ClientRectangle.Width/ grfx.DpiX, ClientRectangle.Height / grfx.DpiY);

			grfx.ScaleTransform(finches * grfx.DpiX / 2000, finches * grfx.DpiY / 2000);
		}

		private void VolumeForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			DrawVolumeGraphics();
		}

		private System.Windows.Forms.ColorDialog colorDialog;

		private Point mousePositionDelta = new Point(0,0);
		private void VolumeForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
				mousePositionDelta = new Point(e.X,e.Y);	
		}

		private void VolumeForm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
			{
				Point mousePositionScreen = this.PointToScreen(new Point(e.X,e.Y));
				this.Left = mousePositionScreen.X - mousePositionDelta.X;
				this.Top = mousePositionScreen.Y - mousePositionDelta.Y;
			}
		}

		private int previousSpeakerVolume = 0;
		private int speakerVolume = 0;
		public int SpeakerVolume
		{
			get
			{
				return speakerVolume;
			}
			set
			{				
				if(value>10)
					speakerVolume=10;
				if(value<0)
					speakerVolume=0;
				if(value>=0 && value <=10)
				{
					previousSpeakerVolume = speakerVolume;
					speakerVolume=value;
				}
				if(!gbNoScrollEvent)
					GetSetControls("volume", "set", (int)((double)speakerVolume * ((double)glMasterVolumeMax / (double)SCALEMAX)));
				DrawVolumeGraphics();
			}
		}

		private bool previousOn = true;
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.Windows.Forms.MenuItem sizeMenuItem;
		private System.Windows.Forms.MenuItem opacityMenuItem;
		private System.Windows.Forms.MenuItem foreColorMenuItem;
		private System.Windows.Forms.MenuItem backColorMenuItem;
		private System.Windows.Forms.MenuItem revertToDefaultsMenuItem;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem stickMenuItem;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem closeMenuItem;
		private bool on = true;
		public bool On
		{
			get
			{
				return on;
			}
			set
			{
				previousOn = on;
				on = value;
				DrawVolumeGraphics();
				if(!gbNoClickEvent)
				{
					if(on)
						GetSetControls("mute", "set", 0);
					else
						GetSetControls("mute", "set", 1);
				}
			}
		}
		
		private void DrawVolumeGraphics()
		{
			Bitmap bitmap = new Bitmap(this.Width, this.ClientRectangle.Height);
			Graphics bitmapGrfx = Graphics.FromImage(bitmap);
			
			Graphics grfxNoInit = CreateGraphics();
			Graphics grfx = CreateGraphics();

			InitializeCoordinates(grfx);
			
			InitializeCoordinates(bitmapGrfx);
			bitmapGrfx.SmoothingMode = SmoothingMode.HighQuality;
			
			DrawSpeaker(bitmapGrfx);

			DrawVolume(bitmapGrfx,SpeakerVolume);
			
			if(!previousOn && (On != previousOn))
			{
				ClearOff(grfx);
				previousOn = On;
			}

			if(previousSpeakerVolume>SpeakerVolume)
			{
				for(int i = previousSpeakerVolume; i > SpeakerVolume; i--)
				{
					ClearVolume(grfx,i, false);
				}
			}
			
			previousSpeakerVolume = SpeakerVolume;
			
			grfxNoInit.DrawImage(bitmap,0,0,this.Width,this.ClientRectangle.Height);

			bitmap.Dispose();
			grfxNoInit.Dispose();
			bitmapGrfx.Dispose();
			grfx.Dispose();
		}

		private void ClearOff(Graphics grfx)
		{
			grfx.FillRectangle(new SolidBrush(this.TransparencyKey),-680 + horizontalCorrection -800,-420,300, 167);
			grfx.FillRectangle(new SolidBrush(this.TransparencyKey),-680 + horizontalCorrection - 800,250,300, 167);			
			grfx.FillRectangle(new SolidBrush(this.TransparencyKey),-749  + horizontalCorrection,-410,110, 220);
			grfx.FillRectangle(new SolidBrush(this.TransparencyKey),-749  + horizontalCorrection,190,110, 220);			
		}

		private void ClearVolume(Graphics grfx,int volume,bool all)
		{
			SolidBrush brush = new SolidBrush(this.TransparencyKey);
			
			if(all)
			{
				grfx.FillRectangle(brush,-720 + horizontalCorrection ,-650,2000,1300);
				return;
			}
			int i = volume;
			grfx.FillRectangle(brush,-900 + horizontalCorrection + (i * 200),
				-200 - (i*50),160 /*+ (i * 20)*/ ,400 + (i * 100));
			if(i>= 2 && i<=7)
			{
				//Upper square (upmost)
				grfx.FillRectangle(brush,-910 + horizontalCorrection + (i * 190),
					-210 - (i*50),160 ,100 + (i * 30));
				//lower square (lowest)
				grfx.FillRectangle(brush,-910 + horizontalCorrection + (i * 190),
					-80 + (i*50),160 ,100 + (i * 30));
			}
			if(i> 7)
			{
				//Upper square (upmost)
				grfx.FillRectangle(brush,-915 + horizontalCorrection + (i * 190),
					-220 - (i*50),160 , (i * 30));
				//Second upper square (second upmost)
				grfx.FillRectangle(brush,-865 + horizontalCorrection + (i * 190),
					-100 - (i*49),160 , (i * 30));
				//Lower square (lowest)
				grfx.FillRectangle(brush,-920 + horizontalCorrection + (i * 190),
					-50 + (i*50),160 , (i * 30));
				//Lower square (second lowest)
				grfx.FillRectangle(brush,-855 + horizontalCorrection + (i * 190),
					-180 + (i*50),160 , (i * 30));
			}
		}
        
		private int horizontalCorrection = 100;

		private int DrawSpeaker(Graphics grfx)
		{
			// Black
			GraphicsPath pathTriangleBlack = new GraphicsPath();
			pathTriangleBlack.AddLine(-1325 + horizontalCorrection,0,-750  + horizontalCorrection,-575);
			pathTriangleBlack.AddLine(-750  + horizontalCorrection,-575,-750 + horizontalCorrection,575);
			pathTriangleBlack.AddLine(-750 + horizontalCorrection,575,-1325 + horizontalCorrection,0);
			grfx.FillRectangle(foreColorBrush, -1500 + horizontalCorrection,-250,500,500);
			grfx.FillPath(foreColorBrush,pathTriangleBlack);
			// White
			GraphicsPath pathTriangleWhite = new GraphicsPath();
			pathTriangleWhite.AddLine(-1250 + horizontalCorrection,0,-800 + horizontalCorrection,-450);
			pathTriangleWhite.AddLine(-800 + horizontalCorrection,-550,-800 + horizontalCorrection,450);
			pathTriangleWhite.AddLine(-800 + horizontalCorrection,450,-1250 + horizontalCorrection,0);
			grfx.FillRectangle(backColorBrush, -1450 + horizontalCorrection,-200,400,400);
			grfx.FillPath(backColorBrush,pathTriangleWhite);
			// Draw Off
			if(!On)
				DrawOff(grfx);
			// return the utmost left point of what we just draw (for centering)
			return -1325 + horizontalCorrection;
			
		}

		private int DrawOff(Graphics grfx)
		{
			Pen penBlack = new Pen(foreColorBrush, 150);
			grfx.DrawLine(penBlack,-600 + horizontalCorrection - 800,-350,100  + horizontalCorrection - 800,350);
			grfx.DrawLine(penBlack,-600 + horizontalCorrection - 800,350,100  + horizontalCorrection - 800,-350);
			Pen penWhite = new Pen(backColorBrush, 50);
			grfx.DrawLine(penWhite,-570 + horizontalCorrection - 800,-320,70 + horizontalCorrection - 800,320);
			grfx.DrawLine(penWhite,-570 + horizontalCorrection - 800,320,70 + horizontalCorrection - 800,-320);

			// return the utmost right point of what we just draw (for centering)
			return 100 + horizontalCorrection - 800;
		}

		private int DrawVolume(Graphics grfx, int volume)
		{
			grfx.SmoothingMode = SmoothingMode.HighQuality;
			Pen penBlack = new Pen(foreColorBrush, 110);
			Pen penWhite = new Pen(backColorBrush, 50);

			penBlack.StartCap = LineCap.Round;
			penBlack.EndCap = LineCap.Round;
			penWhite.StartCap = LineCap.Round;
			penWhite.EndCap = LineCap.Round;

			if(volume==0)return 0;

			for(int i=0;i<volume;i++)
			{
				Rectangle rect = new Rectangle(-1100 + horizontalCorrection ,-250 - (i* 100),500 + (i*200),500 + (i*200));
				grfx.DrawArc(penBlack,rect,-30,60);
				grfx.DrawArc(penWhite,rect,-29,58);
			}

			// return the utmost right point of what we just draw (for centering)
			return -1100 + 500 + ((volume - 1)*200) + horizontalCorrection;
		}

		private void setSize(int percentage)
		{
			int screenWidth = Screen.PrimaryScreen.Bounds.Width;	
			int screenHeight = Screen.PrimaryScreen.Bounds.Height;
			double volumeWidth = screenWidth;
			volumeWidth = (volumeWidth / 100) * percentage;
			double volumeHeight = volumeWidth * 0.6;
			this.Size = new Size((int)volumeWidth, (int)volumeHeight);
		}

		private void setOpacity(int percentage)
		{
			this.Opacity = ((double)percentage) / 100;
		}

		public void OpacityMenuItemEventHandler(object sender, EventArgs e)
		{
			volumeSettings.Opacity = (((MenuItem)sender).Text.StartsWith("100")) ? 100 : Convert.ToInt32(((MenuItem)sender).Text.Substring(0,2));
			this.ClearChecksOnAllChildren(((MenuItem)sender).Parent);
			((MenuItem)sender).Checked = true;
			volumeSettings.PersistVolumeSettings();
			this.setOpacity(volumeSettings.Opacity);
		}

		private void ClearChecksOnAllChildren(Menu menu)
		{
			foreach(MenuItem menuItem in menu.MenuItems)
			{
				menuItem.Checked = false;
			}
		}

		public void SizeMenuItemEventHandler(object sender, EventArgs e)
		{
			volumeSettings.Size = (((MenuItem)sender).Text.StartsWith("100")) ? 100 : Convert.ToInt32(((MenuItem)sender).Text.Substring(0,2));
			this.ClearChecksOnAllChildren(((MenuItem)sender).Parent);
			((MenuItem)sender).Checked = true;
			volumeSettings.PersistVolumeSettings();
			this.setSize(volumeSettings.Size);
		}

		private VolumeSettings volumeSettings = null;

		private void VolumeForm_Load(object sender, System.EventArgs e)
		{
			//Width= Screen.FromControl(this).WorkingArea.Width;
			//this.Height = (int)(Screen.FromControl(this).WorkingArea.Height * 0.6);
			//this.Left = Screen.FromControl(this).WorkingArea.Width;
			//this.Top = (int)(Screen.FromControl(this).WorkingArea.Height * 0.2);

			// Size menu items
			for(int i = 10; i <= 100; i+=10)
			{
				MenuItem menuItem = new MenuItem(i.ToString() + "% of screen");
				menuItem.Click += new EventHandler(this.SizeMenuItemEventHandler);
				menuItem.RadioCheck = true;
				if(this.volumeSettings.Size==i)
					menuItem.Checked = true;
				this.sizeMenuItem.MenuItems.Add(menuItem);
			}
			// Opacity menu items
			for(int i = 10; i <= 100; i+=10)
			{
				MenuItem menuItem = new MenuItem(i.ToString() + "%");
				menuItem.Click += new EventHandler(this.OpacityMenuItemEventHandler);
				menuItem.RadioCheck = true;
				if(this.volumeSettings.Opacity==i)
					menuItem.Checked = true;
				this.opacityMenuItem.MenuItems.Add(menuItem);
			}
			

			GetControlIDs();
			int lvalue = GetSetControls("volume", "get",0);
			this.SpeakerVolume = GetScaledValue(lvalue,glMasterVolumeMax,SCALEMAX);
			lvalue = GetSetControls("mute", "get",0);
			this.On = (lvalue==0);
		}

		private void VolumeForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
//			if(e.KeyCode == Keys.Up)
//				this.SpeakerVolume++;
//			if(e.KeyCode == Keys.Down)
//				this.SpeakerVolume--;
//			if(e.KeyCode == Keys.O)
//				On = (!On);
//			MessageBeep(-1);
			if(e.KeyCode==Keys.LWin || e.KeyCode==Keys.RWin)
				this.Visible = false;
		}

		public int giMasterVolumeID = 0;
		public int glMasterVolumeMin = 0;
		public int glMasterVolumeMax = 0;
		public int giMasterMuteID = 0;
		public bool gbNoScrollEvent = false;
		public bool gbNoClickEvent = false;
		public const int SCALEMAX = 10;

		[StructLayout(LayoutKind.Sequential)]
			public struct MIXERCONTROLDETAILS
		{
			public int cbStruct; //size in Byte of MIXERCONTROLDETAILS
			public int dwControlID; //control id to get/set details on
			public int cChannels; //number of channels in paDetails array
			public int item; //hwndOwner or cMultipleItems
			public int cbDetails; //size of _one_ details_XX struct
			public int paDetails; //pointer to array of details_XX structs			
		}

		[StructLayout(LayoutKind.Sequential)]
			public class MIXERCONTROLDETAILS_BOOLEAN
		{
			public int fValue;
		}
		
		[StructLayout(LayoutKind.Sequential)]
			public class MIXERCONTROLDETAILS_UNSIGNED
		{
			public int dwValue;
		}

		[StructLayout(LayoutKind.Sequential)]
			public struct MIXERCONTROLDETAILS_SIGNED
		{
			public int lValue;
		}

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
			public struct MIXERCAPS
		{
			public Int16 wMid;
			public Int16 wPid;
			public int vDriverVersion;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)] public String szPname;
			public int fdwSupport;
			public int cDestinations;
		}

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
			public struct MIXERLINE
		{
			public int cbStruct;
			public int dwDestination;
			public int dwSource;
			public int dwLineID;
			public int fdwLine;
			public int dwUser;
			public int dwComponentType;
			public int cChannels;
			public int cConnections;
			public int cControls;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=16)] public String szShortName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=64)] public String szName; 
			public Target lpTarget;
		}

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
			public struct Target
		{
			public int dwType;
			public int dwDeviceID;
			public Int16 wMid;
			public Int16 wPid; 
			public int vDriverVersion;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)] public String szPname;
		}

		[StructLayout(LayoutKind.Sequential)]
			public struct MIXERLINECONTROLS
		{
			public int cbStruct;
			public int dwLineID;
			public int dwControl;
			public int cControls;
			public int cbmxctrl;
			public int pamxctrl;
		}

		[StructLayout(LayoutKind.Sequential)]
			public class MIXERCONTROL
		{
			public int cbStruct;
			public int dwControlID;
			public int dwControlType;
			public int fdwControl;
			public int cMultipleItems;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]public byte[] szShortName;// Size = MIXER_SHORT_NAME_CHARS
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]public byte[] szName; // Size = MIXER_LONG_NAME_CHARS)
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=6)]public int[] Bounds; //(1 To 6) As Long
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=6)]public int [] Metrics; //(1 To 6) As Long
		}

		[DllImport("winmm.dll")]
		public static extern int mixerOpen(ref int phmx,int uMxId,int dwCallback,int dwInstance,int fdwOpen);
		[DllImport("winmm.dll")]
		public static extern int mixerGetControlDetailsA(int hmxobj,ref MIXERCONTROLDETAILS pmxcd,int fdwDetails);
		[DllImport("winmm.dll")]
		public static extern int mixerSetControlDetails(int hmxobj, ref MIXERCONTROLDETAILS pmxcd, int fdwDetails);
		[DllImport("winmm.dll")]
		public static extern int mixerClose(int hmx);
		[DllImport("winmm.dll")]
		public static extern int mixerGetDevCapsA(int uMxId,ref MIXERCAPS pmxcaps,int cbmxcaps);
		[DllImport("winmm.dll")]
		public static extern int mixerGetLineInfoA(int hmxobj,ref MIXERLINE pmxl,int fdwInfo);
		[DllImport("winmm.dll")]
		public static extern int mixerGetLineControlsA (int hmxobj,ref MIXERLINECONTROLS pmxlc,int fdwControls);
		[DllImport("user32.dll")]
		public static extern bool MessageBeep(int uType);


		public const int MM_MIXM_LINE_CHANGE = 0x3D0;          // mixer line change notify
		public const int MM_MIXM_CONTROL_CHANGE = 0x3D1;       //mixer control change notify
		public const int MIXER_GETLINEINFOF_DESTINATION = 0;
		public const int MIXER_GETCONTROLDETAILSF_VALUE = 0;
		public const int MIXER_SETCONTROLDETAILSF_VALUE = 0;
		public const int MIXER_GETLINECONTROLSF_ALL = 0;
		public const int CALLBACK_WINDOW = 0x10000;
		public const int MIXERLINE_COMPONENTTYPE_DST_SPEAKERS = 4;
		public const int MIXERCONTROL_CT_CLASS_FADER = 0x50000000;
		public const int MIXERCONTROL_CT_UNITS_UNSIGNED = 0x30000;
		public const int MIXERCONTROL_CT_UNITS_BOOLEAN = 0x10000;
		public const int MIXERCONTROL_CT_SC_SWITCH_BOOLEAN = 0;
		public const int MIXERCONTROL_CT_CLASS_SWITCH = 0x20000000;
		public const int MIXERCONTROL_CONTROLTYPE_FADER = (MIXERCONTROL_CT_CLASS_FADER | MIXERCONTROL_CT_UNITS_UNSIGNED);
		public const int MIXERCONTROL_CONTROLTYPE_VOLUME = (MIXERCONTROL_CONTROLTYPE_FADER + 1);
		public const int MIXERCONTROL_CONTROLTYPE_MUTE = (MIXERCONTROL_CONTROLTYPE_BOOLEAN + 2);
		public const int MIXERCONTROL_CONTROLTYPE_BOOLEAN = (MIXERCONTROL_CT_CLASS_SWITCH | MIXERCONTROL_CT_SC_SWITCH_BOOLEAN | MIXERCONTROL_CT_UNITS_BOOLEAN);

		private int GetControlIDs()
		{
			// Return master volume control ID in ggiMasterVolumeID (return -1 if error)
			// Return master mute control ID in ggiMasterVolumeID (return -1 if error)
			// (uMixerLine should be Speaker Line)
			
			int iDest =0 ;
			//string sName = "";
			int hmx = 0;
			MIXERLINE uSpeakerLine = new MIXERLINE();
			MIXERLINECONTROLS uMixerLineControls = new MIXERLINECONTROLS();
			MIXERCONTROL uMixerControl0 = new MIXERCONTROL();
			
			giMasterVolumeID = -1;    
			giMasterMuteID = -1;      
			
			// Open mixer 0
			MIXERCAPS uMixerCaps = new MIXERCAPS();
			mixerOpen(ref hmx, 0, this.Handle.ToInt32(), 0, CALLBACK_WINDOW);
			// Get mixer capabilities
			mixerGetDevCapsA(0,ref uMixerCaps, Marshal.SizeOf(uMixerCaps));
			// Loop through destination lines, looking for speaker line
			MIXERLINE uMixerLine = new MIXERLINE();
			for(iDest=0;iDest<uMixerCaps.cDestinations;iDest++)
			{
				// Initialize MixerLine structure
				uMixerLine = new MIXERLINE();
				uMixerLine.cbStruct = Marshal.SizeOf(uMixerLine);
				uMixerLine.dwDestination = iDest;
				uMixerLine.dwSource = 0;
				// Call mixerGetLineInfo with DESTINATION flag
				mixerGetLineInfoA(hmx,ref uMixerLine, MIXER_GETLINEINFOF_DESTINATION);
				// Save the mixer line for the speakers
				if(uMixerLine.dwComponentType == MIXERLINE_COMPONENTTYPE_DST_SPEAKERS)
				{
					uSpeakerLine = uMixerLine;
					break;
				}
			}
			
			// Now we have speaker line. Check its controls.
			// Initialize MIXERLINECONTROLS structure
			uMixerLineControls.cbStruct = Marshal.SizeOf(uMixerLineControls);
			uMixerLineControls.dwLineID = uMixerLine.dwLineID;
			uMixerLineControls.dwControl = 0;
			uMixerLineControls.cControls = uMixerLine.cControls;
			uMixerLineControls.cbmxctrl = Marshal.SizeOf(uMixerControl0);
			// Dimension MIXERCONTROL array to proper size
			MIXERCONTROL[] uMixerControl = new MIXERCONTROL[uMixerLine.cControls];
			// Create buffer for unmanaged memory
			IntPtr buffer = Marshal.AllocCoTaskMem( Marshal.SizeOf(uMixerControl0) * uMixerLine.cControls);
			uMixerLineControls.pamxctrl = buffer.ToInt32();
			// Get speaker line controls
			mixerGetLineControlsA(hmx,ref uMixerLineControls, MIXER_GETLINECONTROLSF_ALL);
			// Get the data from the buffer
			for(int i = 0; i < uMixerLineControls.cControls; i++)
			{
				uMixerControl[i] = new MIXERCONTROL();
				uMixerControl[i].Bounds = new int[6];
				uMixerControl[i].Metrics  = new int[6];
				uMixerControl[i].szName = new byte[64];
				uMixerControl[i].szShortName = new byte[16];
				Marshal.PtrToStructure(new IntPtr((buffer.ToInt32() + (i * Marshal.SizeOf(uMixerControl[0])))),uMixerControl[i] );
			}
			// Free unmanaged memory 
			Marshal.FreeCoTaskMem( buffer );
			// Loop through controls, looking for master volume and master mute
			for(int i = 0; i < uMixerLineControls.cControls; i++)
			{
				// Check control type for VOLUME
				if(uMixerControl[i].dwControlType == MIXERCONTROL_CONTROLTYPE_VOLUME)
				{
//					// Check control name for word "Master"
//					if(Encoding.ASCII.GetString(uMixerControl[i].szName).ToLower().IndexOf("master")>=0)
//					{
						giMasterVolumeID = uMixerControl[i].dwControlID;
						glMasterVolumeMin = uMixerControl[i].Bounds[0];
						glMasterVolumeMax = uMixerControl[i].Bounds[1];
						//dMasterVolumeScaleFactor = (double)SliVol.Maximum / (double)glMasterVolumeMax;
//					}     
				}
				// Check control type for MUTE
				if(uMixerControl[i].dwControlType == MIXERCONTROL_CONTROLTYPE_MUTE)
				{
//					if(Encoding.ASCII.GetString(uMixerControl[i].szName).ToLower().IndexOf("master")>=0)
//					{
						giMasterMuteID = uMixerControl[i].dwControlID;
//					}
				}  
			}
			return 0;
		}

		public int GetSetControls(string sControl, string sAction, int lValue)
		{
			//	' Get or set the value of a control (volume or mute) given its ID
			//	' Returns a long in range [0,65535] for volume,
			//	'   an integer in {0,1} for mute, or -1 for error
			int hmx = 0;
			MIXERCONTROLDETAILS uDetails = new MIXERCONTROLDETAILS();
			MIXERCONTROLDETAILS_UNSIGNED uUnsigned0 = new MIXERCONTROLDETAILS_UNSIGNED();;
			MIXERCONTROLDETAILS_BOOLEAN uBoolean0 = new MIXERCONTROLDETAILS_BOOLEAN();
			//	Open mixer 0 to get a mixer handle
			mixerOpen(ref hmx, 0, 0, 0, 0);
			//	Fill in common values for uDetails
			uDetails.cbStruct = Marshal.SizeOf(uDetails);
			uDetails.cChannels = 1;
			uDetails.item = 0;
			if(sControl == "volume" && giMasterVolumeID != -1)
			{
				uDetails.dwControlID = giMasterVolumeID;
				uDetails.cbDetails = Marshal.SizeOf(uUnsigned0);
				// Create buffer for unmanaged memory
				IntPtr buffer = Marshal.AllocCoTaskMem( Marshal.SizeOf(new MIXERCONTROLDETAILS_SIGNED()) * uDetails.cChannels);
				uDetails.paDetails = buffer.ToInt32();
				if(sAction.ToLower() == "get")
				{
					//	Call get details
					mixerGetControlDetailsA(hmx, ref uDetails, MIXER_GETCONTROLDETAILSF_VALUE);
					MIXERCONTROLDETAILS_UNSIGNED mcu = new MIXERCONTROLDETAILS_UNSIGNED();
					Marshal.PtrToStructure(buffer,mcu);
					return mcu.dwValue;
				}
				if(sAction.ToLower() == "set")
				{
					//	Validate using control bounds and set
					if(lValue>=glMasterVolumeMin && lValue<=glMasterVolumeMax)
					{
						MIXERCONTROLDETAILS_UNSIGNED mcu = new MIXERCONTROLDETAILS_UNSIGNED();
						mcu.dwValue = lValue;
						Marshal.StructureToPtr(mcu,buffer,true);
						uDetails.paDetails = buffer.ToInt32();
						mixerSetControlDetails(hmx,ref uDetails, MIXER_SETCONTROLDETAILSF_VALUE);
					}
				}

				Marshal.FreeCoTaskMem(buffer);
			}
			
			if(sControl== "mute" && giMasterMuteID!= -1)
			{
				uDetails.dwControlID = giMasterMuteID;
				uDetails.cbDetails = Marshal.SizeOf(uBoolean0);
				// Create buffer for unmanaged memory
				IntPtr buffer = Marshal.AllocCoTaskMem( Marshal.SizeOf(new MIXERCONTROLDETAILS_SIGNED()) * uDetails.cChannels);
				uDetails.paDetails = buffer.ToInt32();
				if(sAction.ToLower() == "get")
				{
					//	Call get details
					mixerGetControlDetailsA(hmx,ref uDetails, MIXER_GETCONTROLDETAILSF_VALUE);
					MIXERCONTROLDETAILS_BOOLEAN mcb = new MIXERCONTROLDETAILS_BOOLEAN();
					Marshal.PtrToStructure(buffer,mcb);
					return mcb.fValue;
				}
				if(sAction.ToLower() == "set")
				{
					//	' Validate and set
					if(lValue == 0 || lValue == 1)
					{
						MIXERCONTROLDETAILS_BOOLEAN mcb = new MIXERCONTROLDETAILS_BOOLEAN();
						mcb.fValue = lValue;
						Marshal.StructureToPtr(mcb,buffer,true);
						uDetails.paDetails = buffer.ToInt32();
						mixerSetControlDetails(hmx,ref uDetails, MIXER_SETCONTROLDETAILSF_VALUE);
					}
				}
				Marshal.FreeCoTaskMem(buffer);
			}
			mixerClose(hmx);
			return 0;
		}

		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			int lNewVol = 0;
			int iVolume = 0;
			int lNewMute = 0;

			if(m.Msg == MM_MIXM_CONTROL_CHANGE || m.Msg == MM_MIXM_LINE_CHANGE)
			{
				// Volume
				lNewVol = GetSetControls("volume", "get", 0);
				// Set control
				//iVolume = (int)(lNewVol * dMasterVolumeScaleFactor);
				iVolume = GetScaledValue(lNewVol,glMasterVolumeMax,SCALEMAX); //(int)(((double)lNewVol / (double)(glMasterVolumeMax)) * (double)SCALEMAX);
				if (iVolume >= 0 && iVolume <= SCALEMAX)
				{	
					// Turn off scroll event and move scrollbar
					gbNoScrollEvent = true;
					this.speakerVolume = iVolume;
					gbNoScrollEvent = false;
				}
				// Mute
				lNewMute = GetSetControls("mute", "get", 0);
				// Set control
				if(lNewMute == 0 || lNewMute == 1)
				{
					// Turn off click event and move scrollbar
					gbNoClickEvent = true;
					this.On = (lNewMute==0);
					gbNoClickEvent = false;
				}
				//if(this.Left!=0)this.Left=0;
			}
			base.WndProc(ref m);
		}

		private int GetScaledValue(int Value, int MaximumValue, int MaximumScale)
		{
			if(Value<=0)
				return 0;
			if(Value>=MaximumValue)
				return MaximumScale;
			double localValue = (double)Value;
			double evalValue = (double)MaximumValue / (double)MaximumScale;
			for(int i = 0; i < MaximumScale; i++)
			{
				if(localValue>(i*evalValue) && localValue<=((i+1)*evalValue))
					return i+1;
			}
			return MaximumScale;
		}

		SolidBrush foreColorBrush = null;
		SolidBrush backColorBrush = null;
		private void foreColorMenuItem_Click(object sender, System.EventArgs e)
		{
			this.colorDialog.Color = this.volumeSettings.ForeColor;
			if(this.colorDialog.ShowDialog(this)==DialogResult.OK)
			{
				this.foreColorBrush = new SolidBrush(colorDialog.Color);
				this.Refresh();
				this.volumeSettings.ForeColor = colorDialog.Color;
				volumeSettings.PersistVolumeSettings();
			}
		}

		private void VolumeForm_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
			{
				if(volumeSettings.Position!=this.Location)
				{
					volumeSettings.Position=this.Location;
					volumeSettings.PersistVolumeSettings();
				}
			}
		}

		private void sizeMenuItem_Click(object sender, System.EventArgs e)
		{
		
		}

		private void backColorMenuItem_Click(object sender, System.EventArgs e)
		{
			this.colorDialog.Color = this.volumeSettings.BackColor;
			if(this.colorDialog.ShowDialog(this)==DialogResult.OK)
			{
				this.backColorBrush = new SolidBrush(colorDialog.Color);
				this.Refresh();
				this.volumeSettings.BackColor = colorDialog.Color;
				volumeSettings.PersistVolumeSettings();
			}
		}

		private void revertToDefaultsMenuItem_Click(object sender, System.EventArgs e)
		{
			this.volumeSettings = new VolumeSettings();
			this.volumeSettings.PersistVolumeSettings();
			
			this.Location = volumeSettings.Position;
			this.setSize(volumeSettings.Size);
			this.setOpacity(volumeSettings.Opacity);
			this.foreColorBrush = new SolidBrush(this.volumeSettings.ForeColor);
			this.backColorBrush = new SolidBrush(this.volumeSettings.BackColor);

			this.Refresh();
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
				return "Volume control";
			}
		}

		public string Description
		{
			get
			{
				return "Provides easy access to the main volume control.";
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

		private ArrayList actions = null;
		public ArrayList Actions
		{
			get
			{
				if(actions!=null)
					return actions;
				HotKeyAddInAction volumeUp = new HotKeyAddInAction();
				volumeUp.Name = "Volume up";
				volumeUp.Description = "Turn up the sound volume";
				volumeUp.HasConfig = false;
				volumeUp.RequiresConfig = false;
				volumeUp.ID = VOLUPACTIONID;

				HotKeyAddInAction volumeDown = new HotKeyAddInAction();
				volumeDown.Name = "Volume down";
				volumeDown.Description = "Turn down the sound volume";
				volumeDown.HasConfig = false;
				volumeDown.RequiresConfig = false;
				volumeDown.ID = VOLDOWNACTIONID;

				HotKeyAddInAction volumeToggle = new HotKeyAddInAction();
				volumeToggle.Name = "Volume on/off";
				volumeToggle.Description = "Turn the sound on/off";
				volumeToggle.HasConfig = false;
				volumeToggle.RequiresConfig = false;
				volumeToggle.ID = VOLTOGGLEACTIONID;

				actions = new ArrayList();
				actions.Add(volumeUp);
				actions.Add(volumeDown);
				actions.Add(volumeToggle);
				return actions;
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
			this.invoke(Action);
		}

		// {8B9C74B0-DAAC-4614-9BDD-2339D5DBC582}
		public readonly static Guid VOLUPACTIONID = new Guid( "{8B9C74B0-DAAC-4614-9BDD-2339D5DBC582}" );
		// {1A239466-E81D-4acf-87FF-549D4A102A72}
		public readonly static Guid VOLDOWNACTIONID = new Guid( "{1A239466-E81D-4acf-87FF-549D4A102A72}" );
		// {227A5AB5-7CF4-4272-9F08-626A77693CF7}
		public readonly static Guid VOLTOGGLEACTIONID = new Guid( "{227A5AB5-7CF4-4272-9F08-626A77693CF7}" );


		private void invoke(HotKeyConfiguredAddInAction action)
		{
			if(!this.Visible)
			{
				this.Show();
				Window.RestoreAndBringToFront(this.Handle.ToInt32());
			}
			if(action.ActionID == VOLUPACTIONID)
				this.SpeakerVolume++;
			if(action.ActionID == VOLDOWNACTIONID)
				this.SpeakerVolume--;
			if(action.ActionID == VOLTOGGLEACTIONID)
				On = (!On);
			MessageBeep(-1);
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
			StreamReader streamReader = new StreamReader(Assembly.GetAssembly(typeof(VolumeAddIn)).GetManifestResourceStream("HotKeysLib.VolumeAddIn.Volume.ico"));
			return new Icon(streamReader.BaseStream,intSize,intSize);
		}
        
		public readonly static Guid ADDINID = new Guid( "{7999D6B2-B68E-48f6-8826-9C52E08578D8}" );
		public Guid AddInID
		{
			get
			{
				return ADDINID;
			}
		}

		#endregion

		public override string ToString()
		{
			return "Volume control";
		}

	}

	[Serializable]
	public class VolumeSettings
	{
		public VolumeSettings()
		{
			int screenWidth = Screen.PrimaryScreen.Bounds.Width;	
			int screenHeight = Screen.PrimaryScreen.Bounds.Height;
			double volumeWidth = screenWidth;
			volumeWidth = (volumeWidth / 100) * Size;
			double volumeHeight = volumeWidth * 0.6;
			Position = new Point(((screenWidth - (int)volumeWidth)/2) , ((screenHeight - (int)volumeHeight)/2));
		}

		public Color ForeColor = Color.Black;
		public Color BackColor = Color.White;
		public Point Position = new Point(0,0);
		public bool Stick = false;

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

		public int Size = 70;
		public int Opacity = 80;

		public static VolumeSettings GetVolumeSettings()
		{
			FileStream stream = null;
			VolumeSettings settings = null;
			try
			{
				stream = new FileStream(HotKeyHelperFunctions.ApplicationDataPath + "\\volumesettings.xml",FileMode.Open);
				settings = (VolumeSettings)(new SoapFormatter().Deserialize(stream));
			}
			catch{}
			finally{if(stream!=null)stream.Close();}
			if(settings==null)
				settings = new VolumeSettings();
			return settings;
		}

		public void PersistVolumeSettings()
		{
			FileStream stream = new FileStream(HotKeyHelperFunctions.ApplicationDataPath + "\\volumesettings.xml",FileMode.Create);
			new SoapFormatter().Serialize(stream,this);
			stream.Close();
		}
	}
}
