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
    public class EfLessonDal:EfEntityRepositoryBase<Lesson,MyContext>,ILessonDal
    {
        public EfLessonDal(MyContext context):base(context) 
        {
            
        }
    }
}
