using System.ComponentModel.DataAnnotations.Schema;
using ExpensePaymentSystem.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ExpensePaymentSystem.Data.Entity;

[Table("ExpenseClaim", Schema = "dbo")]
public class ExpenseClaim : BaseEntityWithId
{
    public int EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public int PaymentMethodId { get; set; }
    public virtual PaymentMethod PaymentMethod { get; set; }
    public string PaymentLocation { get; set; }
    public string ReceiptNumber { get; set; }
    public string Status { get; set; }
    public string StatusDescription { get; set; }
    public bool IsProcessed { get; set; }
    public decimal Amount { get; set; }
    public bool IsDefault { get; set; }
    public DateTime ClaimDate { get; set; }
    public DateTime ConfirmDate { get; set; }
   
}

public class ExpenseClaimConfiguration : IEntityTypeConfiguration<ExpenseClaim>
{
    public void Configure(EntityTypeBuilder<ExpenseClaim> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.InsertUserId).IsRequired(true);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.UpdateUserId).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
    
        builder.Property(x => x.EmployeeId).IsRequired(true);
        builder.Property(x => x.CategoryId).IsRequired(true);
        builder.Property(x => x.PaymentMethodId).IsRequired(true);
        builder.Property(x => x.PaymentLocation).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.ReceiptNumber).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Status).IsRequired(false).HasMaxLength(50).HasDefaultValue("On hold");
        builder.Property(x => x.StatusDescription).IsRequired(false).HasMaxLength(50).HasDefaultValue("StatusDescription");
        builder.Property(x => x.IsProcessed).IsRequired(true).HasDefaultValue(false);
        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(18, 4);
        builder.Property(x => x.IsDefault).IsRequired(true).HasDefaultValue(false);
        builder.Property(x => x.ClaimDate).IsRequired(true).HasDefaultValue(DateTime.Now);
        builder.Property(x => x.ConfirmDate).IsRequired(true);

        builder.HasIndex(x => x.EmployeeId);
        builder.HasIndex(x => x.CategoryId);
        builder.HasIndex(x => x.PaymentMethodId);
    
    }
}