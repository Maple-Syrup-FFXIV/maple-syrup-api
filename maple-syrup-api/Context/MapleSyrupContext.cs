using maple_syrup_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Configuration;

namespace maple_syrup_api.Context
{
    public class MapleSyrupContext: DbContext
    {
        private readonly IConfiguration _configuration;
        public MapleSyrupContext (DbContextOptions<MapleSyrupContext> options, IConfiguration pConfiguration) : base(options)
        {
            _configuration = pConfiguration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<GuildConfig> GuildConfigs { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<EventRequirement> EventRequirements { get; set; }
        public DbSet<Player> Players { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GuildConfig>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.GuildId).IsRequired();
                entity.Property(e => e.Prefix).IsRequired();
            });
        }
    }
}
