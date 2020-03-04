using System;
using System.Collections.Generic;
using System.Linq;

namespace util.test
{
	public class TestUtil
	{
		readonly Type CheckType;
		public TestUtil(Type type)
		{
			this.CheckType = type;
		}
		/// <summary>
		/// Check if type has a default constructor
		/// </summary>
		/// <returns></returns>
		public bool CheckDefaultConstructor()
		{ 
			return this.CheckType.GetConstructor(new Type[] { })?.Invoke(new object[] { }) != null;
		}
		/// <summary>
		/// Check if class has properties
		/// </summary>
		/// <returns></returns>
		public bool CheckHasProps()
		{
			return this.CheckType.GetProperties().Any();
		}
		/// <summary>
		/// Check if type has methods
		/// </summary>
		/// <returns></returns>
		public bool CheckNameProps()
		{
			var aFirstLetters = this.CheckType.GetProperties().Select(x => x.Name.First().ToString()).ToList();
			var result = true;

			foreach (var item in aFirstLetters)
			{
				result &= item == item.ToUpper();

				if (!result)
					break;
			}

			return result;
		}

		/// <summary>
		/// Check if type has correct attributes name
		/// </summary>
		/// <returns></returns>
		public bool CheckHasAttributes()
		{
			return this.CheckType.GetFields().Any();
		}

		/// <summary>
		/// Check if type has methods
		/// </summary>
		/// <returns></returns>
		public bool CheckNameAttributes()
		{
			var aFirstLetters = this.CheckType.GetFields().Select(x => x.Name.First().ToString()).ToList();
			var result = true;

			foreach (var item in aFirstLetters)
			{
				result &= item == item.ToLower();

				if (!result)
					break;
			}

			return result;
		}

		/// <summary>
		/// Check if type has methods
		/// </summary>
		/// <returns></returns>
		public bool CheckNameMethods()
		{
			var aInvalidNames = new List<string>() { "get", "set" }; 

			var aFirstLetters = this.CheckType.GetMethods()
				.Where(x=> !aInvalidNames.Contains(x.Name.Substring(0,3)))
				.Select(x => x.Name.First().ToString()).ToList();
			var result = true;

			foreach (var item in aFirstLetters)
			{
				result &= item == item.ToUpper();

				if (!result)
					break;
			}

			return result;
		}
	}
}
