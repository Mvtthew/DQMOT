using DQMOT.Entities;
using Microsoft.EntityFrameworkCore;

namespace DQMOT.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<User> Users { get; set; }
}