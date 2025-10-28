using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Business.Abstract;
using StudentApp.Entities.DTOs;

namespace StudentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous] // Bu endpoint için token'a (giriş yapmaya) gerek yok
        [HttpPost("student/loginStudent")] // POST http://localhost.../api/auth/student/login
        public IActionResult LoginStudent(LoginDTO loginDto)
        {
            var result = _authService.Login(loginDto);

            if (result.Success)
            {
                // İşlem başarılıysa, 200 OK ve token verisini döndür
                return Ok(result);
            }

            // İşlem başarısızsa (örn: parola yanlış), 400 Bad Request ve hata mesajını döndür
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("student/registerStudent")] // POST http://localhost.../api/auth/student/register
        public IActionResult RegisterStudent(RegisterDto registerDto)
        {
            var result = _authService.RegisterStudent(registerDto);

            if (result.Success)
            {
                // İşlem başarılıysa, 200 OK ve başarı mesajını döndür
                return Ok(result);
            }

            // İşlem başarısızsa (örn: e-posta zaten var), 400 Bad Request ve hata mesajını döndür
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("teacher/login")] // POST http://localhost.../api/auth/teacher/login
        public IActionResult LoginTeacher(LoginDTO loginDto)
        {
            var result = _authService.LoginTeacher(loginDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("teacher/register")] // POST http://localhost.../api/auth/teacher/register
        public IActionResult RegisterTeacher(RegisterDto registerDto)
        {
            var result = _authService.RegisterTeacher(registerDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
