using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repositories
{
    public interface IAlbumPhotoRepository
    {
    }

    public interface IAlbumPhotoRepository<TAlbumPhoto> : IBaseRepository<TAlbumPhoto> where TAlbumPhoto : class
    {
        
    }
}
