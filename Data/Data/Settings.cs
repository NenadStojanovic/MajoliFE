using System.ComponentModel.DataAnnotations.Schema;

namespace MajoliFE.Data.Data
{
	[Table("Settings")]
	public class Settings : BaseEntity
	{
		public string CompanyName { get; set; }
		public string Address { get; set; }
		public string BankName { get; set; }
		public string BankAccount { get; set; }
		public int PDV { get; set; }
		public bool IsActive { get; set; }

	}


}
