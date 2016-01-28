using StockTracker.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace StockTracker.Infrastructure
{

    public class GenericRepository<T> where T : class, IDbEntity, IActivatable
    {

        protected DbContext _db;

        protected IQueryable<T> Table
        {
            get
            {
                return _db.Set<T>();
            }
        }

        public GenericRepository(DbContext db)
        {
            _db = db;
        }

        public IQueryable<T> List()
        {
            return from e in Table
                   where e.Active
                   select e;
        }

        public IQueryable<T> Get(int id)
        {
            return from e in Table
                   where e.Active && e.Id == id
                   select e;
        }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Delete(int id)
        {
            Delete(Get(id).FirstOrDefault());
        }
        public void Delete(T entity)
        {
            entity.Active = false;
        }

        public void SaveChanges()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException dbError)
            {
                var firstError = dbError.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage;
                throw new ValidationException(firstError);
            }
        }
    }
}