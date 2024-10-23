using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace InventoryAPI.Model.Domain
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } 

        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Description { get; set; } = "";

        [Required]
        public string Category { get; set; } = "";
       

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string ExpiryDate { get; set; }

    }
}
