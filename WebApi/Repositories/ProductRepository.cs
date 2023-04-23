using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.DataTransferObjects;
using WebApi.Models.Entities;

namespace WebApi.Repositories;

public class ProductRepository
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<ProductHttpResponse> GetAsync(int productId)
    {
        var item = await _context.Products.FindAsync(productId);
        
        return item!;
    }

    public async Task<IEnumerable<ProductHttpResponse>> GetAllByTagAsync(string tagName)
    {
        var items = await _context.Products.Where(x => x.Tag == tagName).ToListAsync();
        var products = new List<ProductHttpResponse>();

        foreach (var item in items)
        {
            products.Add(item);
        }

        return products;
    }

    public async Task<IEnumerable<ProductHttpResponse>> GetAllAsync()
    {
        var items = await _context.Products.ToListAsync();
        var categories = await _context.Categories.ToListAsync();
        var products = new List<ProductHttpResponse>();

        foreach (var item in items)
        {
            var category = categories.FirstOrDefault(c => c.CategoryId == item.CategoryId);
            item.Category = category!;

            products.Add(item);
        }
        
        return products;
    }

    public async Task<ProductHttpResponse> CreateAsync(ProductEntity entity)
    {
        entity.DateAdded = DateTime.Now;
        _context.Products.Add(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(int productId)
    {
        try
        {
			var product = await _context.Products.FindAsync(productId);

			_context.Products.Remove(product!);
			await _context.SaveChangesAsync();

			return true;
        }
        catch
        {
            return false;
        }
	}
}
