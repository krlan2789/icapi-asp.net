using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICAPI.Models
{
	public class ArcItem : Item
	{
		public string title { get; set; }
		public string desc { get; set; }
		public int chaptercount { get; set; }
		public string codeDC { get; set; }
	}
}