using System.Text;

namespace MajoliFE.Business.Dtos
{
	public class CustomerDto : BaseDto
	{
		public string Name { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string Zip { get; set; }
		public string PIB { get; set; }
		public string MB { get; set; }
		public string PartnerId { get; set; }
	}
}
