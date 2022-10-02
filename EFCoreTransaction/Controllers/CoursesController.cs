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
    public class CoursesController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourseCollection()
        {
            var result = await _unitOfWork.CourseRepository.GetEntityCollectionByFilterAsync(x => x.IsActive);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var result = await _unitOfWork.CourseRepository.GetEntityByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            var result = await _unitOfWork.CourseRepository.GetEntityByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            await _unitOfWork.CourseRepository.UpdateAsync(course);
            await _unitOfWork.SaveEntityAsync();

            return Ok(200);
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            await _unitOfWork.CourseRepository.AddAsync(course);
            await _unitOfWork.SaveEntityAsync();

            return Ok(200);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _unitOfWork.CourseRepository.GetEntityByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            await _unitOfWork.CourseRepository.RemoveAsync(result);
            await _unitOfWork.SaveEntityAsync();

            return Ok(200);
        }

    }
}
