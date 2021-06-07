using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Interesting.Mediator.Services;
using Interesting.Mediator.Services.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Interesting.Mediator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetCustomerAsync([FromRoute] GetCustomerByEmailRequest request)
        {
            var operation = await mediator.Send(request);
            
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
            var operation = await mediator.Send(request);
            
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