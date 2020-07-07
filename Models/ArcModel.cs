using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICAPI.Models
{
	public class ArcModel : Model<ArcItem>
	{
		public ArcModel()
		{
			this.tableName = "arccollection";
			this.tableAttr.Add("codeAC");
			this.tableAttr.Add("titleAC");
			this.tableAttr.Add("descAC");
			this.tableAttr.Add("chaptercountAC");
			this.tableAttr.Add("dateAC");
			this.tableAttr.Add("codeDC");
		}

		// Import data from database into class item list
		protected override List<ArcItem> FetchAllRecords(MySqlDataReader fetchQuery)
		{
			List<ArcItem> results = new List<ArcItem>();
			while (fetchQuery.Read())
			{
				ArcItem result = new ArcItem();
				result.code = fetchQuery[this.tableAttr[0]].ToString();
				result.title = fetchQuery[this.tableAttr[2]].ToString();
				result.desc = fetchQuery[this.tableAttr[2]].ToString();
				result.chaptercount = Convert.ToInt32(fetchQuery[this.tableAttr[3]]);
				result.date = fetchQuery[this.tableAttr[4]].ToString();
				result.codeDC = fetchQuery[this.tableAttr[5]].ToString();
				results.Add(result);
			}
			return results;
		}

		// Read data from database
		public override List<ArcItem> Read(string code = null)
		{
			MySqlDataReader query = this.ExecuteQuery($"SELECT * FROM {this.tableName}" + ((code != null) ? $" WHERE {this.tableAttr[0]}='{code}'" : ""));
			return this.FetchAllRecords(query);
		}

		// Insert data into database
		public override ResultItem Insert(ArcItem value)
		{
			MySqlDataReader query = this.ExecuteQuery($"INSERT INTO {this.tableName} ({this.tableAttr[0]}, {this.tableAttr[1]}, {this.tableAttr[2]}, {this.tableAttr[3]}, {this.tableAttr[4]}) VALUES ('{value.code}', '{value.title}', '{value.desc}', {value.chaptercount}, '{value.date}')");
			return this.FetchRecord(query, "successfully added data.");
		}

		// Update data in database
		public override ResultItem Update(ArcItem value)
		{
			string setValue = (!value.title.Equals("")) ? $"{this.tableAttr[1]}='{value.title}', " : "";
			setValue += (!value.desc.Equals("")) ? $"{this.tableAttr[2]}='{value.desc}', " : "";
			setValue += (value.chaptercount > 0) ? $"{this.tableAttr[3]}={value.chaptercount}, " : "";
			MySqlDataReader query = this.ExecuteQuery($"UPDATE {this.tableName} SET {setValue}{this.tableAttr[4]}='{value.date}' WHERE {this.tableAttr[0]}='{value.code}'");
			return this.FetchRecord(query, "successfully updated data.");
		}
	}
}