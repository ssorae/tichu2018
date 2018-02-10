using System;
using System.Collections.Generic;
using System.Linq;

namespace EXBoardGame.Utilities
{
	public static class ReflectionUtility
	{
		public static IEnumerable<Type> GetAllTypesOf<T>()
		{
			return from domainAssem in AppDomain.CurrentDomain.GetAssemblies()
					from assemType in domainAssem.GetTypes()
					where assemType.IsSubclassOf(typeof(T))
					select assemType;
		}
	}
}
