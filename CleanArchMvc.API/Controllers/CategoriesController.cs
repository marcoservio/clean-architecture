using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Communication.Request;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAll();

        if (categories?.Count() > 0)
            return Ok(categories);

        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryService.GetById(id);

        if(category == null) 
            return NotFound();

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CategoryRequest request)
    {
        if (request is null)
            return BadRequest();

        await _categoryService.Add(request);

        return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryRequest request)
    {
        if(id != request.Id)
            return BadRequest();

        if(request is null)
            return BadRequest();

        await _categoryService.Update(request);

        return Ok(request);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.GetById(id);

        if(category == null)
            return NotFound();

        await _categoryService.Remove(id);

        return NoContent();
    }
}
