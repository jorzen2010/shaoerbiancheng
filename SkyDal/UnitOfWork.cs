using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SkyEntity;


namespace SkyDal
{
    public class UnitOfWork : IDisposable
    {
        private SkyWebContext context = new SkyWebContext();



        
         private GenericRepository<Category> CategorysRepository;

         public GenericRepository<Category> categorysRepository
         {
             get
             {

                 if (this.CategorysRepository == null)
                 {
                     this.CategorysRepository = new GenericRepository<Category>(context);
                 }
                 return CategorysRepository;
             }
         }

        
   
   
   

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}