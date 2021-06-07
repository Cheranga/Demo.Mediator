namespace Interesting.Mediator.Requests
{
    public class CreateCustomerRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class GetCustomerByEmailRequest
    {
        public string Email { get; set; }
    }
}