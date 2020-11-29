using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using System.Net;
using Newtonsoft.Json;


namespace MajoliFE.Infrastructure.Middleware
{
	public class ExceptionHandlingMiddleware
	{
		private RequestDelegate nextRequestDelegate;
		private IHostingEnvironment environment;

		public ExceptionHandlingMiddleware(
			RequestDelegate nextRequestDelegate,
			IHostingEnvironment environment
		)
		{
			this.nextRequestDelegate = nextRequestDelegate ?? throw new ArgumentNullException(nameof(nextRequestDelegate));
			this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				context.TraceIdentifier = Guid.NewGuid().ToString();
				await this.nextRequestDelegate(context);
			}
			//catch (CustomException ex)
			//{
			//	switch (ex.Type)
			//	{
			//		case CustomExceptionType.Info:
			//		case CustomExceptionType.Warning:
			//		case CustomExceptionType.Error:
			//			await this.HandleCustomException(context, ex);
			//			break;
			//		default:
			//			await this.HandleUnknownExceptionAsync(context, ex);
			//			break;
			//	};
			//}
			catch (Exception ex)
			{
				await this.HandleGlobalExceptionAsync(context, ex);
			}
		}

		//private async Task HandleCustomException(HttpContext context, CustomException ex)
		//{
		//	Log.Error(ex, "Custom Exception");
		//	//var response = new ResponseTemplateDto<bool> { ErrorMessage = ex.Message, IsSuccess = false };
		//	//await this.WriteResponseAsync(context, HttpStatusCode.OK, JsonConvert.SerializeObject(response));
		//	var response = new ExceptionResultDto()
		//	{
		//		Message = ex.Message,
		//		Code = ex.Type
		//	};

		//	await this.WriteResponseAsync(context, HttpStatusCode.BadRequest, JsonConvert.SerializeObject(response));
		//}
		//private async Task HandleUnknownExceptionAsync(HttpContext context, CustomException ex)
		//{
		//	Log.Error(ex, "Unknown Exception");
		//	var response = new ExceptionResultDto()
		//	{
		//		Message = ex.Message,
		//		Code = ex.Type
		//	};
		//	await this.WriteResponseAsync(context, HttpStatusCode.BadRequest, JsonConvert.SerializeObject(response));
		//}

		private async Task HandleGlobalExceptionAsync(HttpContext context, Exception ex)
		{
		
			try
			{
				await this.WriteResponseAsync(context, HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(ex.Message));
			}
			catch (Exception e)
			{
				await this.WriteResponseAsync(context, HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(e.Message));
			}

			var logger = new LoggerConfiguration()
								  .MinimumLevel.Debug()
								  .WriteTo.File(@"AppData\Errors\log-.log", rollingInterval: RollingInterval.Day)
								  .CreateLogger();
			logger.Error(ex, "Unhandled Exception - Detected UserId: {UserId}" /*, context.User.GetUserId()*/);
		}

		private async Task WriteResponseAsync(HttpContext context, HttpStatusCode code, string jsonResponse)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;
			await context.Response.WriteAsync(jsonResponse);
		}
	}
}
