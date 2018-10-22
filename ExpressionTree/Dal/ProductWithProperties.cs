using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ExpressionTree.Database.Models;

namespace ExpressionTree.Dal
{
    public class ProductWithProperties
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PropertyListItem> Properties { get; set; }

        public static Expression<Func<Product, ProductWithProperties>> Projection
        {
            get
            {
                return x => new ProductWithProperties
                {
                    Id = x.Id,
                    Name = x.Name,
                    Properties = x.Properties.AsQueryable().Select(PropertyListItem.Projection)
                };
            }
        }
    }
}