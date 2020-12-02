using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MajoliFE.Models;
using MajoliFE.Business.Interfaces;
using MajoliFE.Business.Dtos;
using MajoliFE.Infrastructure;

namespace MajoliFE.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IModelFactory _modelFactory;

		public HomeController(ILogger<HomeController> logger, IModelFactory modelFactory)
		{
			_logger = logger;
			_modelFactory = modelFactory;
		}

		public IActionResult Index()
		{
			//throw new Exception("Test");
		    //_customerService.CreateCustomer(new CustomerDto { Name = "Pera" });
			var model = _modelFactory.PrepareCustomersVM();
			return View();
		}

		[HttpGet]
		public IActionResult Customers()
		{
			var model = _modelFactory.PrepareCustomersVM();
			return View(model);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
