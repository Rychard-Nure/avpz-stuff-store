using DataLayer.Data;
using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly GameStoreDBContext dbContext;

        public Repository(GameStoreDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await dbContext.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public void Delete(TEntity entity) 
            => dbContext.Set<TEntity>().Remove(entity);

        public async Task DeleteByIdAsync(int id)
            => dbContext.Set<TEntity>().Remove(await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id));

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await dbContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(int id)
            => await dbContext.Set<TEntity>()
                              .FirstOrDefaultAsync(x => x.Id == id);

        public TEntity Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
            return entity;
        }
    }
}
