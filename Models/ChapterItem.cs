using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICAPI.Models
{
	public class ChapterItem : Item
	{
		public string title { get; set; }
		public string content { get; set; }
		public int order { get; set; }
		public string codeAC { get; set; }
	}
}