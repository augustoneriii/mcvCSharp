using System.ComponentModel.DataAnnotations;
using ApiMvc.Models;
using System.Collections.Generic;


namespace ApiMvc.Data.Dtos;

public class UpdateProductDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
       public List<string> Tags { get; set; }
}