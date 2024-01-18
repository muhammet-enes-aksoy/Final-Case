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
    public int ReceiptNumber { get; set; }
    public string Status { get; set; }
    public string StatusDescription { get; set; }
    public string IsActive { get; set; }
    public string Amount { get; set; }
    public DateTime ClaimDate { get; set; }
    public DateTime ConfirmDate { get; set; }
    
}

public class ExpenseClaimConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.InsertUserId).IsRequired(true);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.UpdateUserId).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
    
        builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Role).IsRequired(true).HasMaxLength(30);

        //builder.HasIndex(x => x.UserName).IsUnique(true);

    }
}