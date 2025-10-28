using StudentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Entities.Concrete
{
    public class Teacher : IEntitiy
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Name {  get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
