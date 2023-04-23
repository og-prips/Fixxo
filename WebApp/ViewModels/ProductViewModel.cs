using System.ComponentModel.DataAnnotations;
namespace WebApp.ViewModels;

public class ProductViewModel
{
    [Display(Name = "Product name")]
    [Required(ErrorMessage = "Please enter product name")]
    public string ProductName { get; set; } = null!;

    [Display(Name = "Description")]
    [Required(ErrorMessage = "Please enter a description")]
    public string Description { get; set; } = null!;

    [Display(Name = "Image url")]
    [Required(ErrorMessage = "Please enter an image url")]
    public string ImageUrl { get; set; } = null!;

    [Display(Name = "Tag")]
    [Required(ErrorMessage = "Please choose a tag")]
    public string Tag { get; set; } = null!;

    [Display(Name = "Rating")]
    [Required]
    public int Rating { get; set; }

    [Display(Name = "Price")]
    [Required]
    public decimal Price { get; set; }

    [Display(Name = "Category")]
    [Required]
    public int CategoryId { get; set; }
}
