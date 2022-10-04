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

        //Not working as expected
        public async Task<IDbContextTransaction> BeginEntityTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        //Not working as expected
        public void RollbackEntityTransactionAsync()
        {
            _context.Database.RollbackTransactionAsync();
        }

        public async Task<int> CommitEntityTransactionAsync()
        {
            //int records = 0;
            //IDbContextTransaction transaction = null;

            //try
            //{
            //    using (transaction = await _context.Database.BeginTransactionAsync())
            //    {
            //        records = await _context.SaveChangesAsync();
            //        await transaction.CommitAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var errorMessage = $"{ex.Message} + {ex.InnerException.Message}, + Rolling back.";
            //    await transaction.RollbackAsync();
            //}

            //return records;

            return await _context.SaveChangesAsync();
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
