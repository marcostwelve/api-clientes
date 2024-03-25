using System.Linq.Expressions;

namespace clientes.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        T Get(int id);
        Task<T> GetbyId(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
