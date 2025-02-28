﻿using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Models
{
    public class ApplicationDbContext:DbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<Transaction> Transactions { get; set; } //veritabanı tablosu

    }
}
