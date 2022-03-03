using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain.Entities;

namespace OrderApi.Service.v1.Command
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        public IOrderRepository _orderRespository { get; }
        public CreateOrderCommandHandler(IOrderRepository orderRespository)
        {
            _orderRespository = orderRespository;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRespository.AddAsync(request.Order);
        }
    }
}