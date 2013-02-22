using System;
using System.Security.Permissions;

namespace Fop2DD.Core
{
	[Serializable]
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	internal class InstanceProxy : MarshalByRefObject
	{
		public static bool IsFirstInstance { get; internal set; }
		public static string[] CommandLineArgs { get; internal set; }

		public void SetCommandLineArgs(bool isFirstInstance, string[] commandLineArgs)
		{
			IsFirstInstance = isFirstInstance;
			CommandLineArgs = commandLineArgs;
		}
	}

	public class InstanceCallbackEventArgs : EventArgs
	{
		internal InstanceCallbackEventArgs(bool isFirstInstance, string[] commandLineArgs)
		{
			IsFirstInstance = isFirstInstance;
			CommandLineArgs = commandLineArgs;
		}

		public bool IsFirstInstance { get; private set; }

		public string[] CommandLineArgs { get; private set; }
	}
}