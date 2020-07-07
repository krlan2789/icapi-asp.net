using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICAPI.Models
{
	public class DetailModel : Model<DetailItem>
	{
		public DetailModel()
		{
			this.tableName = "detailcollection";
			this.tableAttr.Add("codeDC");
			this.tableAttr.Add("descDC");
			this.tableAttr.Add("arccountDC");
			this.tableAttr.Add("dateDC");
			this.tableAttr.Add("codeTC");
		}

		// Import data from database into class item list
		protected override List<DetailItem> FetchAllRecords(MySqlDataReader fetchQuery)
		{
			List<DetailItem> results = new List<DetailItem>();
			while (fetchQuery.Read())
			{
				DetailItem result = new DetailItem();
				result.code = fetchQuery[this.tableAttr[0]].ToString();
				result.desc = fetchQuery[this.tableAttr[1]].ToString();
				result.arccount = Convert.ToInt32(fetchQuery[this.tableAttr[2]]);
				result.date = fetchQuery[this.tableAttr[3]].ToString();
				result.codeTC = fetchQuery[this.tableAttr[4]].ToString();
				results.Add(result);
			}
			return results;
		}

		// Read data from database
		public override List<DetailItem> Read(string code = null)
		{
			MySqlDataReader query = this.ExecuteQuery($"SELECT * FROM {this.tableName}" + ((code != null) ? $" WHERE {this.tableAttr[0]}='{code}'" : ""));
			return this.FetchAllRecords(query);
		}

		// Insert data into database
		public override ResultItem Insert(DetailItem value)
		{
			MySqlDataReader query = this.ExecuteQuery($"INSERT INTO {this.tableName} ({this.tableAttr[0]}, {this.tableAttr[1]}, {this.tableAttr[2]}, {this.tableAttr[3]}) VALUES ('{value.code}', '{value.desc}', {value.arccount}, '{value.date}')");
			return this.FetchRecord(query, "successfully added data.");
		}

		// Update data in database
		public override ResultItem Update(DetailItem value)
		{
			string setValue = (!value.desc.Equals("")) ? $"{this.tableAttr[1]}='{value.desc}', " : "";
			setValue += (value.arccount > 0) ? $"{this.tableAttr[2]}={value.arccount}, " : "";
			MySqlDataReader query = this.ExecuteQuery($"UPDATE {this.tableName} SET {setValue}{this.tableAttr[3]}='{value.date}' WHERE {this.tableAttr[0]}='{value.code}'");
			return this.FetchRecord(query, "successfully updated data.");
		}
	}
}