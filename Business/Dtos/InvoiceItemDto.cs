namespace MajoliFE.Business.Dtos
{
	public class InvoiceItemDto : BaseDto
	{
		public string ItemId { get; set; }
		public string Name { get; set; }
		public string Unit { get; set; } = "kom";
		public int Quantity { get; set; }
		public float Price { get; set; }
		public int InvoiceId { get; set; }
		public float PDVValue { get; set; } 
		public float TotalWithoutPDV { get { return Quantity * Price; } }
		public float TotalValue { get; set; }
		public InvoiceDto Invoice { get; set; }

		public float GetPdvValue(int pdv)
		{
			var result = ((Quantity * Price) / 100) * pdv;
			PDVValue = result;
			return result;
		}

		public float GetTotalValue(int pdv)
		{
			var result = (Quantity * Price) + GetPdvValue(pdv);
			TotalValue = result;
			return result;
		}
	}
}
