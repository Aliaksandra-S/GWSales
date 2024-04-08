using GWSales.Services.Models.Order;
using System.Text.Json.Serialization;

namespace GWSales.WebApi.Models.Order;

public class GetOrderDto
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public int CustomerTypeId { get; set; }
    public DateOnly OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus OrderStatus { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    public GetOrderDetailsListDto Details { get; set; }
}
