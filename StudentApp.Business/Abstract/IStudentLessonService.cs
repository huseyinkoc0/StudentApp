using StudentApp.Core.Utilities.Results;
using StudentApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Business.Abstract
{
    public interface IStudentLessonService
    {
        IDataResult<List<StudentLessons>>GetStudentsByLessonId(int lessonId);
        IDataResult<List<StudentLessons>> GetAll();
        IResult Add(int studentId ,int lessonId);
        IResult Update(StudentLessons studentLessons);
        IDataResult<List<StudentLessons>> GetStudentLessons(int studentId);

    }
}
