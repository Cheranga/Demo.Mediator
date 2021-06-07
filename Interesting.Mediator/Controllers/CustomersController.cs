using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Interesting.Mediator.Requests;
using Interesting.Mediator.Services;
using Microsoft.AspNetCore.Mvc;

namespace Interesting.Mediator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IValidator<CreateCustomerRequest> createCustomerRequestValidator;
        private readonly ICustomerService customerService;
        private readonly IValidator<GetCustomerByEmailRequest> getCustomerByEmailRequestValidator;

        public CustomersController(ICustomerService customerService, IValidator<CreateCustomerRequest> createCustomerRequestValidator,
            IValidator<GetCustomerByEmailRequest> getCustomerByEmailRequestValidator)
        {
            this.customerService = customerService;
            this.createCustomerRequestValidator = createCustomerRequestValidator;
            this.getCustomerByEmailRequestValidator = getCustomerByEmailRequestValidator;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetCustomerAsync([FromRoute] GetCustomerByEmailRequest request)
        {
            var validationResult = await getCustomerByEmailRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                // TODO: Return an error object
                return new BadRequestResult();
            }

            var operation = await customerService.GetCustomerAsync(request);
            if (operation.Status)
            {
                return Ok(operation.Data);
            }

            return new ObjectResult(operation)
            {
                StatusCode = (int) (HttpStatusCode.InternalServerError)
            };
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerRequest request)
        {
            var validationResult = await createCustomerRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                // TODO: Return an error object
                return new BadRequestResult();
            }
            
            var operation = await customerService.CreateCustomerAsync(request);
            if (operation.Status)
            {
                return Ok();
            }

            return new ObjectResult(operation)
            {
                StatusCode = (int) (HttpStatusCode.InternalServerError)
            };
        }
    }
}