using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entites;
using MultiShop.Order.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Persistence.Repositories
{
    public class OrderingRepository : IOrderingRepository
    {
        private readonly OrderContext _orderContext;

        public OrderingRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        public List<Ordering> GetOrderingsByUserID(string userID)
        {
           var values= _orderContext.Orderings.Where(o => o.OrderingUserID == userID).ToList();
              return values;
        }
    }
}
