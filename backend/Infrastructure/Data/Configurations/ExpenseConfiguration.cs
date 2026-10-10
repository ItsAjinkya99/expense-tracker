using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ExpenseConfiguration: IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title).IsRequired().HasMaxLength(200);
        
        // Map the money valueobject to flat columns in the expense table
        builder.ComplexProperty(e=> e.Amount, amountBuilder =>
        {
            amountBuilder.Property(m=>m.Amount).HasColumnName("Amount").IsRequired();
            amountBuilder.Property(m=>m.Currency).HasColumnName("Currency").HasMaxLength(3).IsRequired();
        });
        
        // Relations
        builder.HasOne<User>().WithMany().HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Category>().WithMany().HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Restrict);
    }
}