using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsFavourite { get; set; }
    public bool HasHappyHour { get; set; }
    public int Discount { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

}
