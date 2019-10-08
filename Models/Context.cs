using System;
using Microsoft.EntityFrameworkCore;
namespace BeltExam.Models
{
    public class Context: DbContext
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get;set;}
        public DbSet<Item> Items {get;set;}
        public DbSet<Bid> Bids {get;set;}
    }
    
}