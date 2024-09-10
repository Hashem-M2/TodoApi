using DTOS.UserDtos;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS.TodoDto
{
    public class TodoCreateDto
    {
        public string Title { get; set; }     
        public bool Completed { get; set; }

    }
}
