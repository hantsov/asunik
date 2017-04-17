using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Event;

namespace Interfaces.Repositories
{
    public interface IEventRepository : IEventRepositoryBase<Event>
    {
    }

    public interface IEventRepositoryBase<TCourse> : IBaseRepository<TCourse> where TCourse : class
    {
    }
}
