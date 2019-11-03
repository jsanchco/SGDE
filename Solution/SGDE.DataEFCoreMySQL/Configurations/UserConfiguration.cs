namespace SGDE.DataEFCoreMySQL.Configurations
{
    #region Using

    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    #endregion

    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("User");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.Property(x => x.AddedDate).IsRequired();
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.BirthDate).IsRequired(false);
            entity.Ignore(x => x.Token);

            entity.HasIndex(x => x.ProfessionId).HasName("IFK_Profession_User");
            entity.HasOne(u => u.Profession).WithMany(a => a.Users).HasForeignKey(a => a.ProfessionId).HasConstraintName("FK__Profession__UserId");
        }
    }
}
