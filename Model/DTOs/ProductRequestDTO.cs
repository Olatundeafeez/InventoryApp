using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Model.DTOs
{
    public class ProductRequestDTO
    {

        public string Name { get; set; }
        public int Quantity { get; set; }
        
        public string Description { get; set; } 

        public string Category { get; set; } 
       
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
       
    }
}
