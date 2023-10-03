
namespace ApiMvc.Models;


public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductId { get; set; }

    public static implicit operator string(Tag v)
    {
        throw new NotImplementedException();
    }
}
