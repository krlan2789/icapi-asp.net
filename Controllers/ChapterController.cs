using ICAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ICAPI.Controllers
{
  public class ChapterController : ApiController
	{
		[Route("api/coba/{code}")]
		[HttpPut]
		public ChapterItem Coba(string code, [FromBody]ChapterItem value)
		{
			value.content = (value.content == null) ? "" : value.content;
			value.order = (value.order == null || value.order == 0) ? 0 : value.order;
			string setValue = (!value.content.Equals("")) ? $"descAC='{value.content}', " : "";
			setValue += (!value.order.Equals("")) ? $"chaptercountAC={value.order}, " : "";
			value.status = setValue;
			return value;
		}

		[Route("api/chapter")]
		[HttpGet]
		public List<ChapterItem> AllTitle()
		{
			ChapterModel model = new ChapterModel();
			return model.Read();
		}

		[Route("api/chapter/{code}")]
		[HttpGet]
		public List<ChapterItem> Title(string code)
		{
			ChapterModel model = new ChapterModel();
			return model.Read(code);
		}

		[Route("api/chapter")]
		[HttpPost]
		public ResultItem AddTitle([FromBody]ChapterItem value)
		{
			ChapterModel model = new ChapterModel();
			value.code = CodeManager<ChapterItem, ChapterModel>.Create("CC", CodeManager<ChapterItem, ChapterModel>.Check(model)).ToString();
			value.date = CodeManager<ChapterItem, ChapterModel>.GetDateNow();
			return model.Insert(value);
		}

		[Route("api/chapter/{code}")]
		[HttpPut]
		public ResultItem UpdateTitle(string code, [FromBody]ChapterItem value)
		{
			value.title = (value.title == null) ? "" : value.title;
			value.content = (value.content == null) ? "" : value.content;
			value.code = code;
			value.date = CodeManager<ChapterItem, ChapterModel>.GetDateNow();
			ChapterModel model = new ChapterModel();
			return model.Update(value);
		}

		[Route("api/chapter")]
		[HttpDelete]
		public ResultItem RemoveTitle([FromBody]ResultItem result)
		{
			ChapterModel model = new ChapterModel();
			return model.Delete(result.code);
		}
	}
}
