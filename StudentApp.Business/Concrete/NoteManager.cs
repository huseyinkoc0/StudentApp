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
    public class NoteManager : INoteService
    {
        private readonly INoteDal _noteDal;
        public NoteManager(INoteDal noteDal)
        {
            _noteDal = noteDal;
            
        }
        public IResult Add(Note note)
        {
            _noteDal.Add(note);
            return new Result(true);

        }

        public IDataResult<List<Note>> GetAll()
        {
            return new SuccessDataResult<List<Note>>(_noteDal.GetAll());
        }
    }
}
