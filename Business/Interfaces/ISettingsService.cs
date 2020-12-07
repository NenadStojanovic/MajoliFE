using MajoliFE.Business.Dtos;

namespace MajoliFE.Business.Interfaces
{
	public interface ISettingsService
	{
		//public IEnumerable<SettingsDto> GetAll();
		public void Create(SettingsDto model);

		public SettingsDto GetById(int id);

		public void Update(SettingsDto model);
	}
}
