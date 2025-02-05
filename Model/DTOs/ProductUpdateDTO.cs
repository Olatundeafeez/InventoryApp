﻿namespace InventoryAPI.Model.DTOs
{
    public class ProductUpdateDTO
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }
    }
}
