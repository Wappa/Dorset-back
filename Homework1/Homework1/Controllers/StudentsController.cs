using System.Collections.Generic;
using System.Linq;
using Homework1.Data;
using Homework1.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Homework1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly Context _context;

        public StudentsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentsDTO>> GetStudents()
        {
            var student = from students in _context.Students
                join students_description in _context.Students_description on students.id equals students_description.students_id
                select new StudentsDTO
                {
                    Students_id = students.id,
                    Firstname = students_description.firstname,
                    Lastname = students_description.lastname,
                    Adress= students_description.adress,
                    Age = students_description.age,
                    Country = students_description.country
                };

            return Ok(student);
        }

        [HttpGet("{id}")]
        public ActionResult<StudentsDTO> GetStudents_byId(int id)
        {
            var student = from students in _context.Students
                join students_description in _context.Students_description on students.id equals students_description.students_id
                select new StudentsDTO
                {
                    Students_id = students.id,
                    Firstname = students_description.firstname,
                    Lastname = students_description.lastname,
                    Adress= students_description.adress,
                    Age = students_description.age,
                    Country = students_description.country
                };

            var StudentsById = student.ToList().Find(x => x.Students_id == id);

            if (StudentsById == null)
            {
                return NotFound();
            }
            return StudentsById;
        }
    }
}