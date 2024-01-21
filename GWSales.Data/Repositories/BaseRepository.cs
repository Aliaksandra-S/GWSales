using GWSales.Data.Interfaces;
using System.Linq.Expressions;


namespace GWSales.Data;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly SalesDbContext _dbContext;

    public BaseRepository(SalesDbContext context)
    {
        _dbContext = context;
    }
    public void Create(T entity) => _dbContext.Set<T>().Add(entity);

    public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

    public void Update(T entity) => _dbContext.Set<T>().Update(entity);

    public IQueryable<T> GetAll() => _dbContext.Set<T>();

    public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression) =>
        _dbContext.Set<T>().Where(expression);
}
    
