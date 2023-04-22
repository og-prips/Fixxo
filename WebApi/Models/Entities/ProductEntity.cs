using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApi.DataTransferObjects;

namespace WebApi.Models.Entities;

public class ProductEntity
{
    [Key]
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Tag { get; set; } = null!;
    public int Rating { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }
    public DateTime DateAdded { get; set; }

    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;


    public static implicit operator ProductHttpResponse(ProductEntity entity)
    {
        return new ProductHttpResponse
        {
            ProductId = entity.ProductId,
            ProductName = entity.ProductName,
            Description = entity.Description,
            ImageUrl = entity.ImageUrl,
            Tag = entity.Tag,
            Rating = entity.Rating,
            Price = entity.Price,
            DateAdded = entity.DateAdded,
            CategoryId = entity.CategoryId,
            Category = entity.Category,
        };
    }
}
