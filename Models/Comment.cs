using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaurelBranches.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }

        public string? IdentityUserId { get; set; }
        [ForeignKey(nameof(IdentityUserId))]
        public IdentityUser? User { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
    }
}
