using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void RollBack();
    }
    public class UnitOfWork : IUnitOfWork
    {
        readonly DbContext _entitiesContext;
        public DbContextTransaction _transaction;
        public UnitOfWork(DbContext entitiesContext)
        {
            _entitiesContext = entitiesContext;
        }
        public void BeginTransaction()
        {            
            _transaction = _entitiesContext.Database.BeginTransaction();
        }
        
        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
            }
        }

        public void RollBack()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
            }
        }
    }
}
