using InventoryAPI.Helper;
using InventoryAPI.Model.Domain;
using InventoryAPI.Model.DTOs;

namespace InventoryAPI.Interface
{
    public interface IProductService
    {
        //admin interface
        Task<ResponseModel<string>> AddProduct(ProductRequestDTO product);
        Task<Product> Update(ProductRequestDTO Id);
        Task<List<Product>> GetAllProduct (string message);
        Task<ResponseModel<string>> Delete(Guid Id);

        //staff interface

        Task<Product> GetProductById (Guid Id);
        Task<string> GetProductQuantity (Guid Id);
        Task<Product> UpdateProduct (ProductUpdateDTO product,Guid Id);

    }
}
