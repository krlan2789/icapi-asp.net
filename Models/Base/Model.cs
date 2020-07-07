using ICAPI.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICAPI.Models
{
	public abstract class Model<T> where T: Item
	{
		#region Attribute
		protected ResultItem result = new ResultItem();
		public List<string> tableAttr = new List<string>();
		public string tableName;
		#endregion

		#region Abstract Method
		// Read data
		public abstract List<T> Read(string atr = null);
		// Insert data
		public abstract ResultItem Insert(T query);
		// Update data
		public abstract ResultItem Update(T query);
		// Fetch All data into class Item List
		protected abstract List<T> FetchAllRecords(MySqlDataReader fetchQuery);
		#endregion

		#region Method
		// Import data from database into class item
		protected ResultItem FetchRecord(MySqlDataReader fetchQuery, string successMessage)
		{
			ResultItem result = new ResultItem();
			if (fetchQuery.RecordsAffected > 0)
			{
				result.code = "done";
				result.recordAffected = fetchQuery.RecordsAffected;
				result.status = successMessage;
			}
			else
			{
				result.code = "error";
				result.recordAffected = fetchQuery.RecordsAffected;
				result.status = "Failed to add/update data.";
			}
			return result;
		}

		// Delete Selected data
		public ResultItem Delete(string code)
		{
			MySqlDataReader query = this.ExecuteQuery($"DELETE FROM {this.tableName} WHERE {this.tableAttr[0]}='{code}'");
			var result = new ResultItem();
			result.recordAffected = query.RecordsAffected;
			result.status = (result.recordAffected > 0) ? "Successfully deleted data." : "Failed to delete data.";
			return result;
		}

		// Executing query
		protected MySqlDataReader ExecuteQuery(string queryString)
		{
			MySqlCommand query = ICAPIConfig.Connection().CreateCommand();
			query.CommandText = queryString;
			try
			{
				query.Connection.Open();
			}
			catch (MySqlException ex)
			{
				result.code = "-1";
				result.status = ex.Message;
			}
			return query.ExecuteReader();
		}
		#endregion
	}
}