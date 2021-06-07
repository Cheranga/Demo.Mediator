namespace Interesting.Mediator.DataAccess
{
    public class CreateCustomerCommand
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}