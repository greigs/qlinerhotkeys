using System;
using System.Drawing;

namespace HotKeysLib
{
	/// <summary>
	/// Summary description for Window.
	/// </summary>
	public class Window
	{
		private int hwnd = 0;
		private string title = "";
		private Icon icon = null;

		public int Instance
		{
			get
			{
				return Win32Interop.GetWindowLong(this.hwnd, Win32Interop.GWL_HINSTANCE);
			}
		}

		public Icon InstanceIcon
		{
			get
			{
				IntPtr iconPtr = new IntPtr ( Win32Interop.GetClassLong(this.hwnd, Win32Interop.GCL_HICON ) );
				if(iconPtr != IntPtr.Zero)
					return Icon.FromHandle( iconPtr );
				else
					return null;
			}
		}

		public static void Close(int hwnd)
		{
			Win32Interop.PostMessage(hwnd, Win32Interop.WM_CLOSE, 0, 0);
		}

		public static void Minimize(int hwnd)
		{
			Win32Interop.ShowWindow(hwnd, Win32Interop.SW_MINIMIZE);
		}

		public static void Restore(int hwnd)
		{
			Win32Interop.ShowWindow(hwnd, Win32Interop.SW_RESTORE);
		}

		public static void RestoreAndBringToFront(int hwnd)
		{
			// Determine if the window is minimized
			Win32Interop.WINDOWPLACEMENT windowPlacement = new Win32Interop.WINDOWPLACEMENT();
			Win32Interop.GetWindowPlacement(hwnd, ref windowPlacement);
			if((windowPlacement.showCmd == Win32Interop.SW_MINIMIZE) || (windowPlacement.showCmd == Win32Interop.SW_SHOWMINIMIZED))
			{
				// If the window is minimized do a restore
				Win32Interop.ShowWindow(hwnd, Win32Interop.SW_RESTORE);
			}
			// Bring to front and activate
			Win32Interop.SetForegroundWindow(hwnd);
		}

		public Window(int newHwnd, string newTitle, Icon newIcon)
		{
			hwnd = newHwnd;
			title = newTitle;
			icon = newIcon;
		}

		public void Close()
		{
			Window.Close(this.hwnd);
		}

		public void SendMessage(int msg, IntPtr wParam, IntPtr lParam)
		{
			Win32Interop.SendMessage(this.hwnd,msg,wParam.ToInt32(), lParam.ToInt32());
		}

		public void Minimize()
		{
			Window.Minimize(this.hwnd);
		}

		public void Restore()
		{
			Window.Restore(this.hwnd);
		}

		public void RestoreAndBringToFront()
		{
			Window.RestoreAndBringToFront(this.hwnd);
		}

		public string Title
		{
			get{return title;}
		}

		public Icon Icon
		{
			get{return icon;}
		}

		public int Handle
		{
			get
			{
				return this.hwnd;
			}
		}
	}
}
