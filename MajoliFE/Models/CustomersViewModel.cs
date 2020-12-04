using MajoliFE.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajoliFE.Models
{
	public class CustomersViewModel : BaseViewModel
	{
		public IEnumerable<CustomerDto> Customers { get; set; }
	}
}
