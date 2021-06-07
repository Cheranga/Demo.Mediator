namespace Interesting.Mediator.DataAccess
{
    public class UpdateCustomerCommand
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}