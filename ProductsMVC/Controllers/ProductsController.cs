using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Models;
using System.Net.Http.Json;


public class ProductsController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductsController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("WebAPI");
        var response = await client.GetAsync("api/Products");
        if (response.IsSuccessStatusCode)
        {
            var products = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
            return View(products);
        }
        else
        {
            return View(new List<Product>());
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        var client = _httpClientFactory.CreateClient("WebAPI");
        var response = await client.PostAsJsonAsync("api/Products", product);
        response.EnsureSuccessStatusCode();

        return RedirectToAction("Index", "Products");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var client = _httpClientFactory.CreateClient("WebAPI");
        var response = await client.GetAsync($"api/Products/{id}");
        if (response.IsSuccessStatusCode)
        {
            var product = await response.Content.ReadFromJsonAsync<Product>();
            return PartialView("Edit", product); 
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, Product product)
    {
        if (id != product.ID)
        {
            return BadRequest();
        }

        var client = _httpClientFactory.CreateClient("WebAPI");
        var response = await client.PutAsJsonAsync($"api/Products/{id}", product);
        response.EnsureSuccessStatusCode();

        return RedirectToAction("Index", "Products");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var client = _httpClientFactory.CreateClient("WebAPI");
        var response = await client.DeleteAsync($"api/Products/{id}");
        response.EnsureSuccessStatusCode();

        return RedirectToAction("Index", "Products");
    }
}

