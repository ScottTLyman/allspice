using System.ComponentModel.DataAnnotations;

namespace allspice.Models
{
  public class Step
  {
    public int Id { get; set; }
    [Required]
    public int? Place { get; set; }
    public string Body { get; set; }
    public int RecipeId { get; set; }

  }
}