using StudentApp.Core.DataAccess.EntityFramework;
using StudentApp.DataAccessLayer.Abstract;
using StudentApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.DataAccessLayer.Concrete
{
    public class EfTeacherDal : EfEntityRepositoryBase<Teacher, MyContext>, ITeacherDal
    {
        public EfTeacherDal(MyContext context) : base(context) { 
        }
        public List<Student> GetStudentsEnrolledToTeache(int teacherid)
        {


            var students = _context.Lessons.Where(l => l.TeacherId == teacherid).SelectMany(l => l.StudentLessons).Select(sc => sc.Student).Distinct().ToList();

            return students;


        }


    }
}
