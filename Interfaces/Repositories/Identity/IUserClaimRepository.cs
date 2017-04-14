using System.Collections.Generic;
using Domain.Identity;

namespace Interfaces.Repositories.Identity
{
    public interface IUserClaimRepository : IUserClaimRepository<UserClaim>
    {
    }

    //public interface IUserClaimRepository : IUserClaimRepository<UserClaim>
    //{
    //}

    public interface IUserClaimRepository<TUserClaim> : IBaseRepository<TUserClaim>
        where TUserClaim : class
    {
        List<TUserClaim> AllIncludeUser();
    }
}