using InventoryAPI.DataContext;
using InventoryAPI.Helper;
using InventoryAPI.Interface;
using InventoryAPI.Model.Domain;
using InventoryAPI.Model.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Repository
{
    public class ProductRepository : IProductService
    {
        private readonly ApplicationContext context;
        public ProductRepository(ApplicationContext context)
        {
            this.context = context;
        }
        //public async Task<Model.Domain.Product> Add(ProductRequestDTO product)
        //{
        //    try
        //    {
        //        var Product = new Model.Domain.Product()
        //        {
        //            Count = product.Count,
        //            Price = product.Price,
        //            Description = product.Description,
        //            Category = product.Category
        //        };
        //        await context.AddAsync(Product);
        //        await context.SaveChangesAsync();
        //        return Product;


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<ResponseModel<string>> AddProduct(ProductRequestDTO product)
        {
            var response = new ResponseModel<string>();
            try
            {
                var newProduct = new Product()
                {
                    Quantity = product.Quantity,
                    Name = product.Name,
                    Description = product.Description,
                    Category = product.Category,
                    Price = product.Price,
                    ExpiryDate = product.ExpiryDate.ToShortDateString()
                };
                await context.AddAsync(newProduct);
                await context.SaveChangesAsync();

                response = response.Successful(" Product Added successfully ");

            }

            catch (Exception ex) 
            {
            throw new Exception(ex.Message);
            }
            return response;
        }

        public async Task<ResponseModel<string>> Delete(Guid Id)
        {
            var response = new ResponseModel<String>();
            try 
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == Id);
                if(product == null)
                {
                    response = response.FailedResult(" Invalid product ");
                }
                context.Products.Remove(product);
                await context.SaveChangesAsync();

                response = response.Successful("product deleted successfully");
            
            }

            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
            return response;
           
        }
        public async Task<List<Product>> GetAllProduct(string message)
        {
            try
            {
                var product = await context.Products.ToListAsync();
                return product;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> GetProductById(Guid Id)
        {
            try
            {
                var res = await context.Products.FirstOrDefaultAsync( x => x.Id == Id);
                if(res == null)
                {
                    throw new Exception("product does not exist");
                }
                return res;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> GetProductQuantity(Guid Id)
        {
            try
            {
                var res = await context.Products.FirstOrDefaultAsync (x => x.Id == Id );  
                if (res == null)
                {
                    throw new Exception("Invalid product");
                }
                return "your item gotten successfully";
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Task<Product> Update(ProductRequestDTO Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> UpdateProduct(ProductUpdateDTO product, Guid Id)
        {
            try
            {
                var update = await context.Products.FirstOrDefaultAsync(x => x.Id == Id);
                if(update == null)
                {
                    throw new Exception("Invalid Product");

                }

                product.Name = product.Name;
                product.Quantity = product.Quantity;
                product.Description = product.Description;
                product.Category = product.Category;
                product.Price = product.Price;
             
                context.Products.Update(update);
                await context.SaveChangesAsync();
                return update;
            }
            catch(Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }

        
    }
}

  


