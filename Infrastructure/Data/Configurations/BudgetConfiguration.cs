using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class BudgetConfiguration:IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.HasKey(b => b.Id);

        builder.ComplexProperty(b => b.Limit, limitBuilder =>
        {
            limitBuilder.Property(m => m.Amount).HasColumnName("LimitAmount").IsRequired();
            
            limitBuilder.Property(m => m.Currency).HasColumnName("LimitCurrency").HasMaxLength(3).IsRequired();
        });
        
        builder.HasOne<User>().WithMany().HasForeignKey(b => b.UserId).OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Category>().WithMany().HasForeignKey(b => b.CategoryId).OnDelete(DeleteBehavior.Restrict);
    }
}