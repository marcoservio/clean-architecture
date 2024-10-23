using AutoMapper;

using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Communication.Request;
using CleanArchMvc.WebAUI.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebAUI.Controllers;

public class ProductsController(IProductService productService, ICategoryService categoryService, IMapper mapper, IWebHostEnvironment environment) : Controller
{
    private readonly IProductService _productService = productService;
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IMapper _mapper = mapper;
    private readonly IWebHostEnvironment _environment = environment;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAll();

        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAll(), "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductRequest request)
    {
        if (ModelState.IsValid)
        {
            await _productService.Add(request);
            return RedirectToAction(nameof(Index));
        }

        return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var product = await _productService.GetById(id);

        if (product == null)
            return NotFound();

        ViewBag.CategoryId = new SelectList(await _categoryService.GetAll(), "Id", "Name", product.CategoryId);

        var productRequest = _mapper.Map<ProductRequest>(product);

        return View(productRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductRequest request)
    {
        if (ModelState.IsValid)
        {
            await _productService.Update(request);
            return RedirectToAction(nameof(Index));
        }

        return View(request);
    }

    [Authorize(Roles = nameof(Roles.Admin))]
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var product = await _productService.GetById(id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    [HttpPost, ActionName(nameof(Delete))]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var product = await _productService.GetById(id);

        if (product == null)
            return NotFound();

        var wwwroot = _environment.WebRootPath;
        var image = Path.Combine(wwwroot, "images\\" + product.Image);
        var exists = System.IO.File.Exists(image);
        ViewBag.ImageExist = exists;

        return View(product);
    }
}
