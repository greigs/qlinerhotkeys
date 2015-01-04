using System;
using System.Drawing;
using System.Collections;

namespace HotKeysLib
{
	public class HotKeyAddInManager
	{
		private static ArrayList allAddIns = null;
		private static void initAddIns()
		{
			allAddIns = new ArrayList();
			ClockAddIn clockAddIn = new ClockAddIn();
			VolumeAddIn volumeAddIn = new VolumeAddIn();
			allAddIns.Add(clockAddIn);
			allAddIns.Add(volumeAddIn);

		}

		public static ArrayList GetAllAddIns()
		{
			if(allAddIns==null)
				initAddIns();
			return allAddIns;
		}

		public HotKeyAddInManager()
		{
		}

		public static void InvokeAction(HotKeyConfiguredAddInAction Action)
		{
			foreach(IHotKeysAddIn addIn in HotKeyAddInManager.GetAllAddIns())
			{
				if(addIn.AddInID == Action.AddInID)
				{
					addIn.InvokeAction(Action);
				}
			}
		}

		public static Icon GetIcon(Guid AddInID)
		{
			foreach(IHotKeysAddIn addIn in HotKeyAddInManager.GetAllAddIns())
			{
				if(addIn.AddInID == AddInID)
				{
					return addIn.AddInIcon;
				}
			}
			return null;
		}
	}
}
