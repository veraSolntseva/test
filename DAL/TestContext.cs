using DAL.DbObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class TestContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<StudentsInGroups> StudentsInGroups {get; set;}


        public TestContext(DbContextOptions<TestContext> options) : base(options) { }
    }
}
