using ECommerce.Core;

namespace ECommerce.Entity
{
    public class UserType : BaseEntity
    {
        public string UserTypeName { get; set; }
        public string UserTypeDescription { get; set; }

        // Navigation property
        public ICollection<User> Users { get; set; }
    }
} 