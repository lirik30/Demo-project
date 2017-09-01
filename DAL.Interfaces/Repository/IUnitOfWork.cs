using System;

namespace DAL.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable//why disposable?
    {
        void Commit();
    }
}
