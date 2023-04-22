using Microsoft.AspNetCore.Mvc;
using WebApi.DataTransferObjects;
using WebApi.Repositories;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductRepository _repository;

    public ProductsController(ProductRepository repository)
    {
        _repository = repository;
    }

    #region GET

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> Get(int productId)
    {
        var product = await _repository.GetAsync(productId);
        if (product != null)
        {
            return Ok(product);
        }

        return NotFound();
    }

    [HttpGet("tag/{tagName}")]
    public async Task<IActionResult> GetByTag(string tagName)
    {
        return Ok(await _repository.GetAllByTagAsync(tagName));
    }

    #endregion

    #region POST

    [HttpPost]
    public async Task<IActionResult> Create(ProductHttpRequest request)
    {
        if (ModelState.IsValid)
        {
            var product = await _repository.CreateAsync(request);

            if (product != null)
            {
                return Created("", product);
            }
        }

        return BadRequest();
    }

    #endregion

    #region DELETE

    //[HttpDelete("{articleNumber}")]
    //public async Task<IActionResult> Delete(string articleNumber)
    //{
    //    var product = await _repository.GetAsync(articleNumber);

    //    if (product != null)
    //    {
    //        await _repository.DeleteAsync(articleNumber);
    //        return 
    //    }

    //    return NotFound(product);
    //}

    #endregion
}
