using MajoliFE.Business.Dtos;
using System.Collections.Generic;

namespace MajoliFE.Business.Interfaces
{
	public interface IVendorService
	{
		public IEnumerable<VendorDto> GetAll();
		public void Create(VendorDto model);

		public VendorDto GetById(int id);

		public void Update(VendorDto model);
		bool Delete(int id);
	}
}
