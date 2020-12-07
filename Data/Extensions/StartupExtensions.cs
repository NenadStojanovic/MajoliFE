using MajoliFE.Data.DbContexts;
using MajoliFE.Data.Interfaces;
using MajoliFE.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MajoliFE.Data
{
	public class StartupExtensions
	{
		public static void ConfigureServices(IServiceCollection services)
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile(path: "appsettings.json")
				.Build();
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(
					configuration.GetConnectionString("DefaultConnection")));
			services.AddScoped<ICustomerRepository, CustomerRepository>();
			services.AddScoped<IInvoiceRepository, InvoiceRepository>();
			services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
			services.AddScoped<ISettingsRepository, SettingsRepository>();


		}
	}
}
