using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBlogRepository BlogRepository { get; }
        int Complete();
    }
}
