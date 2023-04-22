using WebApi.Models.Entities;

namespace WebApi.DataTransferObjects;

public class ProductHttpRequest
{
    public string ProductName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Tag { get; set; } = null!;
    public int Rating { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }

    public static implicit operator ProductEntity(ProductHttpRequest request)
    {
        return new ProductEntity
        {
            ProductName = request.ProductName,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            Price = request.Price,
            Rating = request.Rating,
            Tag = request.Tag,
            CategoryId = request.CategoryId,
        };
    }
}
