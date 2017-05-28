using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Album;
using Interfaces;
using Interfaces.Repositories;

namespace DAL.Repositories
{
    public class AlbumRepository : EFRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
