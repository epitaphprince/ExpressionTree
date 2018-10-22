using System;
using System.Linq.Expressions;
using ExpressionTree.Database.Models;

namespace ExpressionTree.Dal
{
    public class ProductListItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int NumberOfProperties { get; set; }

        public static Expression<Func<Product, ProductListItem>> Projection
        {
            get
            {
                return x => new ProductListItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    NumberOfProperties = x.Properties.Count
                };
            }
        }
    }
}