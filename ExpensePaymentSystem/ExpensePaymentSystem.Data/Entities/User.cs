using System.ComponentModel.DataAnnotations.Schema;
using ExpensePaymentSystem.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ExpensePaymentSystem.Data.Entity;

[Table("User", Schema = "dbo")]
public class User : BaseEntityWithId
{
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int UserNumber { get; set; }
    public string Role { get; set; }
    
    public virtual List<Contact> Contacts { get; set; }
    public virtual List<Account> Accounts { get; set; }
    public virtual List<Address> Addresses { get; set; }
    public virtual List<ExpenseClaim> ExpenseClaims { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
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