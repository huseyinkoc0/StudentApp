using StudentApp.Business.Abstract;
using StudentApp.Core.Utilities.Results;
using StudentApp.DataAccessLayer.Abstract;
using StudentApp.DataAccessLayer.Concrete;
using StudentApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Business.Concrete
{
    public class TeacherManager : ITeacherService
    {
        private readonly ITeacherDal _manager;

        public TeacherManager(ITeacherDal manager)
        {
            _manager = manager;
        }


        public IDataResult<List<Student>> GetStudentsByTeacherId(int teacherId)
        {
            if (teacherId < 0)
            {
                return new ErrorDataResult<List<Student>>("TeacherID boş olamaz");
            }
            var students = _manager.GetStudentsEnrolledToTeache(teacherId);
            if (students == null || students.Count == 0)
            {
                return new SuccessDataResult<List<Student>>(
                new List<Student>(), // Boş liste döndür
                "Listelenecek öğrenci bulunamadı.");
            }

            return new SuccessDataResult<List<Student>>(students, "Öğrenciler başarıyla listelendi.");


        }
        public IResult Add(Teacher teacher)
        {
            _manager.Add(teacher);
            return new SuccessResult("Öğretmen eklendi");
        }

        public IDataResult<List<Teacher>> GetAll()
        {
            

            return new SuccessDataResult<List<Teacher>>(_manager.GetAll());
        }
    }
}
