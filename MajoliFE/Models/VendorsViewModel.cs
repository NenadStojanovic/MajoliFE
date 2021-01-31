using MajoliFE.Business.Dtos;
using System.Collections.Generic;

namespace MajoliFE.Models
{
	public class VendorsViewModel : BaseViewModel
	{
		public IEnumerable<VendorDto> Vendors { get; set; }
	}
}
