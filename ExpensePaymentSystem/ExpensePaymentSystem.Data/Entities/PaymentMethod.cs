using System.ComponentModel.DataAnnotations.Schema;
using ExpensePaymentSystem.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ExpensePaymentSystem.Data.Entity;

[Table("PaymentMethod", Schema = "dbo")]
public class PaymentMethod : BaseEntityWithId
{
    public string PaymentMethodType { get; set; }   
}

public class PaymentMethodClaimConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.InsertUserId).IsRequired(true);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.UpdateUserId).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
    
        builder.Property(x => x.PaymentMethodType).IsRequired(true).HasMaxLength(50);

    }
}