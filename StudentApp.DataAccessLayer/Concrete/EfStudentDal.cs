using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApp.Core.DataAccess.EntityFramework;
using StudentApp.DataAccessLayer.Abstract;
using StudentApp.Entities.Concrete;

namespace StudentApp.DataAccessLayer.Concrete
{
    public class EfStudentDal : EfEntityRepositoryBase<Student, MyContext>, IStudentDal
    {
        public EfStudentDal(MyContext context) : base(context)
        {

        }

    
    }
}
