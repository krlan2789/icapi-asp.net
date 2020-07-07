using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICAPI.Models
{
	public class ChapterModel : Model<ChapterItem>
	{
		public ChapterModel()
		{
			this.tableName = "chaptercollection";
			this.tableAttr.Add("codeCC");
			this.tableAttr.Add("titleCC");
			this.tableAttr.Add("contentCC");
			this.tableAttr.Add("orderCC");
			this.tableAttr.Add("dateCC");
			this.tableAttr.Add("codeAC");
		}

		// Import data from database into class item list
		protected override List<ChapterItem> FetchAllRecords(MySqlDataReader fetchQuery)
		{
			List<ChapterItem> results = new List<ChapterItem>();
			while (fetchQuery.Read())
			{
				ChapterItem result = new ChapterItem();
				result.code = fetchQuery[this.tableAttr[0]].ToString();
				result.title = fetchQuery[this.tableAttr[2]].ToString();
				result.content = fetchQuery[this.tableAttr[2]].ToString();
				result.order = Convert.ToInt32(fetchQuery[this.tableAttr[3]]);
				result.date = fetchQuery[this.tableAttr[4]].ToString();
				result.codeAC = fetchQuery[this.tableAttr[5]].ToString();
				results.Add(result);
			}
			return results;
		}

		// Read data from database
		public override List<ChapterItem> Read(string code = null)
		{
			MySqlDataReader query = this.ExecuteQuery($"SELECT * FROM {this.tableName}" + ((code != null) ? $" WHERE {this.tableAttr[0]}='{code}'" : ""));
			return this.FetchAllRecords(query);
		}

		// Insert data into database
		public override ResultItem Insert(ChapterItem value)
		{
			MySqlDataReader query = this.ExecuteQuery($"INSERT INTO {this.tableName} ({this.tableAttr[0]}, {this.tableAttr[1]}, {this.tableAttr[2]}, {this.tableAttr[3]}, {this.tableAttr[4]}) VALUES ('{value.code}', '{value.title}', '{value.content}', {value.order}, '{value.date}')");
			return this.FetchRecord(query, "successfully added data.");
		}

		// Update data in database
		public override ResultItem Update(ChapterItem value)
		{
			string setValue = (!value.title.Equals("")) ? $"{this.tableAttr[1]}='{value.title}', " : "";
			setValue += (!value.content.Equals("")) ? $"{this.tableAttr[2]}='{value.content}', " : "";
			setValue += (value.order > 0) ? $"{this.tableAttr[3]}={value.order}, " : "";
			MySqlDataReader query = this.ExecuteQuery($"UPDATE {this.tableName} SET {setValue}{this.tableAttr[4]}='{value.date}' WHERE {this.tableAttr[0]}='{value.code}'");
			return this.FetchRecord(query, "successfully updated data.");
		}
	}
}