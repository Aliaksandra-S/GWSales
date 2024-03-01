namespace GWSales.Services.Models.Customer;

public class GetCustomerModel
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public int CustomerTypeId { get; set; }
    public string TypeName { get; set; }
}
