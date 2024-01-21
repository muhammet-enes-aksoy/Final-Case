using System.ComponentModel.DataAnnotations.Schema;
using ExpensePaymentSystem.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ExpensePaymentSystem.Data.Entity;

[Table("Employee", Schema = "dbo")]
public class Employee : BaseEntityWithId
{
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }  
    public virtual List<Contact> Contacts { get; set; }
    public virtual List<Account> Accounts { get; set; }
    public virtual List<Address> Addresses { get; set; }
    public virtual List<ExpenseClaim> ExpenseClaims { get; set; }
    public DateTime LastActivityDate { get; set; }
    public int PasswordRetryCount { get; set; }
    public int Status { get; set; }
}

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.InsertUserId).IsRequired(true);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.UpdateUserId).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
    
        builder.Property(x => x.IdentityNumber).IsRequired(true).HasMaxLength(11);
        builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Role).IsRequired(true).HasMaxLength(30);

        builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Password).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.LastActivityDate).IsRequired(true);
        builder.Property(x => x.PasswordRetryCount).IsRequired(true).HasDefaultValue(0);
        builder.Property(x => x.Status).IsRequired(true).HasDefaultValue(1);

        builder.HasIndex(x => x.UserName).IsUnique(true);

        builder.HasIndex(x => x.IdentityNumber).IsUnique(true);
        
        builder.HasMany(x => x.Accounts)
            .WithOne(x => x.Employee)
            .HasForeignKey(x => x.EmployeeId)
            .IsRequired(false);

        builder.HasMany(x => x.Contacts)
            .WithOne(x => x.Employee)
            .HasForeignKey(x => x.EmployeeId)
            .IsRequired(false);
        
        builder.HasMany(x => x.Addresses)
            .WithOne(x => x.Employee)
            .HasForeignKey(x => x.EmployeeId)
            .IsRequired(false);

        builder.HasMany(x => x.ExpenseClaims)
            .WithOne(x => x.Employee)
            .HasForeignKey(x => x.EmployeeId)
            .IsRequired(false);
    }
}