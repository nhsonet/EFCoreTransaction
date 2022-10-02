using EFCoreTransaction.Infrastructure.Interfaces;
using EFCoreTransaction.Models;

namespace EFCoreTransaction.Infrastructure
{
    public class StudentRepository : GenericRepository<Student, EntityDbContext>, IStudentRepository
    {
        private readonly EntityDbContext _context;

        public StudentRepository(EntityDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
