using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                   : base(options)
        {
        }
        public DbSet<TodoItem> TodoItems { get; set; }
   
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the one-to-many relationship between User and TodoItem
            builder.Entity<TodoItem>()
                .HasOne(ti => ti.User)
                .WithMany(u => u.TodoItems)
                .HasForeignKey(ti => ti.UserId);
        }
    }
}
