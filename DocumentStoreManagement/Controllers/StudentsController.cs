﻿using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocumentStoreManagement.Controllers
{
    /// <summary>
    /// Student Management API Controller - SQL database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : BaseController
    {
        private readonly IGenericRepository<Student> _studentRepository;

        /// <summary>
        /// Add dependencies to controller
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="studentRepository"></param>
        public StudentsController(IUnitOfWork unitOfWork, IGenericRepository<Student> studentRepository) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Gets the student list from database
        /// </summary>
        /// <returns>A list of all students</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/students
        ///
        /// </remarks>
        [HttpGet]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            // Get list of students
            return await _studentRepository.GetAllAsync();
        }

        /// <summary>
        /// Gets a student bases on student id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A student matches input id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/students/{id}
        ///
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            // Get student by id
            Student student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        /// <summary>
        /// Updates a student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="student"></param>
        /// <returns>An updated student</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/students/{id}
        ///     {
        ///         "id": "Student Id"
        ///         "identityCode": "Student Identity Code",
        ///         "name": "Student #1",
        ///         "age": 18,
        ///         "description": "Wants to learn C#"
        ///     }
        ///
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, [FromBody] Student student)
        {
            // Return bad request if ids don't match
            if (id != student.Id)
            {
                return BadRequest();
            }

            // Update student
            await _studentRepository.UpdateAsync(student);

            try
            {
                // Save changes
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if student exists
                if (!await StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a student
        /// </summary>
        /// <param name="student"></param>
        /// <returns>A newly created student</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/students
        ///     {
        ///         "identityCode": "Student Identity Code",
        ///         "name": "Student #1",
        ///         "age": 18,
        ///         "description": "Wants to learn C#"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent([FromBody] Student student)
        {
            // Add a new student
            await _studentRepository.AddAsync(student);
            try
            {
                // Save changes
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException)
            {
                // Check if student exists
                if (await StudentExists(student.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        /// <summary>
        /// Deletes a student
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Student is deleted</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE api/students/{id}
        ///
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            // Get student by id
            Student student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            // Delete student
            await _studentRepository.RemoveAsync(student);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes all students from database
        /// </summary>
        /// <returns>All students from database are deleted</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE api/students
        ///
        /// </remarks>
        [HttpDelete]
        public async Task<IActionResult> DeleteAllStudents()
        {
            // Get all students from database and delete
            IEnumerable<Student> students = await _studentRepository.GetAllAsync();
            await _studentRepository.RemoveRangeAsync(students);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Check if student exists method
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Boolean</returns>
        private async Task<bool> StudentExists(int id)
        {
            return await _studentRepository.CheckExistsAsync(e => e.Id == id);
        }
    }
}
