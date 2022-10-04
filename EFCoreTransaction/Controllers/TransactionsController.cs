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
            //var transaction = _unitOfWork.BeginEntityTransactionAsync();

            try
            {
                var objStudent = new Student();
                objStudent.Name = "test student";
                objStudent.CreatedAt = DateTime.Now;
                objStudent.IsActive = true;

                var objCourse = new Course();
                objCourse.Name = "test course";
                objCourse.CreatedAt = DateTime.Now;
                objCourse.IsActive = true;

                await _unitOfWork.StudentRepository.AddAsync(objStudent);

                //DO some work

                //int errorValue = Convert.ToInt32("");

                await _unitOfWork.CourseRepository.AddAsync(objCourse);

                var resultCount = await _unitOfWork.CommitEntityTransactionAsync();

                //var transactionIsCompleted = transaction.IsCompleted;
                //var transactionResult = transaction.Result;

                return Ok(200);     //used for simplification
            }
            catch (Exception ex)
            {
                //var transactionIsFaulted = transaction.IsFaulted;
                //_unitOfWork.RollbackEntityTransactionAsync();

                return Ok(400);     //used for simplification
            }
        }

    }
}
