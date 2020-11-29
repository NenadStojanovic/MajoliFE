using MajoliFE.Data.Data;
using MajoliFE.Data.DbContexts;
using MajoliFE.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MajoliFE.Data.Repositories
{
	public class CustomerRepository : Repository<Customer>, ICustomerRepository
	{
		private readonly AppDbContext dbContext;
		public CustomerRepository(AppDbContext dbContext) : base(dbContext) 
		{
			this.dbContext = dbContext;
		}
	}
}
