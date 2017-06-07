using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repositories;
using DAL.Repositories;
using Domain;
using Domain.Event;
using Interfaces;

namespace DAL.Repositories
{
    public class EventsRepository : EFRepository<Event>, IEventRepository
    {
        public EventsRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public List<Event> GetByType(string type)
        {
            return DbSet.Include(e => e.EventMembers.Select(em => em.Member)).Where(e => e.Type == type).ToList();
        }
    }
}
