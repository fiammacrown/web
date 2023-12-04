using Microsoft.AspNetCore.Builder;
using Abeslamidze_Web.Middleware;

namespace Abeslamidze_Web.Extension
{
	public static class AppExtensions
	{
		public static IApplicationBuilder UseFileLogging(this
IApplicationBuilder app)
=> app.UseMiddleware<LogMiddleware>();
	}
}
