using WebApi.Models.Entities;

namespace WebApi.DataTransferObjects;

public class ProductHttpResponse
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Tag { get; set; } = null!;
    public int Rating { get; set; }
    public decimal Price { get; set; }
    public DateTime DateAdded { get; set; }
    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;
}
