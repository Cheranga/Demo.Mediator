using AutoMapper;
using Coles.Customers.Api.Requests;
using Coles.Customers.Application.Requests;

namespace Coles.Customers.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetCustomerByEmailRequestDto, GetCustomerByEmailRequest>();
        }
    }
}