using System;
using System.Linq.Expressions;
using ExpressionTree.Database.Models;

namespace ExpressionTree.Dal
{
    public class CategoryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<ProductCategory, CategoryModel>> Projection
        {
            get
            {
                return x => new CategoryModel
                {
                    Id = x.Id,
                    Name = x.Name
                };
            }
        }

        public static CategoryModel FromEntity(ProductCategory category)
        {
            return Projection.Compile().Invoke(category);
        }
    }
}