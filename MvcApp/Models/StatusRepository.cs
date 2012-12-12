using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MvcApp.Models
{ 
    public class StatusRepository : IStatusRepository
    {
        MvcAppContext context = new MvcAppContext();

        public IQueryable<Status> All
        {
            get { return context.Status; }
        }

        public IQueryable<Status> AllIncluding(params Expression<Func<Status, object>>[] includeProperties)
        {
            IQueryable<Status> query = context.Status;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Status Find(int id)
        {
            return context.Status.Find(id);
        }

        public void InsertOrUpdate(Status status)
        {
            if (status.StatusId == default(int)) {
                // New entity
                context.Status.Add(status);
            } else {
                // Existing entity
                context.Entry(status).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var status = context.Status.Find(id);
            context.Status.Remove(status);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IStatusRepository : IDisposable
    {
        IQueryable<Status> All { get; }
        IQueryable<Status> AllIncluding(params Expression<Func<Status, object>>[] includeProperties);
        Status Find(int id);
        void InsertOrUpdate(Status status);
        void Delete(int id);
        void Save();
    }
}