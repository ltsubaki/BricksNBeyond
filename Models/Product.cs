using System;
using System.Collections.Generic;

namespace IntexQueensSlay.Models;

public partial class Product
{
    public byte ProductId { get; set; }

    public string Name { get; set; } = null!;

    public short Year { get; set; }

    public string NumParts { get; set; } = null!;

    public double Price { get; set; }

    public string ImgLink { get; set; } = null!;

    public string PrimaryColor { get; set; } = null!;

    public string? SecondaryColor { get; set; }

    public string Description { get; set; } = null!;

    public string Category1 { get; set; } = null!;

    public string? Category2 { get; set; }

    public string? Category3 { get; set; }
}
