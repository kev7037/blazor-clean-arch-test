using Mc2.CrudTest.Core.Domain.Customers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Mc2.CrudTest.Infrastructures.Command.Customers
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.Id).IsRequired().UseIdentityColumn();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.PhoneNumber).IsRequired().HasColumnType("DECIMAL(15,0)");
            builder.Property(x => x.Email).IsRequired().HasColumnType("VARCHAR(255)");
            builder.Property(x => x.BankAccountNumber).IsRequired().HasColumnType("VARCHAR(35)");

            builder
            .HasIndex(c => new { c.Email })
            .IsUnique();

            builder
            .HasIndex(c => new { c.FirstName, c.LastName, c.DateOfBirth })
            .IsUnique();
        }
    }
}