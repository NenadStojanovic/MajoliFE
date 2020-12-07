using System.ComponentModel.DataAnnotations.Schema;

namespace MajoliFE.Data.Data
{
	[Table("InvoiceItems")]
	public class InvoiceItem : BaseEntity
	{
		public string ItemId { get; set; }
		public string Name { get; set; }
		public string Unit { get; set; }
		public int Quantity { get; set; }
		public float Price { get; set; }
		public int InvoiceId { get; set; }
		public virtual Invoice Invoice { get; set; }
	}


}
