using AutoMapper;
using MajoliFE.Business.Dtos;
using MajoliFE.Business.Interfaces;
using MajoliFE.Data.Data;
using MajoliFE.Data.Interfaces;
using System.Collections.Generic;

namespace MajoliFE.Business.Services
{
	class SettingsService : ISettingsService
	{
		private ISettingsRepository _settingsRepository;
		private readonly IMapper _mapper;
		public SettingsService(ISettingsRepository settingsRepository, IMapper mapper)
		{
			_settingsRepository = settingsRepository;
			_mapper = mapper;
		}

		public void Create(SettingsDto model)
		{
			_settingsRepository.Create(_mapper.Map<Settings>(model));
			_settingsRepository.SaveChanges();
		}

		public void Update(SettingsDto model)
		{
			_settingsRepository.Update(model.Id, _mapper.Map<Settings>(model));
			_settingsRepository.SaveChanges();
		}

		public IEnumerable<SettingsDto> GetAll()
		{
			var result = _settingsRepository.GetAll();
			var mappedResult = _mapper.Map<IEnumerable<SettingsDto>>(result);
			return mappedResult;
		}

		public SettingsDto GetById(int id)
		{
			var result = _settingsRepository.GetById(id);
			var mappedResult = _mapper.Map<SettingsDto>(result);
			return mappedResult;
		}
	}
}
