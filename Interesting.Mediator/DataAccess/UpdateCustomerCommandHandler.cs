using System.Threading;
using System.Threading.Tasks;
using Interesting.Mediator.Core;
using MediatR;

namespace Interesting.Mediator.DataAccess
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result>
    {
        private readonly ICustomerRepository customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        
        public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var operation = await customerRepository.UpdateCustomerAsync(request);
            return operation;
        }
    }
}