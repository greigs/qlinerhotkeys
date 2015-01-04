using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace HotKeysLib
{
	/// <summary>
	/// Summary description for IHotKeysAddIn.
	/// </summary>
	public interface IHotKeysAddIn
	{
		string Config
		{
			get;
			set;
		}

		Icon AddInIcon
		{
			get;
		}

		string AddInName
		{
			get;
		}

		Guid AddInID
		{
			get;
		}

		string Description
		{
			get;
		}

		bool HasConfig
		{
			get;
		}

		bool RequiresConfig
		{
			get;
		}

		ArrayList Actions
		{
			get;
		}

		EventHandler ConfigChanged
		{
			get;
			set;
		}

		Icon GetIcon(HotkeyIconSize size);

		DialogResult ShowAddInConfigDialog();
		
		DialogResult ShowAddInConfigDialog(IntPtr OwnerHandle);

		DialogResult ShowAddInActionConfigDialog(HotKeyAddInAction AddInAction, ref string ActionConfig);
		
		DialogResult ShowAddInActionConfigDialog(IntPtr OwnerHandle, HotKeyAddInAction AddInAction, ref string ActionConfig);

		void InvokeAction(HotKeyConfiguredAddInAction Action);
	}

}
