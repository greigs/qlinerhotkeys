using System;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using HotKeysLib.UI.Forms;
using HotKeysLib.UI.Dialogs;

namespace HotKeysLib
{
	/// <summary>
	/// Summary description for CommandLineInterface.
	/// </summary>
	public class CommandLineInterface
	{
		private CommandLineInterface()
		{
		}

		public static string HandleCliArguments(string[] args)
		{
			if(args.Length > 0)
			{
				Hashtable arguments = parseCliArguments(args);
				// Search for the proper method
				foreach(MethodInfo methodInfo in typeof(CommandLineInterface).GetMethods())
				{
					// If the method has the same name and the same number of attributes, see if the attributes heave the same name
					ParameterInfo[] methodParameterInfo = methodInfo.GetParameters();
					if(methodInfo.Name == args[0] && arguments.Count == methodParameterInfo.Length)
					{	
						// Try to build the call stack
						object[] callStack = new object[methodParameterInfo.Length];
						int i = 0;
						bool allFound = true;
						foreach(ParameterInfo parameterInfo in methodParameterInfo)
						{
							if(!arguments.Contains(parameterInfo.Name))
							{
								allFound = false;
								break;
							}
							if(parameterInfo.ParameterType.IsEnum)
								callStack[i] = Enum.Parse(parameterInfo.ParameterType,(string)arguments[parameterInfo.Name],true);
							else
								callStack[i] = Convert.ChangeType(arguments[parameterInfo.Name],parameterInfo.ParameterType);
							i++;
						}
						if(!allFound)continue;
						// Invoke method
						return (string)methodInfo.Invoke(null,callStack);
					}	
				}
				return String.Format("Could not resolve method " + args[0] + ", with the current set of arguments.");
			}
			return "";
		}

		private static Regex argumentExpression = new Regex("([\\S]*=)([\\s\\S]*?)",RegexOptions.Compiled);
		private static Hashtable parseCliArguments(string[] args)
		{
			Hashtable result = new Hashtable();
			for(int i = 1; i < args.Length; i++)
			{
				Match match = argumentExpression.Match(args[i],0,args[i].Length);
				if(match == null)
					throw new Exception("Invalid argument format: " + args[i] + ". Required format: Argument=\"Value\"");
				int indexOfAssign = args[i].IndexOf("=");
				string propertyName = args[i].Substring(0,indexOfAssign);
				string propertyValue = args[i].Substring(indexOfAssign + 1);
				result.Add(propertyName,propertyValue);
			}
			return result;
		}

		public static void Instructions()
		{
			HotKeyHelperFunctions.ShowInstructions();
		}

		public static void About()
		{
			HotKeyHelperFunctions.ShowAbout();
		}

		public static void AddNewHotkey(string name, Keys key, string path, string arguments, string workingDirectory)
		{
			HotKeyHelperFunctions.CreateNewHotkey(name,path,key,arguments,workingDirectory);
            KeyBoardForm.GetKeyBoardForm().UpdateScreen();			
		}

		public static void AddNewHotkey(string name, Keys key, string path, string arguments, string workingDirectory, bool overwrite)
		{
			HotKeyHelperFunctions.CreateNewHotkey(name,path,key,arguments,workingDirectory,overwrite);
			KeyBoardForm.GetKeyBoardForm().UpdateScreen();			
		}

		public static void CreateCreationScript(string filename)
		{
			
			HotKeyHelperFunctions.CreateCreationScript(filename);
		}

		public static void MakeCapsLockWin()
		{
			HotKeyHelperFunctions.MakeCapsLockWin();
		}

		public static void MakeCapsLockLose()
		{
			HotKeyHelperFunctions.MakeCapsLockLose();
		}
	}
}
