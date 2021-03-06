﻿using System;
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


        private GenericRepository<Setting> SettingsRepository;

        public GenericRepository<Setting> settingsRepository
        {
            get
            {

                if (this.SettingsRepository == null)
                {
                    this.SettingsRepository = new GenericRepository<Setting>(context);
                }
                return SettingsRepository;
            }
        }

        private GenericRepository<SysUser> SysUsersRepository;

        public GenericRepository<SysUser> sysUsersRepository
        {
            get
            {

                if (this.SysUsersRepository == null)
                {
                    this.SysUsersRepository = new GenericRepository<SysUser>(context);
                }
                return SysUsersRepository;
            }
        }
        
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

         private GenericRepository<VideoCourse> VideoCoursesRepository;

         public GenericRepository<VideoCourse> videoCoursesRepository
         {
             get
             {

                 if (this.VideoCoursesRepository == null)
                 {
                     this.VideoCoursesRepository = new GenericRepository<VideoCourse>(context);
                 }
                 return VideoCoursesRepository;
             }
         }

         private GenericRepository<CodeUser> CodeUsersRepository;

         public GenericRepository<CodeUser> codeUsersRepository
         {
             get
             {

                 if (this.CodeUsersRepository == null)
                 {
                     this.CodeUsersRepository = new GenericRepository<CodeUser>(context);
                 }
                 return CodeUsersRepository;
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