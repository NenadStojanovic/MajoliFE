using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using MajoliFE.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using MajoliFE.Infrastructure.Middleware;
using MajoliFE.Infrastructure;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace MajoliFE
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));
			services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();
			services.AddControllersWithViews();
			services.AddRazorPages()
				.AddRazorRuntimeCompilation();

			Business.StartupExtensions.ConfigureServices(services);

			//DI Configurations
			services.AddScoped<IModelFactory, ModelFactory>();

			//Localization
			// Configure supported cultures and localization options
			services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = new[]
				{
				new CultureInfo("en-US") {
				DateTimeFormat = {
                    LongTimePattern = "HH:mm:ss",
                    ShortTimePattern = "hh:mm tt",
					ShortDatePattern = "dd/mm/yyyy",
					FullDateTimePattern =  "dd/mm/yyyy",
					LongDatePattern = "dd/mm/yyyy"
				},
				}
				};

				// State what the default culture for your application is. This will be used if no specific culture
				// can be determined for a given request.
				options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");

				// You must explicitly state which cultures your application supports.
				// These are the cultures the app supports for formatting numbers, dates, etc.
				options.SupportedCultures = supportedCultures;

				// These are the cultures the app supports for UI strings, i.e. we have localized resources for.
				options.SupportedUICultures = supportedCultures;
			});
		}
	

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}

