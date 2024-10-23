using InventoryAPI.Helper;
using InventoryAPI.Model.DTOs;
using Microsoft.AspNetCore.Identity.Data;

namespace InventoryAPI.Interface
{
    public interface IUserService
    {
        Task<ResponseModel<string>> RegisterAdmin(Register register);
        Task<ResponseModel<string>> RegisterStaff(Register register);
        Task<ResponseModel<string>> RegisterCustomer (Register register);
        Task<ResponseModel<string>> Login(Login login);

    }
}
