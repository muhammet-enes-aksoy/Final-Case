using System.ComponentModel.DataAnnotations.Schema;
using ExpensePaymentSystem.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ExpensePaymentSystem.Data.Entity;

[Table("ExpenseClaim", Schema = "dbo")]
public class ExpenseClaim : BaseEntityWithId
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public int PaymentMethodId { get; set; }
    public virtual PaymentMethod PaymentMethod { get; set; }
    public string PaymentLocation { get; set; }
    public string ReceiptNumber { get; set; }
    public string Status { get; set; }
    public string StatusDescription { get; set; }
    public bool IsProcessed { get; set; }
    public double Amount { get; set; }
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
    
        builder.Property(x => x.CategoryId).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.UserId).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.PaymentMethodId).IsRequired(true).HasMaxLength(30);
    }
}