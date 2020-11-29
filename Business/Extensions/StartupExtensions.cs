using Microsoft.Extensions.DependencyInjection;
using MajoliFE.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;
using MajoliFE.Business.Interfaces;
using MajoliFE.Business.Services;

namespace MajoliFE.Business
{
	public class StartupExtensions
	{
		public static void ConfigureServices(IServiceCollection services)
		{
			Data.StartupExtensions.ConfigureServices(services);

			services.AddScoped<ICustomerService, CustomerService>();


			//var mapperConfiguration = new MapperConfiguration(mc =>
			//{
			//	mc.AddProfile(new MapperProfiler());
			//});

			//IMapper mapper = mapperConfiguration.CreateMapper();
			//services.AddSingleton(mapper);

			//services.AddLocalization(options => options.ResourcesPath = "Infrastructure/Resources");

		}
	}
}
