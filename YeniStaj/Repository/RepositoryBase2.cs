using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using YeniStaj.Abstracts;
using YeniStaj.Models.Context;
using YeniStaj.Models.Entities;

namespace YeniStaj.Repository
{
    public abstract class RepositoryBase2<T, TId> : IDisposable where T : BaseEntity<TId>
    {
        internal static MyContext DbContext;
        private static DbSet<T> DbObject;
        protected RepositoryBase2()   {
            DbContext = DbContext ?? new MyContext();
            TimeSpan dd = DateTime.Now - DbContext.InstanceDate;
            if (IsDisposed) DbContext = new MyContext();
            if (dd.TotalMinutes > 30) DbContext = new MyContext();
            DbObject = DbContext.Set<T>();
        }

        public List<T> GetAll()
        {
            return DbObject.ToList();
        }
        public List<T> GetAll(Func<T, bool> predicate)
        {
            return DbObject.Where(predicate).ToList();
        }
       
       
        public T GetById(params object[] keys)
        {
            return DbObject.Find(keys);
        }
       
        public int Insert(T entity)
        {
            DbObject.Add(entity);
            return DbContext.SaveChanges();
        }
        public void InsertForMark(T entity)
        {
            DbObject.Add(entity);
        }
      
        public int Delete(T entity)
        {
            DbObject.Remove(entity);
            return DbContext.SaveChanges();
        }
        public void DeleteForMark(T entity)
        {
            DbObject.Remove(entity);
        }
        
        public int Save()
        {
            return DbContext.SaveChanges();
        }
       
        
       

        public IQueryable<T> Queryable()
        {
            return DbObject;
        }
        public bool IsDisposed { get; set; }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.IsDisposed = true;
        }
    }
}