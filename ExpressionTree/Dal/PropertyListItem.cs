using System;
using System.Linq.Expressions;
using ExpressionTree.Database.Models;

namespace ExpressionTree.Dal
{
    public class PropertyListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<ProductProperty, PropertyListItem>> Projection
        {
            get
            {
                return x => new PropertyListItem
                {
                    Id = x.Id,
                    Name = x.Name
                };
            }
        }
    }
}