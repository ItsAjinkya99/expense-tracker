using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        // Restrict column length for database security
        builder.Property(param => param.Email).IsRequired().HasMaxLength(255);
        builder.Property(p=>p.FullName).IsRequired().HasMaxLength(100);
        
        // Ensure email is unique at db level
        builder.HasIndex(email => email.Email).IsUnique();  
    }
}