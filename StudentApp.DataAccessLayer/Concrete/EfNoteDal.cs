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
    public class EfNoteDal : EfEntityRepositoryBase<Note, MyContext>, INoteDal

    {
        public EfNoteDal(MyContext context) : base(context)
        {

        }
    }
}
