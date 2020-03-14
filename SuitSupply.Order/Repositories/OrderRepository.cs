using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuitSupply.Order.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SuitSupplyContext _context;

        public OrderRepository(SuitSupplyContext context)
        {
            _context = context;
        }
        
        public async Task Add(Domain.Order order)
        {
            _context.Set<Domain.Order>().Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Domain.Order order)
        {
            _context.Entry(order).State = EntityState.Modified; 
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Order> Get(Guid id)
        {
            return await  _context.Set<Domain.Order>().Include("Alterations").FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Domain.Order> GetAll(Expression<Func<Domain.Order, bool>> expression)
        {
            return   _context.Set<Domain.Order>().Include("Alterations").Where(expression);
        }
    }
}