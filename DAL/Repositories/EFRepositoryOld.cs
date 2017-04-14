//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Migrations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Interfaces;
//using Interfaces.Repositories;

//namespace DAL.Repositories
//{
//    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
//    {
//        protected DbContext RepositoryDbContext;
//        protected DbSet<TEntity> RepositoryDbSet;

//        public EFRepository(IDbContext repositoryDbContext)
//        {
//            if (repositoryDbContext == null)
//            {
//                throw new ArgumentNullException(nameof(repositoryDbContext));
//            }
//            RepositoryDbContext = repositoryDbContext as DbContext;
//            RepositoryDbSet = RepositoryDbContext.Set<TEntity>();
//            if (RepositoryDbSet == null)
//            {
//                throw new NotSupportedException(nameof(RepositoryDbSet));
//            }

//        }

//        public List<TEntity> All => RepositoryDbSet.ToList();

//        public void Add(TEntity entity)
//        {
//            RepositoryDbSet.Add(entity);
//        }

//        public void Remove(TEntity entity)
//        {
//            RepositoryDbSet.Remove(entity);
//        }

//        public void Remove(int id)
//        {
//            RepositoryDbSet.Remove(entity: RepositoryDbSet.Find(id));
//        }

//        public TEntity Find(int id)
//        {
//            return RepositoryDbSet.Find(id);
//        }

//        public int Count()
//        {
//            return RepositoryDbSet.Count();
//        }

//        public int Count(Func<TEntity, bool> func)
//        {
//            return RepositoryDbSet.Count(func);
//        }

//        public int SaveChanges()
//        {
//            return RepositoryDbContext.SaveChanges();
//        }

//        public void Update(TEntity entity)
//        {
//            RepositoryDbContext.Entry(entity).State = EntityState.Modified;
//        }
//    }
//}
