using Microsoft.Extensions.DependencyInjection;
using MajoliFE.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;
using MajoliFE.Business.Interfaces;
using MajoliFE.Business.Services;
using AutoMapper;

namespace MajoliFE.Business
{
	public class StartupExtensions
	{
		public static void ConfigureServices(IServiceCollection services)
		{
			Data.StartupExtensions.ConfigureServices(services);

			services.AddScoped<ICustomerService, CustomerService>();
			services.AddScoped<IInvoiceService, InvoiceService>();
			services.AddScoped<IInvoiceItemService, InvoiceItemService>();
			services.AddScoped<ISettingsService, SettingsService>();
			services.AddScoped<IReportGenerator, ReportGenerator>();
			services.AddScoped<IVendorService, VendorService>();
			services.AddScoped<IVendorInvoiceService, VendorInvoiceService>();


			var mapperConfiguration = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MapperProfile());
			});

			IMapper mapper = mapperConfiguration.CreateMapper();
			services.AddSingleton(mapper);

			//services.AddLocalization(options => options.ResourcesPath = "Infrastructure/Resources");

		}
	}
}
