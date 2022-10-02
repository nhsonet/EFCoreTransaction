using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFCoreTransaction.Infrastructure.Interfaces;
using EFCoreTransaction.Models;

namespace EFCoreTransaction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public TransactionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("testTransaction")]
        public async Task<IActionResult> TestTransaction()
        {
            var transaction = _unitOfWork.BeginEntityTransactionAsync();

            try
            {
                var objStudent = new Student();
                objStudent.Name = "BBB";
                objStudent.CreatedAt = DateTime.Now;
                objStudent.IsActive = true;

                var objCourse = new Course();
                objCourse.Name = "BBB";
                objCourse.CreatedAt = DateTime.Now;
                objCourse.IsActive = true;

                await _unitOfWork.StudentRepository.AddAsync(objStudent);
                await _unitOfWork.CourseRepository.AddAsync(objCourse);

                var resultCount = await _unitOfWork.SaveEntityAsync();

                //var isCompleted = transaction.IsCompleted;
                var result = transaction.Result;

                return Ok(200);
            }
            catch (Exception ex)
            {
                //var isFaulted = transaction.IsFaulted;
                _unitOfWork.RollbackEntityTransactionAsync();

                return Ok(400);
            }
        }

    }
}
