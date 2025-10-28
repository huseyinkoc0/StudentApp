using StudentApp.Core.DataAccess;
using StudentApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.DataAccessLayer.Abstract
{
    public interface IStudentLessonsDal:IEntityRepository<StudentLessons>
    {
    }
}
