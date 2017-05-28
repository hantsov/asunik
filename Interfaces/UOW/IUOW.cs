using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Interfaces.Repositories;
using Interfaces.Repositories.Identity;

namespace Interfaces.UOW
{
    public interface IUow : IDisposable
    {
        //save pending changes to the data store
        void Commit();
        void RefreshAllEntities();

        //get repository for type
        T GetRepository<T>() where T : class;

        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IEventRepository Events { get; }
        ICourseRepository Courses { get; }
        ICourseMemberRepository CourseMembers { get; }
        IAlbumRepository Albums { get; }
        IAlbumPhotoRepository AlbumPhotos { get; }
    }
}