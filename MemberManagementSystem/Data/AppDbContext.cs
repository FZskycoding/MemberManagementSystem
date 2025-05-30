using Microsoft.EntityFrameworkCore;
using MemberManagementSystem.Models;
using System.Collections.Generic;

namespace MemberManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
