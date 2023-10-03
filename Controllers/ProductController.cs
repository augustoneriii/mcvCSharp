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

public class ProductController : ControllerBase
{

    private ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddProduct([FromBody] CreateProductDto productDto)
    {
        var categoria = _context.Categories.FirstOrDefault(c => c.Id == productDto.CategoryId);

        if (categoria != null)
        {
            var produto = new Product
            {
                Code = productDto.Code,
                Name = productDto.Name,
                Description = productDto.Description,
                CategoryId = productDto.CategoryId
            };


            if (productDto.Tags != null)
            {
                produto.Tags = new List<Tag>();
                foreach (var item in productDto.Tags)
                {
                    produto.Tags.Add(new Tag { Name = item });
                }
            }

            _context.Products.Add(produto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProductById), new { id = produto.Id }, produto);
        }
        else
        {
            return BadRequest("Categoria não encontrada.");
        }
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _context.Products
             .Include(p => p.Category)
             .Include(p => p.Tags)
             .ToList();


        if (products == null || !products.Any())
        {
            return NotFound();
        }

        var productDtos = products.Select(produto => new ReadProductDto
        {
            Id = produto.Id,
            Code = produto.Code,
            Name = produto.Name,
            Description = produto.Description,
            CategoryId = produto.CategoryId,
            Category = produto.Category,
            Tags = produto.Tags,
        }).ToList();

        return Ok(productDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var produto = _context.Products
          .Include(p => p.Category)
          .Include(p => p.Tags)
          .FirstOrDefault(produto => produto.Id == id);

        if (produto == null)
        {
            return NotFound();
        }

        var productDto = new ReadProductDto
        {
            Id = produto.Id,
            Code = produto.Code,
            Name = produto.Name,
            Description = produto.Description,
            CategoryId = produto.CategoryId,
            Category = produto.Category,
            Tags = produto.Tags,
        };

        return Ok(productDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, UpdateProductDto updateProductDto)
    {
        var produto = _context.Products
          .Include(p => p.Tags)
          .FirstOrDefault(produto => produto.Id == id);
        var categoria = _context.Categories.Where(c => c.Id == updateProductDto.CategoryId).FirstOrDefault();

        if (produto != null && categoria != null)
        {
            produto.Code = updateProductDto.Code;
            produto.Name = updateProductDto.Name;
            produto.Description = updateProductDto.Description;
            produto.Category = categoria;
            produto.Tags = new List<Tag>();
            if (updateProductDto.Tags != null)
            {
                produto.Tags = new List<Tag>();
                foreach (var item in updateProductDto.Tags)
                {
                    produto.Tags.Add(new Tag { Name = item });
                }
            }
            _context.SaveChanges();
            return Ok(new { message = "Produto editado com sucesso" });
        }
        else
        {
            return NotFound(new { message = "Produto ou categoria não encontrados" });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduto(int id)
    {
        var produto = _context.Products.FirstOrDefault(produto => produto.Id == id);
        if (produto != null)
        {
            _context.Products.Remove(produto);
            _context.SaveChanges();
            return Ok(new { message = "Produto excluído com sucesso" });
        }
        return NotFound(new { message = "Produto não encontrado" });
    }
}
