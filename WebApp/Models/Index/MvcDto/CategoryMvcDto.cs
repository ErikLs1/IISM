namespace WebApp.Models.Index.MvcDto;

public class CategoryMvcDto
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = default!;
    public string CategoryDescription { get; set; } = default!;
}