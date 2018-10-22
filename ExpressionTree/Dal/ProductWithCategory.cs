using System;
using System.Linq.Expressions;
using ExpressionTree.Database.Models;

namespace ExpressionTree.Dal
{
    public class ProductWithCategory
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CategoryModel Category { get; set; }

        public static Expression<Func<Product, ProductWithCategory>> Projection
        {
            get
            {
                return x => new ProductWithCategory
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = CategoryModel.FromEntity(x.Category)
                };
            }
        }
    }
}