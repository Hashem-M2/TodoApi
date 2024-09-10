using DTOS.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DTOS.TodoDto
{
    public class TodoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
      

    }
}
