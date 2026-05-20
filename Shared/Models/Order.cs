namespace Shared.Models;

public class Order
{
    public Guid OrderNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderedByName { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public string OrderByContact { get; set; } = string.Empty;
    public string OrderByEmail { get; set; } = string.Empty;
}