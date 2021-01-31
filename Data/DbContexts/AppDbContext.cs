using MajoliFE.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MajoliFE.Data.DbContexts
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{

		}
		public virtual DbSet<Customer> Customers { get; set; }
		public virtual DbSet<Invoice> Invoices { get; set; }
		public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }
		public virtual DbSet<Settings> Settings { get; set; }
		public virtual DbSet<Vendor> Vendors{ get; set; }
		public virtual DbSet<VendorInvoice> VendorInvoices { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}
	}
}
