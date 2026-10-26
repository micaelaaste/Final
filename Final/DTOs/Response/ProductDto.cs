namespace Final.DTOs.Response;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Discount { get; set; }
    public decimal FinalPrice { get; set; }       // calculado: Price - Price * Discount / 100
    public bool IsFavourite { get; set; }
    public bool HasHappyHour { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}
