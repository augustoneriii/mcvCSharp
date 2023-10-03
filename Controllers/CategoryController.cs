using ApiMvc.Data;
using ApiMvc.Data.Dtos;
using ApiMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace ApiMvc.Controllers;

[ApiController]
[Route("[controller]")]

public class CategoryController : ControllerBase
{

    private ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddProduct([FromBody] CreateCategoryDto categoryDto)
    {
        var category = new Category
        {
            Name = categoryDto.Name,
        };

        _context.Categories.Add(category);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
    }

    [HttpGet]
    public IActionResult GetAllCategories()
    {
        var categories = _context.Categories.ToList();


        if (categories == null || !categories.Any())
        {
            return NotFound();
        }

        var categoryDtos = categories.Select(categories => new ReadCategoryDto
        {
            Id = categories.Id,
            Name = categories.Name,
        }).ToList();

        return Ok(categoryDtos);
    }


    [HttpGet("{id}")]
    public IActionResult GetCategoryById(int id)
    {
        var category = _context.Categories.FirstOrDefault(category => category.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        var categoryDto = new ReadCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
        };

        return Ok(categoryDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategories(int id, UpdateCategoryDto updateCategories)
    {
        var category = _context.Categories.FirstOrDefault(category => category.Id == id);

        if (category == null)
        {
            return NotFound();
        }
        else
        {
            category.Name = updateCategories.Name;
            _context.SaveChanges();
            return Ok(new { message = "Categoria editado com sucesso" });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var category = _context.Categories.FirstOrDefault(category => category.Id == id);

        if (category != null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok(new { message = "Categoria excluída com sucesso" });
        }

        return NotFound(new { message = "Categoria não encontrado" });
    }
}