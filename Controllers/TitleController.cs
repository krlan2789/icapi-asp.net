using ICAPI.Configuration;
using ICAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ICAPI.Controllers
{
  public class TitleController : ApiController
	{
		[Route("api/coba/{code}")]
		[HttpPut]
		public TitleItem Coba(string code, [FromBody]TitleItem value)
		{
			value.title = (value.title == null) ? "" : value.title;
			value.creator = (value.creator == null) ? "" : value.creator;
			string setValue = (!value.title.Equals("")) ? $"titleTC='{value.title}', " : "";
			setValue += (!value.creator.Equals("")) ? $"creatorTC='{value.creator}', " : "";
			value.status = setValue;
			return value;
		}

		[Route("api/title")]
		[HttpGet]
		public List<TitleItem> AllTitle()
		{
			TitleModel model = new TitleModel();
			return model.Read();
		}

		[Route("api/title/{code}")]
		[HttpGet]
		public List<TitleItem> Title(string code)
		{
			TitleModel model = new TitleModel();
			return model.Read(code);
		}

		[Route("api/title")]
		[HttpPost]
		public ResultItem AddTitle([FromBody]TitleItem value)
		{
			TitleModel model = new TitleModel();
			value.code = CodeManager<TitleItem, TitleModel>.Create("TC", CodeManager<TitleItem, TitleModel>.Check(model)).ToString();
			value.date = CodeManager<TitleItem, TitleModel>.GetDateNow();
			return model.Insert(value);
		}

		[Route("api/title/{code}")]
		[HttpPut]
		public ResultItem UpdateTitle(string code, [FromBody]TitleItem value)
		{
			value.title = (value.title == null) ? "" : value.title;
			value.creator = (value.creator == null) ? "" : value.creator;
			value.code = code;
			value.date = CodeManager<TitleItem, TitleModel>.GetDateNow();
			TitleModel model = new TitleModel();
			return model.Update(value);
		}

		[Route("api/title")]
		[HttpDelete]
		public ResultItem RemoveTitle([FromBody]ResultItem result)
		{
			TitleModel model = new TitleModel();
			return model.Delete(result.code);
		}
	}
}
