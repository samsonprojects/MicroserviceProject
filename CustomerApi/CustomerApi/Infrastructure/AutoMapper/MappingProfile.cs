using AutoMapper;
using CustomerApi.Models.v1;
using CustomerApi.Domain.Entities;

namespace CustomerApi.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerModel,Customer>().ForMember(x=> x.Id, opt=> opt.Ignore());
            CreateMap<UpdateCustomerModel,Customer>();
        }
    }
}