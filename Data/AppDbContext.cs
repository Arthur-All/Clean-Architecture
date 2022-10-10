﻿using ApiPerson.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPerson.Data
{
    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Person> Persons { get; set; }

    }

}
