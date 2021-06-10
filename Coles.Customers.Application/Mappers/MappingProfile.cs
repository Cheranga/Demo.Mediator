using AutoMapper;
using Coles.Customers.Application.Queries;
using Coles.Customers.Application.Requests;

namespace Coles.Customers.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetCustomerByEmailRequest, GetCustomerByEmailQuery>();
        }
    }
}