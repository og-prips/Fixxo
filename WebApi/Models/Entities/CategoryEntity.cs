using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models.Entities;

public class CategoryEntity
{
    [Key]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;

    [JsonIgnore]
    public ICollection<ProductEntity> Products { get; set; } = new HashSet<ProductEntity>();
}
