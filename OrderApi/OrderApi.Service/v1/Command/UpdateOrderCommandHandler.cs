using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderApi.Data.Repository.v1;

namespace OrderApi.Service.v1.Command
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        public IOrderRepository _orderRespository { get; private set; }
        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRespository = orderRepository;
        }



        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderRespository.UpdateRangeAsync(request.Orders);
            return Unit.Value;
        }
    }
}