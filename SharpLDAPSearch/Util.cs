using System;
using System.Collections.Generic;
using System.Linq;
#if DEBUG
using System.Diagnostics;
#endif

namespace SharpLDAPSearch
{
	class Util
	{
		/// <summary>
		/// Gets the string argument of a cli switch, like /user:username
		/// </summary>
		/// <param name="param"></param>
		/// <param name="args"></param>
		/// <returns>gets the value after the specified switch</returns>
		public static string getArgsByName(string param, string[] args)
		{
			string retVal = string.Empty;
			foreach (string arg in args)
			{
				string temp = String.Format("/{0}:", param);
				if (arg.StartsWith(temp))
				{
					retVal = arg.Substring(temp.Length);
				}
			}
			printDebug(param, retVal);
			return retVal;
		}

		/// <summary>
		/// Returns a boolean if the specified switch is present
		/// </summary>
		/// <param name="param"></param>
		/// <param name="args"></param>
		/// <returns>true if the switched argument exists</returns>
		public static bool checkArgsByName(string param, string[] args)
		{
			string target = String.Format("/{0}", param);
			printDebug(target);
			if (args.Contains<string>(target)) { return true; }
			return false;
		}

		/// <summary>
		/// Prints values of object list for debugging purposes
		/// </summary>
		/// <param name="paramList">list of objects, e.g., new object[] { 1,2,3 };</param>
		public static void printDebug(params object[] paramList)
		{
#if DEBUG
			StackTrace st = new StackTrace();
			Console.Write("[DEBUG]\n{0}\n", st.GetFrame(1).GetMethod());
			foreach (object param in paramList)
			{
				Console.WriteLine("{0}:{1}", param.GetType(), param.ToString());
			}
#endif
		}

	}
}
