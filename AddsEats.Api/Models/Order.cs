namespace AddsEats.Api.Models
{
    /// <summary>
    /// An order placed at a restaurant.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Unique order id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of the restaurant receiving the order.
        /// </summary>
        public int RestaurantId { get; set; }

        /// <summary>
        /// Id of the ordered menu item.
        /// </summary>
        public int MenuItemId { get; set; }

        /// <summary>
        /// Customer's full name.
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Customer's Ethiopian mobile number.
        /// </summary>
        public string CustomerPhone { get; set; } = string.Empty;

        /// <summary>
        /// Delivery address.
        /// </summary>
        public string DeliveryAddress { get; set; } = string.Empty;

        /// <summary>
        /// Number of portions ordered.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Server-calculated total in ETB.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// UTC time the order was placed.
        /// </summary>
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    }
}
