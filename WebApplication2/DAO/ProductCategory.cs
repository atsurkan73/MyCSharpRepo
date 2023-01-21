using System.ComponentModel.DataAnnotations;

namespace WebApiApplication.DAO
{
    public class ProductCategory
    {
        [Key] public string Category { get; set; }
        public string Subcategory { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}