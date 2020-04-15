using Domain.Core;
using Domain.Entities;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IWebReportsService
    {
        Task<clientfoldermap> Add(clientfoldermap entity);

        Task<clientfoldermap> Delete(clientfoldermap entity);
        Task<clientfoldermap> Edit(clientfoldermap entity);
        Task<clientfoldermap> Get(int Key);
        IQueryable<clientfoldermap> GetAll();
        Task<clientfoldermap> Remove(clientfoldermap entity);
        IQueryable<clientfoldermap> FindBy(Expression<Func<clientfoldermap, bool>> predicate);
        IQueryable<FileInformation> GetFiles(dynamic requestInfo);


    }
    public class WebReportsService : IWebReportsService
    {
        private IEntityRepository<clientfoldermap> entityRepository;
        public WebReportsService(IEntityRepository<clientfoldermap> entityRepository)
        {
            this.entityRepository = entityRepository;
        }
        public Task<clientfoldermap> Add(clientfoldermap entity)
        {
            return entityRepository.Add(entity);
        }

        public Task<clientfoldermap> Delete(clientfoldermap entity)
        {
            return entityRepository.Delete(entity);
        }

        public Task<clientfoldermap> Edit(clientfoldermap entity)
        {
            return entityRepository.Edit(entity);
        }

        public Task<clientfoldermap> Get(int Key)
        {
            return entityRepository.Get(Key);
        }

        public IQueryable<clientfoldermap> GetAll()
        {
            return entityRepository.GetAll();
        }

        public Task<clientfoldermap> Remove(clientfoldermap entity)
        {
            return entityRepository.Remove(entity);
        }

        public IQueryable<clientfoldermap> FindBy(Expression<Func<clientfoldermap, bool>> predicate)
        {
            return entityRepository.FindBy(predicate);
        }

        public IQueryable<FileInformation> GetFiles(dynamic requestInfo)
        {
            String Query = String.Empty;

            int id = requestInfo.PathId;
            DateTime frmDT = requestInfo.FromDate;
            DateTime toDT = requestInfo.ToDate;

            string dir = entityRepository.FindBy(x => x.id == id).Select(x => x.folderpath).FirstOrDefault();
            Stopwatch timer = new Stopwatch();
            timer.Start();

            List<FileInformation> fileDetails = new List<FileInformation>();
            if (!Directory.Exists(dir))
            {
                throw new DirectoryNotFoundException();
            }
            String[] files = Directory.GetFiles(dir);
            Parallel.For(0, files.Length,
                         index =>
                         {
                             FileInformation f = new FileInformation();
                             FileInfo fi = new FileInfo(files[index]);
                             f.FullFileName = fi.FullName;
                             f.FileName = fi.Name;
                             f.CreationTime = fi.CreationTime;

                             if (fi.CreationTime > frmDT && fi.CreationTime < toDT.AddDays(1))
                                 fileDetails.Add(f);
                         });
            timer.Stop();
            var elapsedtime = timer.Elapsed.Milliseconds;
            return fileDetails.AsQueryable();
        }
    }
}
