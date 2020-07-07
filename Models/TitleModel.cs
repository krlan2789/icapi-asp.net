using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICAPI.Models
{
	public class TitleModel : Model<TitleItem>
	{
		public TitleModel()
		{
			this.tableName = "titlecollection";
			this.tableAttr.Add("codeTC");
			this.tableAttr.Add("titleTC");
			this.tableAttr.Add("creatorTC");
			this.tableAttr.Add("dateTC");
		}

		// Import data from database into class item list
		protected override List<TitleItem> FetchAllRecords(MySqlDataReader fetchQuery)
		{
			List<TitleItem> results = new List<TitleItem>();
			while(fetchQuery.Read())
			{
				TitleItem result = new TitleItem();
				result.code = fetchQuery[this.tableAttr[0]].ToString();
				result.title = fetchQuery[this.tableAttr[1]].ToString();
				result.creator = fetchQuery[this.tableAttr[2]].ToString();
				result.date = fetchQuery[this.tableAttr[3]].ToString();
				results.Add(result);
			}
			return results;
		}

		// Read data from database
		public override List<TitleItem> Read(string code = null)
		{
			MySqlDataReader query = this.ExecuteQuery($"SELECT * FROM {this.tableName}" + ((code != null) ? $" WHERE {this.tableAttr[0]}='{code}'" : ""));
			return this.FetchAllRecords(query);
		}

		// Insert data into database
		public override ResultItem Insert(TitleItem value)
		{
			MySqlDataReader query = this.ExecuteQuery($"INSERT INTO {this.tableName} ({this.tableAttr[0]}, {this.tableAttr[1]}, {this.tableAttr[2]}, {this.tableAttr[3]}) VALUES ('{value.code}', '{value.title}', '{value.creator}', '{value.date}')");
			return this.FetchRecord(query, "successfully added data.");
		}

		// Update data in database
		public override ResultItem Update(TitleItem value)
		{
			string setValue = (!value.title.Equals("")) ? $"{this.tableAttr[1]}='{value.title}', " : "";
			setValue += (!value.creator.Equals("")) ? $"{this.tableAttr[2]}='{value.creator}', " : "";
			MySqlDataReader query = this.ExecuteQuery($"UPDATE {this.tableName} SET {setValue}{this.tableAttr[3]}='{value.date}' WHERE {this.tableAttr[0]}='{value.code}'");
			return this.FetchRecord(query, "successfully updated data.");
		}
	}
}