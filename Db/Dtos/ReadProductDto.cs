using System.ComponentModel.DataAnnotations;
using ApiMvc.Models;

namespace ApiMvc.Data.Dtos;

public class ReadProductDto
{
    public int Id {get; set;}
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public List<Tag> Tags { get; set; }
}