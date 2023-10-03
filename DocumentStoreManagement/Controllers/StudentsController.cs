using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocumentStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : BaseController
    {
        private readonly IGenericRepository<Student> _studentRepository;

        public StudentsController(IUnitOfWork unitOfWork, IGenericRepository<Student> studentRepository) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = studentRepository;
        }

        // GET: api/Students
        /// <summary>
        /// ABC
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _studentRepository.GetAllAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            await _studentRepository.UpdateAsync(student);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Students
        ///     {
        ///         "identityCode": "StudentCode",
        ///         "name": "Student #1",
        ///         "age": 18,
        ///         "description": "Very Good"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            await _studentRepository.AddAsync(student);
            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException)
            {
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

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            await _studentRepository.RemoveAsync(student);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        private async Task<bool> StudentExists(int id)
        {
            return await _studentRepository.CheckExistsAsync(e => e.Id == id);
        }
    }
}
