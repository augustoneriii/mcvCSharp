using System.ComponentModel.DataAnnotations;
using ApiMvc.Models;

namespace ApiMvc.Data.Dtos;

public class CreateProductDto
{
    [Required(ErrorMessage = "O código é obrigatório")]
    public string Code { get; set; }
    [Required(ErrorMessage = "O Nome é obrigatório")]
    public string Name { get; set; }
    [Required(ErrorMessage = "A descrição é obrigatória")]

    public string Description { get; set; }
    public int CategoryId { get; set; }
public List<string> Tags { get; set; }
}