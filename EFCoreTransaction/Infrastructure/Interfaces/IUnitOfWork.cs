using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCoreTransaction.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IStudentRepository StudentRepository { get; set; }
        public ICourseRepository CourseRepository { get; set; }

        Task<int> SaveEntityAsync();

        Task<IDbContextTransaction> BeginEntityTransactionAsync();

        void RollbackEntityTransactionAsync();

    }
}
