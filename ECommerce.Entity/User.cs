using ECommerce.Core;

namespace ECommerce.Entity
{
    public class User : BaseEntity
    {
        public int UserTypeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }

        // Navigation property
        public UserType UserType { get; set; }
    }
} 