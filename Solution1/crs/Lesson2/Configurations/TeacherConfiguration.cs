using Lesson2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lesson2.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Teachers");

            builder.HasKey(x => x.TeacherId);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(x => x.LastName) 
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property (x => x.Subject)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property (x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(12);

            builder.HasMany(t => t.Students)
       .WithMany(s => s.Teachers)
       .UsingEntity<Dictionary<string, object>>(
           "Teacher_Student", // O'rtadagi bog'lovchi jadval nomi (Soya entity)
           j => j.HasOne<Student>().WithMany().HasForeignKey("StudentId"),
           j => j.HasOne<Teacher>().WithMany().HasForeignKey("TeacherId"),
           j =>
           {
               j.HasKey("TeacherId", "StudentId"); // Ikkala ID birlashib kalit bo'ladi
           });



        }
    }
}
