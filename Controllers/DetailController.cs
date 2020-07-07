using ICAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ICAPI.Controllers
{
	public class DetailController : ApiController
	{
		[Route("api/coba/{code}")]
		[HttpPut]
		public DetailItem Coba(string code, [FromBody]DetailItem value)
		{
			value.desc = (value.desc == null) ? "" : value.desc;
			value.arccount = (value.arccount == null || value.arccount == 0) ? 0 : value.arccount;
			string setValue = (!value.desc.Equals("")) ? $"descDC='{value.desc}', " : "";
			setValue += (!value.arccount.Equals("")) ? $"arccountDC={value.arccount}, " : "";
			value.status = setValue;
			return value;
		}

		[Route("api/detail")]
		[HttpGet]
		public List<DetailItem> AllTitle()
		{
			DetailModel model = new DetailModel();
			return model.Read();
		}

		[Route("api/detail/{code}")]
		[HttpGet]
		public List<DetailItem> Title(string code)
		{
			DetailModel model = new DetailModel();
			return model.Read(code);
		}

		[Route("api/detail")]
		[HttpPost]
		public ResultItem AddTitle([FromBody]DetailItem value)
		{
			DetailModel model = new DetailModel();
			value.code = CodeManager<DetailItem, DetailModel>.Create("DC", CodeManager<DetailItem, DetailModel>.Check(model)).ToString();
			value.date = CodeManager<DetailItem, DetailModel>.GetDateNow();
			return model.Insert(value);
		}

		[Route("api/detail/{code}")]
		[HttpPut]
		public ResultItem UpdateTitle(string code, [FromBody]DetailItem value)
		{
			value.desc = (value.desc == null) ? "" : value.desc;
			value.code = code;
			value.date = CodeManager<DetailItem, DetailModel>.GetDateNow();
			DetailModel model = new DetailModel();
			return model.Update(value);
		}

		[Route("api/detail")]
		[HttpDelete]
		public ResultItem RemoveTitle([FromBody]ResultItem result)
		{
			DetailModel model = new DetailModel();
			return model.Delete(result.code);
		}
	}
}
