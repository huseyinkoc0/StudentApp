using StudentApp.Business.Abstract;
using StudentApp.Core.Utilities.Results;
using StudentApp.DataAccessLayer.Abstract;
using StudentApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Business.Concrete
{
    public class StudentLessonManager : IStudentLessonService
    {
        private readonly IStudentLessonsDal _studentLessonDal;
        public StudentLessonManager(IStudentLessonsDal studentLessonDal)
        {
            _studentLessonDal = studentLessonDal;
            
        }
        public IResult Add(int studentId ,int lessonId)
        {
            StudentLessons st=new StudentLessons();
            st.StudentId = studentId;
            st.LessonId = lessonId;
            st.CreatedAt = DateTime.Now;
          _studentLessonDal.Add(st);
            return new SuccessResult("Ders Kaydı gerçekleşti");
        }

        public IDataResult<List<StudentLessons>> GetAll()
        {
            return new SuccessDataResult<List<StudentLessons>>(_studentLessonDal.GetAll());
        }

        public IDataResult<List<StudentLessons>> GetStudentLessons(int studentId)
        {
           var result=_studentLessonDal.GetAll().Where(p=> p.StudentId == studentId).ToList();
            return new SuccessDataResult<List<StudentLessons>>(result,"Öğrenciye ait dersler.");
        }

        public IDataResult<List<StudentLessons>> GetStudentsByLessonId(int lessonId)
        {
            var result = _studentLessonDal.GetAll().Where(p=>p.LessonId == lessonId).ToList();
            return new SuccessDataResult<List<StudentLessons>>(result, "Derse ait Öğrenciler");
        }

        public IResult Update(StudentLessons studentLessons)
        {
            _studentLessonDal.Update(studentLessons);
            return new SuccessResult("Ders Kaydı Güncellendi");
        }
    }
}
