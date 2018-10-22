using System.Collections.Generic;

namespace ExpressionTree.Database.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public bool IsForSale { get; set; }
        public ProductCategory Category { get; set; }
        public ICollection<ProductProperty> Properties { get; set; }
        public bool IsAvailable => IsForSale && InStock > 0;
    }
}