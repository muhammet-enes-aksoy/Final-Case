using System.ComponentModel.DataAnnotations.Schema;
using ExpensePaymentSystem.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ExpensePaymentSystem.Data.Entity;

[Table("Account", Schema = "dbo")]
public class Account : BaseEntityWithId
{
    public int EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
    public int AccountNumber { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyType { get; set; }
    public string Name { get; set; }
    public DateTime OpenDate { get; set; }
}

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(x => x.AccountNumber).ValueGeneratedNever();
        
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.InsertUserId).IsRequired(true);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.UpdateUserId).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.EmployeeId).IsRequired(true);
        builder.Property(x => x.AccountNumber).IsRequired(true);
        builder.Property(x => x.IBAN).IsRequired(true).HasMaxLength(34);
        builder.Property(x => x.Balance).IsRequired(true).HasPrecision(18, 4);
        builder.Property(x => x.CurrencyType).IsRequired(true).HasMaxLength(3);
        builder.Property(x => x.Name).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.OpenDate).IsRequired(true);

        builder.HasIndex(x => x.EmployeeId);
        builder.HasIndex(x => x.AccountNumber).IsUnique(true);
        
    }
}