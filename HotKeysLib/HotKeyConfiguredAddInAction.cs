using System;

namespace HotKeysLib
{
	[Serializable]
	public struct HotKeyConfiguredAddInAction
	{
		//		public string AddInName;
		public Guid AddInID;
		public Guid ActionID;
		public string Config;
	}
}
