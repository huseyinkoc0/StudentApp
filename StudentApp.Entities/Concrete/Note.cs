using StudentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Entities.Concrete
{
    public class Note : IEntitiy
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;

        public int StudentLessonId { get; set; }
        public virtual StudentLessons StudentLessons { get; set; }
        public double Score { get; set; }
        public string Type { get; set; }

      


    }
}
