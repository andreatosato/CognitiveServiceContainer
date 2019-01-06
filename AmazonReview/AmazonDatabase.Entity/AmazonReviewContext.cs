using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonDatabase.Entity
{
    public class AmazonReviewContext : DbContext
    {
        public DbSet<AmazonReviewEntity> AmazonReview { get; set; }

        public AmazonReviewContext(DbContextOptions<AmazonReviewContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
