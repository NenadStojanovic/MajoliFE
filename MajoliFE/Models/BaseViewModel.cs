namespace MajoliFE.Models
{
	public class BaseViewModel
	{
		public bool ShowMessage { get; set; }
		public bool IsSuccess { get; set; } = true;
		public string Message { get; set; } = "Akcija je uspešno završena";
	}
}
