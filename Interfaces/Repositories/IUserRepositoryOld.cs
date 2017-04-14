using System;
using System.Collections.Generic;
using Domain;
using Domain.Identity;

namespace Interfaces.Repositories
{
    public interface IUserRepositoryOld : IRepository<User>
    {
        List<User> GetUsersOrderyByName();
    }
}
