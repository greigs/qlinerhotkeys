using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace HotKeysLib
{
	internal class FileSytemItemSelector 
	{
		private FileSystemItemTypes selectType = FileSystemItemTypes.FileSystemAncestors;
		public FileSystemItemTypes SelectType 
		{
			get{return this.selectType;}
			set{this.selectType = value;}
		}

		private string title = "";
		public string Title 
		{
			get{return title;}
			set 
			{
				if(value == null)
					this.title = "";
				else
					this.title = value;
			}
		}

		private string selected = "";
		public string Selected 
		{
			get{return this.selected;}
		}

		public DialogResult ShowDialog() 
		{
			return ShowDialog(null);
		}

		public DialogResult ShowDialog(IWin32Window owner) 
		{
			IntPtr handle;
			if(owner!=null)
				handle = owner.Handle;
			else
				handle = IntPtr.Zero;
			if(displayDialog(handle)) 
				return DialogResult.OK;
			else
				return DialogResult.Cancel;
		}
	
		private bool displayDialog(IntPtr hWndOwner) 
		{
			int maxlength = 260;
			Win32Interop.BROWSEINFO browseInfo = new Win32Interop.BROWSEINFO();
			IntPtr lpIDList;
			GCHandle hTitle = GCHandle.Alloc(this.Title, GCHandleType.Pinned);
			browseInfo.hWndOwner = hWndOwner;
			browseInfo.lpszTitle =  this.Title;
			browseInfo.ulFlags  = (int)this.SelectType;
			StringBuilder buffer = new StringBuilder(maxlength);
			buffer.Length = maxlength;
			browseInfo.pszDisplayName = buffer.ToString();
			lpIDList = Win32Interop.SHBrowseForFolder(ref browseInfo);
			hTitle.Free();
			if(lpIDList.ToInt64() != 0) 
			{
				if (SelectType == FileSystemItemTypes.Computers) 
					selected = browseInfo.pszDisplayName.Trim();
				else 
				{
					StringBuilder path = new StringBuilder(maxlength);
					Win32Interop.SHGetPathFromIDList(lpIDList, path);
					selected = path.ToString();
				}
				Win32Interop.CoTaskMemFree(lpIDList);
			} 
			else
				return false;
			return true;
		}

		internal enum FileSystemItemTypes 
		{
			Computers = 0x1000,
			Directories = 0x1,
			FilesAndDirectories = 0x4000,
			FileSystemAncestors = 0x8
		}
	}
}