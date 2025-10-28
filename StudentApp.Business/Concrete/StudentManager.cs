using StudentApp.Business.Abstract;
using StudentApp.Core.Utilities.Results;
using StudentApp.Core.Utilities.Security;
using StudentApp.DataAccessLayer.Abstract;
using StudentApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Business.Concrete
{
    public class StudentManager:IStudentService
    {
        private readonly IStudentDal _studentDal;
        private readonly ILessonDal _lessonDal;
        public StudentManager(IStudentDal studentDal, ILessonDal lessonDal)
        {
            _studentDal = studentDal;
            _lessonDal = lessonDal;
        }

        public IResult Add(Student student)
        {
          
            _studentDal.Add(student);
            return new Result(true, "Öğrenci Eklendi");
        }

        public IDataResult<List<Student>> GetAll()
        {
            var students= _studentDal.GetAll();

            return new DataResult<List<Student>>(true,"Öğrenci Listesi",students);

        }

        

        public IResult Update(Student student)
        {
           _studentDal.Update(student);
            return new Result(true, "Güncellendi");
        }
    }
}
