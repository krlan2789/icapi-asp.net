using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICAPI.Models
{
	public static class CodeManager<T, S> where T : Item where S : Model<T>
	{
		// Creating Code
		public static string Create(string tableCode, int value)
		{
			string additional = "";
			while (true)
			{
				additional += "0";
				if ((additional + value).Length == 6)
				{
					break;
				}
			}
			return (tableCode + additional + (value + 1));
		}

		// Creating Code
		public static string Create(string tableCode, List<T> results)
		{
			string additional = "";
			while (true)
			{
				additional += "0";
				if ((additional + results.Count).Length == 6)
				{
					break;
				}
			}
			return (tableCode + additional + (results.Count + 1));
		}

		// Check Code Available from class Item
		public static int Check(S model)
		{
			List<T> resultDB = new object() as List<T>;
			resultDB = model.Read();
			return Check(resultDB);
		}

		// Check Code Available from class Item List
		public static int Check(List<T> results)
		{
			string text = "123456789";
			int x1, x2 = 1;
			
			foreach(T item in results)
			{
				string subStr = item.code.Substring(2);
				int index = (subStr.IndexOfAny(text.ToCharArray()) < 0) ? subStr.Length - 1 : subStr.IndexOfAny(text.ToCharArray());
				text = subStr.Substring(index);
				x1 = Convert.ToInt32(text);
				if(x1 == x2)
				{
					x2 = x1+1;
				}
				else
				{
					break;
				}
			}
			return x2-1;
		}
		// Get date time
		public static string GetDateNow()
		{
			return DateTime.Now.ToString().Replace(" ", "").Replace("/", "").Replace(":", "");
		}
	}
}