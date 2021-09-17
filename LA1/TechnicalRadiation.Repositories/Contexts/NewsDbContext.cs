using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Repositories.Interfaces;

namespace TechnicalRadiation.Repositories.Contexts
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options)
        : base(options)
        { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<NewsItem> NewsItemsDetails { get; set; }
    }
}