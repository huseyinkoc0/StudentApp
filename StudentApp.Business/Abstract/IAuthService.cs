
using StudentApp.Core.Utilities.Results;
using StudentApp.Core.Utilities.Security.JWT;
using StudentApp.Entities.Concrete;
using StudentApp.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Business.Abstract
{
    public interface IAuthService
    {
        IResult RegisterStudent(RegisterDto registerDto);
        IResult RegisterTeacher(RegisterDto registerDto);
        IDataResult<AccessToken> Login(LoginDTO loginDto);
        public IDataResult<AccessToken> LoginTeacher(LoginDTO loginDto);


    }
}
