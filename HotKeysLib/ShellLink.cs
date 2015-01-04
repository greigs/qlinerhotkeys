using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace HotKeysLib
{
	public class ShellLink : IDisposable
	{
		[ComImportAttribute()]
		[GuidAttribute("0000010C-0000-0000-C000-000000000046")]
		[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		private interface IPersist
		{
			[PreserveSig]
			void GetClassID(out Guid pClassID);
		}

		[ComImportAttribute()]
		[GuidAttribute("0000010B-0000-0000-C000-000000000046")]
		[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		private interface IPersistFile
		{
			[PreserveSig]
			void GetClassID(out Guid pClassID);	
			void IsDirty();	
			void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, uint dwMode);
			void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);	
			void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);
			void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
		}

		#region IShellLink Interface
		[ComImportAttribute()]
		[GuidAttribute("000214EE-0000-0000-C000-000000000046")]
		[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		private interface IShellLinkA
		{
			void GetPath([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszFile, int cchMaxPath, ref _WIN32_FIND_DATAA pfd, uint fFlags);
			void GetIDList(out IntPtr ppidl);
			void SetIDList(IntPtr pidl);
			void GetDescription([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszFile,int cchMaxName);
			void SetDescription([MarshalAs(UnmanagedType.LPStr)] string pszName);
			void GetWorkingDirectory([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszDir,int cchMaxPath);
			void SetWorkingDirectory([MarshalAs(UnmanagedType.LPStr)] string pszDir);
			void GetArguments([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszArgs, int cchMaxPath);
			void SetArguments([MarshalAs(UnmanagedType.LPStr)] string pszArgs);
			void GetHotkey(out short pwHotkey);
			void SetHotkey(short pwHotkey);
			void GetShowCmd(out uint piShowCmd);
			void SetShowCmd(uint piShowCmd);
			void GetIconLocation([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
			void SetIconLocation([MarshalAs(UnmanagedType.LPStr)] string pszIconPath, int iIcon);
			void SetRelativePath([MarshalAs(UnmanagedType.LPStr)] string pszPathRel, uint dwReserved);
			void Resolve(IntPtr hWnd, uint fFlags);
			void SetPath([MarshalAs(UnmanagedType.LPStr)] string pszFile);
		}

		[ComImportAttribute()]
		[GuidAttribute("000214F9-0000-0000-C000-000000000046")]
		[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		private interface IShellLinkW
		{
			void GetPath([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, ref _WIN32_FIND_DATAW pfd, uint fFlags);
			void GetIDList(out IntPtr ppidl);
			void SetIDList(IntPtr pidl);
			void GetDescription([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,	int cchMaxName);
			void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
			void GetWorkingDirectory([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir,	int cchMaxPath);
			void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
			void GetArguments([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
			void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
			void GetHotkey(out short pwHotkey);
			void SetHotkey(short pwHotkey);
			void GetShowCmd(out uint piShowCmd);
			void SetShowCmd(uint piShowCmd);
			void GetIconLocation([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
			void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, 	int iIcon);
			void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, uint dwReserved);
			void Resolve(IntPtr hWnd, uint fFlags);
			void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
		}

		[GuidAttribute("00021401-0000-0000-C000-000000000046")]
		[ClassInterfaceAttribute(ClassInterfaceType.None)]
		[ComImportAttribute()]
		private class CShellLink{}
	
		private enum EShellLinkGP : uint
		{
			SLGP_SHORTPATH = 1,
			SLGP_UNCPRIORITY = 2
		}

		[Flags]
		private enum EShowWindowFlags : uint
		{
			SW_HIDE = 0,
			SW_SHOWNORMAL = 1,
			SW_NORMAL = 1,
			SW_SHOWMINIMIZED = 2,
			SW_SHOWMAXIMIZED = 3,
			SW_MAXIMIZE = 3,
			SW_SHOWNOACTIVATE = 4,
			SW_SHOW = 5,
			SW_MINIMIZE = 6,
			SW_SHOWMINNOACTIVE = 7,
			SW_SHOWNA = 8,
			SW_RESTORE = 9,
			SW_SHOWDEFAULT = 10,
			SW_MAX = 10
		}

		[StructLayoutAttribute(LayoutKind.Sequential, Pack=4, Size=0, CharSet=CharSet.Unicode)]
		private struct _WIN32_FIND_DATAW
		{
			public uint dwFileAttributes;
			public _FILETIME ftCreationTime;
			public _FILETIME ftLastAccessTime;
			public _FILETIME ftLastWriteTime;
			public uint nFileSizeHigh;
			public uint nFileSizeLow;
			public uint dwReserved0;
			public uint dwReserved1;
			[MarshalAs(UnmanagedType.ByValTStr , SizeConst = 260)] // MAX_PATH
			public string cFileName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
			public string cAlternateFileName;
		}

		[StructLayoutAttribute(LayoutKind.Sequential, Pack=4, Size=0, CharSet=CharSet.Ansi)]
		private struct _WIN32_FIND_DATAA
		{
			public uint dwFileAttributes;
			public _FILETIME ftCreationTime;
			public _FILETIME ftLastAccessTime;
			public _FILETIME ftLastWriteTime;
			public uint nFileSizeHigh;
			public uint nFileSizeLow;
			public uint dwReserved0;
			public uint dwReserved1;
			[MarshalAs(UnmanagedType.ByValTStr , SizeConst = 260)] // MAX_PATH
			public string cFileName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
			public string cAlternateFileName;
		}

		[StructLayoutAttribute(LayoutKind.Sequential, Pack=4, Size=0)]
		private struct _FILETIME 
		{
			public uint dwLowDateTime;
			public uint dwHighDateTime;
		}  
		#endregion	

		[Flags]
		public enum EShellLinkResolveFlags : uint
		{
			SLR_ANY_MATCH = 0x2,
			SLR_INVOKE_MSI = 0x80,
			SLR_NOLINKINFO = 0x40,								    
			SLR_NO_UI = 0x1,
			SLR_NO_UI_WITH_MSG_PUMP = 0x101,
			SLR_NOUPDATE = 0x8,																																																																																																																																																																																																														
			SLR_NOSEARCH = 0x10,
			SLR_NOTRACK = 0x20,
			SLR_UPDATE  = 0x4
		}

		public enum LinkDisplayMode : uint
		{
			edmNormal = EShowWindowFlags.SW_NORMAL,
			edmMinimized = EShowWindowFlags.SW_SHOWMINNOACTIVE,
			edmMaximized = EShowWindowFlags.SW_MAXIMIZE
		}

		private IShellLinkW link;
		private string shortcutFile = "";

		public ShellLink()
		{
			link = (IShellLinkW)new CShellLink();
		}

		public ShellLink(string linkFile) : this()
		{
			Open(linkFile);
		}

		~ShellLink()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (link != null ) 
			{
				Marshal.ReleaseComObject(link);
				link = null;
			}
		}

		public string ShortCutFile
		{
			get{return this.shortcutFile;}
			set{this.shortcutFile = value;}
		}

		public Icon LargeIcon
		{
			get{return getIcon(true);}
		}

		public Icon SmallIcon
		{
			get{return getIcon(false);}
		}

		private Icon getIcon(bool large)
		{
			int iconIndex = 0;
			StringBuilder iconPath = new StringBuilder(260, 260);
				link.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
			string iconFile = iconPath.ToString();
			if (iconFile.Length == 0)
			{
				FileIcon.SHGetFileInfoConstants flags = FileIcon.SHGetFileInfoConstants.SHGFI_ICON |
					FileIcon.SHGetFileInfoConstants.SHGFI_ATTRIBUTES;
				if(large)
					flags = flags | FileIcon.SHGetFileInfoConstants.SHGFI_LARGEICON;
				else
					flags = flags | FileIcon.SHGetFileInfoConstants.SHGFI_SMALLICON;
				FileIcon fileIcon = new FileIcon(Target, flags);
				return fileIcon.ShellIcon;
			}
			else
			{
				IntPtr[] hIconEx = new IntPtr[1] {IntPtr.Zero};			
				int iconCount = 0;
				if(large)
					iconCount = ExtractIconEx(iconFile,iconIndex,hIconEx,null,	1);
				else
					iconCount = ExtractIconEx(iconFile,iconIndex,null,	hIconEx,1);
				Icon icon = null;
				if(hIconEx[0] != IntPtr.Zero)
					icon = Icon.FromHandle(hIconEx[0]);
				return icon;
			}				
		}

		[DllImport("Shell32", CharSet=CharSet.Auto)]
		private extern static int ExtractIconEx ([MarshalAs(UnmanagedType.LPTStr)] string lpszFile,	int nIconIndex,	IntPtr[] phIconLarge, IntPtr[] phIconSmall,	int nIcons);
		[DllImport("user32")]
		private static extern int DestroyIcon(IntPtr hIcon);

		public string IconPath
		{
			get
			{
				StringBuilder iconPath = new StringBuilder(260, 260);
				int iconIndex = 0;
				link.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
				return iconPath.ToString();
			}
			set
			{
				StringBuilder iconPath = new StringBuilder(260, 260);
				int iconIndex = 0;
				link.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
				link.SetIconLocation(value, iconIndex);
			}
		}

		public int IconIndex
		{
			get
			{
				StringBuilder iconPath = new StringBuilder(260, 260);
				int iconIndex = 0;
				link.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
				return iconIndex;
			}
			set
			{
				StringBuilder iconPath = new StringBuilder(260, 260);
				int iconIndex = 0;
				link.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
				link.SetIconLocation(iconPath.ToString(), value);
			}
		}

		public string Target
		{
			get
			{		
				StringBuilder target = new StringBuilder(260, 260);
				link.Resolve(IntPtr.Zero, (uint)(EShellLinkResolveFlags.SLR_ANY_MATCH | EShellLinkResolveFlags.SLR_INVOKE_MSI));
				IntPtr ppidl = IntPtr.Zero;
				link.GetIDList(out ppidl); 
				Win32Interop.SHGetPathFromIDList(ppidl,target);
				return target.ToString();
			}
			set{link.SetPath(value);}
		}

		public string WorkingDirectory
		{
			get
			{
				StringBuilder path = new StringBuilder(260, 260);
				link.GetWorkingDirectory(path, path.Capacity);
				return path.ToString();
			}
			set{link.SetWorkingDirectory(value);}
		}

		public string Description
		{
			get
			{
				StringBuilder description = new StringBuilder(1024, 1024);
				link.GetDescription(description, description.Capacity);
				return description.ToString();
			}
			set{link.SetDescription(value);}
		}

		public string Arguments
		{
			get
			{				
				StringBuilder arguments = new StringBuilder(260, 260);
				link.GetArguments(arguments, arguments.Capacity);
				return arguments.ToString();
			}
			set{link.SetArguments(value);}
		}

		public LinkDisplayMode DisplayMode
		{
			get
			{
				uint cmd = 0;
				link.GetShowCmd(out cmd);
				return (LinkDisplayMode)cmd;
			}
			set{link.SetShowCmd((uint)value);}
		}

		public Keys HotKey
		{
			get
			{
				short key = 0;
				link.GetHotkey(out key);
				return (Keys)key;
			}
			set{link.SetHotkey((short)value);}
		}

		public void Save()
		{
			Save(shortcutFile);
		}

		public void Save(string linkFile)
		{   
			((IPersistFile)link).Save(linkFile, true);
			shortcutFile = linkFile;
		}

		
		public void Open(string linkFile)
		{
			Open(linkFile,IntPtr.Zero,(EShellLinkResolveFlags.SLR_ANY_MATCH | EShellLinkResolveFlags.SLR_NO_UI),1);
		}
		
		public void Open(string linkFile, IntPtr hWnd,EShellLinkResolveFlags resolveFlags)
		{
			Open(linkFile,hWnd,resolveFlags,1);
		}

		public void Open(string linkFile,IntPtr hWnd,EShellLinkResolveFlags resolveFlags,ushort timeOut)
		{
			uint flags;
			if ((resolveFlags & EShellLinkResolveFlags.SLR_NO_UI) == EShellLinkResolveFlags.SLR_NO_UI)
				flags = (uint)((int)resolveFlags | (timeOut << 16));
			else
				flags = (uint)resolveFlags;
			((IPersistFile)link).Load(linkFile, 0);
			link.Resolve(hWnd, flags);
			this.shortcutFile = linkFile;
		}
	}

}
