using System;

namespace AddisEats.Exercises.Models;

public class MenuItem
{
    public MenuItem(int id, string name, string category, decimal price)
    {
        Id = id;
        Name = name;
        Category = category;
        Price = price;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }

    public decimal PriceWithVat() => Price * 1.15m;
}
