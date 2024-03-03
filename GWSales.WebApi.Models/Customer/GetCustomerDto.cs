namespace GWSales.WebApi.Models.Customer;

public class GetCustomerDto
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public int CustomerTypeId { get; set; }
    public string TypeName { get; set; }
}
