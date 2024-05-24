using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaurelBranches.Models
{
    public class OrderVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Amount { get; set; }
        public bool? Active { get; set; }
        public bool? Promotional { get; set; } = false;
        public bool? Deliverable { get; set; }
        public int? DeliveryFee { get; set; }

        [Required]
        public string? IdentityUserId { get; set; }
        [ForeignKey(nameof(IdentityUserId))]
        public IdentityUser? User { get; set; }
    }

    public class ProductVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public IFormFile? Image { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? Discount { get; set; } = 0;
        public int? Weight { get; set; }
        public string? Composition { get; set; }
        public int? Calories { get; set; }
        public double? Proteins { get; set; }
        public double? Fats { get; set; }
        public double? Carbohydrates { get; set; }

        public int? OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
    }
}
