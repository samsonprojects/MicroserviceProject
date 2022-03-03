using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain.Entities;

namespace OrderApi.Service.v1.Command
{
    public class PayOrderCommandHandler : IRequestHandler<PayOrderCommand,Order>
    {
        public IOrderRepository _orderRepository { get; private set; }
        public PayOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.UpdateAsync(request.Order);
        }
        
    }
}