using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SuitSupply.Order
{
    public interface IOrderRepository
    {
        Task Add(Domain.Order order);
        Task Update(Domain.Order order);
        Task <Domain.Order> Get(Guid id);
        IQueryable<Domain.Order> GetAll(Expression<Func<Domain.Order, bool>> expression);
    }
}