namespace ECommerce.Entity.DTOs
{
    public class ProductDto : BaseDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
    }
} 