using EFCoreTransaction.Infrastructure.Interfaces;
using EFCoreTransaction.Models;

namespace EFCoreTransaction.Infrastructure
{
    public class CourseRepository : GenericRepository<Course, EntityDbContext>, ICourseRepository
    {
        private readonly EntityDbContext _context;

        public CourseRepository(EntityDbContext context) : base(context)
        {
            _context = context;
        }

        //DO some related work

    }
}
