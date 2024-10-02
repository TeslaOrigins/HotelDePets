using HDP.Persistence.Contexts;
using HDP.Persistence.Repository.Contracts;

namespace HDP.Persistence.Repository.Implementations
{

    public class GeneralRepository : IGeneralRepository
    {
        protected readonly HDPContext _context;

        public GeneralRepository(HDPContext context)
        {
            _context = context;
        }

        public virtual void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public virtual void AddRange<T>(T[] entityArray) where T:class
        {
            _context.AddRange(entityArray);
        }
        public virtual void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public virtual void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public virtual void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}