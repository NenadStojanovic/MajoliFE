﻿using MajoliFE.Data.Data;
using MajoliFE.Data.DbContexts;
using MajoliFE.Data.Interfaces;

namespace MajoliFE.Data.Repositories
{
	public class SettingsRepository : Repository<Settings>, ISettingsRepository
	{
		private readonly AppDbContext dbContext;
		public SettingsRepository(AppDbContext dbContext) : base(dbContext)
		{
			this.dbContext = dbContext;
		}
	}
}
