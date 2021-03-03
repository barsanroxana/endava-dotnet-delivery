using FoodPal.Deliveries.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodPal.Deliveries.Data.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> FindByIdAsync(int id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(int id);
    }
}
