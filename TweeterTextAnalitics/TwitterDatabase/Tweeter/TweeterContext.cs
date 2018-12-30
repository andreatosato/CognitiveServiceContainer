using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterDatabase.Tweeter
{
    public class TweeterContext : DbContext
    {
        public DbSet<TweeterResult> Tweets { get; set; }
        public DbSet<Retweeted> Retweets { get; set; }
        //public DbSet<UserMention> UserMentions { get; set; }

        public TweeterContext(DbContextOptions<TweeterContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
