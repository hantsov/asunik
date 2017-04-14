using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Interfaces;
using Interfaces.Repositories;

namespace DAL.Repositories
{
    //public class UserRepository : EFRepository<User>, IUserRepository
    //{

    //    public UserRepository(IDbContext repositoryDbContext) : base(repositoryDbContext)
    //    {
    //    }

    //    public List<User> GetUsersOrderyByName()
    //    {
    //        return RepositoryDbSet.OrderBy(a => a.Firstname).Take(10).ToList();
    //    }

    //    public List<User> test()
    //    {
    //        var query = RepositoryDbSet.AsQueryable();
    //        query = query.Where(a => a.Firstname.Contains("Pets"));
    //        query = query.OrderBy(u => u.Birthdate);
    //        return query.ToList();
    //    } 
    //}
}
