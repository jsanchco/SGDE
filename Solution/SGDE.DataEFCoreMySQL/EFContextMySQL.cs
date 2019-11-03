// ReSharper disable InconsistentNaming
namespace SGDE.DataEFCoreMySQL
{
    #region Using

    using System.Threading;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Configurations;

    #endregion

    public class EFContextMySQL : DbContext
    {
        #region Members

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Profession> Profession { get; set; }

        public static long InstanceCount;

        #endregion

        public EFContextMySQL(DbContextOptions options) : base(options) => Interlocked.Increment(ref InstanceCount);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserConfiguration(modelBuilder.Entity<User>());
            new AddressConfiguration(modelBuilder.Entity<Address>());
            new ProfessionConfiguration(modelBuilder.Entity<Profession>());
        }
    }
}
