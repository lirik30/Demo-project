using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable//why disposable?
    {
        void Commit();
    }
}
