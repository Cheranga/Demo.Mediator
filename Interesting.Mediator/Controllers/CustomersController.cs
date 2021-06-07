using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Interesting.Mediator.DTO.Requests;
using Interesting.Mediator.Requests;
using Interesting.Mediator.Services;
using Microsoft.AspNetCore.Mvc;

namespace Interesting.Mediator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IValidator<CreateCustomerRequest> validator;

        public CustomersController(ICustomerService customerService, IValidator<CreateCustomerRequest> validator)
        {
            this.customerService = customerService;
            this.validator = validator;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerRequestDto request)
        {
            // TODO: Create the create customer request (service)
            var createCustomerRequest = new CreateCustomerRequest
            {

            };
            
            var validationResult = await validator.ValidateAsync(createCustomerRequest);
            if (!validationResult.IsValid)
            {
                // TODO: Return an error object
                return new BadRequestResult();
            }

            var operation = await customerService.CreateCustomerAsync(createCustomerRequest);
            if (operation)
            {
                return Ok();
            }

            return new StatusCodeResult((int) (HttpStatusCode.InternalServerError));
        }
    }
}