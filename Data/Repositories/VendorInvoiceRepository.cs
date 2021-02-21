using MajoliFE.Data.Data;
using MajoliFE.Data.DbContexts;
using MajoliFE.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MajoliFE.Data.Repositories
{
	public class VendorInvoiceRepository : Repository<VendorInvoice>, IVendorInvoiceRepository
	{
		private readonly AppDbContext dbContext;
		public VendorInvoiceRepository(AppDbContext dbContext) : base(dbContext)
		{
			this.dbContext = dbContext;
		}
		public override IEnumerable<VendorInvoice> GetAll()
		{
			var result = dbContext.VendorInvoices.Include(c => c.Vendor).ToList();
			return result;
		}

		public override VendorInvoice GetById(int id)
		{
			var result = dbContext.VendorInvoices.Where(x => x.Id == id).Include(c => c.Vendor).FirstOrDefault();
			return result;
		}

		public List<VendorInvoice> GetInvocesByVendorId(int vendorId)
		{
			var result = dbContext.VendorInvoices.Where(x => x.VendorId == vendorId).ToList();
			return result;
		}

		public List<VendorInvoice> GetVendorInvocesFromRange(DateTime dateFrom, DateTime dateTo)
		{
			var result = dbContext.VendorInvoices.Where(x => x.Date >= dateFrom && x.Date <= dateTo).Include(x=>x.Vendor).ToList();
			return result;
		}
	}
}
