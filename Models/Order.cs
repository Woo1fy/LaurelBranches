using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaurelBranches.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public bool Active { get; set; }
        public bool Promotional { get; set; } 
        public bool Deliverable { get; set; }
        public int DeliveryFee { get; set; }

        public string? IdentityUserId { get; set; }
        [ForeignKey(nameof(IdentityUserId))]
        public IdentityUser? User {  get; set; }

        public List<Product>? Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public int Weight { get; set; }
        public string Composition { get; set; }
        public int Calories { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }

        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}
