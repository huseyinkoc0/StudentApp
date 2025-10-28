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
    public class LessonManager : ILessonService
    {
        private readonly ILessonDal _lessonDal;
        public LessonManager(ILessonDal lessonDal)
        {
            _lessonDal = lessonDal;
        }
        public IResult Add(Lesson lesson)
        {
           _lessonDal.Add(lesson);
            return new SuccessResult("Ders Eklendi");
        }

        public IResult Delete(Lesson lesson)
        {
            _lessonDal.Delete(lesson);
            return new SuccessResult("Ders silindi");
        }

        public IDataResult<List<Lesson>> GetAll()
        {
           var result = _lessonDal.GetAll();
            return new SuccessDataResult<List<Lesson>>(result);
            
        }

        public IResult Update(Lesson lesson)
        {
            _lessonDal.Update(lesson);
            return new SuccessResult("Ders Eklendi");
        }
    }
}
