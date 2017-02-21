using System.Web.Http;

namespace PagarmeAsp2
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			PagarMe.PagarMeService.DefaultApiKey = "ak_test_zXjKL8u5uxn25HNxHviPbhthNV0nL7";
			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
