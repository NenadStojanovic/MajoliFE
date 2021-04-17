using MajoliFE.Data.Data;
using MajoliFE.Data.DbContexts;
using MajoliFE.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MajoliFE.Data.Repositories
{
	public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
	{
		private readonly AppDbContext dbContext;
		public InvoiceRepository(AppDbContext dbContext) : base(dbContext)
		{
			this.dbContext = dbContext;
		}

		public override IEnumerable<Invoice> GetAll()
		{
			var result = dbContext.Invoices.Include(c => c.Customer).ToList();
			return result;
		}

		public override Invoice GetById(int id)
		{
			var result = dbContext.Invoices.Where(x=>x.Id==id).Include(c => c.Customer).Include(x=>x.InvoiceItems).FirstOrDefault();
			return result;
		}

		public List<Invoice> GetInvocesByCustomerId(int customerId)
		{
			var result = dbContext.Invoices.Where(x => x.CustomerId == customerId).ToList();
			return result;
		}

		public List<Invoice> GetInvocesFromRange(DateTime dateFrom, DateTime dateTo)
		{
			var result = dbContext.Invoices.Where(x => x.DateIssued >= dateFrom && x.DateIssued <= dateTo).Include(x=>x.Customer).ToList();
			return result;
		}
	}
}
