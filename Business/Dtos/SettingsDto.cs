namespace MajoliFE.Business.Dtos
{
	public class SettingsDto : BaseDto
	{
		public string CompanyName { get; set; }
		public string Address { get; set; }
		public string BankName { get; set; }
		public string BankAccount { get; set; }
		public int PDV { get; set; } = 20;

	}
}
