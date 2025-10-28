using StudentApp.Core.Utilities.Results;
using StudentApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Business.Abstract
{
    public interface INoteService
    {

        IResult Add(Note note);
        IDataResult<List<Note>> GetAll();



    }
}
