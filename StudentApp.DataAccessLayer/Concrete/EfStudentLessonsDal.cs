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
    public class EfStudentLessonsDal:EfEntityRepositoryBase<StudentLessons,MyContext>,IStudentLessonsDal
    {
        public EfStudentLessonsDal(MyContext context):base(context) { }
        
            
        
    }
}
