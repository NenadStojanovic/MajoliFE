using System;
using System.Collections.Generic;
using System.Text;

namespace MajoliFE.Data.Interfaces
{
	public interface IBaseEntity
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
