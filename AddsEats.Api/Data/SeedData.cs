using AddsEats.Api.Models;

namespace AddsEats.Api.Data;

public class SeedData
{
    public static List<Restaurant> restaurants = new List<Restaurant>
    {
        new Restaurant { Id = 1, Name = "Addis Ababa Restaurant", Address = "Bole", Phone = "555-1234", IsActive = true, OwnerAddress = "456 Elm St" },
        new Restaurant { Id = 2, Name = "Blue Nile Restaurant", Address = "Ayat", Phone = "555-5678", IsActive = false, OwnerAddress = "789 Pine St" },
        new Restaurant { Id = 3, Name = "Lucy Restaurant", Address = "Mexico", Phone = "555-9012", IsActive = true, OwnerAddress = "321 Cedar St" },
        new Restaurant { Id = 4, Name = "Ethio Restaurant", Address = "Megenagna", Phone = "555-3456", IsActive = true, OwnerAddress = "654 Spruce St" },
        new Restaurant { Id = 5, Name = "Tena Yistilign Restaurant", Address = "Bole", Phone = "555-7890", IsActive = true, OwnerAddress = "987 Birch St" },
        new Restaurant { Id = 6, Name = "Kategna Restaurant", Address = "Megenagna", Phone = "555-2345", IsActive = true, OwnerAddress = "123 Main St" },
        new Restaurant { Id = 7, Name = "Sishu Restaurant", Address = "Bole", Phone = "555-6789", IsActive = false, OwnerAddress = "246 Aspen St" },
        new Restaurant { Id = 8, Name = "Abay Restaurant", Address = "Megenagna", Phone = "555-0123", IsActive = true, OwnerAddress = "357 Cherry St" },
        new Restaurant { Id = 9, Name = "Fendika Restaurant", Address = "Mexico", Phone = "555-4567", IsActive = false, OwnerAddress = "468 Walnut St" },
        new Restaurant { Id = 10, Name = "Doro Wat Restaurant", Address = "Megenagna", Phone = "555-8901", IsActive = true, OwnerAddress = "579 Chestnut St" },
        new Restaurant { Id = 11, Name = "Injera Restaurant", Address = "Mexico", Phone = "555-2345", IsActive = true, OwnerAddress = "680 Poplar St" },
        new Restaurant { Id = 12, Name = "Bunna Restaurant", Address = "Ayat", Phone = "555-6789", IsActive = true, OwnerAddress = "791 Fir St" }
    };

    public static List<MenuItem> menuItems = new List<MenuItem>
    {
        // Restaurant 1 (Addis Ababa Restaurant)
        new MenuItem { Id = 1, RestaurantId = 1, Name = "Doro Wat", Description = "Spicy chicken stew with boiled egg", Price = 250.0m, Category = "Traditional" },
        new MenuItem { Id = 2, RestaurantId = 1, Name = "Kitfo", Description = "Minced raw beef seasoned with mitmita and spiced butter", Price = 300.0m, Category = "Traditional" },
        new MenuItem { Id = 3, RestaurantId = 1, Name = "Injera Firfir", Description = "Shredded injera sautéed in berbere and spiced butter", Price = 150.0m, Category = "Traditional" },

        // Restaurant 2 (Blue Nile Restaurant)
        new MenuItem { Id = 4, RestaurantId = 2, Name = "Fish Dullet", Description = "Minced tilapia sautéed with onions and chili", Price = 220.0m, Category = "Seafood" },
        new MenuItem { Id = 5, RestaurantId = 2, Name = "Fried Whole Fish", Description = "Crispy whole tilapia served with spicy dip", Price = 320.0m, Category = "Seafood" },

        // Restaurant 3 (Lucy Restaurant)
        new MenuItem { Id = 6, RestaurantId = 3, Name = "Tibs", Description = "Sautéed beef strips with rosemary and peppers", Price = 280.0m, Category = "Meat" },
        new MenuItem { Id = 7, RestaurantId = 3, Name = "Veggie Combo", Description = "Assorted fasting dishes with shiro, lentils, and collard greens", Price = 200.0m, Category = "Vegetarian" },

        // Restaurant 4 (Ethio Restaurant)
        new MenuItem { Id = 8, RestaurantId = 4, Name = "Shiro Tegabino", Description = "Slow-cooked spiced chickpea stew in clay pot", Price = 160.0m, Category = "Vegetarian" },
        new MenuItem { Id = 9, RestaurantId = 4, Name = "Gomen Besiga", Description = "Collard greens stewed with beef cubes", Price = 230.0m, Category = "Meat" },

        // Restaurant 5 (Tena Yistilign Restaurant)
        new MenuItem { Id = 10, RestaurantId = 5, Name = "Special Kitfo", Description = "Served with ayib and gomen kitfo", Price = 350.0m, Category = "Traditional" },
        new MenuItem { Id = 11, RestaurantId = 5, Name = "Bole Burger", Description = "Beef burger with house special sauce", Price = 180.0m, Category = "Fast Food" },

        // Restaurant 6 (Kategna Restaurant)
        new MenuItem { Id = 12, RestaurantId = 6, Name = "Chikina Tibs", Description = "Tender beef fillet tips sautéed with onions and awaze", Price = 310.0m, Category = "Meat" },

        // Restaurant 12 (Bunna Restaurant)
        new MenuItem { Id = 13, RestaurantId = 12, Name = "Traditional Coffee", Description = "Freshly roasted Ethiopian jebena coffee", Price = 50.0m, Category = "Beverage" },
        new MenuItem { Id = 14, RestaurantId = 12, Name = "Macchiato", Description = "Double shot espresso with steamed milk foam", Price = 60.0m, Category = "Beverage" }
    };
}
