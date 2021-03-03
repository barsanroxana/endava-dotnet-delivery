using FoodPal.Deliveries.Data.Abstractions;
using FoodPal.Deliveries.Domain;
using System.Threading.Tasks;

namespace FoodPal.Deliveries.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DeliveryDbContext _notificationDbContext;

        public UnitOfWork(DeliveryDbContext notificationDbContext)
        {
            this._notificationDbContext = notificationDbContext;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {
            return new Repository<TEntity>(this._notificationDbContext);
        }

        public async Task<bool> SaveChangesAsnyc() => await this._notificationDbContext.SaveChangesAsync() > 0;
    }
}
