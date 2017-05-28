using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Album;

namespace Interfaces.Repositories
{
    public interface IAlbumRepository : IAlbumRepository<Album>
    {
    }

    public interface IAlbumRepository<TAlbum> : IBaseRepository<TAlbum> where TAlbum : class
    {
    }
}
