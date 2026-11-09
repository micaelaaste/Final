namespace Final.DTOs.Response;

public class RestaurantDto
{
    public int Id { get; set; }
    public string RestaurantName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}