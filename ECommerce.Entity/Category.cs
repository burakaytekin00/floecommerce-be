using ECommerce.Core;

/*
 ENTİTY KATMANI :
Veritabanındaki tabloların sınıf karşılıklarını oluşturur. Bu sınıflar, veritabanı ile uygulama arasında veri taşıyan modellerdir.
 */

namespace ECommerce.Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
