using System;
using System.Collections.Generic;
using System.Text;

namespace MajoliFE.Data.Data
{
	public class BaseEntity
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
