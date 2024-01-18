using System.ComponentModel.DataAnnotations.Schema;
using ExpensePaymentSystem.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ExpensePaymentSystem.Data.Entity;

[Table("Contact", Schema = "dbo")]
public class Contact : BaseEntityWithId
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public string ContactType { get; set; }
    public string Information { get; set; }
    public bool IsDefault { get; set; }
}
public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.InsertUserId).IsRequired(true);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.UpdateUserId).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
        
        builder.Property(x => x.UserId).IsRequired(true);
        builder.Property(x => x.ContactType).IsRequired(true).HasMaxLength(10);
        builder.Property(x => x.Information).IsRequired(true).HasMaxLength(100);
        builder.Property(x => x.IsDefault).IsRequired(true).HasDefaultValue(false);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.Information, x.ContactType }).IsUnique(true);
    }
}