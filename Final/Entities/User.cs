namespace Final.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; }
    public string LastName { get; set; }
    public string RestaurantName { get; set; }
    public string Address { get; set; }
    public int PhoneNumber { get; set; }

}
