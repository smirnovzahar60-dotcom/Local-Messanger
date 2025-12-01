using Microsoft.EntityFrameworkCore;
using LocalMessenger.Models;

namespace LocalMessenger.Data
{
    public class SettingsBD : DbContext
    {
        public SettingsBD(DbContextOptions<SettingsBD> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурация для User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            
            modelBuilder.Entity<User>()
                .Property(u => u.ExternalId)
                .IsRequired();
                
            modelBuilder.Entity<User>()
                .HasIndex(u => u.ExternalId)
                .IsUnique();

            // Конфигурация для Message
            modelBuilder.Entity<Message>()
                .HasKey(m => m.Id);
                
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Person)
                .WithMany()
                .HasForeignKey(m => m.UserId);
        }
    }
}