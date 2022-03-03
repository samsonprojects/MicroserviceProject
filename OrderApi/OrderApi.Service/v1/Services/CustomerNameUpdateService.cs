using System;
using System.Diagnostics;
using MediatR;
using OrderApi.Service.v1.Command;
using OrderApi.Service.v1.Models;
using OrderApi.Service.v1.Query;

namespace OrderApi.Service.v1.Services
{
    public class CustomerNameUpdateService : ICustomerNameUpdateService
    {
        public IMediator _mediator { get; private set; }
        public CustomerNameUpdateService(IMediator mediator)
        {
            _mediator= mediator;
        }

        public async void UpdateCustomerNameInOrders(UpdateCustomerFullNameModel updateCustomerFullNameModel)
        {
            try
            {
                var ordersOfCustomer = await _mediator.Send(new GetOrderByCustomerGuidQuery
                {
                    CustomerId = updateCustomerFullNameModel.Id 
                });

                if(ordersOfCustomer.Count != 0)
                {
                    ordersOfCustomer.ForEach(x=> 
                        x.CustomerFullName = $"{updateCustomerFullNameModel.FirstName} {updateCustomerFullNameModel.LastName}");
                }

                await _mediator.Send(new UpdateOrderCommand
                {
                    Orders = ordersOfCustomer
                });
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}