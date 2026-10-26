namespace Final.DTOs.Response;

public class MenuCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProductDto> Products { get; set; } = new();
}
