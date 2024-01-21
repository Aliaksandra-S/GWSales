using System.Linq.Expressions;

namespace GWSales.Data.Interfaces;

public interface IRepository<T>
{
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    IQueryable<T> GetAll();
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
}
