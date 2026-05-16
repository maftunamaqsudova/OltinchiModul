using Lesson2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lesson2.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(x => x.StudentId);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(12);

            builder.Property(x => x.Grade)
                .IsRequired();
            
        }
    }
}
