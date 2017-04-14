using System.Data.Entity.ModelConfiguration;
using Domain.Identity;

namespace DAL.EFConfiguration
{
    public class UserClaimMap : EntityTypeConfiguration<UserClaim>
    {
        public UserClaimMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.UserId)
                .IsRequired();
        }
    }
}