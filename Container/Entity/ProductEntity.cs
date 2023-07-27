using ProductApiVSC.Models;

namespace ProductApiVSC.Entity
{
    public class ProductEntity
    {
        // public int Id { get; set; }
        // public string? ProductName { get; set; }
        // public int Price { get; set; }
        // public string? Status { get; set; }

            // [Key]
    // public int Id { get; set; }

    // public string? Name { get; set; }

    // public int? Price { get; set; }

    // [Key]
    public int Id { get; set; }

    public string? Brand { get; set; }

    public string? Sku { get; set; }
    
    public string ProductName { get; set; }

    public string? Slug { get; set; }

    public int? Price { get; set; }

    public int Discount { get; set; }
 
    public Task<NewProduct> NewProduct { get; set; } = new Task<NewProduct>();
    // public bool? New { get; set; }

    // public int? Rating { get; set; }
    
    // public int RatingCount { get; set; }

    // public int? SaleCount { get; set; }

    // public string? Category { get; set; }

    // public string Tag { get; set; }

    // public string? Variation { get; set; }

    // public string? thumbImage { get; set; }

    // public string? Image { get; set; }

    public string? ShortDescription { get; set; }

    public string? fullDescription { get; set; }

    }


}