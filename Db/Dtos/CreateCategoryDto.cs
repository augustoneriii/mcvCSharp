using System.ComponentModel.DataAnnotations;
using ApiMvc.Models;

namespace ApiMvc.Data.Dtos;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "O Nome é obrigatório")]
    public string Name { get; set; }
}