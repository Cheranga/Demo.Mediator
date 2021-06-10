using System.Threading.Tasks;
using AutoMapper;
using Coles.Customers.Application.Requests;
using Coles.Customers.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coles.Customers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }
        
        [HttpGet("{email}")]
        public async Task<IActionResult> GetCustomerAsync([FromRoute] GetCustomerByEmailRequest request)
        {
            var getCustomerByEmailRequest = mapper.Map<GetCustomerByEmailRequest>(request);
            var operation = await customerService.GetCustomerAsync(getCustomerByEmailRequest);

            // TODO: Return the appropriate response to the caller
            return Ok();
        }
        
        // [HttpPost("create")]
        // public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerRequest request)
        // {
        //     var createCustomerRequest = mapper.Map<CreateCustomerRequest>(request);
        //     var operation = await customerService.CreateCustomerAsync(createCustomerRequest);
        //
        //     // TODO: Return the appropriate response to the caller
        //     return Ok();
        // }
    }
}