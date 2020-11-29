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


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}
	}
}
