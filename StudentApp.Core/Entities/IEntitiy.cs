using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Entities
{
    public interface IEntitiy
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }




    }
}
