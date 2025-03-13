/*
 CORE KATMANI :
Tüm projede kullanılabilecek genel altyapı, ortak arayüzler ve yardımcı sınıfları içerir.
BaseEntity genellikle tüm tablolar için ortak olan özellikleri içerir.
 */

namespace ECommerce.Core
{
    public abstract class BaseEntity // Abstract class nedir ? araştır.Class ı bilmiyorsan onu da araştır. Ortak kodlar, Base Entity gibi temel sınıflar Core katmanında olacak.
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
