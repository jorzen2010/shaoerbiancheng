﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SkyEntity;


namespace SkyDal
{
    public class SkyWebContext : DbContext
    {
        public SkyWebContext()
            : base("SkyWebContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Category> Categorys { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<VideoCourse> VideoCourses { get; set; }
        public DbSet<CodeUser> CodeUsers { get; set; }
    }
}
