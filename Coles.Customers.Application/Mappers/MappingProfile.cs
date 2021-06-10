using AutoMapper;
using Coles.Customers.Application.Queries;
using Coles.Customers.Application.Requests;
using Coles.Customers.Infrastructure.DataAccess.Commands;

namespace Coles.Customers.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetCustomerByEmailRequest, GetCustomerByEmailQuery>();
            CreateMap<CreateCustomerRequest, CreateCustomerCommand>();
        }
    }
}