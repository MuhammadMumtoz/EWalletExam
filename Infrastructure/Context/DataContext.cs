using Domain.Entities;
using Microsoft.EntityFrameworkCore;
// using static System.Collections.Immutable.ImmutableArray<T>;

namespace Infrastructure.Context;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Wallet> Wallets {get; set;}
       
}