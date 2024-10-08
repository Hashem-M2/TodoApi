﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class User : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<TodoItem> TodoItems { get; set; }
    }
}
