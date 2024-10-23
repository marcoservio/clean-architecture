using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Communication.Request;

using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService, ICategoryService categoryService) : ControllerBase
{
    private readonly IProductService _productService = productService;
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAll();

        if (products?.Count() > 0)
            return Ok(products);

        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetById(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProductRequest request)
    {
        await _productService.Add(request);

        return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductRequest request)
    {
        var product = await _productService.GetById(request.Id);

        if (product is null)
            return NotFound("Product Not Found.");

        var category = await _categoryService.GetById(request.CategoryId);

        if (category is null)
            return NotFound("Category Not Found.");

        await _productService.Update(request);

        return Ok(request);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetById(id);

        if (product == null)
            return NotFound();

        await _productService.Delete(id);

        return NoContent();
    }
}
