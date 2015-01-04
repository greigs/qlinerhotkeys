using System;

namespace HotKeysLib
{
	[Serializable]
	public struct HotKeyAddInAction
	{
		public string Name;
		public string Description;
		public bool HasConfig;
		public bool RequiresConfig;
		public Guid ID;

		public override string ToString()
		{
			return Name;
		}
	}
}
