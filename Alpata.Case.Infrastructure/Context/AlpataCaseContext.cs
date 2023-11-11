using Alpata.Case.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using System.Text;

namespace Alpata.Case.Infrastructure.Context
{
    public class AlpataCaseContext : DbContext
    {
        private readonly byte[] _encryptionKey = Encoding.ASCII.GetBytes("R5u7x!A%D*G-KaPdSgVkYp3s6v9y/BzM");
        private readonly byte[] _encryptionIV = Encoding.ASCII.GetBytes("R5u7x!A%D*G-KaPd");
        private readonly IEncryptionProvider _encryptionProvider;
        public AlpataCaseContext(DbContextOptions<AlpataCaseContext> options) : base(options)
        {
            _encryptionProvider = new AesProvider(_encryptionKey, _encryptionIV);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<MeetingAttachment> MeetingAttachments { get; set; }

        protected   void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.UseEncryption(_encryptionProvider);
            _ = modelBuilder.Entity<User>().Property(e => e.Id).ValueGeneratedOnAdd();
            _ = modelBuilder.Entity<Meeting>().Property(e => e.Id).ValueGeneratedOnAdd();
            _ = modelBuilder.Entity<MeetingAttachment>().Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}
