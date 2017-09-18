﻿using System.Data.Entity;
using DAL.Interfaces.Repository;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context) => Context = context;

        public void Dispose() => Context?.Dispose();

        public void Commit() => Context.SaveChanges();
    }
}
