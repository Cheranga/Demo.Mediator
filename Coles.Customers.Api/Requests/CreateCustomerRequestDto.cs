namespace Coles.Customers.Api.Requests
{
    public class CreateCustomerRequestDto
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
    }
}