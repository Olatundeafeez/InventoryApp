using Microsoft.Extensions.Localization;

namespace InventoryAPI.Model.DTOs
{
    public class Register
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Email { get; set; }
        public string Password{ get; set; }
        public string ConfirmPassword { get; set; }
    }
}
