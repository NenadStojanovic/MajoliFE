using MajoliFE.Data.Data;

namespace MajoliFE.Data.Interfaces
{
	public interface ISettingsRepository : IRepository<Settings>
	{
		public Settings GetActiveSettings();
	}
}
