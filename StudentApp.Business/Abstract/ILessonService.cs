using StudentApp.Core.Utilities.Results;
using StudentApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Business.Abstract
{
    public interface ILessonService
    {

        IDataResult<List<Lesson>> GetAll();

        IResult Add(Lesson lesson);

        IResult Update(Lesson lesson);
        IResult Delete(Lesson lesson);



    }
}
