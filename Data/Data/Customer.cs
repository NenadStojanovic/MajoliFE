using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MajoliFE.Data.Data
{
	[Table("Customers")]
	public class Customer : BaseEntity
	{
		public string Name { get; set; }
	}
}
