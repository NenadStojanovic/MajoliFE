using MajoliFE.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MajoliFE.Data.Interfaces
{
	public interface IRepository<TEntity> : IUnitOfWork
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity Get(int id);

        void Create(TEntity entity);

        void Update(int id, TEntity entity);

		void Delete(TEntity entity);
    }
}
