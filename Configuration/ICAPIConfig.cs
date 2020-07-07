using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace ICAPI.Configuration
{
	public class ICAPIConfig
	{
		public static string server = "localhost";
		public static string port = "3306";
		public static string database = "icapi";
		public static string username = "root";
		public static string password = "";

		// Create Connection
		public static MySqlConnection Connection()
		{
			return new MySqlConnection($"server={server};port={port};database={database};username={username};password={password};");
		}

		public static void Register(HttpConfiguration config)
		{
			// Web API routes
			config.MapHttpAttributeRoutes();

			// Title Routes
			config.Routes.MapHttpRoute(
					name: "Title",
					routeTemplate: "api/title/{code}",
					defaults: new { code = RouteParameter.Optional }
			);

			// Detail Routes
			config.Routes.MapHttpRoute(
					name: "Detail",
					routeTemplate: "api/detail/{code}",
					defaults: new { code = RouteParameter.Optional }
			);

			// Arc Routes
			config.Routes.MapHttpRoute(
					name: "Arc",
					routeTemplate: "api/arc/{code}",
					defaults: new { code = RouteParameter.Optional }
			);

			// Arc Routes
			config.Routes.MapHttpRoute(
					name: "Chapter",
					routeTemplate: "api/chapter/{code}",
					defaults: new { code = RouteParameter.Optional }
			);

			// Configure additional webapi settings here
		}
	}
}