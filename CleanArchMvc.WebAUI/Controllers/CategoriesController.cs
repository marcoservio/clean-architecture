using AutoMapper;

using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Communication.Request;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebAUI.Controllers;

[Authorize]
public class CategoriesController(ICategoryService service, IMapper mapper) : Controller
{
    private readonly ICategoryService _service = service;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _service.GetAll();

        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryRequest request)
    {
        if (ModelState.IsValid)
        {
            await _service.Add(request);
            return RedirectToAction(nameof(Index));
        }

        return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
            return NotFound();

        var category = await _service.GetById(id);

        if (category is null)
            return NotFound();

        var categoryRequest = _mapper.Map<CategoryRequest>(category);

        return View(categoryRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryRequest request)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _service.Update(request);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if(id == null)
            return NotFound();

        var category = await _service.GetById(id);

        if(category is null)
            return NotFound();

        var categoryRequest = _mapper.Map<CategoryRequest>(category);

        return View(categoryRequest);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.Remove(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if(id == null)
            return NotFound();

        var category = await _service.GetById(id);

        if(category is null)
            return NotFound();

        return View(category);
    }
}
