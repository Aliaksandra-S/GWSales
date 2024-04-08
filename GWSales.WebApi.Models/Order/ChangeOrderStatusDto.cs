using GWSales.Services.Models.Order;
using System.Text.Json.Serialization;

namespace GWSales.WebApi.Models.Order;

public class ChangeOrderStatusDto
{
    public int OrderId { get; set; }

    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus OrderStatus { get; set; }
}
