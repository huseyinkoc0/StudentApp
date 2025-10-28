using StudentApp.Core.Utilities.Results;
using StudentApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Business.Abstract
{
    public interface ITeacherService
    {
        IDataResult<List<Teacher>> GetAll();
        IResult Add(Teacher teacher);

        IDataResult<List<Student>> GetStudentsByTeacherId(int teacherId);
    }
}
