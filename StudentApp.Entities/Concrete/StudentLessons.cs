using StudentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Entities.Concrete
{
    public class StudentLessons : IEntitiy
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<Note>? Grades { get; set; }

    }
}
