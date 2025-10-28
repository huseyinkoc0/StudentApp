using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Business.Abstract;
using StudentApp.Entities.DTOs;
using System.Security.Claims;

namespace StudentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Stud    ent")]
    public class StudentController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly INoteService _noteService;
        private readonly IStudentLessonService _studentLessonService;

        public StudentController(
            ILessonService lessonService,
            INoteService noteService,
            IStudentLessonService studentLessonService)
        {
            _lessonService = lessonService;
            _noteService = noteService;
            _studentLessonService = studentLessonService;
        }
        private int GetCurrentStudentId()
        {
            // Token'daki 'NameIdentifier' (Kullanıcı ID'si) claim'ini bul ve int'e çevir.
            var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // (Gerçek bir projede 'int.TryParse' ile daha güvenli bir çevrim yapılabilir)
            return int.Parse(studentId);
        }

        [HttpGet("derslerim")]
        public IActionResult GetMyEnrolledLessons()
        {
            int studentId = GetCurrentStudentId();


            var result = _studentLessonService.GetStudentLessons(studentId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("ders-kayit")]


        public IActionResult RegisterLesson([FromBody] LessonRegisterDTO lessonRegister)
        {
            int studentId = GetCurrentStudentId();
            var result = _studentLessonService.Add(studentId, lessonRegister.LessonId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);


        }
    }
}
