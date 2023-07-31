using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductApiVSC.Models;

public partial class Product
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public int Price { get; set; }

    public int? Status { get; set; }

    public string? Brand { get; set; }

    public string? Sku { get; set; }

    public string? Slug { get; set; }

    public int Discount { get; set; }

    // // public bool? New { get; set; }

    public int? Rating { get; set; }
    
    public int RatingCount { get; set; }

    public int? SaleCount { get; set; }

    // public string? Category { get; set; }

    // public string Tag { get; set; }

    // public string? Variation { get; set; }

    // public string? thumbImage { get; set; }

    // public string? Image { get; set; }

    public string? ShortDescription { get; set; }

    public string? FullDescription { get; set; }

    

}
