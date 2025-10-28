using Azure.Core;
using StudentApp.Business.Abstract;
using StudentApp.Core.Utilities.Results;
using StudentApp.Core.Utilities.Security;
using StudentApp.Core.Utilities.Security.JWT;
using StudentApp.DataAccessLayer.Abstract;
using StudentApp.Entities.Concrete;
using StudentApp.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AccessToken = StudentApp.Core.Utilities.Security.JWT.AccessToken;

namespace StudentApp.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IStudentDal _studentDal;
        private readonly ITeacherDal _teacherDal;
        private readonly ITokenHelper _tokenHelper;
        public AuthManager(IStudentDal studentDal, ITeacherDal teacherDal, ITokenHelper tokenHelper)
        {
            _studentDal = studentDal;
            _teacherDal = teacherDal;
            _tokenHelper = tokenHelper;
        }



        public IDataResult<AccessToken> Login(LoginDTO loginDto)
        {
            var student = _studentDal.Get(s => s.Name == loginDto.Name);
            if (student == null) return new ErrorDataResult<AccessToken>("Kullanıcı bulunamadı.");
            if (!HashingHelper.VerifyPasswordHash(loginDto.Password, student.PasswordHash, student.PasswordSalt))
            {
                return new ErrorDataResult<AccessToken>("Parola hatalı.");
            }
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()),

            new Claim(ClaimTypes.Name, student.Name),
            new Claim(ClaimTypes.Role, "Student")
        };
            var accessToken = _tokenHelper.CreateToken(claims);
            return new SuccessDataResult<AccessToken>(accessToken, "Giriş başarılı.");
        }

        public IDataResult<AccessToken> LoginTeacher(LoginDTO loginDto)
        {
            var teacher = _teacherDal.Get(t => t.Name == loginDto.Name);
            if (teacher == null) return new ErrorDataResult<AccessToken>("Kullanıcı bulunamadı.");
            if (!HashingHelper.VerifyPasswordHash(loginDto.Password, teacher.PasswordHash, teacher.PasswordSalt))
            {
                return new ErrorDataResult<AccessToken>("Parola hatalı.");
            }
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, teacher.Id.ToString()),
          
            new Claim(ClaimTypes.Name, teacher.Name),
            new Claim(ClaimTypes.Role, "Teacher")
        };
            var accessToken = _tokenHelper.CreateToken(claims);

            return new SuccessDataResult<AccessToken>(accessToken, "Giriş başarılı.");
        }

        public IResult RegisterStudent(RegisterDto registerDto)
        {
            HashingHelper.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            if (_studentDal.Get(s => s.Name == registerDto.Name) != null)
            {
                return new ErrorResult("Bu e-posta adresi zaten kayıtlı.");
            }
            var student = new Student
            {

                Name = registerDto.Name,
                // ... (FirstName, LastName, StudentNumber vb. DTO'dan ne geliyorsa)
                CreatedAt = DateTime.Now,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt

            };
            _studentDal.Add(student);
            return new SuccessResult("Öğrenci başarıyla kaydedildi.");
            throw new NotImplementedException();
        }

        public IResult RegisterTeacher(RegisterDto registerDto)
        {
            HashingHelper.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var teacher = new Teacher
            {
                Name = registerDto.Name,
                // ... (FirstName, LastName vb.)
                CreatedAt = DateTime.Now,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt

            };
            _teacherDal.Add(teacher);
            return new SuccessResult("Öğretmen başarıyla kaydedildi.");

        }



    }
}
