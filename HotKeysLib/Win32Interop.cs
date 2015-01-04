using System;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace HotKeysLib
{
	/// <summary>
	/// Summary description for Win32Interop.
	/// </summary>
	internal class Win32Interop
	{
		private Win32Interop()
		{
		}
				
		public delegate int HookProc(int nCode, int wParam, int lParam);

		[System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
		public static extern bool BitBlt(
			IntPtr hdcDest, // handle to destination DC
			int nXDest, // x-coord of destination upper-left corner
			int nYDest, // y-coord of destination upper-left corner
			int nWidth, // width of destination rectangle
			int nHeight, // height of destination rectangle
			IntPtr hdcSrc, // handle to source DC
			int nXSrc, // x-coordinate of source upper-left corner
			int nYSrc, // y-coordinate of source upper-left corner
			System.Int32 dwRop // raster operation code
			);

		[System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
		public static extern bool PrintWindow(
			IntPtr hwnd,               // Window to copy
			IntPtr  hdcBlt,             // HDC to print into
			int nFlags              // Optional flags
			);

		[DllImport("user32.dll", EntryPoint="SetWindowsHookExA")]
		public static extern Int32 SetWindowsHookEx ( 
			Int32 idHook,
			HookProc lpfn,
			Int32 hmod,
			Int32 dwThreadId);

		[DllImport("user32.dll")]
		public static extern Int32 CallNextHookEx ( 
			Int32 hHook,
			Int32 ncode,
			Int32 wParam,
			Int32 lParam);

		[DllImport("gdi32.dll")]
		public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
		
		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr hObject);

		[DllImport("user32.dll")]
		public static extern Int16 GetKeyState ( 
			Int32 nVirtKey);

		[DllImport("user32.dll")]
		public static extern Int32 UnhookWindowsHookEx ( 
			Int32 hHook);

		[DllImport("Shell32.dll", EntryPoint="ExtractIcon")]
		public static extern int ExtractIcon(int hInst,string FileName, int nIconIndex);

		[DllImport("Shell32.dll", EntryPoint="ExtractIconEx")]
		public static extern int ExtractIconEx(string lpszFile,	int nIconIndex, int[] phiconLarge, int[] phiconSmall, int nIcons);

		[DllImport("User32.dll", EntryPoint="LoadImage")]
		public static extern int LoadImage(int hinst,string lpszName,int uType, int cxDesired, int cyDesired, int fuLoad);

		[DllImport("Kernel32.dll", EntryPoint="LoadLibrary")]
		public static extern int LoadLibrary(string lpFileName);

		[DllImport("Shell32.dll")]
		public static extern int ExtractAssociatedIcon(IntPtr hInst,string FileName,ref int nIconIndex);

		[DllImport("user32.dll", EntryPoint="RegisterHotKey")]
		public static extern int RegisterHotKey(int hwnd, int id, int fsModifiers, int vk);

		[DllImport("user32.dll", EntryPoint="UnregisterHotKey")]
		public static extern int UnregisterHotKey(int hwnd, int id);

		[DllImport("kernel32.dll", EntryPoint="GlobalAddAtom")]
		public static extern int GlobalAddAtom(string lpString);

		[DllImport("user32.dll")]
		public static extern int GetDesktopWindow();

		[DllImport("user32.dll")]
		public static extern int GetWindow(int hWnd, int uCmd);

		[DllImport("user32.dll")]
		public static extern int GetWindowLong(int hWnd, int nIndex);

		[DllImport("user32.dll")]
		public static extern int GetWindowText(int hWnd,StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll")]
		public static extern int GetClassLong(int hWnd, int nIndex);

		[DllImport("user32.dll")]
		public static extern bool IsWindowVisible(int hWnd);

		[DllImport("user32.dll")]
		public static extern int GetWindowModuleFileName(int hwnd,StringBuilder lpszFileName, int cchFileNameMax);

		[DllImport("psapi.dll")]
		public static extern int GetProcessImageFileName(
			int hProcess,
			StringBuilder lpImageFileName,
			int nSize
			);

		[DllImport("kernel32.dll")]
		public static extern int OpenProcess(
			int dwDesiredAccess,
			bool bInheritHandle,
			int dwProcessId
			);

		[DllImport("kernel32.dll")]
		public static extern int GetLongPathName(
			StringBuilder lpszShortPath,
			StringBuilder lpszLongPath,
			int cchBuffer
			);

		public const int MAX_PATH = 4096;

		[DllImport("user32.dll")]
		public static extern int GetWindowThreadProcessId(          
			int hWnd,
			ref int lpdwProcessId
			);

		[DllImport("user32.dll")]
		public static extern bool ShowWindow(int hwnd, int cmdShow);

		[StructLayout(LayoutKind.Explicit)]
			public struct RectWin32
		{
			[FieldOffset(0)] public int left;
			[FieldOffset(4)] public int top;
			[FieldOffset(8)] public int right;
			[FieldOffset(12)] public int bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
			public struct PointWin32
		{
			public int x;
			public int y;
		}
  
		[StructLayout(LayoutKind.Sequential)]
			public struct Point
		{
			public Int32 x;
			public Int32 y;

			public Point(Int32 x, Int32 y) { this.x= x; this.y= y; }
		}


		[StructLayout(LayoutKind.Sequential)]
			public struct Size 
		{
			public Int32 cx;
			public Int32 cy;

			public Size(Int32 cx, Int32 cy) { this.cx= cx; this.cy= cy; }
		}

		[StructLayout(LayoutKind.Sequential)]
			public struct WINDOWPLACEMENT
		{
			public int length; 
			public int flags; 
			public int showCmd; 
			public PointWin32 ptMinPosition; 
			public PointWin32 ptMaxPosition; 
			public RectWin32  rcNormalPosition;
		}

		[DllImport("shell32.dll")]
		public static extern IntPtr SHBrowseForFolder(ref BROWSEINFO lpbi);
		[DllImport("shell32.dll")]
		public static extern int SHGetPathFromIDList(IntPtr pidList, StringBuilder lpBuffer);

		[DllImport("user32.dll")]
		public static extern bool GetWindowPlacement(int hWnd, ref WINDOWPLACEMENT lpwndpl);
		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow(int hwnd);
		[DllImport("user32.dll")]
		public static extern int PostMessage (int hWnd, int wMsg, int wParam,int lParam);
		[DllImport("user32.dll")]
		public static extern int RegisterWindowMessage(string lpString);

		[DllImport("psapi.dll")]
		public static extern bool EnumProcessModules(
			int hProcess,
			ref IntPtr lphModule,
			int cb,
			ref int lpcbNeeded
			);

		[DllImport("psapi.dll")]
		public static extern int GetModuleFileNameEx(
			int hProcess,
			int hModule,
			StringBuilder lpImageFileName,
			int nSize
			);

		[DllImport("kernel32.dll")]
		public static extern int GetModuleFileName(
			int hModule,
			StringBuilder lpFilename,
			int nSize
			);

		[DllImport("user32.dll", EntryPoint = "SendMessageA")]
		public static extern uint SendMessage(int hWnd, int  wMsg, int wParam, int lParam);

		[DllImport("ole32.dll")]
		public static extern int CoTaskMemFree(IntPtr hMem);
		[DllImport("kernel32.dll")]
		public static extern IntPtr lstrcat(string lpString1, string lpString2);

		[DllImport("user32.dll")]
		public static extern IntPtr GetDC(IntPtr hWnd);
		[DllImport("user32.dll")]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
		[DllImport("gdi32.dll")]
		public static extern bool DeleteDC(IntPtr hdc);
		
		public const int SW_SHOWNORMAL = 1;
		public const int SW_SHOW = 5;
		public const int SW_MINIMIZE = 6;
		public const int SW_SHOWMINIMIZED = 2;
		public const int SW_RESTORE = 9;

		public const int PROCESS_QUERY_INFORMATION = 0x0400;
		public const int PROCESS_VM_READ = 0x10;

		public const int MOD_ALT = 1;
		public const int MOD_Control = 2;
		public const int MOD_SHIFT = 4;
		public const int MOD_WIN = 8;

		public const int WM_KEYDOWN = 256;
		public const int WM_HOTKEY = 0x0312;
		public const int WM_CLOSE = 0x010;

		public const int GW_CHILD = 5;
		public const int GW_HWNDNEXT = 2;
		public const int GW_OWNER = 4;

		public const int IMAGE_ICON = 1;

		public const int LR_DEFAULTCOLOR = 0x00;
		public const int LR_MONOCHROME = 0x01;
		public const int LR_COLOR = 0x02;
		public const int LR_COPYRETURNORG = 0x04;
		public const int LR_COPYDELETEORG = 0x08;
		public const int LR_LOADFROMFILE = 0x010;
		public const int LR_LOADTRANSPARENT = 0x020;
		public const int LR_DEFAULTSIZE = 0x040;
		public const int LR_LOADMAP3DCOLORS = 0x01000;
		public const int LR_CREATEDIBHeader = 0x02000;
		public const int LR_COPYFROMRESOURCE = 0x04000;
		public const int LR_SHARED = 0x08000;

		public const int HWND_BROADCAST = 0x0FFFF;

		public const int GWL_HINSTANCE = -6;

		public const int GCL_HICON = -14;

		public const int WH_KEYBOARD = 2;
		public const int VK_SHIFT = 0x010;

		[StructLayout(LayoutKind.Sequential)]
		public struct SHFILEINFO 
		{
			public IntPtr hIcon;
			public IntPtr iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		};

		public const uint SHGFI_ICON = 0x100;
		public const uint SHGFI_USEFILEATTRIBUTES = 0x10;
		public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
		public const uint SHGFI_SMALLICON = 0x1; // 'Small icon
		public const uint SHGFI_SHELLICONSIZE = 0x4;

		[StructLayout( LayoutKind.Sequential)]
		public struct BROWSEINFO 
		{
			public IntPtr hWndOwner;
			public int pIDLRoot;
			public string pszDisplayName;
			public string lpszTitle;
			public int ulFlags;
			public int lpfnCallback;
			public int lParam;
			public int iImage;
		}

		[StructLayout(LayoutKind.Sequential, Pack=1)]
			struct ARGB
		{
			public byte Blue;
			public byte Green;
			public byte Red;
			public byte Alpha;
		}


		[StructLayout(LayoutKind.Sequential, Pack=1)]
			public struct BLENDFUNCTION
		{
			public byte BlendOp;
			public byte BlendFlags;
			public byte SourceConstantAlpha;
			public byte AlphaFormat;
		}


		public const Int32 ULW_COLORKEY = 0x00000001;
		public const Int32 ULW_ALPHA    = 0x00000002;
		public const Int32 ULW_OPAQUE   = 0x00000004;

		public const byte AC_SRC_OVER  = 0x00;
		public const byte AC_SRC_ALPHA = 0x01;
		
		[DllImport("shell32.dll")]
		public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

		//Private Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Long, ByVal hWnd2 As Long, ByVal lpsz1 As String, ByVal lpsz2 As String) As Long
		[DllImport("user32.dll", EntryPoint = "FindWindowExA")]
		public static extern int FindWindowEx(int hWnd1, int hWnd2, string lpsz1 , string lpsz2);
		//Private Declare Function EnableWindow Lib "user32" (ByVal hwnd As Long, ByVal fEnable As Long) As Long
		[DllImport("user32.dll")]
		public static extern int EnableWindow(int hWnd, int fEnable);
		[DllImport("user32.dll")]
		public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);
	}
}
