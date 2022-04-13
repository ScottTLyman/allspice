
namespace allspice.Models
{
  public class Recipe
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
    public string CreatorId { get; set; }

    // virtuals
    public Account? Creator { get; set; }
  }
}