using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExpressionTree.Dal;
using ExpressionTree.Database.Context;
using ExpressionTree.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExpressionTree
{
    public class Program
    {

        public static void SimpleQuery(MainContext dbContext)
        {
            var values = dbContext.Product
                .Select(p => new ProductListItem
                {
                    Id = p.Id,
                    Name = p.Name,
                    NumberOfProperties = p.Properties.Count
                }).ToList();

            foreach (var value in values)
            {
                Console.WriteLine($"{value.Id} {value.Name} {value.NumberOfProperties}");
            }
        }

        public static void SimpleProjection(MainContext dbContext)
        {
            var values = dbContext.Product.Select(ProductListItem.Projection).ToList();
            
            foreach (var value in values)
            {
                Console.WriteLine($"{value.Id} {value.Name} {value.NumberOfProperties}");
            }
        }

        public static void ProjectionWithNestedColletion(MainContext dbContext)
        {
            var properties = dbContext.Product.Include(x => x.Properties).ToList();

            var values = properties.AsQueryable().Select(ProductWithProperties.Projection).ToList();
            
            foreach (var value in values)
            {
                Console.WriteLine($"{value.Id} {value.Name}");
                foreach (var property in value.Properties)
                {
                    Console.WriteLine($"{property.Id} {property.Name}");
                }
            }
        }

        public static void ProjectionWithNestedCollectionAutomapper(MainContext dbContext)
        {
            var values = dbContext.Product
                .Include(x => x.Properties)
                .ProjectTo<ProductWithProperties>()
                .ToList();
            
            foreach (var value in values)
            {
                Console.WriteLine($"{value.Id} {value.Name}");
                foreach (var property in value.Properties)
                {
                    Console.WriteLine($"{property.Id} {property.Name}");
                }
            }
        }

        public static void ProjectionWithNestedEntity(MainContext dbContext)
        {
            var values = dbContext.Product.Include(x => x.Category).Select(ProductWithCategory.Projection).ToList();
            
            foreach (var value in values)
            {
                Console.WriteLine($"{value.Id} {value.Name} {value.Category.Id} {value.Category.Name}");
            }
        }

        public static void ProjectionWithNestedEntityAutomapper(MainContext dbContext)
        {
            var values = dbContext.Product
                .Include(x => x.Category)
                .ProjectTo<ProductWithCategory>()
                .ToList();  
            
            foreach (var value in values)
            {
                Console.WriteLine($"{value.Id} {value.Name} {value.Category.Id} {value.Category.Name}");
            }
        }
        
        public static void Main()
        {
            var dbConext = new MainContext();
            
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ProductProperty, PropertyListItem>();
                cfg.CreateMap<Product, ProductWithProperties>()
                    .ForMember(dto => dto.Properties, conf => conf.MapFrom(ol => ol.Properties));
                cfg.CreateMap<ProductCategory, CategoryModel>();
                cfg.CreateMap<Product, ProductWithCategory>()
                    .ForMember(dto => dto.Category, conf => conf.MapFrom(ol => ol.Category));
            });
            
            SimpleQuery(dbConext);
            Console.WriteLine();
            SimpleProjection(dbConext);
            Console.WriteLine();
            ProjectionWithNestedColletion(dbConext);
            Console.WriteLine();
            ProjectionWithNestedCollectionAutomapper(dbConext);
            Console.WriteLine();
            ProjectionWithNestedEntity(dbConext);
            Console.WriteLine();
            ProjectionWithNestedEntityAutomapper(dbConext);
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}