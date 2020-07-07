using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICAPI.Models
{
	public class DetailItem : Item
	{
		public string desc { get; set; }
		public int arccount { get; set; }
		public string codeTC { get; set; }
	}
}