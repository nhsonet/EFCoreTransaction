using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using EFCoreTransaction.Infrastructure.Interfaces;

namespace EFCoreTransaction.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EntityDbContext _context;
        private bool _disposed;

        public IStudentRepository StudentRepository { get; set; }
        public ICourseRepository CourseRepository { get; set; }

        public UnitOfWork(EntityDbContext context)
        {
            _context = context;

            StudentRepository = new StudentRepository(context);
            CourseRepository = new CourseRepository(context);
        }

        public async Task<int> SaveEntityAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginEntityTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public void RollbackEntityTransactionAsync()
        {
            _context.Database.RollbackTransactionAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
