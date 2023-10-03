using System.ComponentModel.DataAnnotations;
using ApiMvc.Models;

namespace ApiMvc.Data.Dtos;

public class ReadCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}