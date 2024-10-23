namespace InventoryAPI.Model.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PasswordHash { get; set; }
        public List<UserRole> Roles { get; set; }
    }
}
