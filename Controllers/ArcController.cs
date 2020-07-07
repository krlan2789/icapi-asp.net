using ICAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ICAPI.Controllers
{
  public class ArcController : ApiController
	{
		[Route("api/coba/{code}")]
		[HttpPut]
		public ArcItem Coba(string code, [FromBody]ArcItem value)
		{
			value.desc = (value.desc == null) ? "" : value.desc;
			value.chaptercount = (value.chaptercount == null || value.chaptercount == 0) ? 0 : value.chaptercount;
			string setValue = (!value.desc.Equals("")) ? $"descAC='{value.desc}', " : "";
			setValue += (!value.chaptercount.Equals("")) ? $"chaptercountAC={value.chaptercount}, " : "";
			value.status = setValue;
			return value;
		}

		[Route("api/arc")]
		[HttpGet]
		public List<ArcItem> AllTitle()
		{
			ArcModel model = new ArcModel();
			return model.Read();
		}

		[Route("api/arc/{code}")]
		[HttpGet]
		public List<ArcItem> Title(string code)
		{
			ArcModel model = new ArcModel();
			return model.Read(code);
		}

		[Route("api/arc")]
		[HttpPost]
		public ResultItem AddTitle([FromBody]ArcItem value)
		{
			ArcModel model = new ArcModel();
			value.code = CodeManager<ArcItem, ArcModel>.Create("AC", CodeManager<ArcItem, ArcModel>.Check(model)).ToString();
			value.date = CodeManager<ArcItem, ArcModel>.GetDateNow();
			return model.Insert(value);
		}

		[Route("api/arc/{code}")]
		[HttpPut]
		public ResultItem UpdateTitle(string code, [FromBody]ArcItem value)
		{
			value.title = (value.title == null) ? "" : value.title;
			value.desc = (value.desc == null) ? "" : value.desc;
			value.code = code;
			value.date = CodeManager<ArcItem, ArcModel>.GetDateNow();
			ArcModel model = new ArcModel();
			return model.Update(value);
		}

		[Route("api/arc")]
		[HttpDelete]
		public ResultItem RemoveTitle([FromBody]ResultItem result)
		{
			ArcModel model = new ArcModel();
			return model.Delete(result.code);
		}
	}
}
