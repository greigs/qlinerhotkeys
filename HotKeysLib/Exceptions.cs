using System;

namespace HotKeysLib
{
	public class DuplicateHotKeyNameException : Exception
	{
		public override string Message
		{
			get
			{
				return "Duplicate HotKey name. Name must be unique. (Do not blame us but the msft shell team.)";
			}
		}
	}

	public class MissingHotKeyConfigFileException : Exception
	{
		public override string Message
		{
			get
			{
				return "The HotKey Config file does not exist.";
			}
		}
	}

	public class UnableToOpenHotKeyConfigFileException : Exception
	{
		public override string Message
		{
			get
			{
				return "Unable to open HotKey Config file";
			}
		}

	}

	public class EmptyHotKeyNameException : Exception
	{
		public override string Message
		{
			get
			{
				return "The HotKey name cannot be empty.";
			}
		}
	}
}
