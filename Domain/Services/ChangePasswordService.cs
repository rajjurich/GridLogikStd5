﻿using Domain.Core;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{

    public interface IChangePasswordService
    {
        Task<mstuser> Add(mstuser entity);
        Task<IEnumerable<mstuser>> AddRange(IEnumerable<mstuser> entities);
        Task<mstuser> Delete(mstuser entity);
        Task<mstuser> Edit(mstuser entity);
        Task<mstuser> Get(int Key);
        IQueryable<mstuser> GetAll();
        Task<mstuser> Remove(mstuser entity);
        IQueryable<mstuser> FindBy(Expression<Func<mstuser, bool>> predicate);
        Task<IEnumerable<mstuser>> RemoveRange(IEnumerable<mstuser> entities);
    }

    public class ChangePasswordService : IChangePasswordService
    {

        IEntityRepository<mstuser> _entityRepository;

        public ChangePasswordService(IEntityRepository<mstuser> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<mstuser> Add(mstuser entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<mstuser>> AddRange(IEnumerable<mstuser> entities)
        {
            throw new NotImplementedException();
        }

        public Task<mstuser> Delete(mstuser entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<mstuser> Edit(mstuser entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<mstuser> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<mstuser> GetAll()
        {
            var obj = _entityRepository.GetAll().Where(x=>x.usrisdeleted==0 ||x.usrisdeleted == null);
            return obj;
        }

        public Task<mstuser> Remove(mstuser entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<mstuser> FindBy(Expression<Func<mstuser, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<mstuser>> RemoveRange(IEnumerable<mstuser> entities)
        {
            throw new NotImplementedException();
        }
    }
}
