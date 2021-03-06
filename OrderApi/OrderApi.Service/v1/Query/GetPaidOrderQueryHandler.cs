using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain.Entities;

namespace OrderApi.Service.v1.Query
{
    public class GetPaidOrderQueryHandler : IRequestHandler<GetPaidOrderQuery, List<Order>>
    {
        public IOrderRepository _orderRepository;
        public GetPaidOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            
        }

        public async Task<List<Order>> Handle(GetPaidOrderQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetPaidOrderAsync(cancellationToken);
        }
    }
}