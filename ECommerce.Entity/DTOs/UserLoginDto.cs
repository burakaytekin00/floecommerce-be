namespace ECommerce.Entity.DTOs
{
    public class UserLoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginResponseDto
    {
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
    }
} 