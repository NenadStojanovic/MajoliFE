using MajoliFE.Data.Data;
using MajoliFE.Data.DbContexts;
using MajoliFE.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MajoliFE.Data.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private readonly AppDbContext dbContext;

		public Repository(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public virtual void Create(TEntity entity)
		{
			 dbContext.Set<TEntity>().Add(entity);
		}

		public virtual void Delete(TEntity entity)
		{
			dbContext.Set<TEntity>().Remove(entity);
		
		}

		public virtual IEnumerable<TEntity> GetAll()
		{
			return  dbContext.Set<TEntity>().ToList();
		}

		public virtual TEntity GetById(int id)
		{
			return  dbContext.Set<TEntity>().Find(id);
		}

		public virtual void Update(int id, TEntity entity)
		{
			dbContext.Set<TEntity>().Update(entity);
		}

		public int SaveChanges()
		{
			 return dbContext.SaveChanges();
		}
	}
}
