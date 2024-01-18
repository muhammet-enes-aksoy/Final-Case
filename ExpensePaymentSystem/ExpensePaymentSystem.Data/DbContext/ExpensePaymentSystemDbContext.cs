using ExpensePaymentSystem.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Data;
public class ExpensePaymentSystemDbContext : DbContext
{
    public ExpensePaymentSystemDbContext(DbContextOptions<ExpensePaymentSystemDbContext> options): base(options)
    {
    
    }   
    
    // dbset 
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categorys { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ExpenseClaim> ExpenseClaims { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseClaimConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
        base.OnModelCreating(modelBuilder);
    }
    
}