using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EFCoreTransaction.Models;
using EFCoreTransaction.Infrastructure.Interfaces;

namespace EFCoreTransaction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentCollection()
        {
            var result = await _unitOfWork.StudentRepository.GetEntityCollectionByFilterAsync(x => x.IsActive);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var result = await _unitOfWork.StudentRepository.GetEntityByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            var result = await _unitOfWork.StudentRepository.GetEntityByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            await _unitOfWork.StudentRepository.UpdateAsync(student);
            await _unitOfWork.CommitEntityTransactionAsync();

            return Ok(200);
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            await _unitOfWork.StudentRepository.AddAsync(student);
            await _unitOfWork.CommitEntityTransactionAsync();

            return Ok(200);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _unitOfWork.StudentRepository.GetEntityByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            await _unitOfWork.StudentRepository.RemoveAsync(result);
            await _unitOfWork.CommitEntityTransactionAsync();

            return Ok(200);
        }
    }
}
